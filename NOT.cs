namespace PetzoldComputer
{
	public class NOT : InvertedRelay, INot
	{
	}

	public class NOT_2
	{
		public NOT_2()
		{
			_relay = new Relay_2(true);
		}

		private readonly Relay_2 _relay;

		public ConnectionPoint Voltage => _relay.Voltage;
		public ConnectionPoint Input => _relay.Input;
		public ConnectionPoint Output => _relay.Output;

		public override string ToString() => $"{Output.Voltage}";
	}
}
