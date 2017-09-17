using System;

namespace PetzoldComputer
{
	public class Phase1Computer : IPhase1Computer, IOutput
	{
		#region Construction
		public Phase1Computer()
		{
			_counter = new CounterSynchronous16();
			_ram = new RAM64KB();
			_adder = new RippleAdder8();
			_latch = new LatchEdge8();

			_adder.CarryIn = VoltageSignal.LOW;

			DoWireup();
		}
		#endregion

		#region Implementation
		private ICounterSynchronous16 _counter;
		private IRam64KB _ram;
		private IRippleAdder8 _adder;
		private ILatchEdge8 _latch;
		#endregion

		#region IPhase1Computer Members

		public VoltageSignal Voltage
		{
			get => _counter.Voltage;
			set => _counter.Voltage =
					 _ram.Voltage =
					 _adder.Voltage =
					 _latch.Voltage = value;
		}

		public VoltageSignal Clr
		{
			get => _counter.Clr;
			set => _counter.Clr = ((IPresetAndClear)_latch).Clr = value;
		}

		public VoltageSignal Clk
		{
			get => _counter.Clk;
			set
			{
				// These are actually done in this order on purpose; otherwise, the latch input
				// gets changed before it's done its job.
				_latch.Clk = value;
				_counter.Clk = value;
			}
		}

		public VoltageSignal D0 => _latch.Q0;
		public VoltageSignal D1 => _latch.Q1;
		public VoltageSignal D2 => _latch.Q2;
		public VoltageSignal D3 => _latch.Q3;
		public VoltageSignal D4 => _latch.Q4;
		public VoltageSignal D5 => _latch.Q5;
		public VoltageSignal D6 => _latch.Q6;
		public VoltageSignal D7 => _latch.Q7;

		public void WriteByte(ushort address, byte data)
		{
			_ram.A0  = ((address & 0x0001) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A1  = ((address & 0x0002) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A2  = ((address & 0x0004) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A3  = ((address & 0x0008) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A4  = ((address & 0x0010) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A5  = ((address & 0x0020) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A6  = ((address & 0x0040) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A7  = ((address & 0x0080) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A8  = ((address & 0x0100) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A9  = ((address & 0x0200) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A10 = ((address & 0x0400) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A11 = ((address & 0x0800) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A12 = ((address & 0x1000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A13 = ((address & 0x2000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A14 = ((address & 0x4000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A15 = ((address & 0x8000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);

			_ram.Din0 = ((data & 0x01) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.Din1 = ((data & 0x02) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.Din2 = ((data & 0x04) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.Din3 = ((data & 0x08) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.Din4 = ((data & 0x10) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.Din5 = ((data & 0x20) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.Din6 = ((data & 0x40) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.Din7 = ((data & 0x80) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);

			_ram.Write = VoltageSignal.HIGH;
			_ram.Write = VoltageSignal.LOW;

			InternalCounterHandler(this);	// resets the RAM address back to zero so when the computer
													// starts up it's in the proper state.
		}

		#endregion

		#region IOutput Members

		public void AddOutputHandler(Action<object> handler) => throw new NotImplementedException();

		#endregion

		#region Object Override Members
		// this is a case where string.Format is clearer than an interpolated string
		public override string ToString() => string.Format(
					"{0}{1}{2}{3}{4}{5}{6}{7}",
					D7 == VoltageSignal.HIGH ? 1 : 0,
					D6 == VoltageSignal.HIGH ? 1 : 0,
					D5 == VoltageSignal.HIGH ? 1 : 0,
					D4 == VoltageSignal.HIGH ? 1 : 0,
					D3 == VoltageSignal.HIGH ? 1 : 0,
					D2 == VoltageSignal.HIGH ? 1 : 0,
					D1 == VoltageSignal.HIGH ? 1 : 0,
					D0 == VoltageSignal.HIGH ? 1 : 0);
		#endregion

		#region Private Members
		private void DoWireup()
		{
			((IOutput)_counter).AddOutputHandler(InternalCounterHandler);
			((IOutput)_ram).AddOutputHandler(InternalRAMHandler);
			((ISum)_adder).AddSumHandler(InternalAdderHandler);
			((IOutput)_latch).AddOutputHandler(InternalLatchHandler);
		}

		private void InternalCounterHandler(object o)
		{
			_ram.A0 = _counter.Q0;
			_ram.A1 = _counter.Q1;
			_ram.A2 = _counter.Q2;
			_ram.A3 = _counter.Q3;
			_ram.A4 = _counter.Q4;
			_ram.A5 = _counter.Q5;
			_ram.A6 = _counter.Q6;
			_ram.A7 = _counter.Q7;
			_ram.A8 = _counter.Q8;
			_ram.A9 = _counter.Q9;
			_ram.A10 = _counter.Q10;
			_ram.A11 = _counter.Q11;
			_ram.A12 = _counter.Q12;
			_ram.A13 = _counter.Q13;
			_ram.A14 = _counter.Q14;
			_ram.A15 = _counter.Q15;
		}

		private void InternalRAMHandler(object o)
		{
			_adder.A0 = _ram.Dout0;
			_adder.A1 = _ram.Dout1;
			_adder.A2 = _ram.Dout2;
			_adder.A3 = _ram.Dout3;
			_adder.A4 = _ram.Dout4;
			_adder.A5 = _ram.Dout5;
			_adder.A6 = _ram.Dout6;
			_adder.A7 = _ram.Dout7;
		}

		private void InternalAdderHandler(object o)
		{
			_latch.D0 = _adder.S0;
			_latch.D1 = _adder.S1;
			_latch.D2 = _adder.S2;
			_latch.D3 = _adder.S3;
			_latch.D4 = _adder.S4;
			_latch.D5 = _adder.S5;
			_latch.D6 = _adder.S6;
			_latch.D7 = _adder.S7;
		}

		private void InternalLatchHandler(object o)
		{
			_adder.B0 = _latch.Q0;
			_adder.B1 = _latch.Q1;
			_adder.B2 = _latch.Q2;
			_adder.B3 = _latch.Q3;
			_adder.B4 = _latch.Q4;
			_adder.B5 = _latch.Q5;
			_adder.B6 = _latch.Q6;
			_adder.B7 = _latch.Q7;
		}
		#endregion
	}
}
