namespace PetzoldComputer
{
	public class NOT : InvertedRelay, INot
	{ }

	public class NOT_2
	{
		public NOT_2(string name)
		{
			_relay = new Relay_2($"{name}-not", inverted: true);
		}

		private readonly Relay_2 _relay;

		public ConnectionPoint V => _relay.Voltage;
		public ConnectionPoint Input => _relay.Input;
		public ConnectionPoint Output => _relay.Output;

		public override string ToString() => Output.ToString();
	}
}
