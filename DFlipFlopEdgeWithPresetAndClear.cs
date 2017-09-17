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
			((IOutput)_not).AddOutputHandler(InternalEventHandler);
			((IOutput)_nor3Clr).AddOutputHandler(InternalEventHandler);
			((IOutput)_nor3Pre).AddOutputHandler(InternalEventHandler);
			((IOutput)_nor3Clk).AddOutputHandler(InternalEventHandler);
			((IOutput)_nor3D).AddOutputHandler(InternalEventHandler);
			((IOutput)_nor3Q).AddOutputHandler(InternalEventHandler);
			((IOutput)_nor3Qnot).AddOutputHandler(InternalEventHandler);
		}

		private void InternalEventHandler(object o)
		{
			// _not.Input is user set

			// _nor3Clr.A is user set
			_nor3Clr.B = _nor3D.O;
			_nor3Clr.C = _nor3Pre.O;

			_nor3Pre.A = _nor3Clr.O;
			// _nor3Pre.B is user set
			_nor3Pre.C = _not.Output;

			_nor3Clk.A = _nor3Pre.O;
			_nor3Clk.B = _not.Output;
			_nor3Clk.C = _nor3D.O;

			_nor3D.A = _nor3Clk.O;
			// _nor3D.B is user set
			// _nor3D.C is user set

			// _nor3Q.A is user set
			_nor3Q.B = _nor3Pre.O;
			_nor3Q.C = _nor3Qnot.O;

			_nor3Qnot.A = _nor3Q.O;
			// _nor3Qnot.B is user set
			_nor3Qnot.C = _nor3Clk.O;
		}
		#endregion
	}
}
