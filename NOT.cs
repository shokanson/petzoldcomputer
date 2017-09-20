namespace PetzoldComputer
{
	public class NOT : InvertedRelay, INot
	{
	}

	public class NOT_2
	{
		public NOT_2()
		{
			Relay = new Relay_2(SwitchType.NormallyClosed);
			Output = new ConnectionPoint();
			//Relay.Switch.Input.VoltageChanged += _ => Output.Voltage = Relay.Switch.Output.Voltage;
			Relay.Switch.Output.VoltageChanged += _ => Output.Voltage = Relay.Switch.Output.Voltage;
		}

		private Relay_2 Relay { get; set; }

		public ConnectionPoint Voltage
		{
			get => Relay.Switch.Input;
		}
		public ConnectionPoint Input => Relay.Coil.Voltage;
		public ConnectionPoint Output { get; private set; }

		public override string ToString() => $"V: {Voltage.Voltage}; I:{Input.Voltage}; O:{Output.Voltage}";
	}
}
