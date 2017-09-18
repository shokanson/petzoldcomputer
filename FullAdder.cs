using System;

namespace PetzoldComputer
{
	public class FullAdder : IFullAdder, ISum, ICarry
	{
		#region Construction
		public FullAdder()
		{
			_sumHalfAdder = new HalfAdder();
			_carryHalfAdder = new HalfAdder();
			_carryOr = new OR();

			_sum = VoltageSignal.LOW;
			_carry = VoltageSignal.LOW;

			DoWireup();
		}
		#endregion

		#region Implementation
		private IHalfAdder _sumHalfAdder;
		private IHalfAdder _carryHalfAdder;
		private IOr _carryOr;
		private VoltageSignal _sum;
		private VoltageSignal _carry;

		private Action<object> SumEvent;
		private Action<object> CarryEvent;
		#endregion

		#region IFullAdder Members

		public VoltageSignal Voltage
		{
			get => _sumHalfAdder.Voltage;
			set
			{
				_sumHalfAdder.Voltage = _carryHalfAdder.Voltage = _carryOr.Voltage = value;

				HandleOutputsAndEvents();
			}
		}

		public VoltageSignal A
		{
			get { return _sumHalfAdder.A; }
			set
			{
				_sumHalfAdder.A = value;

				HandleOutputsAndEvents();
			}
		}

		public VoltageSignal B
		{
			get { return _sumHalfAdder.B; }
			set
			{
				_sumHalfAdder.B = value;

				HandleOutputsAndEvents();
			}
		}

		public VoltageSignal CarryIn
		{
			get { return _carryHalfAdder.A; }
			set
			{
				_carryHalfAdder.A = value;

				HandleOutputsAndEvents();
			}
		}

		public VoltageSignal Sum
		{
			get { return _sum; }
		}

		public VoltageSignal Carry
		{
			get { return _carry; }
		}

		#endregion

		#region ISumEvent Members

		public void AddSumHandler(Action<object> handler) => SumEvent += handler;

		#endregion

		#region ICarryEvent Members

		public void AddCarryHandler(Action<object> handler) => CarryEvent += handler;

		#endregion

		#region Object Override Methods
		public override string ToString() => $"Sum: {Sum}; Carry: {Carry}";
		#endregion

		#region Private Methods
		private void DoWireup()
		{
			((ISum)_sumHalfAdder).AddSumHandler(_ => _carryHalfAdder.B = _sumHalfAdder.Sum);
			((ICarry)_sumHalfAdder).AddCarryHandler(_ => _carryOr.B = _sumHalfAdder.Carry);
			((ICarry)_carryHalfAdder).AddCarryHandler(_ => _carryOr.A = _carryHalfAdder.Carry);
		}

		private void SetSum() => _sum = _carryHalfAdder.Sum;

		private void SetCarry() => _carry = _carryOr.O;

		private void FireSumEvent(VoltageSignal oldSum)
		{
			if (oldSum != _sum)
			{
				SumEvent?.Invoke(this);
			}
		}

		private void FireCarryEvent(VoltageSignal oldCarry)
		{
			if (oldCarry != _carry)
			{
				CarryEvent?.Invoke(this);
			}
		}

		private void HandleOutputsAndEvents()
		{
			VoltageSignal oldSum = _sum;
			VoltageSignal oldCarry = _carry;

			SetSum();
			SetCarry();
			FireSumEvent(oldSum);
			FireCarryEvent(oldCarry);
		}
		#endregion
	}
}
