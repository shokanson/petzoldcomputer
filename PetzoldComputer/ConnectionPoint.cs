namespace PetzoldComputer
{
    // This class is closely modeled on Peter James' Node class
    // from his https://github.com/hiptopjones/Logic project.
    public class ConnectionPoint
    {
        public ConnectionPoint(string name) => _name = name;

        private readonly string _name;
        private VoltageSignal _voltage;

        public System.Action<ConnectionPoint> Changed;

        public VoltageSignal V
        {
            get => _voltage;
            set
            {
                VoltageSignal original = _voltage;
                _voltage = value;
                if (original != value) Changed?.Invoke(this);
            }
        }

        // Clients can attach directly to the Changed event.
        // This method is a convenience for doing that, and also provides
        // a chaining mechanism for making multiple connections.
        public ConnectionPoint ConnectTo(ConnectionPoint sink)
        {
            Changed += source => sink.V = source.V;

            return this;
        }

        public override string ToString() => V.ToString();
    }
}
