using System;

namespace PetzoldComputer
{
	public class NOR3 : INor3, IOutput
	{
		#region Construction
		public NOR3()
		{
			_relay1 = new InvertedRelay();
			_relay2 = new InvertedRelay();
			_relay3 = new InvertedRelay();

			DoWireup();
		}
		#endregion

		#region Implementation
		private IRelay _relay1;
		private IRelay _relay2;
		private IRelay _relay3;
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

		public VoltageSignal O => _relay3.Output;

		#endregion

		#region INor3 Members

		public VoltageSignal C
		{
			get => _relay3.Input;
			set => _relay3.Input = value;
		}

		#endregion

		#region IOutput Members

		public void AddOutputHandler(Action<object> handler) => ((IOutput)_relay3).AddOutputHandler(handler);

		#endregion

		#region Object Override Methods
		public override string ToString() => _relay3.Output.ToString();
		#endregion

		#region Private Methods
		private void DoWireup()
		{
			((IOutput)_relay1).AddOutputHandler(_ => { _relay2.Voltage = _relay1.Output; });
			((IOutput)_relay2).AddOutputHandler(_ => { _relay3.Voltage = _relay2.Output; });
		}
		#endregion
	}

	public class NOR3_2
	{
		public NOR3_2()
		{
			DoWireUp();
		}

		private readonly NOT_2 _not1 = new NOT_2();
		private readonly NOT_2 _not2 = new NOT_2();
		private readonly NOT_2 _not3 = new NOT_2();

		public ConnectionPoint V => _not1.V;
		public ConnectionPoint A => _not1.Input;
		public ConnectionPoint B => _not2.Input;
		public ConnectionPoint C => _not3.Input;
		public ConnectionPoint O => _not3.Output;

		public override string ToString() => O.ToString();

		private void DoWireUp()
		{
			_not1.Output.ConnectTo(_not2.V);
			_not2.Output.ConnectTo(_not3.V);
		}
	}
}
