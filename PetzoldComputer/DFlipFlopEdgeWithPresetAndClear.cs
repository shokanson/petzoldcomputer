namespace PetzoldComputer
{
	public class DFlipFlopEdgeWithPresetAndClear
	{
		#region Construction
		public DFlipFlopEdgeWithPresetAndClear(string name)
		{
			_not = new NOT($"{name}-flop.clk");
			_nor3Clr = new NOR3($"{name}-flop.clr");
			_nor3Pre = new NOR3($"{name}-flop.pre");
			_nor3Clk = new NOR3($"{name}-flop.clk");
			_nor3D = new NOR3($"{name}-flop.d");
			_nor3Q = new NOR3($"{name}-flop.q");
			_nor3Qnot = new NOR3($"{name}-flop.qnot");

			V = new ConnectionPoint($"{name}-flop.v");
			Clr = new ConnectionPoint($"{name}-flop.clr");
			Pre = new ConnectionPoint($"{name}-flop.pre");

			DoWireUp();

			Components.Record(nameof(DFlipFlopEdgeWithPresetAndClear));
		}
		#endregion

		#region Implementation
		private readonly NOT _not;
		private readonly NOR3 _nor3Clr;
		private readonly NOR3 _nor3Pre;
		private readonly NOR3 _nor3Clk;
		private readonly NOR3 _nor3D;
		private readonly NOR3 _nor3Q;
		private readonly NOR3 _nor3Qnot;
        #endregion

        #region Public Properties
        public ConnectionPoint V { get; }
        public ConnectionPoint Clr { get; }
        public ConnectionPoint Pre { get; }
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
			V.ConnectTo(_nor3Qnot.V)
			  .ConnectTo(_nor3Q.V)
			  .ConnectTo(_nor3D.V)
			  .ConnectTo(_nor3Clk.V)
			  .ConnectTo(_nor3Pre.V)
			  .ConnectTo(_nor3Clr.V)
			  .ConnectTo(_not.V);
			Clr.ConnectTo(_nor3Q.A).ConnectTo(_nor3Clr.A);
			Pre.ConnectTo(_nor3Qnot.B).ConnectTo(_nor3D.B).ConnectTo(_nor3Pre.B);
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
