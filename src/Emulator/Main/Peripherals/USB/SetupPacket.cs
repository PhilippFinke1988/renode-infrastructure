//
// Copyright (c) 2010-2018 Antmicro
//
// This file is licensed under the MIT License.
// Full license text is available in 'licenses/MIT.txt'.
//
using Antmicro.Renode.Utilities.Packets;

namespace Antmicro.Renode.Core.USB
{
    [LeastSignificantByteFirst]
    public struct SetupPacket
    {
        [PacketField, Width(5)]
        public PacketRecipient Recipient;
        [PacketField, Offset(bytes: 0, bits: 5), Width(2)]
        public PacketType Type;
        [PacketField, Offset(bytes: 0, bits: 7), Width(1)]
        public Direction Direction;
        [PacketField]
        public byte Request;
        [PacketField]
        public short Value;
        [PacketField]
        public short Index;
        [PacketField]
        public ushort Count;

        public override string ToString()
        {
            return $"[Recipient: {Recipient}, Type: {Type}, Direction: {Direction}, Request: 0x{Request:X}, Value: 0x{Value:X}, Index: 0x{Index:X}, Count: 0x{Count:X}]";
        }
    }
}
