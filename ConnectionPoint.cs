using System;

namespace PetzoldComputer
{
	public class ConnectionPoint
	{
		private VoltageSignal _voltage;

		public Action<ConnectionPoint> Changed;

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

		public override string ToString() => $"{V}";
	}
}
