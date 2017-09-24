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
			_voltage = new ConnectionPoint($"{name}-ram64.v");
			_clk = new ConnectionPoint($"{name}-ram64.clk");

			_a0 = new ConnectionPoint($"{name}-ram64.a0");
			_a1 = new ConnectionPoint($"{name}-ram64.a1");
			_a2 = new ConnectionPoint($"{name}-ram64.a2");
			_a3 = new ConnectionPoint($"{name}-ram64.a3");
			_a4 = new ConnectionPoint($"{name}-ram64.a4");
			_a5 = new ConnectionPoint($"{name}-ram64.a5");
			_a6 = new ConnectionPoint($"{name}-ram64.a6");
			_a7 = new ConnectionPoint($"{name}-ram64.a7");
			_a8 = new ConnectionPoint($"{name}-ram64.a8");
			_a9 = new ConnectionPoint($"{name}-ram64.a9");
			_a10 = new ConnectionPoint($"{name}-ram64.a10");
			_a11 = new ConnectionPoint($"{name}-ram64.a11");
			_a12 = new ConnectionPoint($"{name}-ram64.a12");
			_a13 = new ConnectionPoint($"{name}-ram64.a13");
			_a14 = new ConnectionPoint($"{name}-ram64.a14");
			_a15 = new ConnectionPoint($"{name}-ram64.a15");

			_din0 = new ConnectionPoint($"{name}-ram64.din0");
			_din1 = new ConnectionPoint($"{name}-ram64.din1");
			_din2 = new ConnectionPoint($"{name}-ram64.din2");
			_din3 = new ConnectionPoint($"{name}-ram64.din3");
			_din4 = new ConnectionPoint($"{name}-ram64.din4");
			_din5 = new ConnectionPoint($"{name}-ram64.din5");
			_din6 = new ConnectionPoint($"{name}-ram64.din6");
			_din7 = new ConnectionPoint($"{name}-ram64.din7");

			_dout0 = new ConnectionPoint($"{name}-ram64.dout0");
			_dout1 = new ConnectionPoint($"{name}-ram64.dout1");
			_dout2 = new ConnectionPoint($"{name}-ram64.dout2");
			_dout3 = new ConnectionPoint($"{name}-ram64.dout3");
			_dout4 = new ConnectionPoint($"{name}-ram64.dout4");
			_dout5 = new ConnectionPoint($"{name}-ram64.dout5");
			_dout6 = new ConnectionPoint($"{name}-ram64.dout6");
			_dout7 = new ConnectionPoint($"{name}-ram64.dout7");

			DoWireUp();
			Reset();
		}
		#endregion

		#region Implementation
		private byte[] _array = new byte[65536];
		private readonly ConnectionPoint _voltage;
		private readonly ConnectionPoint _clk;
		private readonly ConnectionPoint _a0;
		private readonly ConnectionPoint _a1;
		private readonly ConnectionPoint _a2;
		private readonly ConnectionPoint _a3;
		private readonly ConnectionPoint _a4;
		private readonly ConnectionPoint _a5;
		private readonly ConnectionPoint _a6;
		private readonly ConnectionPoint _a7;
		private readonly ConnectionPoint _a8;
		private readonly ConnectionPoint _a9;
		private readonly ConnectionPoint _a10;
		private readonly ConnectionPoint _a11;
		private readonly ConnectionPoint _a12;
		private readonly ConnectionPoint _a13;
		private readonly ConnectionPoint _a14;
		private readonly ConnectionPoint _a15;
		private readonly ConnectionPoint _din0;
		private readonly ConnectionPoint _din1;
		private readonly ConnectionPoint _din2;
		private readonly ConnectionPoint _din3;
		private readonly ConnectionPoint _din4;
		private readonly ConnectionPoint _din5;
		private readonly ConnectionPoint _din6;
		private readonly ConnectionPoint _din7;
		private readonly ConnectionPoint _dout0;
		private readonly ConnectionPoint _dout1;
		private readonly ConnectionPoint _dout2;
		private readonly ConnectionPoint _dout3;
		private readonly ConnectionPoint _dout4;
		private readonly ConnectionPoint _dout5;
		private readonly ConnectionPoint _dout6;
		private readonly ConnectionPoint _dout7;
		#endregion

		#region IRam64KB Members

		public ConnectionPoint V => _voltage;
		public ConnectionPoint Write => _clk;

		public ConnectionPoint A0 => _a0;
		public ConnectionPoint A1 => _a1;
		public ConnectionPoint A2 => _a2;
		public ConnectionPoint A3 => _a3;
		public ConnectionPoint A4 => _a4;
		public ConnectionPoint A5 => _a5;
		public ConnectionPoint A6 => _a6;
		public ConnectionPoint A7 => _a7;
		public ConnectionPoint A8 => _a8;
		public ConnectionPoint A9 => _a9;
		public ConnectionPoint A10 => _a10;
		public ConnectionPoint A11 => _a11;
		public ConnectionPoint A12 => _a12;
		public ConnectionPoint A13 => _a13;
		public ConnectionPoint A14 => _a14;
		public ConnectionPoint A15 => _a15;

		public ConnectionPoint Din0 => _din0;
		public ConnectionPoint Din1 => _din1;
		public ConnectionPoint Din2 => _din2;
		public ConnectionPoint Din3 => _din3;
		public ConnectionPoint Din4 => _din4;
		public ConnectionPoint Din5 => _din5;
		public ConnectionPoint Din6 => _din6;
		public ConnectionPoint Din7 => _din7;

		public ConnectionPoint Dout0 => _dout0;
		public ConnectionPoint Dout1 => _dout1;
		public ConnectionPoint Dout2 => _dout2;
		public ConnectionPoint Dout3 => _dout3;
		public ConnectionPoint Dout4 => _dout4;
		public ConnectionPoint Dout5 => _dout5;
		public ConnectionPoint Dout6 => _dout6;
		public ConnectionPoint Dout7 => _dout7;

		#endregion

		#region Private Members
		private void Reset()
		{
			for (int i = 0; i < 65536; ++i)
			{
				_array[i] = 0;
			}
			_voltage.V = VoltageSignal.LOW;
			_clk.V = VoltageSignal.LOW;
			_a0.V = VoltageSignal.LOW;
			_a1.V = VoltageSignal.LOW;
			_a2.V = VoltageSignal.LOW;
			_a3.V = VoltageSignal.LOW;
			_a4.V = VoltageSignal.LOW;
			_a5.V = VoltageSignal.LOW;
			_a6.V = VoltageSignal.LOW;
			_a7.V = VoltageSignal.LOW;
			_a8.V = VoltageSignal.LOW;
			_a9.V = VoltageSignal.LOW;
			_a10.V = VoltageSignal.LOW;
			_a11.V = VoltageSignal.LOW;
			_a12.V = VoltageSignal.LOW;
			_a13.V = VoltageSignal.LOW;
			_a14.V = VoltageSignal.LOW;
			_a15.V = VoltageSignal.LOW;
			_dout0.V = VoltageSignal.LOW;
			_dout1.V = VoltageSignal.LOW;
			_dout2.V = VoltageSignal.LOW;
			_dout3.V = VoltageSignal.LOW;
			_dout4.V = VoltageSignal.LOW;
			_dout5.V = VoltageSignal.LOW;
			_dout6.V = VoltageSignal.LOW;
			_dout7.V = VoltageSignal.LOW;
			_din0.V = VoltageSignal.LOW;
			_din1.V = VoltageSignal.LOW;
			_din2.V = VoltageSignal.LOW;
			_din3.V = VoltageSignal.LOW;
			_din4.V = VoltageSignal.LOW;
			_din5.V = VoltageSignal.LOW;
			_din6.V = VoltageSignal.LOW;
			_din7.V = VoltageSignal.LOW;
		}

		private void ReadData()
		{
			ushort address = 0;

			address |= (ushort)(_a0.V == VoltageSignal.HIGH ? 0x0001 : 0x0000);
			address |= (ushort)(_a1.V == VoltageSignal.HIGH ? 0x0002 : 0x0000);
			address |= (ushort)(_a2.V == VoltageSignal.HIGH ? 0x0004 : 0x0000);
			address |= (ushort)(_a3.V == VoltageSignal.HIGH ? 0x0008 : 0x0000);
			address |= (ushort)(_a4.V == VoltageSignal.HIGH ? 0x0010 : 0x0000);
			address |= (ushort)(_a5.V == VoltageSignal.HIGH ? 0x0020 : 0x0000);
			address |= (ushort)(_a6.V == VoltageSignal.HIGH ? 0x0040 : 0x0000);
			address |= (ushort)(_a7.V == VoltageSignal.HIGH ? 0x0080 : 0x0000);
			address |= (ushort)(_a8.V == VoltageSignal.HIGH ? 0x0100 : 0x0000);
			address |= (ushort)(_a9.V == VoltageSignal.HIGH ? 0x0200 : 0x0000);
			address |= (ushort)(_a10.V == VoltageSignal.HIGH ? 0x0400 : 0x0000);
			address |= (ushort)(_a11.V == VoltageSignal.HIGH ? 0x0800 : 0x0000);
			address |= (ushort)(_a12.V == VoltageSignal.HIGH ? 0x1000 : 0x0000);
			address |= (ushort)(_a13.V == VoltageSignal.HIGH ? 0x2000 : 0x0000);
			address |= (ushort)(_a14.V == VoltageSignal.HIGH ? 0x4000 : 0x0000);
			address |= (ushort)(_a15.V == VoltageSignal.HIGH ? 0x8000 : 0x0000);

			byte data = _array[address];

			_dout0.V = ((data & 0x01) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_dout1.V = ((data & 0x02) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_dout2.V = ((data & 0x04) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_dout3.V = ((data & 0x08) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_dout4.V = ((data & 0x10) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_dout5.V = ((data & 0x20) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_dout6.V = ((data & 0x40) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_dout7.V = ((data & 0x80) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
		}

		private void WriteData()
		{
			ushort address = 0;

			address |= (ushort)(_a0.V == VoltageSignal.HIGH ? 0x0001 : 0x0);
			address |= (ushort)(_a1.V == VoltageSignal.HIGH ? 0x0002 : 0x0);
			address |= (ushort)(_a2.V == VoltageSignal.HIGH ? 0x0004 : 0x0);
			address |= (ushort)(_a3.V == VoltageSignal.HIGH ? 0x0008 : 0x0);
			address |= (ushort)(_a4.V == VoltageSignal.HIGH ? 0x0010 : 0x0);
			address |= (ushort)(_a5.V == VoltageSignal.HIGH ? 0x0020 : 0x0);
			address |= (ushort)(_a6.V == VoltageSignal.HIGH ? 0x0040 : 0x0);
			address |= (ushort)(_a7.V == VoltageSignal.HIGH ? 0x0080 : 0x0);
			address |= (ushort)(_a8.V == VoltageSignal.HIGH ? 0x0100 : 0x0);
			address |= (ushort)(_a9.V == VoltageSignal.HIGH ? 0x0200 : 0x0);
			address |= (ushort)(_a10.V == VoltageSignal.HIGH ? 0x0400 : 0x0);
			address |= (ushort)(_a11.V == VoltageSignal.HIGH ? 0x0800 : 0x0);
			address |= (ushort)(_a12.V == VoltageSignal.HIGH ? 0x1000 : 0x0);
			address |= (ushort)(_a13.V == VoltageSignal.HIGH ? 0x2000 : 0x0);
			address |= (ushort)(_a14.V == VoltageSignal.HIGH ? 0x4000 : 0x0);
			address |= (ushort)(_a15.V == VoltageSignal.HIGH ? 0x8000 : 0x0);

			byte data = 0;

			data |= (byte)(_din0.V == VoltageSignal.HIGH ? 0x01 : 0x0);
			data |= (byte)(_din1.V == VoltageSignal.HIGH ? 0x02 : 0x0);
			data |= (byte)(_din2.V == VoltageSignal.HIGH ? 0x04 : 0x0);
			data |= (byte)(_din3.V == VoltageSignal.HIGH ? 0x08 : 0x0);
			data |= (byte)(_din4.V == VoltageSignal.HIGH ? 0x10 : 0x0);
			data |= (byte)(_din5.V == VoltageSignal.HIGH ? 0x20 : 0x0);
			data |= (byte)(_din6.V == VoltageSignal.HIGH ? 0x40 : 0x0);
			data |= (byte)(_din7.V == VoltageSignal.HIGH ? 0x80 : 0x0);

			_array[address] = data;

			_dout0.V = _din0.V;
			_dout1.V = _din1.V;
			_dout2.V = _din2.V;
			_dout3.V = _din3.V;
			_dout4.V = _din4.V;
			_dout5.V = _din5.V;
			_dout6.V = _din6.V;
			_dout7.V = _din7.V;
		}

		private void DoWireUp()
		{
			_voltage.Changed += voltage => { if (voltage.V == VoltageSignal.LOW) Reset(); };
			_clk.Changed += clk => { if (clk.V == VoltageSignal.HIGH) WriteData(); };
			_a0.Changed += _ => ReadData();
			_a1.Changed += _ => ReadData();
			_a2.Changed += _ => ReadData();
			_a3.Changed += _ => ReadData();
			_a4.Changed += _ => ReadData();
			_a5.Changed += _ => ReadData();
			_a6.Changed += _ => ReadData();
			_a7.Changed += _ => ReadData();
			_a8.Changed += _ => ReadData();
			_a9.Changed += _ => ReadData();
			_a10.Changed += _ => ReadData();
			_a11.Changed += _ => ReadData();
			_a12.Changed += _ => ReadData();
			_a13.Changed += _ => ReadData();
			_a14.Changed += _ => ReadData();
			_a15.Changed += _ => ReadData();
		}
		#endregion
	}
}
