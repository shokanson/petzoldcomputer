using System;

namespace PetzoldComputer
{
	public class ConnectionPoint
	{
		public ConnectionPoint(string name) => _name = name;

		private readonly string _name;
		private VoltageSignal _voltage;

		public event Action<ConnectionPoint> Changed;

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

		public ConnectionPoint ConnectTo(ConnectionPoint sink)
		{
			Changed += source => sink.V = source.V;

			sink.V = V;

			return this;
		}

		public override string ToString() => V.ToString();
	}
}
