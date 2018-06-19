namespace PetzoldComputer
{
    /* Phase 2 computer w/ a control panel. In other words, this is the full realization
	 * of the diagram on page 208.
	 */
    public class Phase3Computer : Phase2Computer
    {
        #region Construction
        public Phase3Computer(string name)
            : this(name, 0)
        { }

        public Phase3Computer(string name, uint nIterations)
            : base(name, nIterations)
        {
            Panel = new ControlPanel($"{name}-computer.panel");
            DoWireUp();

            Components.Record(nameof(Phase3Computer));
        }
        #endregion

        public ControlPanel Panel { get; }

        public override void WriteByte(ushort address, byte data)
        {
            Panel.Takeover.V = VoltageSignal.HIGH;

            Panel.A0_sw.V = ((address & 0x0001) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Panel.A1_sw.V = ((address & 0x0002) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Panel.A2_sw.V = ((address & 0x0004) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Panel.A3_sw.V = ((address & 0x0008) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Panel.A4_sw.V = ((address & 0x0010) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Panel.A5_sw.V = ((address & 0x0020) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Panel.A6_sw.V = ((address & 0x0040) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Panel.A7_sw.V = ((address & 0x0080) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Panel.A8_sw.V = ((address & 0x0100) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Panel.A9_sw.V = ((address & 0x0200) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Panel.A10_sw.V = ((address & 0x0400) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Panel.A11_sw.V = ((address & 0x0800) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Panel.A12_sw.V = ((address & 0x1000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Panel.A13_sw.V = ((address & 0x2000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Panel.A14_sw.V = ((address & 0x4000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Panel.A15_sw.V = ((address & 0x8000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);

            Panel.D0_sw.V = ((data & 0x01) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Panel.D1_sw.V = ((data & 0x02) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Panel.D2_sw.V = ((data & 0x04) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Panel.D3_sw.V = ((data & 0x08) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Panel.D4_sw.V = ((data & 0x10) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Panel.D5_sw.V = ((data & 0x20) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Panel.D6_sw.V = ((data & 0x40) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
            Panel.D7_sw.V = ((data & 0x80) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);

            Panel.Write_sw.V = VoltageSignal.HIGH;
            Panel.Write_sw.V = VoltageSignal.LOW;

            Panel.Takeover.V = VoltageSignal.LOW;
        }

        private void DoWireUp()
        {
            V.ConnectTo(Panel.V);
            WireUpPanelOutput();
            WireUpPanelBulbs();
        }

        private void WireUpPanelOutput()
        {
            Panel.A0.ConnectTo(_ram.A0);
            Panel.A1.ConnectTo(_ram.A1);
            Panel.A2.ConnectTo(_ram.A2);
            Panel.A3.ConnectTo(_ram.A3);
            Panel.A4.ConnectTo(_ram.A4);
            Panel.A5.ConnectTo(_ram.A5);
            Panel.A6.ConnectTo(_ram.A6);
            Panel.A7.ConnectTo(_ram.A7);
            Panel.A8.ConnectTo(_ram.A8);
            Panel.A9.ConnectTo(_ram.A9);
            Panel.A10.ConnectTo(_ram.A10);
            Panel.A11.ConnectTo(_ram.A11);
            Panel.A12.ConnectTo(_ram.A12);
            Panel.A13.ConnectTo(_ram.A13);
            Panel.A14.ConnectTo(_ram.A14);
            Panel.A15.ConnectTo(_ram.A15);

            Panel.D0.ConnectTo(_ram.Din0);
            Panel.D1.ConnectTo(_ram.Din1);
            Panel.D2.ConnectTo(_ram.Din2);
            Panel.D3.ConnectTo(_ram.Din3);
            Panel.D4.ConnectTo(_ram.Din4);
            Panel.D5.ConnectTo(_ram.Din5);
            Panel.D6.ConnectTo(_ram.Din6);
            Panel.D7.ConnectTo(_ram.Din7);

            Panel.Write.ConnectTo(_ram.Write);
        }

        private void WireUpPanelBulbs()
        {
            // show on the panel whatever's in RAM
            _ram.Dout0.ConnectTo(Panel.B0);
            _ram.Dout1.ConnectTo(Panel.B1);
            _ram.Dout2.ConnectTo(Panel.B2);
            _ram.Dout3.ConnectTo(Panel.B3);
            _ram.Dout4.ConnectTo(Panel.B4);
            _ram.Dout5.ConnectTo(Panel.B5);
            _ram.Dout6.ConnectTo(Panel.B6);
            _ram.Dout7.ConnectTo(Panel.B7);
        }
    }
}
