using System;

namespace PetzoldComputer
{
	public class NOR : INor, IOutput
	{
		#region Construction
		public NOR()
		{
			_relay1 = new InvertedRelay();
			_relay2 = new InvertedRelay();

			DoWireup();
		}
		#endregion

		#region Implementation
		private IRelay _relay1;
		private IRelay _relay2;
		#endregion

		#region INor Members
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

	public class NOR_2
	{
		public NOR_2()
		{
			DoWireUp();
		}

		private readonly NOT_2 _not1 = new NOT_2();
		private readonly NOT_2 _not2 = new NOT_2();

		public ConnectionPoint V { get => _not1.V; }
		public ConnectionPoint A { get => _not1.Input; }
		public ConnectionPoint B { get => _not2.Input; }
		public ConnectionPoint O { get => _not2.Output; }

		public override string ToString() => $"{O}";

		private void DoWireUp()
		{
			_not1.Output.ConnectTo(_not2.V);
		}
	}
}
