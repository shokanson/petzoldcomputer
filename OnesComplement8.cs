using System;
namespace PetzoldComputer
{
	public class OnesComplement8 : IOnesComplement8, IOutput
	{
		#region Construction
		public OnesComplement8()
		{
			_xor0 = new XOR();
			_xor1 = new XOR();
			_xor2 = new XOR();
			_xor3 = new XOR();
			_xor4 = new XOR();
			_xor5 = new XOR();
			_xor6 = new XOR();
			_xor7 = new XOR();

			_out = 0x00;
		}
		#endregion

		#region Implementation
		private IXor _xor0;
		private IXor _xor1;
		private IXor _xor2;
		private IXor _xor3;
		private IXor _xor4;
		private IXor _xor5;
		private IXor _xor6;
		private IXor _xor7;

		private byte _out;

		private Action<object> OutEvent;
		#endregion

		#region IInverter8 Members

		public VoltageSignal Voltage
		{
			get { return _xor0.Voltage; }
			set
			{
				byte oldOutput = _out;

				_xor0.Voltage = value;
				_xor1.Voltage = value;
				_xor2.Voltage = value;
				_xor3.Voltage = value;
				_xor4.Voltage = value;
				_xor5.Voltage = value;
				_xor6.Voltage = value;
				_xor7.Voltage = value;

				SetOutput();
				FireEvent(oldOutput);
			}
		}

		public VoltageSignal Invert
		{
			get { return _xor0.A; }
			set
			{
				byte oldOutput = _out;

				_xor0.A = value;
				_xor1.A = value;
				_xor2.A = value;
				_xor3.A = value;
				_xor4.A = value;
				_xor5.A = value;
				_xor6.A = value;
				_xor7.A = value;

				SetOutput();
				FireEvent(oldOutput);
			}
		}

		public VoltageSignal I0
		{
			get { return _xor0.B; }
			set
			{
				byte oldOutput = _out;

				_xor0.B = value;

				SetOutput();
				FireEvent(oldOutput);
			}
		}

		public VoltageSignal I1
		{
			get { return _xor1.B; }
			set
			{
				byte oldOutput = _out;

				_xor1.B = value;

				SetOutput();
				FireEvent(oldOutput);
			}
		}

		public VoltageSignal I2
		{
			get { return _xor2.B; }
			set
			{
				byte oldOutput = _out;

				_xor2.B = value;

				SetOutput();
				FireEvent(oldOutput);
			}
		}

		public VoltageSignal I3
		{
			get { return _xor3.B; }
			set
			{
				byte oldOutput = _out;

				_xor3.B = value;

				SetOutput();
				FireEvent(oldOutput);
			}
		}

		public VoltageSignal I4
		{
			get { return _xor4.B; }
			set
			{
				byte oldOutput = _out;

				_xor4.B = value;

				SetOutput();
				FireEvent(oldOutput);
			}
		}

		public VoltageSignal I5
		{
			get { return _xor5.B; }
			set
			{
				byte oldOutput = _out;

				_xor5.B = value;

				SetOutput();
				FireEvent(oldOutput);
			}
		}

		public VoltageSignal I6
		{
			get { return _xor6.B; }
			set
			{
				byte oldOutput = _out;

				_xor6.B = value;

				SetOutput();
				FireEvent(oldOutput);
			}
		}

		public VoltageSignal I7
		{
			get { return _xor7.B; }
			set
			{
				byte oldOutput = _out;

				_xor7.B = value;

				SetOutput();
				FireEvent(oldOutput);
			}
		}

		public VoltageSignal O0
		{
			get { return _xor0.O; }
		}

		public VoltageSignal O1
		{
			get { return _xor1.O; }
		}

		public VoltageSignal O2
		{
			get { return _xor2.O; }
		}

		public VoltageSignal O3
		{
			get { return _xor3.O; }
		}

		public VoltageSignal O4
		{
			get { return _xor4.O; }
		}

		public VoltageSignal O5
		{
			get { return _xor5.O; }
		}

		public VoltageSignal O6
		{
			get { return _xor6.O; }
		}

		public VoltageSignal O7
		{
			get { return _xor7.O; }
		}

		#endregion

		#region IOutputEvent Members

		public void AddOutputHandler(Action<object> handler)
		{
			OutEvent += handler;
		}

		#endregion

		#region Object Override Methods
		public override string ToString()
		{
			return
				string.Format(
					"{0}{1}{2}{3}{4}{5}{6}{7}",
					_xor7.O == VoltageSignal.HIGH ? 1 : 0,
					_xor6.O == VoltageSignal.HIGH ? 1 : 0,
					_xor5.O == VoltageSignal.HIGH ? 1 : 0,
					_xor4.O == VoltageSignal.HIGH ? 1 : 0,
					_xor3.O == VoltageSignal.HIGH ? 1 : 0,
					_xor2.O == VoltageSignal.HIGH ? 1 : 0,
					_xor1.O == VoltageSignal.HIGH ? 1 : 0,
					_xor0.O == VoltageSignal.HIGH ? 1 : 0);
		}
		#endregion

		#region Private Members
		private void SetOutput()
		{
			_out = 0x00;

			_out |= (_xor0.O == VoltageSignal.HIGH ? (byte)0x01 : (byte)0x00);
			_out |= (_xor1.O == VoltageSignal.HIGH ? (byte)0x02 : (byte)0x00);
			_out |= (_xor2.O == VoltageSignal.HIGH ? (byte)0x04 : (byte)0x00);
			_out |= (_xor3.O == VoltageSignal.HIGH ? (byte)0x08 : (byte)0x00);
			_out |= (_xor4.O == VoltageSignal.HIGH ? (byte)0x10 : (byte)0x00);
			_out |= (_xor5.O == VoltageSignal.HIGH ? (byte)0x20 : (byte)0x00);
			_out |= (_xor6.O == VoltageSignal.HIGH ? (byte)0x40 : (byte)0x00);
			_out |= (_xor7.O == VoltageSignal.HIGH ? (byte)0x80 : (byte)0x00);
		}

		private void FireEvent(byte oldOutput)
		{
			if (oldOutput != _out)
			{
				if (OutEvent != null)
				{
					OutEvent(this);
				}
			}
		}
		#endregion
	}
}

/*
$Log: /PetzoldComputer/OnesComplement8.cs $ $NoKeyWords:$
 * 
 * 3     1/21/07 11:58p Sean
 * results of ReSharper analysis
*/
