namespace PetzoldComputer
{
	public class NOT
	{
		public NOT(string name)
		{
			_relay = new Relay($"{name}-not", inverted: true);
		}

		private readonly Relay _relay;

		public ConnectionPoint V => _relay.Voltage;
		public ConnectionPoint Input => _relay.Input;
		public ConnectionPoint Output => _relay.Output;

		public override string ToString() => Output.ToString();
	}
}
