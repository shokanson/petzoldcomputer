namespace PetzoldComputer
{
	/* Phase 2 computer w/ a control panel. In other words, this is the full realization
	 * of the diagram on page 208.
	 */
	public class Phase3Computer_2 : Phase2Computer_2
	{
		#region Construction
		public Phase3Computer_2(string name)
			: this(name, 0)
		{ }

		public Phase3Computer_2(string name, uint nIterations)
			: base(name, nIterations)
		{
			_panel = new ControlPanel_2($"{name}-computer.panel");
			DoWireUp();
		}
		#endregion

		#region Implementation
		private readonly ControlPanel_2 _panel;
		#endregion

		public ControlPanel_2 Panel => _panel;

		public override void WriteByte(ushort address, byte data)
		{
			_panel.Takeover.V = VoltageSignal.HIGH;

			_panel.A0_sw.V = ((address & 0x0001) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A1_sw.V = ((address & 0x0002) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A2_sw.V = ((address & 0x0004) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A3_sw.V = ((address & 0x0008) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A4_sw.V = ((address & 0x0010) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A5_sw.V = ((address & 0x0020) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A6_sw.V = ((address & 0x0040) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A7_sw.V = ((address & 0x0080) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A8_sw.V = ((address & 0x0100) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A9_sw.V = ((address & 0x0200) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A10_sw.V = ((address & 0x0400) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A11_sw.V = ((address & 0x0800) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A12_sw.V = ((address & 0x1000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A13_sw.V = ((address & 0x2000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A14_sw.V = ((address & 0x4000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A15_sw.V = ((address & 0x8000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);

			_panel.D0_sw.V = ((data & 0x01) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.D1_sw.V = ((data & 0x02) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.D2_sw.V = ((data & 0x04) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.D3_sw.V = ((data & 0x08) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.D4_sw.V = ((data & 0x10) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.D5_sw.V = ((data & 0x20) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.D6_sw.V = ((data & 0x40) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.D7_sw.V = ((data & 0x80) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);

			_panel.Write_sw.V = VoltageSignal.HIGH;
			_panel.Write_sw.V = VoltageSignal.LOW;

			_panel.Takeover.V = VoltageSignal.LOW;
		}

		private void DoWireUp()
		{
			V.ConnectTo(_panel.V);
			WireUpPanelOutput();
			WireUpPanelBulbs();
		}

		private void WireUpPanelOutput()
		{
			_panel.A0.ConnectTo(_ram.A0);
			_panel.A1.ConnectTo(_ram.A1);
			_panel.A2.ConnectTo(_ram.A2);
			_panel.A3.ConnectTo(_ram.A3);
			_panel.A4.ConnectTo(_ram.A4);
			_panel.A5.ConnectTo(_ram.A5);
			_panel.A6.ConnectTo(_ram.A6);
			_panel.A7.ConnectTo(_ram.A7);
			_panel.A8.ConnectTo(_ram.A8);
			_panel.A9.ConnectTo(_ram.A9);
			_panel.A10.ConnectTo(_ram.A10);
			_panel.A11.ConnectTo(_ram.A11);
			_panel.A12.ConnectTo(_ram.A12);
			_panel.A13.ConnectTo(_ram.A13);
			_panel.A14.ConnectTo(_ram.A14);
			_panel.A15.ConnectTo(_ram.A15);

			_panel.D0.ConnectTo(_ram.Din0);
			_panel.D1.ConnectTo(_ram.Din1);
			_panel.D2.ConnectTo(_ram.Din2);
			_panel.D3.ConnectTo(_ram.Din3);
			_panel.D4.ConnectTo(_ram.Din4);
			_panel.D5.ConnectTo(_ram.Din5);
			_panel.D6.ConnectTo(_ram.Din6);
			_panel.D7.ConnectTo(_ram.Din7);

			_panel.Write.ConnectTo(_ram.Write);
		}

		private void WireUpPanelBulbs()
		{
			// show on the panel whatever's in RAM
			_ram.Dout0.ConnectTo(_panel.B0);
			_ram.Dout1.ConnectTo(_panel.B1);
			_ram.Dout2.ConnectTo(_panel.B2);
			_ram.Dout3.ConnectTo(_panel.B3);
			_ram.Dout4.ConnectTo(_panel.B4);
			_ram.Dout5.ConnectTo(_panel.B5);
			_ram.Dout6.ConnectTo(_panel.B6);
			_ram.Dout7.ConnectTo(_panel.B7);
		}
	}
}
