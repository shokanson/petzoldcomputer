using System;

namespace PetzoldComputer
{
	public class Selector2to1 : ISelector2to1, IOutput
	{
		#region Construction
		public Selector2to1()
		{
			_not = new NOT();
			_andA = new AND();
			_andB = new AND();
			_or = new OR();

			DoWireup();
		}
		#endregion

		#region Implementation
		private INot _not;
		private IAnd _andA;
		private IAnd _andB;
		private IOr _or;

		private Action<object> OutEvent;
		#endregion

		#region ISelector2to1 Methods
		public VoltageSignal Voltage
		{
			get => _or.Voltage;
			set
			{
				VoltageSignal oldOutput = O;
				_not.Voltage = _andA.Voltage = _andB.Voltage = _or.Voltage = value;
				HandleEvent(oldOutput);
			}
		}

		public VoltageSignal A
		{
			get => _andA.A;
			set
			{
				VoltageSignal oldOutput = O;
				_andA.A = value;
				HandleEvent(oldOutput);
			}
		}

		public VoltageSignal B
		{
			get => _andB.A;
			set
			{
				VoltageSignal oldOutput = O;
				_andB.A = value;
				HandleEvent(oldOutput);
			}
		}

		public VoltageSignal Select
		{
			get => _not.Input;
			set
			{
				VoltageSignal oldOutput = O;
				_not.Input = value;
				HandleEvent(oldOutput);
			}
		}

		public VoltageSignal O => _or.O;
		#endregion

		#region IOutput Methods
		public void AddOutputHandler(Action<object> handler) => OutEvent += handler;
		#endregion

		#region Object Override Methods
		public override string ToString() => O.ToString();
		#endregion

		#region Private Methods
		private void DoWireup()
		{
			((IOutput)_not).AddOutputHandler(InternalEventHandler);
			((IOutput)_andA).AddOutputHandler(InternalEventHandler);
			((IOutput)_andB).AddOutputHandler(InternalEventHandler);
		}

		private void InternalEventHandler(object o)
		{
			_andA.B = _not.Output;
			_andB.B = _not.Input;
			_or.A = _andA.O;
			_or.B = _andB.O;
		}

		private void HandleEvent(VoltageSignal oldOutput)
		{
			if (oldOutput != O)
			{
				OutEvent?.Invoke(this);
			}
		}
		#endregion
	}
}
