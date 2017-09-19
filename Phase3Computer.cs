namespace PetzoldComputer
{
	/* Phase 2 computer w/ a control panel. In other words, this is the full realization
	 * of the diagram on page 208.
	 */
	public class Phase3Computer : Phase2Computer
	{
		#region Construction
		public Phase3Computer()
			: this(0)
		{ }

		public Phase3Computer(uint nIterations)
			: base(nIterations)
		{
			_panel = new ControlPanel();
			DoWireUp();
		}
		#endregion

		#region Implementation
		private readonly IControlPanel _panel;
		#endregion

		public IControlPanel Panel => _panel;

		public override void WriteByte(ushort address, byte data)
		{
			_panel.Takeover = VoltageSignal.HIGH;

			_panel.A0_sw = ((address & 0x0001) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A1_sw = ((address & 0x0002) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A2_sw = ((address & 0x0004) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A3_sw = ((address & 0x0008) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A4_sw = ((address & 0x0010) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A5_sw = ((address & 0x0020) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A6_sw = ((address & 0x0040) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A7_sw = ((address & 0x0080) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A8_sw = ((address & 0x0100) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A9_sw = ((address & 0x0200) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A10_sw = ((address & 0x0400) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A11_sw = ((address & 0x0800) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A12_sw = ((address & 0x1000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A13_sw = ((address & 0x2000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A14_sw = ((address & 0x4000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.A15_sw = ((address & 0x8000) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);

			_panel.D0_sw = ((data & 0x01) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.D1_sw = ((data & 0x02) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.D2_sw = ((data & 0x04) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.D3_sw = ((data & 0x08) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.D4_sw = ((data & 0x10) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.D5_sw = ((data & 0x20) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.D6_sw = ((data & 0x40) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);
			_panel.D7_sw = ((data & 0x80) != 0 ? VoltageSignal.HIGH : VoltageSignal.LOW);

			_panel.Write_sw = VoltageSignal.HIGH;
			_panel.Write_sw = VoltageSignal.LOW;

			_panel.Takeover = VoltageSignal.LOW;
		}

		public override VoltageSignal Voltage
		{
			get => _panel.Voltage;
			set { _panel.Voltage = base.Voltage = value; }
		}

		private void DoWireUp()
		{
			((IOutput)_panel).AddOutputHandler(HandleOutput);
			((IOutput)_ram).AddOutputHandler(HandleRAMOutput);
		}

		private void HandleOutput(object o)
		{
			_ram.A0 = _panel.A0;
			_ram.A1 = _panel.A1;
			_ram.A2 = _panel.A2;
			_ram.A3 = _panel.A3;
			_ram.A4 = _panel.A4;
			_ram.A5 = _panel.A5;
			_ram.A6 = _panel.A6;
			_ram.A7 = _panel.A7;
			_ram.A8 = _panel.A8;
			_ram.A9 = _panel.A9;
			_ram.A10 = _panel.A10;
			_ram.A11 = _panel.A11;
			_ram.A12 = _panel.A12;
			_ram.A13 = _panel.A13;
			_ram.A14 = _panel.A14;
			_ram.A15 = _panel.A15;

			_ram.Din0 = _panel.D0;
			_ram.Din1 = _panel.D1;
			_ram.Din2 = _panel.D2;
			_ram.Din3 = _panel.D3;
			_ram.Din4 = _panel.D4;
			_ram.Din5 = _panel.D5;
			_ram.Din6 = _panel.D6;
			_ram.Din7 = _panel.D7;

			_ram.Write = _panel.Write;
		}

		private void HandleRAMOutput(object o)
		{
			// show on the panel whatever's in RAM
			_panel.B0 = _ram.Dout0;
			_panel.B1 = _ram.Dout1;
			_panel.B2 = _ram.Dout2;
			_panel.B3 = _ram.Dout3;
			_panel.B4 = _ram.Dout4;
			_panel.B5 = _ram.Dout5;
			_panel.B6 = _ram.Dout6;
			_panel.B7 = _ram.Dout7;
		}
	}
}
