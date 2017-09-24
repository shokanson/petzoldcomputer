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
		public VoltageSignal V
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
			((IOutput)_not).AddOutputHandler(_ => { _andA.B = _not.Output; _andB.B = _not.Input; });
			((IOutput)_andA).AddOutputHandler(_ => _or.A = _andA.O);
			((IOutput)_andB).AddOutputHandler(_ => _or.B = _andB.O);
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

	public class Selector2to1_2
	{
		public Selector2to1_2(string name)
		{
			_v = new ConnectionPoint($"{name}-selector.v");
			_select = new ConnectionPoint($"{name}-selector.select");
			_not = new NOT_2($"{name}-selector.select");
			_andA = new AND_2($"{name}-selector.a");
			_andB = new AND_2($"{name}-selector.b");
			_or = new OR_2($"{name}-selector.out");

			DoWireUp();
		}

		private readonly ConnectionPoint _v;
		private readonly ConnectionPoint _select;
		private readonly NOT_2 _not;
		private readonly AND_2 _andA;
		private readonly AND_2 _andB;
		private readonly OR_2 _or;

		public ConnectionPoint V => _v;
		public ConnectionPoint Select => _select;
		public ConnectionPoint A => _andA.A;
		public ConnectionPoint B => _andB.A;
		public ConnectionPoint O => _or.O;

		public override string ToString() => O.ToString();

		private void DoWireUp()
		{
			_v.ConnectTo(_or.V).ConnectTo(_andB.V).ConnectTo(_andA.V).ConnectTo(_not.V);
			_select.ConnectTo(_not.Input).ConnectTo(_andB.B);
			_not.Output.ConnectTo(_andA.B);
			_andA.O.ConnectTo(_or.A);
			_andB.O.ConnectTo(_or.B);
		}
	}
}
