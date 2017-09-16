using System;
namespace PetzoldComputer
{
	public class AddAndSub8 : IAddAndSub8, ISum, IOverUnderFlow
	{
		#region Construction
		public AddAndSub8()
		{
			_adder = new RippleAdder8();
			_ones = new OnesComplement8();
			_xorOverUnder = new XOR();

			_sum = 0x00;

			DoWireup();
		}
		#endregion

		#region Implementation
		private IRippleAdder8 _adder;
		private IOnesComplement8 _ones;
		private IXor _xorOverUnder;

		private byte _sum;

		private Action<object> SumEvent;
		private Action<object> OverUnderFlowEvent;
		#endregion

		#region IAddAndSub8 Members

		public VoltageSignal Voltage
		{
			get { return _adder.Voltage; }
			set
			{
				VoltageSignal oldCarry = _xorOverUnder.O;
				byte oldSum = _sum;

				_adder.Voltage = value;
				_ones.Voltage = value;
				_xorOverUnder.Voltage = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal A0
		{
			get { return _adder.A0; }
			set
			{
				VoltageSignal oldCarry = _xorOverUnder.O;
				byte oldSum = _sum;

				_adder.A0 = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal A1
		{
			get { return _adder.A1; }
			set
			{
				VoltageSignal oldCarry = _xorOverUnder.O;
				byte oldSum = _sum;

				_adder.A1 = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal A2
		{
			get { return _adder.A2; }
			set
			{
				VoltageSignal oldCarry = _xorOverUnder.O;
				byte oldSum = _sum;

				_adder.A2 = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal A3
		{
			get { return _adder.A3; }
			set
			{
				VoltageSignal oldCarry = _xorOverUnder.O;
				byte oldSum = _sum;

				_adder.A3 = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal A4
		{
			get { return _adder.A4; }
			set
			{
				VoltageSignal oldCarry = _xorOverUnder.O;
				byte oldSum = _sum;

				_adder.A4 = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal A5
		{
			get { return _adder.A5; }
			set
			{
				VoltageSignal oldCarry = _xorOverUnder.O;
				byte oldSum = _sum;

				_adder.A5 = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal A6
		{
			get { return _adder.A6; }
			set
			{
				VoltageSignal oldCarry = _xorOverUnder.O;
				byte oldSum = _sum;

				_adder.A6 = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal A7
		{
			get { return _adder.A7; }
			set
			{
				VoltageSignal oldCarry = _xorOverUnder.O;
				byte oldSum = _sum;

				_adder.A7 = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal B0
		{
			get { return _ones.I0; }
			set
			{
				VoltageSignal oldCarry = _xorOverUnder.O;
				byte oldSum = _sum;

				_ones.I0 = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal B1
		{
			get { return _ones.I1; }
			set
			{
				VoltageSignal oldCarry = _xorOverUnder.O;
				byte oldSum = _sum;

				_ones.I1 = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal B2
		{
			get { return _ones.I2; }
			set
			{
				VoltageSignal oldCarry = _xorOverUnder.O;
				byte oldSum = _sum;

				_ones.I2 = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal B3
		{
			get { return _ones.I3; }
			set
			{
				VoltageSignal oldCarry = _xorOverUnder.O;
				byte oldSum = _sum;

				_ones.I3 = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal B4
		{
			get { return _ones.I4; }
			set
			{
				VoltageSignal oldCarry = _xorOverUnder.O;
				byte oldSum = _sum;

				_ones.I4 = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal B5
		{
			get { return _ones.I5; }
			set
			{
				VoltageSignal oldCarry = _xorOverUnder.O;
				byte oldSum = _sum;

				_ones.I5 = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal B6
		{
			get { return _ones.I6; }
			set
			{
				VoltageSignal oldCarry = _xorOverUnder.O;
				byte oldSum = _sum;

				_ones.I6 = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal B7
		{
			get { return _ones.I7; }
			set
			{
				VoltageSignal oldCarry = _xorOverUnder.O;
				byte oldSum = _sum;

				_ones.I7 = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal Sub
		{
			get { return _ones.Invert; }
			set
			{
				VoltageSignal oldCarry = _xorOverUnder.O;
				byte oldSum = _sum;

				_ones.Invert = value;
				_adder.CarryIn = value;
				_xorOverUnder.B = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal S0
		{
			get { return _adder.S0; }
		}

		public VoltageSignal S1
		{
			get { return _adder.S1; }
		}

		public VoltageSignal S2
		{
			get { return _adder.S2; }
		}

		public VoltageSignal S3
		{
			get { return _adder.S3; }
		}

		public VoltageSignal S4
		{
			get { return _adder.S4; }
		}

		public VoltageSignal S5
		{
			get { return _adder.S5; }
		}

		public VoltageSignal S6
		{
			get { return _adder.S6; }
		}

		public VoltageSignal S7
		{
			get { return _adder.S7; }
		}

		public VoltageSignal OverUnderFlow
		{
			get { return _xorOverUnder.O; }
		}

		#endregion

		#region ISumEvent Members

		public void AddSumHandler(Action<object> handler)
		{
			SumEvent += handler;
		}

		#endregion

		#region IOverUnderFlowEvent Members

		public void AddOverUnderFlowHandler(Action<object> handler)
		{
			OverUnderFlowEvent += handler;
		}

		#endregion

		#region Object Override Methods
		public override string ToString()
		{
			return
				string.Format(
					"{0}:{1}{2}{3}{4}{5}{6}{7}{8}",
					_xorOverUnder.O == VoltageSignal.HIGH ? 1 : 0,
					_adder.S7 == VoltageSignal.HIGH ? 1 : 0,
					_adder.S6 == VoltageSignal.HIGH ? 1 : 0,
					_adder.S5 == VoltageSignal.HIGH ? 1 : 0,
					_adder.S4 == VoltageSignal.HIGH ? 1 : 0,
					_adder.S3 == VoltageSignal.HIGH ? 1 : 0,
					_adder.S2 == VoltageSignal.HIGH ? 1 : 0,
					_adder.S1 == VoltageSignal.HIGH ? 1 : 0,
					_adder.S0 == VoltageSignal.HIGH ? 1 : 0);
		}
		#endregion

		#region Private Methods
		private void DoWireup()
		{
			((IOutput)_ones).AddOutputHandler(InternalEventHandler);
			((IOutput)_xorOverUnder).AddOutputHandler(InternalEventHandler);
		}

		private void InternalEventHandler(object o)
		{
			_adder.B0 = _ones.O0;
			_adder.B1 = _ones.O1;
			_adder.B2 = _ones.O2;
			_adder.B3 = _ones.O3;
			_adder.B4 = _ones.O4;
			_adder.B5 = _ones.O5;
			_adder.B6 = _ones.O6;
			_adder.B7 = _ones.O7;
			_xorOverUnder.A = _adder.Carry;
		}

		private void HandleSum(byte oldSum)
		{
			SetSum();

			if (oldSum != _sum)
			{
				if (SumEvent != null)
				{
					SumEvent(this);
				}
			}
		}

		private void SetSum()
		{
			_sum = 0x00;

			_sum |= (_adder.S0 == VoltageSignal.HIGH ? (byte)0x01 : (byte)0x00);
			_sum |= (_adder.S1 == VoltageSignal.HIGH ? (byte)0x02 : (byte)0x00);
			_sum |= (_adder.S2 == VoltageSignal.HIGH ? (byte)0x04 : (byte)0x00);
			_sum |= (_adder.S3 == VoltageSignal.HIGH ? (byte)0x08 : (byte)0x00);
			_sum |= (_adder.S4 == VoltageSignal.HIGH ? (byte)0x10 : (byte)0x00);
			_sum |= (_adder.S5 == VoltageSignal.HIGH ? (byte)0x20 : (byte)0x00);
			_sum |= (_adder.S6 == VoltageSignal.HIGH ? (byte)0x40 : (byte)0x00);
			_sum |= (_adder.S7 == VoltageSignal.HIGH ? (byte)0x80 : (byte)0x00);
		}

		private void HandleCarry(VoltageSignal oldCarry)
		{
			if (oldCarry != _xorOverUnder.O)
			{
				if (OverUnderFlowEvent != null)
				{
					OverUnderFlowEvent(this);
				}
			}
		}
		#endregion
	}
}

/*
$Log: /PetzoldComputer/AddAndSub8.cs $ $NoKeyWords:$
 * 
 * 3     1/21/07 11:58p Sean
 * results of ReSharper analysis
*/