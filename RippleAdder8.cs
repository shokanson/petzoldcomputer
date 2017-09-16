using System;
namespace PetzoldComputer
{
	public class RippleAdder8 : IRippleAdder8 , ISum, ICarry
	{
		#region Construction
		public RippleAdder8()
		{
			_adder0 = new FullAdder();
			_adder1 = new FullAdder();
			_adder2 = new FullAdder();
			_adder3 = new FullAdder();
			_adder4 = new FullAdder();
			_adder5 = new FullAdder();
			_adder6 = new FullAdder();
			_adder7 = new FullAdder();

			_sum = 0x00;
			_carry = VoltageSignal.LOW;

			DoWireup();
		}
		#endregion

		#region Implementation
		private IFullAdder _adder0;
		private IFullAdder _adder1;
		private IFullAdder _adder2;
		private IFullAdder _adder3;
		private IFullAdder _adder4;
		private IFullAdder _adder5;
		private IFullAdder _adder6;
		private IFullAdder _adder7;

		private byte _sum;
		private VoltageSignal _carry;

		private Action<object> SumEvent;
		private Action<object> CarryEvent;
		#endregion

		#region IRippleAdder8 Members

		public VoltageSignal Voltage
		{
			get { return _adder0.Voltage; }
			set
			{
				VoltageSignal oldCarry = _carry;
				byte oldSum = _sum;

				_adder0.Voltage = value;
				_adder1.Voltage = value;
				_adder2.Voltage = value;
				_adder3.Voltage = value;
				_adder4.Voltage = value;
				_adder5.Voltage = value;
				_adder6.Voltage = value;
				_adder7.Voltage = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal A0
		{
			get { return _adder0.A; }
			set
			{
				VoltageSignal oldCarry = _carry;
				byte oldSum = _sum;

				_adder0.A = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal A1
		{
			get { return _adder1.A; }
			set
			{
				VoltageSignal oldCarry = _carry;
				byte oldSum = _sum;

				_adder1.A = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal A2
		{
			get { return _adder2.A; }
			set
			{
				VoltageSignal oldCarry = _carry;
				byte oldSum = _sum;

				_adder2.A = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal A3
		{
			get { return _adder3.A; }
			set
			{
				VoltageSignal oldCarry = _carry;
				byte oldSum = _sum;

				_adder3.A = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal A4
		{
			get { return _adder4.A; }
			set
			{
				VoltageSignal oldCarry = _carry;
				byte oldSum = _sum;

				_adder4.A = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal A5
		{
			get { return _adder5.A; }
			set
			{
				VoltageSignal oldCarry = _carry;
				byte oldSum = _sum;

				_adder5.A = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal A6
		{
			get { return _adder6.A; }
			set
			{
				VoltageSignal oldCarry = _carry;
				byte oldSum = _sum;

				_adder6.A = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal A7
		{
			get { return _adder7.A; }
			set
			{
				VoltageSignal oldCarry = _carry;
				byte oldSum = _sum;

				_adder7.A = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal B0
		{
			get { return _adder0.B; }
			set
			{
				VoltageSignal oldCarry = _carry;
				byte oldSum = _sum;

				_adder0.B = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal B1
		{
			get { return _adder1.B; }
			set
			{
				VoltageSignal oldCarry = _carry;
				byte oldSum = _sum;

				_adder1.B = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal B2
		{
			get { return _adder2.B; }
			set
			{
				VoltageSignal oldCarry = _carry;
				byte oldSum = _sum;

				_adder2.B = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal B3
		{
			get { return _adder3.B; }
			set
			{
				VoltageSignal oldCarry = _carry;
				byte oldSum = _sum;

				_adder3.B = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal B4
		{
			get { return _adder4.B; }
			set
			{
				VoltageSignal oldCarry = _carry;
				byte oldSum = _sum;

				_adder4.B = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal B5
		{
			get { return _adder5.B; }
			set
			{
				VoltageSignal oldCarry = _carry;
				byte oldSum = _sum;

				_adder5.B = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal B6
		{
			get { return _adder6.B; }
			set
			{
				VoltageSignal oldCarry = _carry;
				byte oldSum = _sum;

				_adder6.B = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal B7
		{
			get { return _adder7.B; }
			set
			{
				VoltageSignal oldCarry = _carry;
				byte oldSum = _sum;

				_adder7.B = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal CarryIn
		{
			get { return _adder0.CarryIn; }
			set
			{
				VoltageSignal oldCarry = _carry;
				byte oldSum = _sum;

				_adder0.CarryIn = value;

				HandleSum(oldSum);
				HandleCarry(oldCarry);
			}
		}

		public VoltageSignal S0
		{
			get { return _adder0.Sum; }
		}

		public VoltageSignal S1
		{
			get { return _adder1.Sum; }
		}

		public VoltageSignal S2
		{
			get { return _adder2.Sum; }
		}

		public VoltageSignal S3
		{
			get { return _adder3.Sum; }
		}

		public VoltageSignal S4
		{
			get { return _adder4.Sum; }
		}

		public VoltageSignal S5
		{
			get { return _adder5.Sum; }
		}

		public VoltageSignal S6
		{
			get { return _adder6.Sum; }
		}

		public VoltageSignal S7
		{
			get { return _adder7.Sum; }
		}

		public VoltageSignal Carry
		{
			get { return _carry; }
		}

		#endregion

		#region ISumEvent Members

		public void AddSumHandler(Action<object> handler)
		{
			SumEvent += handler;
		}

		#endregion

		#region ICarryEvent Members

		public void AddCarryHandler(Action<object> handler)
		{
			CarryEvent += handler;
		}

		#endregion

		#region Object Override Methods
		public override string ToString()
		{
			return
				string.Format(
					"{0}:{1}{2}{3}{4}{5}{6}{7}{8}",
					_adder7.Carry == VoltageSignal.HIGH ? 1 : 0,
					_adder7.Sum == VoltageSignal.HIGH ? 1 : 0,
					_adder6.Sum == VoltageSignal.HIGH ? 1 : 0,
					_adder5.Sum == VoltageSignal.HIGH ? 1 : 0,
					_adder4.Sum == VoltageSignal.HIGH ? 1 : 0,
					_adder3.Sum == VoltageSignal.HIGH ? 1 : 0,
					_adder2.Sum == VoltageSignal.HIGH ? 1 : 0,
					_adder1.Sum == VoltageSignal.HIGH ? 1 : 0,
					_adder0.Sum == VoltageSignal.HIGH ? 1 : 0);
		}
		#endregion

		#region Private Members
		private void DoWireup()
		{
			((ICarry)_adder0).AddCarryHandler(InternalEventHandler);
			((ICarry)_adder1).AddCarryHandler(InternalEventHandler);
			((ICarry)_adder2).AddCarryHandler(InternalEventHandler);
			((ICarry)_adder3).AddCarryHandler(InternalEventHandler);
			((ICarry)_adder4).AddCarryHandler(InternalEventHandler);
			((ICarry)_adder5).AddCarryHandler(InternalEventHandler);
			((ICarry)_adder6).AddCarryHandler(InternalEventHandler);
		}

		private void InternalEventHandler(object o)
		{
			_adder1.CarryIn = _adder0.Carry;
			_adder2.CarryIn = _adder1.Carry;
			_adder3.CarryIn = _adder2.Carry;
			_adder4.CarryIn = _adder3.Carry;
			_adder5.CarryIn = _adder4.Carry;
			_adder6.CarryIn = _adder5.Carry;
			_adder7.CarryIn = _adder6.Carry;
		}

		private void HandleCarry(VoltageSignal oldCarry)
		{
			_carry = _adder7.Carry;

			if (oldCarry != _carry)
			{
				if (CarryEvent != null)
				{
					CarryEvent(this);
				}
			}
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

			_sum |= (_adder0.Sum == VoltageSignal.HIGH ? (byte)0x01 : (byte)0x00);
			_sum |= (_adder1.Sum == VoltageSignal.HIGH ? (byte)0x02 : (byte)0x00);
			_sum |= (_adder2.Sum == VoltageSignal.HIGH ? (byte)0x04 : (byte)0x00);
			_sum |= (_adder3.Sum == VoltageSignal.HIGH ? (byte)0x08 : (byte)0x00);
			_sum |= (_adder4.Sum == VoltageSignal.HIGH ? (byte)0x10 : (byte)0x00);
			_sum |= (_adder5.Sum == VoltageSignal.HIGH ? (byte)0x20 : (byte)0x00);
			_sum |= (_adder6.Sum == VoltageSignal.HIGH ? (byte)0x40 : (byte)0x00);
			_sum |= (_adder7.Sum == VoltageSignal.HIGH ? (byte)0x80 : (byte)0x00);
		}
		#endregion
	}
}

/*
$Log: /PetzoldComputer/RippleAdder8.cs $ $NoKeyWords:$
 * 
 * 3     1/21/07 11:58p Sean
 * results of ReSharper analysis
*/
