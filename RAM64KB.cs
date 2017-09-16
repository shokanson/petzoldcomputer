using System;
namespace PetzoldComputer
{
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
			get { return _voltage; }
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
			get { return _clk; }
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
			get { return _a0; }
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
			get { return _a1; }
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
			get { return _a2; }
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
			get { return _a3; }
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
			get { return _a4; }
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
			get { return _a5; }
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
			get { return _a6; }
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
			get { return _a7; }
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
			get { return _a8; }
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
			get { return _a9; }
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
			get { return _a10; }
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
			get { return _a11; }
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
			get { return _a12; }
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
			get { return _a13; }
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
			get { return _a14; }
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
			get { return _a15; }
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
			get { return _din0; }
			set { _din0 = value; }
		}

		public VoltageSignal Din1
		{
			get { return _din1; }
			set { _din1 = value; }
		}

		public VoltageSignal Din2
		{
			get { return _din2; }
			set { _din2 = value; }
		}

		public VoltageSignal Din3
		{
			get { return _din3; }
			set { _din3 = value; }
		}

		public VoltageSignal Din4
		{
			get { return _din4; }
			set { _din4 = value; }
		}

		public VoltageSignal Din5
		{
			get { return _din5; }
			set { _din5 = value; }
		}

		public VoltageSignal Din6
		{
			get { return _din6; }
			set { _din6 = value; }
		}

		public VoltageSignal Din7
		{
			get { return _din7; }
			set { _din7 = value; }
		}

		public VoltageSignal Dout0
		{
			get { return _dout0; }
		}

		public VoltageSignal Dout1
		{
			get { return _dout1; }
		}

		public VoltageSignal Dout2
		{
			get { return _dout2; }
		}

		public VoltageSignal Dout3
		{
			get { return _dout3; }
		}

		public VoltageSignal Dout4
		{
			get { return _dout4; }
		}

		public VoltageSignal Dout5
		{
			get { return _dout5; }
		}

		public VoltageSignal Dout6
		{
			get { return _dout6; }
		}

		public VoltageSignal Dout7
		{
			get { return _dout7; }
		}

		#endregion

		#region IOutput Members

		public void AddOutputHandler(Action<object> handler)
		{
			OutEvent += handler;
		}

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
}

/*
$Log: /PetzoldComputer/RAM64KB.cs $ $NoKeyWords:$
 * 
 * 3     1/26/07 6:54a Sean
 * results of ReSharper analysis
 * 
 * 2     1/21/07 11:58p Sean
 * results of ReSharper analysis
*/
