using System;

namespace PetzoldComputer
{
	public class ConnectionPoint
	{
		private VoltageSignal _voltage;

		public Action<ConnectionPoint> VoltageChanged;

		public VoltageSignal Voltage
		{
			get => _voltage;
			set
			{
				VoltageSignal original = _voltage;
				_voltage = value;
				if (original != value) VoltageChanged?.Invoke(this);
			}
		}

		public void ConnectTo(ConnectionPoint sink)
		{
			VoltageChanged += source => sink.Voltage = source.Voltage;

			sink.Voltage = Voltage;
		}

		public override string ToString() => $"{Voltage}";
	}
}
