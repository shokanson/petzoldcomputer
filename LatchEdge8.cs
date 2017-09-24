namespace PetzoldComputer
{
	public class LatchEdge8_2
	{
		public LatchEdge8_2(string name)
		{
			_v = new ConnectionPoint($"{name}-latch8.v");
			_clr = new ConnectionPoint($"{name}-latch8.clr");
			_clk = new ConnectionPoint($"{name}-latch8.clk");
			_pre = new ConnectionPoint($"{name}-latch8.pre");

			_flop0 = new DFlipFlopEdgeWithPresetAndClear_2($"{name}-latch8.0");
			_flop1 = new DFlipFlopEdgeWithPresetAndClear_2($"{name}-latch8.1");
			_flop2 = new DFlipFlopEdgeWithPresetAndClear_2($"{name}-latch8.2");
			_flop3 = new DFlipFlopEdgeWithPresetAndClear_2($"{name}-latch8.3");
			_flop4 = new DFlipFlopEdgeWithPresetAndClear_2($"{name}-latch8.4");
			_flop5 = new DFlipFlopEdgeWithPresetAndClear_2($"{name}-latch8.5");
			_flop6 = new DFlipFlopEdgeWithPresetAndClear_2($"{name}-latch8.6");
			_flop7 = new DFlipFlopEdgeWithPresetAndClear_2($"{name}-latch8.7");

			DoWireUp();
		}

		private readonly ConnectionPoint _v;
		private readonly ConnectionPoint _clr;
		private readonly ConnectionPoint _clk;
		private readonly ConnectionPoint _pre;

		private readonly DFlipFlopEdgeWithPresetAndClear_2 _flop0;
		private readonly DFlipFlopEdgeWithPresetAndClear_2 _flop1;
		private readonly DFlipFlopEdgeWithPresetAndClear_2 _flop2;
		private readonly DFlipFlopEdgeWithPresetAndClear_2 _flop3;
		private readonly DFlipFlopEdgeWithPresetAndClear_2 _flop4;
		private readonly DFlipFlopEdgeWithPresetAndClear_2 _flop5;
		private readonly DFlipFlopEdgeWithPresetAndClear_2 _flop6;
		private readonly DFlipFlopEdgeWithPresetAndClear_2 _flop7;

		public ConnectionPoint V => _v;
		public ConnectionPoint Clr => _clr;
		public ConnectionPoint Clk => _clk;
		public ConnectionPoint Pre => _pre;

		public ConnectionPoint Din0 => _flop0.D;
		public ConnectionPoint Din1 => _flop1.D;
		public ConnectionPoint Din2 => _flop2.D;
		public ConnectionPoint Din3 => _flop3.D;
		public ConnectionPoint Din4 => _flop4.D;
		public ConnectionPoint Din5 => _flop5.D;
		public ConnectionPoint Din6 => _flop6.D;
		public ConnectionPoint Din7 => _flop7.D;

		public ConnectionPoint Dout0 => _flop0.Q;
		public ConnectionPoint Dout1 => _flop1.Q;
		public ConnectionPoint Dout2 => _flop2.Q;
		public ConnectionPoint Dout3 => _flop3.Q;
		public ConnectionPoint Dout4 => _flop4.Q;
		public ConnectionPoint Dout5 => _flop5.Q;
		public ConnectionPoint Dout6 => _flop6.Q;
		public ConnectionPoint Dout7 => _flop7.Q;

		// this is a case where string.Format is clearer than an interpolated string
		public override string ToString() => string.Format(
					"{0}{1}{2}{3}{4}{5}{6}{7}",
					_flop7.Q.V == VoltageSignal.HIGH ? 1 : 0,
					_flop6.Q.V == VoltageSignal.HIGH ? 1 : 0,
					_flop5.Q.V == VoltageSignal.HIGH ? 1 : 0,
					_flop4.Q.V == VoltageSignal.HIGH ? 1 : 0,
					_flop3.Q.V == VoltageSignal.HIGH ? 1 : 0,
					_flop2.Q.V == VoltageSignal.HIGH ? 1 : 0,
					_flop1.Q.V == VoltageSignal.HIGH ? 1 : 0,
					_flop0.Q.V == VoltageSignal.HIGH ? 1 : 0);

		private void DoWireUp()
		{
			_v.ConnectTo(_flop7.V)
			  .ConnectTo(_flop6.V)
			  .ConnectTo(_flop5.V)
			  .ConnectTo(_flop4.V)
			  .ConnectTo(_flop3.V)
			  .ConnectTo(_flop2.V)
			  .ConnectTo(_flop1.V)
			  .ConnectTo(_flop0.V);
			_clr.ConnectTo(_flop7.Clr)
				 .ConnectTo(_flop6.Clr)
				 .ConnectTo(_flop5.Clr)
				 .ConnectTo(_flop4.Clr)
				 .ConnectTo(_flop3.Clr)
				 .ConnectTo(_flop2.Clr)
				 .ConnectTo(_flop1.Clr)
				 .ConnectTo(_flop0.Clr);
			_clk.ConnectTo(_flop7.Clk)
				 .ConnectTo(_flop6.Clk)
				 .ConnectTo(_flop5.Clk)
				 .ConnectTo(_flop4.Clk)
				 .ConnectTo(_flop3.Clk)
				 .ConnectTo(_flop2.Clk)
				 .ConnectTo(_flop1.Clk)
				 .ConnectTo(_flop0.Clk);
			_pre.ConnectTo(_flop7.Pre)
				 .ConnectTo(_flop6.Pre)
				 .ConnectTo(_flop5.Pre)
				 .ConnectTo(_flop4.Pre)
				 .ConnectTo(_flop3.Pre)
				 .ConnectTo(_flop2.Pre)
				 .ConnectTo(_flop1.Pre)
				 .ConnectTo(_flop0.Pre);
		}
	}
}
