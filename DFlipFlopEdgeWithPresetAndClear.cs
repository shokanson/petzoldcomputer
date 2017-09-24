using System;

namespace PetzoldComputer
{
	public class DFlipFlopEdgeWithPresetAndClear : IDFlipFlop, IPresetAndClear, IOutput
	{
		#region Construction
		public DFlipFlopEdgeWithPresetAndClear()
		{
			_not = new NOT();
			_nor3Clr = new NOR3();
			_nor3Pre = new NOR3();
			_nor3Clk = new NOR3();
			_nor3D = new NOR3();
			_nor3Q = new NOR3();
			_nor3Qnot = new NOR3();

			DoWireup();
		}
		#endregion

		#region Implementation
		private INot _not;
		private INor3 _nor3Clr;
		private INor3 _nor3Pre;
		private INor3 _nor3Clk;
		private INor3 _nor3D;
		private INor3 _nor3Q;
		private INor3 _nor3Qnot;
		#endregion

		#region IDFlipFlop Members

		public VoltageSignal Voltage
		{
			get => _not.Voltage;
			set => _not.Voltage =
					_nor3Clr.Voltage =
					_nor3Pre.Voltage =
					_nor3Clk.Voltage =
					_nor3D.Voltage =
					_nor3Q.Voltage =
					_nor3Qnot.Voltage = value;
		}

		public VoltageSignal D
		{
			get { return _nor3D.C; }
			set { _nor3D.C = value; }
		}

		public VoltageSignal Clk
		{
			get { return _not.Input; }
			set { _not.Input = value; }
		}

		public VoltageSignal Q
		{
			get { return _nor3Q.O; }
		}

		public VoltageSignal Qnot
		{
			get { return _nor3Qnot.O; }
		}

		#endregion

		#region IPresetAndClear Members

		public VoltageSignal Pre
		{
			get => _nor3Pre.B;
			set => _nor3Pre.B = _nor3D.B = _nor3Qnot.B = value;
		}

		public VoltageSignal Clr
		{
			get => _nor3Clr.A;
			set => _nor3Clr.A = _nor3Q.A = value;
		}

		#endregion

		#region IOutput Members

		public void AddOutputHandler(Action<object> handler) => ((IOutput)_nor3Q).AddOutputHandler(handler);

		#endregion

		#region Object Override Methods
		public override string ToString() => $"Q: {Q}; Qnot: {Qnot}";
		#endregion

		#region Private Members
		private void DoWireup()
		{
			((IOutput)_not).AddOutputHandler(_ => { _nor3Pre.C = _not.Output; _nor3Clk.B = _not.Output; });
			((IOutput)_nor3Clr).AddOutputHandler(_ => _nor3Pre.A = _nor3Clr.O);
			((IOutput)_nor3Pre).AddOutputHandler(InternalPreHandler);
			((IOutput)_nor3Clk).AddOutputHandler(_ => { _nor3D.A = _nor3Clk.O; _nor3Qnot.C = _nor3Clk.O; });
			((IOutput)_nor3D).AddOutputHandler(_ => { _nor3Clr.B = _nor3D.O; _nor3Clk.C = _nor3D.O; });
			((IOutput)_nor3Q).AddOutputHandler(_ => _nor3Qnot.A = _nor3Q.O);
			((IOutput)_nor3Qnot).AddOutputHandler(_ => { _nor3Clk.B = _not.Output; _nor3Q.C = _nor3Qnot.O; });
			//     don't understand or remember why this ^^^^^^^^^^^^^^^^^^^^^^^^^ is here (?!?!)
		}

		private void InternalPreHandler(object o)
		{
			_nor3Clr.C = _nor3Pre.O;
			_nor3Clk.A = _nor3Pre.O;
			_nor3Q.B = _nor3Pre.O;
		}
		#endregion
	}

	public class DFlipFlopEdgeWithPresetAndClear_2
	{
		#region Construction
		public DFlipFlopEdgeWithPresetAndClear_2(string name)
		{
			_not = new NOT_2($"{name}-flop.clk");
			_nor3Clr = new NOR3_2($"{name}-flop.clr");
			_nor3Pre = new NOR3_2($"{name}-flop.pre");
			_nor3Clk = new NOR3_2($"{name}-flop.clk");
			_nor3D = new NOR3_2($"{name}-flop.d");
			_nor3Q = new NOR3_2($"{name}-flop.q");
			_nor3Qnot = new NOR3_2($"{name}-flop.qnot");

			_v = new ConnectionPoint($"{name}-flop.v");
			_clr = new ConnectionPoint($"{name}-flop.clr");
			_pre = new ConnectionPoint($"{name}-flop.pre");

			DoWireUp();
		}
		#endregion

		#region Implementation
		private readonly NOT_2 _not;
		private readonly NOR3_2 _nor3Clr;
		private readonly NOR3_2 _nor3Pre;
		private readonly NOR3_2 _nor3Clk;
		private readonly NOR3_2 _nor3D;
		private readonly NOR3_2 _nor3Q;
		private readonly NOR3_2 _nor3Qnot;

		private readonly ConnectionPoint _v;
		private readonly ConnectionPoint _clr;
		private readonly ConnectionPoint _pre;
		#endregion

		#region Public Properties
		public ConnectionPoint V => _v;
		public ConnectionPoint Clr => _clr;
		public ConnectionPoint Pre => _pre;
		public ConnectionPoint Clk => _not.Input;
		public ConnectionPoint D => _nor3D.C;
		public ConnectionPoint Q => _nor3Q.O;
		public ConnectionPoint Qnot => _nor3Qnot.O;
		#endregion

		#region Object Override Methods
		public override string ToString() => $"Q: {Q}; Qnot: {Qnot}";
		#endregion

		#region Private Methods
		private void DoWireUp()
		{
			// inputs
			_v.ConnectTo(_nor3Qnot.V)
			  .ConnectTo(_nor3Q.V)
			  .ConnectTo(_nor3D.V)
			  .ConnectTo(_nor3Clk.V)
			  .ConnectTo(_nor3Pre.V)
			  .ConnectTo(_nor3Clr.V)
			  .ConnectTo(_not.V);
			_clr.ConnectTo(_nor3Q.A).ConnectTo(_nor3Clr.A);
			_pre.ConnectTo(_nor3Qnot.B).ConnectTo(_nor3D.B).ConnectTo(_nor3Pre.B);
			_not.Output.ConnectTo(_nor3Pre.C).ConnectTo(_nor3Clk.B);
			_nor3D.O.ConnectTo(_nor3Clr.B).ConnectTo(_nor3Clk.C);

			// internals
			_nor3Clr.O.ConnectTo(_nor3Pre.A);
			_nor3Pre.O.ConnectTo(_nor3Clr.C).ConnectTo(_nor3Clk.A).ConnectTo(_nor3Q.B);
			_nor3Clk.O.ConnectTo(_nor3D.A).ConnectTo(_nor3Qnot.C);
			_nor3Q.O.ConnectTo(_nor3Qnot.A);
			_nor3Qnot.O.Changed += _ => _nor3Clk.B.V = _not.Output.V;   // not sure why this is needed...
			_nor3Qnot.O.ConnectTo(_nor3Q.C);
		}
		#endregion
	}
}
