namespace PetzoldComputer
{
	/* Phase1Computer represents the computer as diagrammed on page 208, sans oscillator and control panel.
	 * The MSTest.PetzoldComputer.Phase1Test class exercises the computer as described starting with "Here's
	 * how it works..." on page 209.
	 */
	public class Phase1Computer
	{
		public Phase1Computer(string name)
		{
			_v = new ConnectionPoint($"{name}-phase1computer.v");
			_clk = new ConnectionPoint($"{name}-phase1computer.clk");
			_clr= new ConnectionPoint($"{name}-phase1computer.clr");
			_counter = new CounterRipple16($"{name}-phase1computer.counter");
			_ram = new RAM64KB($"{name}-phase1computer.ram");
			_adder = new RippleAdder8($"{name}-phase1computer.accumulator");	// _adder.CarryIn is LOW
			_latch = new LatchEdge8($"{name}-phase1computer.register");

			DoWireUp();
		}

		private readonly ConnectionPoint _v;
		private readonly ConnectionPoint _clk;
		private readonly ConnectionPoint _clr;
		private readonly CounterRipple16 _counter;
		protected readonly RAM64KB _ram;   // make it available to subclasses
		private RippleAdder8 _adder;
		private LatchEdge8 _latch;

		public ConnectionPoint V => _v;
		public ConnectionPoint Clr => _clr;
		public ConnectionPoint Clk => _clk;

		public string PC => _counter.ToString();

		public ConnectionPoint D0 => _latch.Dout0;
		public ConnectionPoint D1 => _latch.Dout1;
		public ConnectionPoint D2 => _latch.Dout2;
		public ConnectionPoint D3 => _latch.Dout3;
		public ConnectionPoint D4 => _latch.Dout4;
		public ConnectionPoint D5 => _latch.Dout5;
		public ConnectionPoint D6 => _latch.Dout6;
		public ConnectionPoint D7 => _latch.Dout7;

		// this is a case where string.Format is clearer than an interpolated string
		public override string ToString() => string.Format(
					"{0}{1}{2}{3}{4}{5}{6}{7}",
					D7.V == VoltageSignal.HIGH ? 1 : 0,
					D6.V == VoltageSignal.HIGH ? 1 : 0,
					D5.V == VoltageSignal.HIGH ? 1 : 0,
					D4.V == VoltageSignal.HIGH ? 1 : 0,
					D3.V == VoltageSignal.HIGH ? 1 : 0,
					D2.V == VoltageSignal.HIGH ? 1 : 0,
					D1.V == VoltageSignal.HIGH ? 1 : 0,
					D0.V == VoltageSignal.HIGH ? 1 : 0);

		public virtual void WriteByte(ushort address, byte data)
		{
			_ram.A0.V = ((address & 0x0001) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A1.V = ((address & 0x0002) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A2.V = ((address & 0x0004) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A3.V = ((address & 0x0008) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A4.V = ((address & 0x0010) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A5.V = ((address & 0x0020) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A6.V = ((address & 0x0040) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A7.V = ((address & 0x0080) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A8.V = ((address & 0x0100) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A9.V = ((address & 0x0200) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A10.V = ((address & 0x0400) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A11.V = ((address & 0x0800) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A12.V = ((address & 0x1000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A13.V = ((address & 0x2000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A14.V = ((address & 0x4000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.A15.V = ((address & 0x8000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);

			_ram.Din0.V = ((data & 0x01) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.Din1.V = ((data & 0x02) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.Din2.V = ((data & 0x04) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.Din3.V = ((data & 0x08) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.Din4.V = ((data & 0x10) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.Din5.V = ((data & 0x20) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.Din6.V = ((data & 0x40) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_ram.Din7.V = ((data & 0x80) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);

			_ram.Write.V = VoltageSignal.HIGH;
			_ram.Write.V = VoltageSignal.LOW;

			ResetRAMAddressCounterOutput();
		}

		private void ResetRAMAddressCounterOutput()
		{
			_ram.A0.V = _counter.Q0.V;
			_ram.A1.V = _counter.Q1.V;
			_ram.A2.V = _counter.Q2.V;
			_ram.A3.V = _counter.Q3.V;
			_ram.A4.V = _counter.Q4.V;
			_ram.A5.V = _counter.Q5.V;
			_ram.A6.V = _counter.Q6.V;
			_ram.A7.V = _counter.Q7.V;
			_ram.A8.V = _counter.Q8.V;
			_ram.A9.V = _counter.Q9.V;
			_ram.A10.V = _counter.Q10.V;
			_ram.A11.V = _counter.Q11.V;
			_ram.A12.V = _counter.Q12.V;
			_ram.A13.V = _counter.Q13.V;
			_ram.A14.V = _counter.Q14.V;
			_ram.A15.V = _counter.Q15.V;
		}

		private void DoWireUp()
		{
			_v.ConnectTo(_latch.V).ConnectTo(_adder.V).ConnectTo(_ram.V).ConnectTo(_counter.V);
			// Clk is done in this order on purpose; otherwise, the latch input
			// gets changed before it's done its job.  This mimics propagation delay.
			_clk.ConnectTo(_latch.Clk).ConnectTo(_counter.Clk);
			_clr.ConnectTo(_latch.Clr).ConnectTo(_counter.Clr);
			WireUpCounterOutToRAMAddress();
			WireUpRAMOuttoAdderA();
			WireUpAdderOutToLatchIn();
			WireUpLatchOutToAdderB();
		}

		private void WireUpCounterOutToRAMAddress()
		{
			_counter.Q0.ConnectTo(_ram.A0);
			_counter.Q1.ConnectTo(_ram.A1);
			_counter.Q2.ConnectTo(_ram.A2);
			_counter.Q3.ConnectTo(_ram.A3);
			_counter.Q4.ConnectTo(_ram.A4);
			_counter.Q5.ConnectTo(_ram.A5);
			_counter.Q6.ConnectTo(_ram.A6);
			_counter.Q7.ConnectTo(_ram.A7);
			_counter.Q8.ConnectTo(_ram.A8);
			_counter.Q9.ConnectTo(_ram.A9);
			_counter.Q10.ConnectTo(_ram.A10);
			_counter.Q11.ConnectTo(_ram.A11);
			_counter.Q12.ConnectTo(_ram.A12);
			_counter.Q13.ConnectTo(_ram.A13);
			_counter.Q14.ConnectTo(_ram.A14);
			_counter.Q15.ConnectTo(_ram.A15);
		}

		private void WireUpRAMOuttoAdderA()
		{
			_ram.Dout0.ConnectTo(_adder.A0);
			_ram.Dout1.ConnectTo(_adder.A1);
			_ram.Dout2.ConnectTo(_adder.A2);
			_ram.Dout3.ConnectTo(_adder.A3);
			_ram.Dout4.ConnectTo(_adder.A4);
			_ram.Dout5.ConnectTo(_adder.A5);
			_ram.Dout6.ConnectTo(_adder.A6);
			_ram.Dout7.ConnectTo(_adder.A7);
		}

		private void WireUpAdderOutToLatchIn()
		{
			_adder.S0.ConnectTo(_latch.Din0);
			_adder.S1.ConnectTo(_latch.Din1);
			_adder.S2.ConnectTo(_latch.Din2);
			_adder.S3.ConnectTo(_latch.Din3);
			_adder.S4.ConnectTo(_latch.Din4);
			_adder.S5.ConnectTo(_latch.Din5);
			_adder.S6.ConnectTo(_latch.Din6);
			_adder.S7.ConnectTo(_latch.Din7);
		}

		private void WireUpLatchOutToAdderB()
		{
			_latch.Dout0.ConnectTo(_adder.B0);
			_latch.Dout1.ConnectTo(_adder.B1);
			_latch.Dout2.ConnectTo(_adder.B2);
			_latch.Dout3.ConnectTo(_adder.B3);
			_latch.Dout4.ConnectTo(_adder.B4);
			_latch.Dout5.ConnectTo(_adder.B5);
			_latch.Dout6.ConnectTo(_adder.B6);
			_latch.Dout7.ConnectTo(_adder.B7);
		}
	}
}
