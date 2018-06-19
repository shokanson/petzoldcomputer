namespace PetzoldComputer
{
    // This RAM, while it has the interface as described in Chapter 16 "An Assemblage of Memory",
    // it isn't implemented internally with the circuitry described there.  Rather, it's a simple
    // C# array of bytes.  An exercise for a future day to implement it as a collection of latches,
    // selectors, and decoders...
    public class RAM64KB
    {
        #region Construction
        public RAM64KB(string name)
        {
            V = new ConnectionPoint($"{name}-ram64.v");
            Write = new ConnectionPoint($"{name}-ram64.clk");

            A0 = new ConnectionPoint($"{name}-ram64.a0");
            A1 = new ConnectionPoint($"{name}-ram64.a1");
            A2 = new ConnectionPoint($"{name}-ram64.a2");
            A3 = new ConnectionPoint($"{name}-ram64.a3");
            A4 = new ConnectionPoint($"{name}-ram64.a4");
            A5 = new ConnectionPoint($"{name}-ram64.a5");
            A6 = new ConnectionPoint($"{name}-ram64.a6");
            A7 = new ConnectionPoint($"{name}-ram64.a7");
            A8 = new ConnectionPoint($"{name}-ram64.a8");
            A9 = new ConnectionPoint($"{name}-ram64.a9");
            A10 = new ConnectionPoint($"{name}-ram64.a10");
            A11 = new ConnectionPoint($"{name}-ram64.a11");
            A12 = new ConnectionPoint($"{name}-ram64.a12");
            A13 = new ConnectionPoint($"{name}-ram64.a13");
            A14 = new ConnectionPoint($"{name}-ram64.a14");
            A15 = new ConnectionPoint($"{name}-ram64.a15");

            Din0 = new ConnectionPoint($"{name}-ram64.din0");
            Din1 = new ConnectionPoint($"{name}-ram64.din1");
            Din2 = new ConnectionPoint($"{name}-ram64.din2");
            Din3 = new ConnectionPoint($"{name}-ram64.din3");
            Din4 = new ConnectionPoint($"{name}-ram64.din4");
            Din5 = new ConnectionPoint($"{name}-ram64.din5");
            Din6 = new ConnectionPoint($"{name}-ram64.din6");
            Din7 = new ConnectionPoint($"{name}-ram64.din7");

            Dout0 = new ConnectionPoint($"{name}-ram64.dout0");
            Dout1 = new ConnectionPoint($"{name}-ram64.dout1");
            Dout2 = new ConnectionPoint($"{name}-ram64.dout2");
            Dout3 = new ConnectionPoint($"{name}-ram64.dout3");
            Dout4 = new ConnectionPoint($"{name}-ram64.dout4");
            Dout5 = new ConnectionPoint($"{name}-ram64.dout5");
            Dout6 = new ConnectionPoint($"{name}-ram64.dout6");
            Dout7 = new ConnectionPoint($"{name}-ram64.dout7");

            DoWireUp();
            Reset();

            Components.Record(nameof(RAM64KB));
        }
        #endregion

        #region Implementation
        private readonly byte[] _array = new byte[65536];
        #endregion

        #region IRam64KB Members
        public ConnectionPoint V { get; }
        public ConnectionPoint Write { get; }

        public ConnectionPoint A0 { get; }
        public ConnectionPoint A1 { get; }
        public ConnectionPoint A2 { get; }
        public ConnectionPoint A3 { get; }
        public ConnectionPoint A4 { get; }
        public ConnectionPoint A5 { get; }
        public ConnectionPoint A6 { get; }
        public ConnectionPoint A7 { get; }
        public ConnectionPoint A8 { get; }
        public ConnectionPoint A9 { get; }
        public ConnectionPoint A10 { get; }
        public ConnectionPoint A11 { get; }
        public ConnectionPoint A12 { get; }
        public ConnectionPoint A13 { get; }
        public ConnectionPoint A14 { get; }
        public ConnectionPoint A15 { get; }

        public ConnectionPoint Din0 { get; }
        public ConnectionPoint Din1 { get; }
        public ConnectionPoint Din2 { get; }
        public ConnectionPoint Din3 { get; }
        public ConnectionPoint Din4 { get; }
        public ConnectionPoint Din5 { get; }
        public ConnectionPoint Din6 { get; }
        public ConnectionPoint Din7 { get; }

        public ConnectionPoint Dout0 { get; }
        public ConnectionPoint Dout1 { get; }
        public ConnectionPoint Dout2 { get; }
        public ConnectionPoint Dout3 { get; }
        public ConnectionPoint Dout4 { get; }
        public ConnectionPoint Dout5 { get; }
        public ConnectionPoint Dout6 { get; }
        public ConnectionPoint Dout7 { get; }
        #endregion

        #region Private Members
        private void Reset()
        {
            for (int i = 0; i < 65536; ++i)
            {
                _array[i] = 0;
            }
            V.V = VoltageSignal.LOW;
            Write.V = VoltageSignal.LOW;
            A0.V = VoltageSignal.LOW;
            A1.V = VoltageSignal.LOW;
            A2.V = VoltageSignal.LOW;
            A3.V = VoltageSignal.LOW;
            A4.V = VoltageSignal.LOW;
            A5.V = VoltageSignal.LOW;
            A6.V = VoltageSignal.LOW;
            A7.V = VoltageSignal.LOW;
            A8.V = VoltageSignal.LOW;
            A9.V = VoltageSignal.LOW;
            A10.V = VoltageSignal.LOW;
            A11.V = VoltageSignal.LOW;
            A12.V = VoltageSignal.LOW;
            A13.V = VoltageSignal.LOW;
            A14.V = VoltageSignal.LOW;
            A15.V = VoltageSignal.LOW;
            Dout0.V = VoltageSignal.LOW;
            Dout1.V = VoltageSignal.LOW;
            Dout2.V = VoltageSignal.LOW;
            Dout3.V = VoltageSignal.LOW;
            Dout4.V = VoltageSignal.LOW;
            Dout5.V = VoltageSignal.LOW;
            Dout6.V = VoltageSignal.LOW;
            Dout7.V = VoltageSignal.LOW;
            Din0.V = VoltageSignal.LOW;
            Din1.V = VoltageSignal.LOW;
            Din2.V = VoltageSignal.LOW;
            Din3.V = VoltageSignal.LOW;
            Din4.V = VoltageSignal.LOW;
            Din5.V = VoltageSignal.LOW;
            Din6.V = VoltageSignal.LOW;
            Din7.V = VoltageSignal.LOW;
        }

        private void ReadData()
        {
            ushort address = 0;

            address |= (ushort)(A0.V == VoltageSignal.HIGH ? 0x0001 : 0x0000);
            address |= (ushort)(A1.V == VoltageSignal.HIGH ? 0x0002 : 0x0000);
            address |= (ushort)(A2.V == VoltageSignal.HIGH ? 0x0004 : 0x0000);
            address |= (ushort)(A3.V == VoltageSignal.HIGH ? 0x0008 : 0x0000);
            address |= (ushort)(A4.V == VoltageSignal.HIGH ? 0x0010 : 0x0000);
            address |= (ushort)(A5.V == VoltageSignal.HIGH ? 0x0020 : 0x0000);
            address |= (ushort)(A6.V == VoltageSignal.HIGH ? 0x0040 : 0x0000);
            address |= (ushort)(A7.V == VoltageSignal.HIGH ? 0x0080 : 0x0000);
            address |= (ushort)(A8.V == VoltageSignal.HIGH ? 0x0100 : 0x0000);
            address |= (ushort)(A9.V == VoltageSignal.HIGH ? 0x0200 : 0x0000);
            address |= (ushort)(A10.V == VoltageSignal.HIGH ? 0x0400 : 0x0000);
            address |= (ushort)(A11.V == VoltageSignal.HIGH ? 0x0800 : 0x0000);
            address |= (ushort)(A12.V == VoltageSignal.HIGH ? 0x1000 : 0x0000);
            address |= (ushort)(A13.V == VoltageSignal.HIGH ? 0x2000 : 0x0000);
            address |= (ushort)(A14.V == VoltageSignal.HIGH ? 0x4000 : 0x0000);
            address |= (ushort)(A15.V == VoltageSignal.HIGH ? 0x8000 : 0x0000);

            byte data = _array[address];

            Dout0.V = ((data & 0x01) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Dout1.V = ((data & 0x02) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Dout2.V = ((data & 0x04) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Dout3.V = ((data & 0x08) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Dout4.V = ((data & 0x10) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Dout5.V = ((data & 0x20) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Dout6.V = ((data & 0x40) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Dout7.V = ((data & 0x80) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
        }

        private void WriteData()
        {
            ushort address = 0;

            address |= (ushort)(A0.V == VoltageSignal.HIGH ? 0x0001 : 0x0);
            address |= (ushort)(A1.V == VoltageSignal.HIGH ? 0x0002 : 0x0);
            address |= (ushort)(A2.V == VoltageSignal.HIGH ? 0x0004 : 0x0);
            address |= (ushort)(A3.V == VoltageSignal.HIGH ? 0x0008 : 0x0);
            address |= (ushort)(A4.V == VoltageSignal.HIGH ? 0x0010 : 0x0);
            address |= (ushort)(A5.V == VoltageSignal.HIGH ? 0x0020 : 0x0);
            address |= (ushort)(A6.V == VoltageSignal.HIGH ? 0x0040 : 0x0);
            address |= (ushort)(A7.V == VoltageSignal.HIGH ? 0x0080 : 0x0);
            address |= (ushort)(A8.V == VoltageSignal.HIGH ? 0x0100 : 0x0);
            address |= (ushort)(A9.V == VoltageSignal.HIGH ? 0x0200 : 0x0);
            address |= (ushort)(A10.V == VoltageSignal.HIGH ? 0x0400 : 0x0);
            address |= (ushort)(A11.V == VoltageSignal.HIGH ? 0x0800 : 0x0);
            address |= (ushort)(A12.V == VoltageSignal.HIGH ? 0x1000 : 0x0);
            address |= (ushort)(A13.V == VoltageSignal.HIGH ? 0x2000 : 0x0);
            address |= (ushort)(A14.V == VoltageSignal.HIGH ? 0x4000 : 0x0);
            address |= (ushort)(A15.V == VoltageSignal.HIGH ? 0x8000 : 0x0);

            byte data = 0;

            data |= (byte)(Din0.V == VoltageSignal.HIGH ? 0x01 : 0x0);
            data |= (byte)(Din1.V == VoltageSignal.HIGH ? 0x02 : 0x0);
            data |= (byte)(Din2.V == VoltageSignal.HIGH ? 0x04 : 0x0);
            data |= (byte)(Din3.V == VoltageSignal.HIGH ? 0x08 : 0x0);
            data |= (byte)(Din4.V == VoltageSignal.HIGH ? 0x10 : 0x0);
            data |= (byte)(Din5.V == VoltageSignal.HIGH ? 0x20 : 0x0);
            data |= (byte)(Din6.V == VoltageSignal.HIGH ? 0x40 : 0x0);
            data |= (byte)(Din7.V == VoltageSignal.HIGH ? 0x80 : 0x0);

            _array[address] = data;

            Dout0.V = Din0.V;
            Dout1.V = Din1.V;
            Dout2.V = Din2.V;
            Dout3.V = Din3.V;
            Dout4.V = Din4.V;
            Dout5.V = Din5.V;
            Dout6.V = Din6.V;
            Dout7.V = Din7.V;
        }

        private void DoWireUp()
        {
            V.Changed += voltage => { if (voltage.V == VoltageSignal.LOW) Reset(); };
            Write.Changed += clk => { if (clk.V == VoltageSignal.HIGH) WriteData(); };
            A0.Changed += _ => ReadData();
            A1.Changed += _ => ReadData();
            A2.Changed += _ => ReadData();
            A3.Changed += _ => ReadData();
            A4.Changed += _ => ReadData();
            A5.Changed += _ => ReadData();
            A6.Changed += _ => ReadData();
            A7.Changed += _ => ReadData();
            A8.Changed += _ => ReadData();
            A9.Changed += _ => ReadData();
            A10.Changed += _ => ReadData();
            A11.Changed += _ => ReadData();
            A12.Changed += _ => ReadData();
            A13.Changed += _ => ReadData();
            A14.Changed += _ => ReadData();
            A15.Changed += _ => ReadData();
        }
        #endregion
    }
}
