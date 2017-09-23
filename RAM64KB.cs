using System;

namespace PetzoldComputer
{
	// This RAM, while it has the interface as described in Chapter 16 "An Assemblage of Memory",
	// it isn't implemented internally with the circuitry described there.  Rather, it's a simple
	// C# array of bytes.  An exercise for a future day to implement it as a collection of latches,
	// selectors, and decoders...
	public class RAM64KB : IRam64KB, IOutput
	{
		#region Construction
		public RAM64KB()
		{
			Reset();
		}
		#endregion

		#region Implementation
		private byte[] _array = new byte[65536];
		private VoltageSignal _voltage;
		private VoltageSignal _a0;
		private VoltageSignal _a1;
		private VoltageSignal _a2;
		private VoltageSignal _a3;
		private VoltageSignal _a4;
		private VoltageSignal _a5;
		private VoltageSignal _a6;
		private VoltageSignal _a7;
		private VoltageSignal _a8;
		private VoltageSignal _a9;
		private VoltageSignal _a10;
		private VoltageSignal _a11;
		private VoltageSignal _a12;
		private VoltageSignal _a13;
		private VoltageSignal _a14;
		private VoltageSignal _a15;
		private VoltageSignal _dout0;
		private VoltageSignal _dout1;
		private VoltageSignal _dout2;
		private VoltageSignal _dout3;
		private VoltageSignal _dout4;
		private VoltageSignal _dout5;
		private VoltageSignal _dout6;
		private VoltageSignal _dout7;
		private VoltageSignal _clk;
		private VoltageSignal _din0;
		private VoltageSignal _din1;
		private VoltageSignal _din2;
		private VoltageSignal _din3;
		private VoltageSignal _din4;
		private VoltageSignal _din5;
		private VoltageSignal _din6;
		private VoltageSignal _din7;

		private Action<object> OutEvent;
		#endregion

		#region IRam64KB Members

		public VoltageSignal Voltage
		{
			get => _voltage;
			set
			{
				if (_voltage == VoltageSignal.HIGH && value == VoltageSignal.LOW)
				{
					Reset();
				}
				_voltage = value;
			}
		}

		public VoltageSignal Write
		{
			get => _clk;
			set
			{
				if (_voltage == VoltageSignal.HIGH)
				{
					if (_clk == VoltageSignal.LOW && value == VoltageSignal.HIGH)
					{
						WriteData();
					}
				}
				_clk = value;
			}
		}

		public VoltageSignal A0
		{
			get => _a0;
			set
			{
				_a0 = value;
				if (_voltage == VoltageSignal.HIGH)
				{
					ReadData();
				}
			}
		}

		public VoltageSignal A1
		{
			get => _a1;
			set
			{
				_a1 = value;
				if (_voltage == VoltageSignal.HIGH)
				{
					ReadData();
				}
			}
		}

		public VoltageSignal A2
		{
			get => _a2;
			set
			{
				_a2 = value;
				if (_voltage == VoltageSignal.HIGH)
				{
					ReadData();
				}
			}
		}

		public VoltageSignal A3
		{
			get => _a3;
			set
			{
				_a3 = value;
				if (_voltage == VoltageSignal.HIGH)
				{
					ReadData();
				}
			}
		}

		public VoltageSignal A4
		{
			get => _a4;
			set
			{
				_a4 = value;
				if (_voltage == VoltageSignal.HIGH)
				{
					ReadData();
				}
			}
		}

		public VoltageSignal A5
		{
			get => _a5;
			set
			{
				_a5 = value;
				if (_voltage == VoltageSignal.HIGH)
				{
					ReadData();
				}
			}
		}

		public VoltageSignal A6
		{
			get => _a6;
			set
			{
				_a6 = value;
				if (_voltage == VoltageSignal.HIGH)
				{
					ReadData();
				}
			}
		}

		public VoltageSignal A7
		{
			get => _a7;
			set
			{
				_a7 = value;
				if (_voltage == VoltageSignal.HIGH)
				{
					ReadData();
				}
			}
		}

		public VoltageSignal A8
		{
			get => _a8;
			set
			{
				_a8 = value;
				if (_voltage == VoltageSignal.HIGH)
				{
					ReadData();
				}
			}
		}

		public VoltageSignal A9
		{
			get => _a9;
			set
			{
				_a9 = value;
				if (_voltage == VoltageSignal.HIGH)
				{
					ReadData();
				}
			}
		}

		public VoltageSignal A10
		{
			get => _a10;
			set
			{
				_a10 = value;
				if (_voltage == VoltageSignal.HIGH)
				{
					ReadData();
				}
			}
		}

		public VoltageSignal A11
		{
			get => _a11;
			set
			{
				_a11 = value;
				if (_voltage == VoltageSignal.HIGH)
				{
					ReadData();
				}
			}
		}

		public VoltageSignal A12
		{
			get => _a12;
			set
			{
				_a12 = value;
				if (_voltage == VoltageSignal.HIGH)
				{
					ReadData();
				}
			}
		}

		public VoltageSignal A13
		{
			get => _a13;
			set
			{
				_a13 = value;
				if (_voltage == VoltageSignal.HIGH)
				{
					ReadData();
				}
			}
		}

		public VoltageSignal A14
		{
			get => _a14;
			set
			{
				_a14 = value;
				if (_voltage == VoltageSignal.HIGH)
				{
					ReadData();
				}
			}
		}

		public VoltageSignal A15
		{
			get => _a15;
			set
			{
				_a15 = value;
				if (_voltage == VoltageSignal.HIGH)
				{
					ReadData();
				}
			}
		}

		public VoltageSignal Din0
		{
			get => _din0;
			set => _din0 = value;
		}

		public VoltageSignal Din1
		{
			get => _din1;
			set => _din1 = value;
		}

		public VoltageSignal Din2
		{
			get => _din2;
			set => _din2 = value;
		}

		public VoltageSignal Din3
		{
			get => _din3;
			set => _din3 = value;
		}

		public VoltageSignal Din4
		{
			get => _din4;
			set => _din4 = value;
		}

		public VoltageSignal Din5
		{
			get => _din5;
			set => _din5 = value;
		}

		public VoltageSignal Din6
		{
			get => _din6;
			set => _din6 = value;
		}

		public VoltageSignal Din7
		{
			get => _din7;
			set => _din7 = value;
		}

		public VoltageSignal Dout0 => _dout0;
		public VoltageSignal Dout1 => _dout1;
		public VoltageSignal Dout2 => _dout2;
		public VoltageSignal Dout3 => _dout3;
		public VoltageSignal Dout4 => _dout4;
		public VoltageSignal Dout5 => _dout5;
		public VoltageSignal Dout6 => _dout6;
		public VoltageSignal Dout7 => _dout7;

		#endregion

		#region IOutput Members

		public void AddOutputHandler(Action<object> handler) => OutEvent += handler;

		#endregion

		#region Private Members
		private void Reset()
		{
			for (int i = 0; i < 65536; ++i)
			{
				_array[i] = 0;
			}
			_voltage = VoltageSignal.LOW;
			_a0 = VoltageSignal.LOW;
			_a1 = VoltageSignal.LOW;
			_a2 = VoltageSignal.LOW;
			_a3 = VoltageSignal.LOW;
			_a4 = VoltageSignal.LOW;
			_a5 = VoltageSignal.LOW;
			_a6 = VoltageSignal.LOW;
			_a7 = VoltageSignal.LOW;
			_a8 = VoltageSignal.LOW;
			_a9 = VoltageSignal.LOW;
			_a10 = VoltageSignal.LOW;
			_a11 = VoltageSignal.LOW;
			_a12 = VoltageSignal.LOW;
			_a13 = VoltageSignal.LOW;
			_a14 = VoltageSignal.LOW;
			_a15 = VoltageSignal.LOW;
			_dout0 = VoltageSignal.LOW;
			_dout1 = VoltageSignal.LOW;
			_dout2 = VoltageSignal.LOW;
			_dout3 = VoltageSignal.LOW;
			_dout4 = VoltageSignal.LOW;
			_dout5 = VoltageSignal.LOW;
			_dout6 = VoltageSignal.LOW;
			_dout7 = VoltageSignal.LOW;
			_clk = VoltageSignal.LOW;
			_din0 = VoltageSignal.LOW;
			_din1 = VoltageSignal.LOW;
			_din2 = VoltageSignal.LOW;
			_din3 = VoltageSignal.LOW;
			_din4 = VoltageSignal.LOW;
			_din5 = VoltageSignal.LOW;
			_din6 = VoltageSignal.LOW;
			_din7 = VoltageSignal.LOW;
		}

		private void ReadData()
		{
			ushort address = 0;

			address |= (ushort)(_a0  == VoltageSignal.HIGH ? 0x0001 : 0x0000);
			address |= (ushort)(_a1  == VoltageSignal.HIGH ? 0x0002 : 0x0000);
			address |= (ushort)(_a2  == VoltageSignal.HIGH ? 0x0004 : 0x0000);
			address |= (ushort)(_a3  == VoltageSignal.HIGH ? 0x0008 : 0x0000);
			address |= (ushort)(_a4  == VoltageSignal.HIGH ? 0x0010 : 0x0000);
			address |= (ushort)(_a5  == VoltageSignal.HIGH ? 0x0020 : 0x0000);
			address |= (ushort)(_a6  == VoltageSignal.HIGH ? 0x0040 : 0x0000);
			address |= (ushort)(_a7  == VoltageSignal.HIGH ? 0x0080 : 0x0000);
			address |= (ushort)(_a8  == VoltageSignal.HIGH ? 0x0100 : 0x0000);
			address |= (ushort)(_a9  == VoltageSignal.HIGH ? 0x0200 : 0x0000);
			address |= (ushort)(_a10 == VoltageSignal.HIGH ? 0x0400 : 0x0000);
			address |= (ushort)(_a11 == VoltageSignal.HIGH ? 0x0800 : 0x0000);
			address |= (ushort)(_a12 == VoltageSignal.HIGH ? 0x1000 : 0x0000);
			address |= (ushort)(_a13 == VoltageSignal.HIGH ? 0x2000 : 0x0000);
			address |= (ushort)(_a14 == VoltageSignal.HIGH ? 0x4000 : 0x0000);
			address |= (ushort)(_a15 == VoltageSignal.HIGH ? 0x8000 : 0x0000);

			byte data = _array[address];

			bool outputChanged = false;
			VoltageSignal oldData;

			oldData = _dout0;
			_dout0 = ((data & 0x01) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			if (oldData != _dout0)
			{
				outputChanged = true;
			}

			oldData = _dout1;
			_dout1 = ((data & 0x02) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			if (!outputChanged && oldData != _dout1)
			{
				outputChanged = true;
			}

			oldData = _dout2;
			_dout2 = ((data & 0x04) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			if (!outputChanged && oldData != _dout2)
			{
				outputChanged = true;
			}

			oldData = _dout3;
			_dout3 = ((data & 0x08) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			if (!outputChanged && oldData != _dout3)
			{
				outputChanged = true;
			}

			oldData = _dout4;
			_dout4 = ((data & 0x10) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			if (!outputChanged && oldData != _dout4)
			{
				outputChanged = true;
			}

			oldData = _dout5;
			_dout5 = ((data & 0x20) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			if (!outputChanged && oldData != _dout5)
			{
				outputChanged = true;
			}

			oldData = _dout6;
			_dout6 = ((data & 0x40) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			if (!outputChanged && oldData != _dout6)
			{
				outputChanged = true;
			}

			oldData = _dout7;
			_dout7 = ((data & 0x80) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			if (!outputChanged && oldData != _dout7)
			{
				outputChanged = true;
			}

			if (outputChanged && OutEvent != null)
			{
				OutEvent(this);
			}
		}

		private void WriteData()
		{
			ushort address = 0;

			address |= (ushort)(_a0  == VoltageSignal.HIGH ? 0x0001 : 0x0);
			address |= (ushort)(_a1  == VoltageSignal.HIGH ? 0x0002 : 0x0);
			address |= (ushort)(_a2  == VoltageSignal.HIGH ? 0x0004 : 0x0);
			address |= (ushort)(_a3  == VoltageSignal.HIGH ? 0x0008 : 0x0);
			address |= (ushort)(_a4  == VoltageSignal.HIGH ? 0x0010 : 0x0);
			address |= (ushort)(_a5  == VoltageSignal.HIGH ? 0x0020 : 0x0);
			address |= (ushort)(_a6  == VoltageSignal.HIGH ? 0x0040 : 0x0);
			address |= (ushort)(_a7  == VoltageSignal.HIGH ? 0x0080 : 0x0);
			address |= (ushort)(_a8  == VoltageSignal.HIGH ? 0x0100 : 0x0);
			address |= (ushort)(_a9  == VoltageSignal.HIGH ? 0x0200 : 0x0);
			address |= (ushort)(_a10 == VoltageSignal.HIGH ? 0x0400 : 0x0);
			address |= (ushort)(_a11 == VoltageSignal.HIGH ? 0x0800 : 0x0);
			address |= (ushort)(_a12 == VoltageSignal.HIGH ? 0x1000 : 0x0);
			address |= (ushort)(_a13 == VoltageSignal.HIGH ? 0x2000 : 0x0);
			address |= (ushort)(_a14 == VoltageSignal.HIGH ? 0x4000 : 0x0);
			address |= (ushort)(_a15 == VoltageSignal.HIGH ? 0x8000 : 0x0);

			byte data = 0;

			data |= (byte)(_din0 == VoltageSignal.HIGH ? 0x01 : 0x0);
			data |= (byte)(_din1 == VoltageSignal.HIGH ? 0x02 : 0x0);
			data |= (byte)(_din2 == VoltageSignal.HIGH ? 0x04 : 0x0);
			data |= (byte)(_din3 == VoltageSignal.HIGH ? 0x08 : 0x0);
			data |= (byte)(_din4 == VoltageSignal.HIGH ? 0x10 : 0x0);
			data |= (byte)(_din5 == VoltageSignal.HIGH ? 0x20 : 0x0);
			data |= (byte)(_din6 == VoltageSignal.HIGH ? 0x40 : 0x0);
			data |= (byte)(_din7 == VoltageSignal.HIGH ? 0x80 : 0x0);

			_array[address] = data;

			bool outputChanged = false;

			if (_dout0 != _din0)
			{
				_dout0 = _din0;
				outputChanged = true;
			}
			if (_dout1 != _din1)
			{
				_dout1 = _din1;
				outputChanged = true;
			}
			if (_dout2 != _din2)
			{
				_dout2 = _din2;
				outputChanged = true;
			}
			if (_dout3 != _din3)
			{
				_dout3 = _din3;
				outputChanged = true;
			}
			if (_dout4 != _din4)
			{
				_dout4 = _din4;
				outputChanged = true;
			}
			if (_dout5 != _din5)
			{
				_dout5 = _din5;
				outputChanged = true;
			}
			if (_dout6 != _din6)
			{
				_dout6 = _din6;
				outputChanged = true;
			}
			if (_dout7 != _din7)
			{
				_dout7 = _din7;
				outputChanged = true;
			}

			if (outputChanged && OutEvent != null)
			{
				OutEvent(this);
			}
		}
		#endregion
	}

	// This RAM, while it has the interface as described in Chapter 16 "An Assemblage of Memory",
	// it isn't implemented internally with the circuitry described there.  Rather, it's a simple
	// C# array of bytes.  An exercise for a future day to implement it as a collection of latches,
	// selectors, and decoders...
	public class RAM64KB_2
	{
		#region Construction
		public RAM64KB_2()
		{
			DoWireUp();
			Reset();
		}
		#endregion

		#region Implementation
		private byte[] _array = new byte[65536];
		private readonly ConnectionPoint _voltage = new ConnectionPoint();
		private readonly ConnectionPoint _a0 = new ConnectionPoint();
		private readonly ConnectionPoint _a1 = new ConnectionPoint();
		private readonly ConnectionPoint _a2 = new ConnectionPoint();
		private readonly ConnectionPoint _a3 = new ConnectionPoint();
		private readonly ConnectionPoint _a4 = new ConnectionPoint();
		private readonly ConnectionPoint _a5 = new ConnectionPoint();
		private readonly ConnectionPoint _a6 = new ConnectionPoint();
		private readonly ConnectionPoint _a7 = new ConnectionPoint();
		private readonly ConnectionPoint _a8 = new ConnectionPoint();
		private readonly ConnectionPoint _a9 = new ConnectionPoint();
		private readonly ConnectionPoint _a10 = new ConnectionPoint();
		private readonly ConnectionPoint _a11 = new ConnectionPoint();
		private readonly ConnectionPoint _a12 = new ConnectionPoint();
		private readonly ConnectionPoint _a13 = new ConnectionPoint();
		private readonly ConnectionPoint _a14 = new ConnectionPoint();
		private readonly ConnectionPoint _a15 = new ConnectionPoint();
		private readonly ConnectionPoint _dout0 = new ConnectionPoint();
		private readonly ConnectionPoint _dout1 = new ConnectionPoint();
		private readonly ConnectionPoint _dout2 = new ConnectionPoint();
		private readonly ConnectionPoint _dout3 = new ConnectionPoint();
		private readonly ConnectionPoint _dout4 = new ConnectionPoint();
		private readonly ConnectionPoint _dout5 = new ConnectionPoint();
		private readonly ConnectionPoint _dout6 = new ConnectionPoint();
		private readonly ConnectionPoint _dout7 = new ConnectionPoint();
		private readonly ConnectionPoint _clk = new ConnectionPoint();
		private readonly ConnectionPoint _din0 = new ConnectionPoint();
		private readonly ConnectionPoint _din1 = new ConnectionPoint();
		private readonly ConnectionPoint _din2 = new ConnectionPoint();
		private readonly ConnectionPoint _din3 = new ConnectionPoint();
		private readonly ConnectionPoint _din4 = new ConnectionPoint();
		private readonly ConnectionPoint _din5 = new ConnectionPoint();
		private readonly ConnectionPoint _din6 = new ConnectionPoint();
		private readonly ConnectionPoint _din7 = new ConnectionPoint();
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
			_voltage.Changed += cp => { if (cp.V == VoltageSignal.LOW) Reset(); };
			_clk.Changed += cp => { if (cp.V == VoltageSignal.HIGH) WriteData(); };
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
