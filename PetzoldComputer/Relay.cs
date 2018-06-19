namespace PetzoldComputer
{
    // This class models the fundamental building block for all other
    // components in the system.  It consists of the three connection
    // points of all relays: Voltage, Input, and Output.  I could have
    // modeled the internal switch-and-coil structure of relays, a la
    // Peter James' https://github.com/hiptopjones/Logic project, but
    // have chosen to focus on the Relay rather than its constituent parts.
    public class Relay
    {
        private static int NumInstances = 0;

        public Relay(string name, bool inverted = false)
        {
            _name = name;
            _inverted = inverted;
            _myId = ++NumInstances;

            Voltage = new ConnectionPoint($"{name}-relay.v");
            Input = new ConnectionPoint($"{name}-relay.in");
            Output = new ConnectionPoint($"{name}-relay.out");

            DoWireUp();

            Components.Record(nameof(Relay));
        }

        private readonly string _name;
        private readonly bool _inverted;
        private readonly int _myId;

        public ConnectionPoint Voltage { get; private set; }
        public ConnectionPoint Input { get; private set; }
        public ConnectionPoint Output { get; private set; }

        public override string ToString() => Output.ToString();

        private void DoWireUp()
        {
            Voltage.Changed += _ => SetOutput();
            Input.Changed += _ => SetOutput();
        }

        private void SetOutput() =>
            Output.V = _inverted
                ? Input.V == VoltageSignal.HIGH ? VoltageSignal.LOW : Voltage.V
                : Input.V == VoltageSignal.HIGH ? Voltage.V : VoltageSignal.LOW;
    }
}