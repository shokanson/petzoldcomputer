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
			get => _xor0.Voltage;
			set
			{
				byte oldOutput = _out;

				_xor0.Voltage = 
					_xor1.Voltage = 
					_xor2.Voltage = 
					_xor3.Voltage = 
					_xor4.Voltage = 
					_xor5.Voltage = 
					_xor6.Voltage =
					_xor7.Voltage = value;

				SetOutput();
				FireEvent(oldOutput);
			}
		}

		public VoltageSignal Invert
		{
			get => _xor0.A;
			set
			{
				byte oldOutput = _out;

				_xor0.A = 
					_xor1.A = 
					_xor2.A = 
					_xor3.A = 
					_xor4.A =
					_xor5.A = 
					_xor6.A = 
					_xor7.A = value;

				SetOutput();
				FireEvent(oldOutput);
			}
		}

		public VoltageSignal I0
		{
			get => _xor0.B;
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
			get => _xor1.B;
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
			get => _xor2.B;
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
			get => _xor3.B;
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
			get => _xor4.B;
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
			get => _xor5.B;
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
			get => _xor6.B;
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
			get => _xor7.B;
			set
			{
				byte oldOutput = _out;

				_xor7.B = value;

				SetOutput();
				FireEvent(oldOutput);
			}
		}

		public VoltageSignal O0 => _xor0.O;
		public VoltageSignal O1 => _xor1.O;
		public VoltageSignal O2 => _xor2.O;
		public VoltageSignal O3 => _xor3.O;
		public VoltageSignal O4 => _xor4.O;
		public VoltageSignal O5 => _xor5.O;
		public VoltageSignal O6 => _xor6.O;
		public VoltageSignal O7 => _xor7.O;

		#endregion

		#region IOutputEvent Members

		public void AddOutputHandler(Action<object> handler) => OutEvent += handler;

		#endregion

		#region Object Override Methods
		// this is a case where string.Format is clearer than an interpolated string
		public override string ToString() => string.Format(
					"{0}{1}{2}{3}{4}{5}{6}{7}",
					_xor7.O == VoltageSignal.HIGH ? 1 : 0,
					_xor6.O == VoltageSignal.HIGH ? 1 : 0,
					_xor5.O == VoltageSignal.HIGH ? 1 : 0,
					_xor4.O == VoltageSignal.HIGH ? 1 : 0,
					_xor3.O == VoltageSignal.HIGH ? 1 : 0,
					_xor2.O == VoltageSignal.HIGH ? 1 : 0,
					_xor1.O == VoltageSignal.HIGH ? 1 : 0,
					_xor0.O == VoltageSignal.HIGH ? 1 : 0);
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
				OutEvent?.Invoke(this);
			}
		}
		#endregion
	}
}
