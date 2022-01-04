//
// Copyright (c) 2010-2021 Antmicro
//
// This file is licensed under the MIT License.
// Full license text is available in 'licenses/MIT.txt'.
//
using System;
using System.Collections.Generic;
using Antmicro.Renode.Core;
using Antmicro.Renode.Core.Structure;
using Antmicro.Renode.Exceptions;
using Antmicro.Renode.Logging;
using Antmicro.Renode.Peripherals.Timers;
using Antmicro.Renode.Peripherals.UART;
using Antmicro.Renode.Utilities;
using Antmicro.Renode.Utilities.Binding;
using Endianess = ELFSharp.ELF.Endianess;

namespace Antmicro.Renode.Peripherals.CPU
{
    public partial class Xtensa : TranslationCPU, IPeripheralRegister<SemihostingUart, NullRegistrationPoint>
    {
        public Xtensa(string cpuType, Machine machine, long frequency = 10000000)
                : base(cpuType, machine, Endianess.LittleEndian)
        {
            innerTimers = new ComparingTimer[InnerTimersCount];
            for(var i = 0; i < innerTimers.Length; i++)
            {
                var j = i;
                innerTimers[i] = new ComparingTimer(machine.ClockSource, frequency, this, "", enabled: true, eventEnabled: true);
                innerTimers[i].CompareReached += () => HandleCompareReached(j) ;
            }
            Reset();
        }

        public override void OnGPIO(int number, bool value)
        {
            TlibSetIrqPendingBit((uint)number, value ? 1u : 0u);
            base.OnGPIO(number, value);
        }

        public void Register(SemihostingUart peripheral, NullRegistrationPoint registrationPoint)
        {
            if(semihostingUart != null)
            {
                throw new RegistrationException("A semihosting uart is already registered.");
            }
            semihostingUart = peripheral;
            machine.RegisterAsAChildOf(this, peripheral, registrationPoint);
        }

        public override void Reset()
        {
            base.Reset();
            ShouldEnterDebugMode = true;
        }

        public void Unregister(SemihostingUart peripheral)
        {
            semihostingUart = null;
            machine.UnregisterAsAChildOf(this, peripheral);
        }

        public override string Architecture { get { return "xtensa"; } }

        public override string GDBArchitecture { get { return "xtensa"; } }

        public override List<GDBFeatureDescriptor> GDBFeatures => new List<GDBFeatureDescriptor>();

        protected override void AddNonMappedRegistersValues(ref Table table)
        {
            var nonMappedRegisterValues = new Dictionary<string, ulong>();
            var enumType = typeof(XtensaMaskedRegister);
            foreach(var register in enumType.GetEnumValues())
            {
                nonMappedRegisterValues.Add(
                    enumType.GetEnumName(register),
                    GetRegisterUnsafe((int)register).RawValue
                );
            }
            table.AddRows(nonMappedRegisterValues, x => x.Key, x => "0x{0:X}".FormatWith(x.Value));
        }

        protected override Interrupt DecodeInterrupt(int number)
        {
            return Interrupt.Hard;
        }

        protected IEnumerable<CPURegister> GetNonMappedRegisters()
        {
            var registers = new List<CPURegister>();
            foreach(var register in Enum.GetValues(typeof(XtensaMaskedRegister)))
            {
                registers.Add(new CPURegister((int)register, 32, false, false));
            }
            return registers;
        }

        protected bool TryGetNonMappedRegister(int register, out RegisterValue value)
        {
            if(register >= (int)XtensaMaskedRegister.PSINTLEVEL
                && register <= (int)XtensaMaskedRegister.PSOWB)
            {
                if(GetRegisterMask(register, out var maskOffset, out var maskWidth))
                {
                    value = GetRegisterMaskedValue(PS, maskOffset, maskWidth);
                    return true;
                }
            }
            value = default(RegisterValue);
            return false;
        }

        protected bool TrySetNonMappedRegister(int register, RegisterValue value)
        {
            this.Log(LogLevel.Error, "Writing to PS* registers isn't implemented.");
            return false;
        }

        [Export]
        /* AKA SIMCALL. After the simcall: "a return code will be stored to a2 and an error number to a3." */
        private void DoSemihosting()
        {
            uint op = A[2];

            switch((XtensaSimcallOperation)op){
            case XtensaSimcallOperation.WRITE:
                uint fd = A[3];
                uint vaddr = A[4];
                uint len = A[5];
                this.Log(LogLevel.Debug, "WRITE SIMCALL: fd={0}; vaddr=0x{1:X}; len={2}", fd, vaddr, len);

                if(semihostingUart == null)
                {
                    this.Log(LogLevel.Warning, "WRITE SIMCALL: Semihosting UART not available!");
                    break;
                }
                if(fd != 1 && fd != 2)
                {
                    this.Log(LogLevel.Warning, "WRITE SIMCALL: Only writing to fd=1 or fd=2 is supported!", fd);
                    break;
                }

                var buf = Bus.ReadBytes(vaddr, (int)len);
                var bufString = System.Text.Encoding.ASCII.GetString(buf);
                semihostingUart.SemihostingWriteString(bufString);

                A[2] = bufString.Length;
                A[3] = 0; // errno
                return;
            default:
                var opType = typeof(XtensaSimcallOperation);
                this.Log(LogLevel.Warning, "Unimplemented simcall op={0} ({1})!", op,
                    Enum.IsDefined(opType, op) ? Enum.GetName(opType, op) : "UNKNOWN");
                break;
            }
            A[2] = uint.MaxValue;
            A[3] = 88; // ENOSYS
        }

        [Export]
        private ulong GetCPUTime()
        {
            SyncTime();
            return innerTimers[0].Value;
        }

        private static bool GetRegisterMask(int register, out int offset, out int width)
        {
            switch((XtensaMaskedRegister)register)
            {
                case XtensaMaskedRegister.PSINTLEVEL:
                    offset = 0; width = 4;
                    break;
                case XtensaMaskedRegister.PSUM:
                    offset = 5; width = 1;
                    break;
                case XtensaMaskedRegister.PSWOE:
                    offset = 18; width = 1;
                    break;
                case XtensaMaskedRegister.PSEXCM:
                    offset = 4; width = 1;
                    break;
                case XtensaMaskedRegister.PSCALLINC:
                    offset = 16; width = 2;
                    break;
                case XtensaMaskedRegister.PSOWB:
                    offset = 8; width = 4;
                    break;
                default:
                    offset = -1; width = -1;
                    return false;
            }
            return true;
        }

        private RegisterValue GetRegisterMaskedValue(RegisterValue source, int maskOffset, int maskSize)
        {
            return RegisterValue.Create(
                BitHelper.GetMaskedValue((uint)source.RawValue, maskOffset, maskSize),
                (uint)maskSize);
        }

        private void HandleCompareReached(int id)
        {
            // this is a mapping for sample_controller
            var intMap = new uint[] { 6, 10, 13 };

            // this is a mapping for baytrail
            // var intMap = new uint[] { 1, 5, 7 };
            TlibSetIrqPendingBit(intMap[id], 1u);
        }

        [Export]
        private void TimerMod(uint id, ulong value)
        {
            if(id >= InnerTimersCount)
            {
                throw new Exception($"Unsupported compare #{id}");
            }

            innerTimers[id].Compare = value;
        }

        private readonly ComparingTimer[] innerTimers;
        private SemihostingUart semihostingUart = null;

        private const int InnerTimersCount = 3;

        // 649:  Field '...' is never assigned to, and will always have its default value null
#pragma warning disable 649
        [Import]
        private ActionUInt32UInt32 TlibSetIrqPendingBit;
#pragma warning restore 649

        private enum XtensaMaskedRegister
        {
            PSINTLEVEL = 105,
            PSUM = 106,
            PSWOE = 107,
            PSEXCM = 108,
            PSCALLINC = 109,
            PSOWB = 110,
        }

        private enum XtensaSimcallOperation : uint
        {
            EXIT = 1,
            READ = 3,
            WRITE = 4,
            OPEN = 5,
            CLOSE = 6,
            LSEEK = 19,
            SELECT_ONE = 29,
            ARGC = 1000,
            ARGV_SZ = 1001,
            ARGV = 1002,
            MEMSET = 1004,
        }
    }
}

