﻿/********************************************************
*
* Warning!
* This file was generated automatically.
* Please do not edit. Changes should be made in the
* appropriate *.tt file.
*
*/
using System;
using System.Linq;
using System.Collections.Generic;
using Antmicro.Renode.Peripherals.CPU.Registers;
using Antmicro.Renode.Utilities.Binding;
using Antmicro.Renode.Exceptions;

namespace Antmicro.Renode.Peripherals.CPU
{
    public partial class RiscV64
    {
        public override void SetRegisterUnsafe(int register, ulong value)
        {
            if(!mapping.TryGetValue((RiscV64Registers)register, out var r))
            {
                throw new RecoverableException($"Wrong register index: {register}");
            }
            if(r.IsReadonly)
            {
                throw new RecoverableException($"Register: {register} value is not writable.");
            }

            SetRegisterValue64(r.Index, checked((UInt64)value));
        }

        public override RegisterValue GetRegisterUnsafe(int register)
        {
            if(!mapping.TryGetValue((RiscV64Registers)register, out var r))
            {
                throw new RecoverableException($"Wrong register index: {register}");
            }

            return GetRegisterValue64(r.Index);
        }

        public override IEnumerable<CPURegister> GetRegisters()
        {
            return mapping.Values.OrderBy(x => x.Index);
        }

        [Register]
        public RegisterValue ZERO
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.ZERO);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.ZERO, value);
            }
        }
        [Register]
        public RegisterValue RA
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.RA);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.RA, value);
            }
        }
        [Register]
        public RegisterValue SP
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.SP);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.SP, value);
            }
        }
        [Register]
        public RegisterValue GP
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.GP);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.GP, value);
            }
        }
        [Register]
        public RegisterValue TP
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.TP);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.TP, value);
            }
        }
        [Register]
        public RegisterValue FP
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.FP);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.FP, value);
            }
        }
        [Register]
        public override RegisterValue PC
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.PC);
            }
            set
            {
                value = BeforePCWrite(value);
                SetRegisterValue64((int)RiscV64Registers.PC, value);
            }
        }
        [Register]
        public RegisterValue SSTATUS
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.SSTATUS);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.SSTATUS, value);
            }
        }
        [Register]
        public RegisterValue SIE
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.SIE);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.SIE, value);
            }
        }
        [Register]
        public RegisterValue STVEC
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.STVEC);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.STVEC, value);
            }
        }
        [Register]
        public RegisterValue SSCRATCH
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.SSCRATCH);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.SSCRATCH, value);
            }
        }
        [Register]
        public RegisterValue SEPC
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.SEPC);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.SEPC, value);
            }
        }
        [Register]
        public RegisterValue SCAUSE
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.SCAUSE);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.SCAUSE, value);
            }
        }
        [Register]
        public RegisterValue STVAL
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.STVAL);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.STVAL, value);
            }
        }
        [Register]
        public RegisterValue SIP
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.SIP);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.SIP, value);
            }
        }
        [Register]
        public RegisterValue SATP
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.SATP);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.SATP, value);
            }
        }
        [Register]
        public RegisterValue SPTBR
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.SPTBR);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.SPTBR, value);
            }
        }
        [Register]
        public RegisterValue MSTATUS
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.MSTATUS);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.MSTATUS, value);
            }
        }
        [Register]
        public RegisterValue MISA
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.MISA);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.MISA, value);
            }
        }
        [Register]
        public RegisterValue MEDELEG
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.MEDELEG);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.MEDELEG, value);
            }
        }
        [Register]
        public RegisterValue MIDELEG
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.MIDELEG);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.MIDELEG, value);
            }
        }
        [Register]
        public RegisterValue MIE
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.MIE);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.MIE, value);
            }
        }
        [Register]
        public RegisterValue MTVEC
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.MTVEC);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.MTVEC, value);
            }
        }
        [Register]
        public RegisterValue MSCRATCH
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.MSCRATCH);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.MSCRATCH, value);
            }
        }
        [Register]
        public RegisterValue MEPC
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.MEPC);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.MEPC, value);
            }
        }
        [Register]
        public RegisterValue MCAUSE
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.MCAUSE);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.MCAUSE, value);
            }
        }
        [Register]
        public RegisterValue MTVAL
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.MTVAL);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.MTVAL, value);
            }
        }
        [Register]
        public RegisterValue MIP
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.MIP);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.MIP, value);
            }
        }
        [Register]
        public RegisterValue PRIV
        {
            get
            {
                return GetRegisterValue64((int)RiscV64Registers.PRIV);
            }
            set
            {
                SetRegisterValue64((int)RiscV64Registers.PRIV, value);
            }
        }
        public RegistersGroup X { get; private set; }
        public RegistersGroup T { get; private set; }
        public RegistersGroup S { get; private set; }
        public RegistersGroup A { get; private set; }
        public RegistersGroup F { get; private set; }

        protected override void InitializeRegisters()
        {
            var indexValueMapX = new Dictionary<int, RiscV64Registers>
            {
                { 0, RiscV64Registers.X0 },
                { 1, RiscV64Registers.X1 },
                { 2, RiscV64Registers.X2 },
                { 3, RiscV64Registers.X3 },
                { 4, RiscV64Registers.X4 },
                { 5, RiscV64Registers.X5 },
                { 6, RiscV64Registers.X6 },
                { 7, RiscV64Registers.X7 },
                { 8, RiscV64Registers.X8 },
                { 9, RiscV64Registers.X9 },
                { 10, RiscV64Registers.X10 },
                { 11, RiscV64Registers.X11 },
                { 12, RiscV64Registers.X12 },
                { 13, RiscV64Registers.X13 },
                { 14, RiscV64Registers.X14 },
                { 15, RiscV64Registers.X15 },
                { 16, RiscV64Registers.X16 },
                { 17, RiscV64Registers.X17 },
                { 18, RiscV64Registers.X18 },
                { 19, RiscV64Registers.X19 },
                { 20, RiscV64Registers.X20 },
                { 21, RiscV64Registers.X21 },
                { 22, RiscV64Registers.X22 },
                { 23, RiscV64Registers.X23 },
                { 24, RiscV64Registers.X24 },
                { 25, RiscV64Registers.X25 },
                { 26, RiscV64Registers.X26 },
                { 27, RiscV64Registers.X27 },
                { 28, RiscV64Registers.X28 },
                { 29, RiscV64Registers.X29 },
                { 30, RiscV64Registers.X30 },
                { 31, RiscV64Registers.X31 },
            };
            X = new RegistersGroup(
                indexValueMapX.Keys,
                i => GetRegisterUnsafe((int)indexValueMapX[i]),
                (i, v) => SetRegisterUnsafe((int)indexValueMapX[i], v));

            var indexValueMapT = new Dictionary<int, RiscV64Registers>
            {
                { 0, RiscV64Registers.T0 },
                { 1, RiscV64Registers.T1 },
                { 2, RiscV64Registers.T2 },
                { 3, RiscV64Registers.T3 },
                { 4, RiscV64Registers.T4 },
                { 5, RiscV64Registers.T5 },
                { 6, RiscV64Registers.T6 },
            };
            T = new RegistersGroup(
                indexValueMapT.Keys,
                i => GetRegisterUnsafe((int)indexValueMapT[i]),
                (i, v) => SetRegisterUnsafe((int)indexValueMapT[i], v));

            var indexValueMapS = new Dictionary<int, RiscV64Registers>
            {
                { 0, RiscV64Registers.S0 },
                { 1, RiscV64Registers.S1 },
                { 2, RiscV64Registers.S2 },
                { 3, RiscV64Registers.S3 },
                { 4, RiscV64Registers.S4 },
                { 5, RiscV64Registers.S5 },
                { 6, RiscV64Registers.S6 },
                { 7, RiscV64Registers.S7 },
                { 8, RiscV64Registers.S8 },
                { 9, RiscV64Registers.S9 },
                { 10, RiscV64Registers.S10 },
                { 11, RiscV64Registers.S11 },
            };
            S = new RegistersGroup(
                indexValueMapS.Keys,
                i => GetRegisterUnsafe((int)indexValueMapS[i]),
                (i, v) => SetRegisterUnsafe((int)indexValueMapS[i], v));

            var indexValueMapA = new Dictionary<int, RiscV64Registers>
            {
                { 0, RiscV64Registers.A0 },
                { 1, RiscV64Registers.A1 },
                { 2, RiscV64Registers.A2 },
                { 3, RiscV64Registers.A3 },
                { 4, RiscV64Registers.A4 },
                { 5, RiscV64Registers.A5 },
                { 6, RiscV64Registers.A6 },
                { 7, RiscV64Registers.A7 },
            };
            A = new RegistersGroup(
                indexValueMapA.Keys,
                i => GetRegisterUnsafe((int)indexValueMapA[i]),
                (i, v) => SetRegisterUnsafe((int)indexValueMapA[i], v));

            var indexValueMapF = new Dictionary<int, RiscV64Registers>
            {
                { 0, RiscV64Registers.F0 },
                { 1, RiscV64Registers.F1 },
                { 2, RiscV64Registers.F2 },
                { 3, RiscV64Registers.F3 },
                { 4, RiscV64Registers.F4 },
                { 5, RiscV64Registers.F5 },
                { 6, RiscV64Registers.F6 },
                { 7, RiscV64Registers.F7 },
                { 8, RiscV64Registers.F8 },
                { 9, RiscV64Registers.F9 },
                { 10, RiscV64Registers.F10 },
                { 11, RiscV64Registers.F11 },
                { 12, RiscV64Registers.F12 },
                { 13, RiscV64Registers.F13 },
                { 14, RiscV64Registers.F14 },
                { 15, RiscV64Registers.F15 },
                { 16, RiscV64Registers.F16 },
                { 17, RiscV64Registers.F17 },
                { 18, RiscV64Registers.F18 },
                { 19, RiscV64Registers.F19 },
                { 20, RiscV64Registers.F20 },
                { 21, RiscV64Registers.F21 },
                { 22, RiscV64Registers.F22 },
                { 23, RiscV64Registers.F23 },
                { 24, RiscV64Registers.F24 },
                { 25, RiscV64Registers.F25 },
                { 26, RiscV64Registers.F26 },
                { 27, RiscV64Registers.F27 },
                { 28, RiscV64Registers.F28 },
                { 29, RiscV64Registers.F29 },
                { 30, RiscV64Registers.F30 },
                { 31, RiscV64Registers.F31 },
            };
            F = new RegistersGroup(
                indexValueMapF.Keys,
                i => GetRegisterUnsafe((int)indexValueMapF[i]),
                (i, v) => SetRegisterUnsafe((int)indexValueMapF[i], v));

        }

        // 649:  Field '...' is never assigned to, and will always have its default value null
        #pragma warning disable 649

        [Import(Name = "tlib_set_register_value_64")]
        protected ActionInt32UInt64 SetRegisterValue64;
        [Import(Name = "tlib_get_register_value_64")]
        protected FuncUInt64Int32 GetRegisterValue64;

        #pragma warning restore 649

        private static readonly Dictionary<RiscV64Registers, CPURegister> mapping = new Dictionary<RiscV64Registers, CPURegister>
        {
            { RiscV64Registers.ZERO,  new CPURegister(0, 64, isGeneral: true, isReadonly: true) },
            { RiscV64Registers.RA,  new CPURegister(1, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.SP,  new CPURegister(2, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.GP,  new CPURegister(3, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.TP,  new CPURegister(4, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X5,  new CPURegister(5, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X6,  new CPURegister(6, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X7,  new CPURegister(7, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.FP,  new CPURegister(8, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X9,  new CPURegister(9, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X10,  new CPURegister(10, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X11,  new CPURegister(11, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X12,  new CPURegister(12, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X13,  new CPURegister(13, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X14,  new CPURegister(14, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X15,  new CPURegister(15, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X16,  new CPURegister(16, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X17,  new CPURegister(17, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X18,  new CPURegister(18, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X19,  new CPURegister(19, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X20,  new CPURegister(20, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X21,  new CPURegister(21, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X22,  new CPURegister(22, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X23,  new CPURegister(23, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X24,  new CPURegister(24, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X25,  new CPURegister(25, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X26,  new CPURegister(26, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X27,  new CPURegister(27, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X28,  new CPURegister(28, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X29,  new CPURegister(29, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X30,  new CPURegister(30, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.X31,  new CPURegister(31, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.PC,  new CPURegister(32, 64, isGeneral: true, isReadonly: false) },
            { RiscV64Registers.F0,  new CPURegister(33, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F1,  new CPURegister(34, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F2,  new CPURegister(35, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F3,  new CPURegister(36, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F4,  new CPURegister(37, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F5,  new CPURegister(38, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F6,  new CPURegister(39, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F7,  new CPURegister(40, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F8,  new CPURegister(41, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F9,  new CPURegister(42, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F10,  new CPURegister(43, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F11,  new CPURegister(44, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F12,  new CPURegister(45, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F13,  new CPURegister(46, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F14,  new CPURegister(47, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F15,  new CPURegister(48, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F16,  new CPURegister(49, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F17,  new CPURegister(50, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F18,  new CPURegister(51, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F19,  new CPURegister(52, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F20,  new CPURegister(53, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F21,  new CPURegister(54, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F22,  new CPURegister(55, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F23,  new CPURegister(56, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F24,  new CPURegister(57, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F25,  new CPURegister(58, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F26,  new CPURegister(59, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F27,  new CPURegister(60, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F28,  new CPURegister(61, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F29,  new CPURegister(62, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F30,  new CPURegister(63, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.F31,  new CPURegister(64, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.SSTATUS,  new CPURegister(321, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.SIE,  new CPURegister(325, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.STVEC,  new CPURegister(326, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.SSCRATCH,  new CPURegister(385, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.SEPC,  new CPURegister(386, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.SCAUSE,  new CPURegister(387, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.STVAL,  new CPURegister(388, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.SIP,  new CPURegister(389, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.SATP,  new CPURegister(449, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.MSTATUS,  new CPURegister(833, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.MISA,  new CPURegister(834, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.MEDELEG,  new CPURegister(835, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.MIDELEG,  new CPURegister(836, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.MIE,  new CPURegister(837, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.MTVEC,  new CPURegister(838, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.MSCRATCH,  new CPURegister(897, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.MEPC,  new CPURegister(898, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.MCAUSE,  new CPURegister(899, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.MTVAL,  new CPURegister(900, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.MIP,  new CPURegister(901, 64, isGeneral: false, isReadonly: false) },
            { RiscV64Registers.PRIV,  new CPURegister(4161, 64, isGeneral: false, isReadonly: false) },
        };
    }

    public enum RiscV64Registers
    {
        ZERO = 0,
        RA = 1,
        SP = 2,
        GP = 3,
        TP = 4,
        FP = 8,
        PC = 32,
        SSTATUS = 321,
        SIE = 325,
        STVEC = 326,
        SSCRATCH = 385,
        SEPC = 386,
        SCAUSE = 387,
        STVAL = 388,
        SIP = 389,
        SATP = 449,
        SPTBR = 449,
        MSTATUS = 833,
        MISA = 834,
        MEDELEG = 835,
        MIDELEG = 836,
        MIE = 837,
        MTVEC = 838,
        MSCRATCH = 897,
        MEPC = 898,
        MCAUSE = 899,
        MTVAL = 900,
        MIP = 901,
        PRIV = 4161,
        X0 = 0,
        X1 = 1,
        X2 = 2,
        X3 = 3,
        X4 = 4,
        X5 = 5,
        X6 = 6,
        X7 = 7,
        X8 = 8,
        X9 = 9,
        X10 = 10,
        X11 = 11,
        X12 = 12,
        X13 = 13,
        X14 = 14,
        X15 = 15,
        X16 = 16,
        X17 = 17,
        X18 = 18,
        X19 = 19,
        X20 = 20,
        X21 = 21,
        X22 = 22,
        X23 = 23,
        X24 = 24,
        X25 = 25,
        X26 = 26,
        X27 = 27,
        X28 = 28,
        X29 = 29,
        X30 = 30,
        X31 = 31,
        T0 = 5,
        T1 = 6,
        T2 = 7,
        T3 = 28,
        T4 = 29,
        T5 = 30,
        T6 = 31,
        S0 = 8,
        S1 = 9,
        S2 = 18,
        S3 = 19,
        S4 = 20,
        S5 = 21,
        S6 = 22,
        S7 = 23,
        S8 = 24,
        S9 = 25,
        S10 = 26,
        S11 = 27,
        A0 = 10,
        A1 = 11,
        A2 = 12,
        A3 = 13,
        A4 = 14,
        A5 = 15,
        A6 = 16,
        A7 = 17,
        F0 = 33,
        F1 = 34,
        F2 = 35,
        F3 = 36,
        F4 = 37,
        F5 = 38,
        F6 = 39,
        F7 = 40,
        F8 = 41,
        F9 = 42,
        F10 = 43,
        F11 = 44,
        F12 = 45,
        F13 = 46,
        F14 = 47,
        F15 = 48,
        F16 = 49,
        F17 = 50,
        F18 = 51,
        F19 = 52,
        F20 = 53,
        F21 = 54,
        F22 = 55,
        F23 = 56,
        F24 = 57,
        F25 = 58,
        F26 = 59,
        F27 = 60,
        F28 = 61,
        F29 = 62,
        F30 = 63,
        F31 = 64,
    }
}
