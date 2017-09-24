namespace PetzoldComputer
{
	public class Relay
	{
		private static int Counter = 0;

		public Relay(string name, bool inverted = false)
		{
			_inverted = inverted;
			_myId = ++Counter;
			_name = name;

			_voltage = new ConnectionPoint($"{name}-relay.v");
			_input = new ConnectionPoint($"{name}-relay.in");
			_output = new ConnectionPoint($"{name}-relay.out");

			DoWireUp();
		}

		private readonly int _myId;
		private readonly string _name;
		private bool _inverted;
		private readonly ConnectionPoint _voltage;
		private readonly ConnectionPoint _input;
		private readonly ConnectionPoint _output;

		public ConnectionPoint Voltage => _voltage;
		public ConnectionPoint Input => _input;
		public ConnectionPoint Output => _output;

		public override string ToString() => _output.ToString();

		private void DoWireUp()
		{
			// when the input voltage changes, (de)activate the switch
			_input.Changed += input => IsSwitchActivated = input.V == VoltageSignal.HIGH;

			_voltage.Changed += voltage => UpdateOutput(voltage.V);
		}

		// internal switch details
		private bool _switchActive;
		private bool IsSwitchActivated
		{
			get => _switchActive;
			set
			{
				bool originalValue = _switchActive;
				_switchActive = value;
				if (originalValue != value) UpdateOutput(_voltage.V);
			}
		}

		private void UpdateOutput(VoltageSignal voltage)
		{
			_output.V = _inverted
				? _switchActive ? VoltageSignal.LOW : voltage
				: _switchActive ? voltage : VoltageSignal.LOW;
		}
	}
}
