//
// Copyright (c) 2010-2021 Antmicro
//
// This file is licensed under the MIT License.
// Full license text is available in 'licenses/MIT.txt'.
// 
using System;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Antmicro.Renode.Exceptions;
using Antmicro.Renode.Logging;
using Antmicro.Renode.Core;
using Antmicro.Renode.Utilities;

namespace Antmicro.Renode.Peripherals.CPU
{
    public static class ExecutionTracerExtensions
    {
        public static void EnableExecutionTracing(this TranslationCPU @this, string file, ExecutionTracer.Format format)
        {
            var tracer = new ExecutionTracer(@this, file, format);
            // we keep it as external to dispose/flush on quit 
            EmulationManager.Instance.CurrentEmulation.ExternalsManager.AddExternal(tracer, "executionTracer");
            
            tracer.Start();
        }
        
        public static void DisableExecutionTracing(this TranslationCPU @this)
        {
            var em = EmulationManager.Instance.CurrentEmulation.ExternalsManager;
            var tracers = em.GetExternalsOfType<ExecutionTracer>().Where(t => t.AttachedCPU == @this).ToList();
            foreach(var tracer in tracers)
            {
                tracer.Stop();
                em.RemoveExternal(tracer);
            }
        }
    }
    
    public class ExecutionTracer : IDisposable, IExternal
    {
        public ExecutionTracer(TranslationCPU cpu, string file, Format format)
        {
            cache = new LRUCache<uint, Antmicro.Renode.Peripherals.CPU.Disassembler.DisassemblyResult?>(CacheSize);
            this.file = file;
            this.format = format;
            AttachedCPU = cpu;

            try
            {
                // truncate the file
                File.WriteAllText(file, string.Empty);
            }
            catch(Exception e)
            {
                throw new RecoverableException($"There was an error when preparing the execution trace output file {file}: {e.Message}");
            }
            
            AttachedCPU.SetHookAtBlockEnd(HandleBlock);
        }

        public void Dispose()
        {
            Stop();
        }

        public void Start()
        {
            blocks = new BlockingCollection<Block>();
            
            underlyingThread = new Thread(WriterThreadBody);
            underlyingThread.IsBackground = true;
            underlyingThread.Name = "Execution tracer worker";
            underlyingThread.Start();
        }

        public void Stop()
        {
            if(underlyingThread == null)
            {
                return;
            }

            this.Log(LogLevel.Info, "Stopping the execution tracer worker and dumping the trace to a file...");
            
            blocks.CompleteAdding();
            underlyingThread.Join();
            underlyingThread = null;

            this.Log(LogLevel.Info, "Execution tracer stopped");
        }

        public TranslationCPU AttachedCPU { get; }

        private void HandleBlock(Block block, StringBuilder sb)
        {
            var pc = block.StartingPC;
            var counter = 0;

            while(counter < (int)block.InstructionsCount)
            {
                // here we read only 4-bytes as it should cover most cases
                var key = AttachedCPU.Bus.ReadDoubleWord(pc);
                if(!cache.TryGetValue(key, out var cachedItem))
                {
                    // here we are prepared for longer opcodes
                    var mem = AttachedCPU.Bus.ReadBytes(pc, MaxOpcodeBytes, context: AttachedCPU);
                    // TODO: what about flags?
                    if(!AttachedCPU.Disassembler.TryDisassembleInstruction(pc, mem, 0, out var result))
                    {
                        cachedItem = null;
                        // mark this as an invalid opcode
                        cache.Add(key, null);
                    }
                    else
                    {
                        cachedItem = result;
                        // we only cache opcodes up to 4-bytes
                        if(result.OpcodeSize <= 4)
                        {
                            cache.Add(key, result);
                        }
                    }
                }

                if(!cachedItem.HasValue)
                {
                    sb.AppendFormat("Couldn't disassemble opcode at PC 0x{0:X}\n", pc);
                    break;
                }
                else
                {
                    var result = cachedItem.Value;
                    
                    switch(format)
                    {
                        case Format.PC:
                            sb.AppendFormat("0x{0:X}\n", pc);
                            break;

                        case Format.Opcode:
                            sb.AppendFormat("0x{0}\n", result.OpcodeString.ToUpper());
                            break;
                            
                        case Format.PCAndOpcode:
                            sb.AppendFormat("0x{0:X}: 0x{1}\n", pc, result.OpcodeString.ToUpper());
                            break;

                        default:
                            AttachedCPU.Log(LogLevel.Error, "Unsupported format: {0}", format);
                            break;
                    }
                    
                    pc += (ulong)result.OpcodeSize;
                    counter++;
                }
            }
        }

        private void DumpBuffer(StringBuilder sb)
        {
            File.AppendAllText(file, sb.ToString());
            sb.Clear();
        }

        private void WriterThreadBody()
        {
            var sb = new StringBuilder();
            
            while(true)
            {
                try
                {
                    var block = blocks.Take();
                    do
                    {
                        HandleBlock(block, sb);
                        if(sb.Length > BufferFlushLevel)
                        {
                            DumpBuffer(sb);
                        }
                    }
                    while(blocks.TryTake(out block));
                    DumpBuffer(sb);
                }
                catch(InvalidOperationException)
                {
                    // this happens when the blocking collection is empty and is marked as completed - i.e., we are sure there will be no more elements
                    break;
                }
            }
            DumpBuffer(sb);
        }

        private void HandleBlock(ulong pc, uint instructionsInBlock)
        {
            if(instructionsInBlock == 0)
            {
                // ignore
                return;
            }

            try
            {
                blocks.Add(new Block { StartingPC = pc, InstructionsCount = instructionsInBlock });
            }
            catch(InvalidOperationException)
            {
                // this might happen when disposing after `blocks` is marked as closed (not accepting new data)
            }
        }
        
        private Thread underlyingThread;
        private BlockingCollection<Block> blocks;

        private readonly string file;
        private readonly Format format;
        private readonly LRUCache<uint, Antmicro.Renode.Peripherals.CPU.Disassembler.DisassemblyResult?> cache;

        private const int MaxOpcodeBytes = 16;
        private const int BufferFlushLevel = 1000000;
        private const int CacheSize = 100000;
        
        public enum Format
        {
            PC,
            Opcode,
            PCAndOpcode
        }

        private struct Block
        {
            public ulong StartingPC;
            public ulong InstructionsCount;

            public override string ToString()
            {
                return $"[Block: starting at 0x{StartingPC:X} with {InstructionsCount} instructions]";
            }
        }
    }
}
