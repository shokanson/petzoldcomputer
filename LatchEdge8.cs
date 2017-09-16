using System;
namespace PetzoldComputer
{
	public class LatchEdge8 : ILatchEdge8, IPresetAndClear, IOutput
	{
		#region Construction
		public LatchEdge8()
		{
			_flop0 = new DFlipFlopEdgeWithPresetAndClear();
			_flop1 = new DFlipFlopEdgeWithPresetAndClear();
			_flop2 = new DFlipFlopEdgeWithPresetAndClear();
			_flop3 = new DFlipFlopEdgeWithPresetAndClear();
			_flop4 = new DFlipFlopEdgeWithPresetAndClear();
			_flop5 = new DFlipFlopEdgeWithPresetAndClear();
			_flop6 = new DFlipFlopEdgeWithPresetAndClear();
			_flop7 = new DFlipFlopEdgeWithPresetAndClear();

			_output = 0x00;
		}
		#endregion

		#region Implementation
		private IDFlipFlop _flop0;
		private IDFlipFlop _flop1;
		private IDFlipFlop _flop2;
		private IDFlipFlop _flop3;
		private IDFlipFlop _flop4;
		private IDFlipFlop _flop5;
		private IDFlipFlop _flop6;
		private IDFlipFlop _flop7;

		private byte _output;

		private Action<object> OutEvent;
		#endregion

		#region ILatchEdge8 Members

		public VoltageSignal Voltage
		{
			get { return _flop0.Voltage; }
			set
			{
				_flop0.Voltage =
					_flop1.Voltage =
					_flop2.Voltage =
					_flop3.Voltage =
					_flop4.Voltage =
					_flop5.Voltage =
					_flop6.Voltage =
					_flop7.Voltage = value;
			}
		}

		public VoltageSignal Clk
		{
			get { return _flop0.Clk; }
			set
			{
				byte oldOutput = _output;
				VoltageSignal oldClk = Clk;

				_flop0.Clk =
					_flop1.Clk =
					_flop2.Clk =
					_flop3.Clk =
					_flop4.Clk =
					_flop5.Clk =
					_flop6.Clk =
					_flop7.Clk = value;

				// Because this is an edge-triggered device, this stuff only needs to called when Clk is set. In fact,
				// because it's a rising-edge-triggered device, it only needs to get called when Clk goes HIGH.
				if (oldClk == VoltageSignal.LOW && value == VoltageSignal.HIGH)
				{
					SetOutput();
					HandleEvent(oldOutput);
				}
			}
		}

		public VoltageSignal D0
		{
			get { return _flop0.D; }
			set { _flop0.D = value; }
		}

		public VoltageSignal D1
		{
			get { return _flop1.D; }
			set { _flop1.D = value; }
		}

		public VoltageSignal D2
		{
			get { return _flop2.D; }
			set { _flop2.D = value; }
		}

		public VoltageSignal D3
		{
			get { return _flop3.D; }
			set { _flop3.D = value; }
		}

		public VoltageSignal D4
		{
			get { return _flop4.D; }
			set { _flop4.D = value; }
		}

		public VoltageSignal D5
		{
			get { return _flop5.D; }
			set { _flop5.D = value; }
		}

		public VoltageSignal D6
		{
			get { return _flop6.D; }
			set { _flop6.D = value; }
		}

		public VoltageSignal D7
		{
			get { return _flop7.D; }
			set { _flop7.D = value; }
		}

		public VoltageSignal Q0
		{
			get { return _flop0.Q; }
		}

		public VoltageSignal Q1
		{
			get { return _flop1.Q; }
		}

		public VoltageSignal Q2
		{
			get { return _flop2.Q; }
		}

		public VoltageSignal Q3
		{
			get { return _flop3.Q; }
		}

		public VoltageSignal Q4
		{
			get { return _flop4.Q; }
		}

		public VoltageSignal Q5
		{
			get { return _flop5.Q; }
		}

		public VoltageSignal Q6
		{
			get { return _flop6.Q; }
		}

		public VoltageSignal Q7
		{
			get { return _flop7.Q; }
		}

		#endregion

		#region IOutput Members

		public void AddOutputHandler(Action<object> handler)
		{
			OutEvent += handler;
		}

		#endregion

		#region IPresetAndClear Members

		public VoltageSignal Pre
		{
			get { return ((IPresetAndClear)_flop0).Pre; }
			set
			{
				byte oldOutput = _output;

				((IPresetAndClear)_flop0).Pre =
					((IPresetAndClear)_flop1).Pre =
					((IPresetAndClear)_flop2).Pre =
					((IPresetAndClear)_flop3).Pre =
					((IPresetAndClear)_flop4).Pre =
					((IPresetAndClear)_flop5).Pre =
					((IPresetAndClear)_flop6).Pre =
					((IPresetAndClear)_flop7).Pre = value;

				SetOutput();
				HandleEvent(oldOutput);
			}
		}

		public VoltageSignal Clr
		{
			get { return ((IPresetAndClear)_flop0).Clr; }
			set
			{
				byte oldOutput = _output;

				((IPresetAndClear)_flop0).Clr =
					((IPresetAndClear)_flop1).Clr =
					((IPresetAndClear)_flop2).Clr =
					((IPresetAndClear)_flop3).Clr =
					((IPresetAndClear)_flop4).Clr =
					((IPresetAndClear)_flop5).Clr =
					((IPresetAndClear)_flop6).Clr =
					((IPresetAndClear)_flop7).Clr = value;

				SetOutput();
				HandleEvent(oldOutput);
			}
		}

		#endregion

		#region Object Override Methods
		public override string ToString()
		{
			return
				string.Format(
					"{0}{1}{2}{3}{4}{5}{6}{7}",
					_flop7.Q == VoltageSignal.HIGH ? 1 : 0,
					_flop6.Q == VoltageSignal.HIGH ? 1 : 0,
					_flop5.Q == VoltageSignal.HIGH ? 1 : 0,
					_flop4.Q == VoltageSignal.HIGH ? 1 : 0,
					_flop3.Q == VoltageSignal.HIGH ? 1 : 0,
					_flop2.Q == VoltageSignal.HIGH ? 1 : 0,
					_flop1.Q == VoltageSignal.HIGH ? 1 : 0,
					_flop0.Q == VoltageSignal.HIGH ? 1 : 0);
		}
		#endregion

		#region Private Members
		private void SetOutput()
		{
			_output = 0x00;

			_output |= (_flop0.Q == VoltageSignal.HIGH ? (byte)0x01 : (byte)0x00);
			_output |= (_flop1.Q == VoltageSignal.HIGH ? (byte)0x02 : (byte)0x00);
			_output |= (_flop2.Q == VoltageSignal.HIGH ? (byte)0x04 : (byte)0x00);
			_output |= (_flop3.Q == VoltageSignal.HIGH ? (byte)0x08 : (byte)0x00);
			_output |= (_flop4.Q == VoltageSignal.HIGH ? (byte)0x10 : (byte)0x00);
			_output |= (_flop5.Q == VoltageSignal.HIGH ? (byte)0x20 : (byte)0x00);
			_output |= (_flop6.Q == VoltageSignal.HIGH ? (byte)0x40 : (byte)0x00);
			_output |= (_flop7.Q == VoltageSignal.HIGH ? (byte)0x80 : (byte)0x00);
		}

		private void HandleEvent(byte oldOutput)
		{
			if (oldOutput != _output)
			{
				if (OutEvent != null)
				{
					OutEvent(this);
				}
			}			
		}
		#endregion
	}
}

/*
$Log: /PetzoldComputer/LatchEdge8.cs $ $NoKeyWords:$
 * 
 * 2     1/21/07 11:58p Sean
 * results of ReSharper analysis
*/
