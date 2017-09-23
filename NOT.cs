namespace PetzoldComputer
{
	public class NOT : InvertedRelay, INot
	{ }

	public class NOT_2
	{
		private readonly Relay_2 _relay = new Relay_2(inverted: true);

		public ConnectionPoint V => _relay.Voltage;
		public ConnectionPoint Input => _relay.Input;
		public ConnectionPoint Output => _relay.Output;

		public override string ToString() => Output.ToString();
	}
}
