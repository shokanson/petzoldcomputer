using System;

namespace PetzoldComputer
{
	public class AND : IAnd, IOutput
	{
		#region Construction
		public AND()
		{
			_relay1 = new Relay();
			_relay2 = new Relay();

			DoWireup();
		}
		#endregion

		#region Implementation
		private IRelay _relay1;
		private IRelay _relay2;
		#endregion

		#region IAnd Members
		public VoltageSignal Voltage
		{
			get => _relay1.Voltage;
			set => _relay1.Voltage = value;
		}

		public VoltageSignal A
		{
			get => _relay1.Input;
			set => _relay1.Input = value;
		}

		public VoltageSignal B
		{
			get => _relay2.Input;
			set => _relay2.Input = value;
		}

		public VoltageSignal O => _relay2.Output;
		#endregion

		#region IOutput Members
		public void AddOutputHandler(Action<object> handler) => ((IOutput)_relay2).AddOutputHandler(handler);
		#endregion

		#region Object Overrides
		public override string ToString() => _relay2.Output.ToString();
		#endregion

		#region Private Members
		private void DoWireup() => ((IOutput)_relay1).AddOutputHandler(_ => { _relay2.Voltage = _relay1.Output; });
		#endregion
	}

	public class AND_2
	{
		public AND_2()
		{
			DoWireUp();
		}

		private readonly Relay_2 _relay1 = new Relay_2();
		private readonly Relay_2 _relay2 = new Relay_2();

		public ConnectionPoint V { get => _relay1.Voltage; }
		public ConnectionPoint A { get => _relay1.Input; }
		public ConnectionPoint B { get => _relay2.Input; }
		public ConnectionPoint O { get => _relay2.Output; }

		public override string ToString() => $"{O}";

		private void DoWireUp()
		{
			_relay1.Output.ConnectTo(_relay2.Voltage);
		}
	}
}
