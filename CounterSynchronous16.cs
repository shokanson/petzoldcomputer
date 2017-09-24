using System;

namespace PetzoldComputer
{
	// This class is implemented internally as a ripple counter, but since I only fire
	// the output event after all outputs have changed, to the user it looks like a synchronous counter.
	public class CounterSynchronous16 : ICounterSynchronous16, IOutput
	{
		#region Construction
		public CounterSynchronous16()
		{
			_flop0 = new DFlipFlopEdgeWithPresetAndClear();
			_flop1 = new DFlipFlopEdgeWithPresetAndClear();
			_flop2 = new DFlipFlopEdgeWithPresetAndClear();
			_flop3 = new DFlipFlopEdgeWithPresetAndClear();
			_flop4 = new DFlipFlopEdgeWithPresetAndClear();
			_flop5 = new DFlipFlopEdgeWithPresetAndClear();
			_flop6 = new DFlipFlopEdgeWithPresetAndClear();
			_flop7 = new DFlipFlopEdgeWithPresetAndClear();
			_flop8 = new DFlipFlopEdgeWithPresetAndClear();
			_flop9 = new DFlipFlopEdgeWithPresetAndClear();
			_flop10 = new DFlipFlopEdgeWithPresetAndClear();
			_flop11 = new DFlipFlopEdgeWithPresetAndClear();
			_flop12 = new DFlipFlopEdgeWithPresetAndClear();
			_flop13 = new DFlipFlopEdgeWithPresetAndClear();
			_flop14 = new DFlipFlopEdgeWithPresetAndClear();
			_flop15 = new DFlipFlopEdgeWithPresetAndClear();

			_output = 0x0000;

			DoWireup();
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
		private IDFlipFlop _flop8;
		private IDFlipFlop _flop9;
		private IDFlipFlop _flop10;
		private IDFlipFlop _flop11;
		private IDFlipFlop _flop12;
		private IDFlipFlop _flop13;
		private IDFlipFlop _flop14;
		private IDFlipFlop _flop15;

		private ushort _output;

		private Action<object> OutEvent;
		#endregion

		#region ICounterSynchronous16 Members

		public VoltageSignal Voltage
		{
			get => _flop0.Voltage;
			set
			{
				ushort oldOutput = _output;

				_flop0.Voltage =
					_flop1.Voltage =
					_flop2.Voltage =
					_flop3.Voltage =
					_flop4.Voltage =
					_flop5.Voltage =
					_flop6.Voltage =
					_flop7.Voltage =
					_flop8.Voltage =
					_flop9.Voltage =
					_flop10.Voltage =
					_flop11.Voltage =
					_flop12.Voltage =
					_flop13.Voltage =
					_flop14.Voltage =
					_flop15.Voltage = value;

				if (value == VoltageSignal.HIGH)
				{
					// a little bump to get things started
					_flop0.D = _flop0.Qnot;
				}

				SetOutput();
				HandleEvent(oldOutput);
			}
		}

		public VoltageSignal Clk
		{
			get => _flop0.Clk;
			set
			{
				ushort oldOutput = _output;

				_flop0.Clk = value;

				SetOutput();
				HandleEvent(oldOutput);
			}
		}

		public VoltageSignal Clr
		{
			get => ((IPresetAndClear)_flop0).Clr;
			set
			{
				ushort oldOutput = _output;

				((IPresetAndClear)_flop0).Clr =
					((IPresetAndClear)_flop1).Clr =
					((IPresetAndClear)_flop2).Clr =
					((IPresetAndClear)_flop3).Clr =
					((IPresetAndClear)_flop4).Clr =
					((IPresetAndClear)_flop5).Clr =
					((IPresetAndClear)_flop6).Clr =
					((IPresetAndClear)_flop7).Clr =
					((IPresetAndClear)_flop8).Clr =
					((IPresetAndClear)_flop9).Clr =
					((IPresetAndClear)_flop10).Clr =
					((IPresetAndClear)_flop11).Clr =
					((IPresetAndClear)_flop12).Clr =
					((IPresetAndClear)_flop13).Clr =
					((IPresetAndClear)_flop14).Clr =
					((IPresetAndClear)_flop15).Clr = value;

				SetOutput();
				HandleEvent(oldOutput);
			}
		}

		public VoltageSignal Q0 => _flop0.Q;
		public VoltageSignal Q1 => _flop1.Q;
		public VoltageSignal Q2 => _flop2.Q;
		public VoltageSignal Q3 => _flop3.Q;
		public VoltageSignal Q4 => _flop4.Q;
		public VoltageSignal Q5 => _flop5.Q;
		public VoltageSignal Q6 => _flop6.Q;
		public VoltageSignal Q7 => _flop7.Q;
		public VoltageSignal Q8 => _flop8.Q;
		public VoltageSignal Q9 => _flop9.Q;
		public VoltageSignal Q10 => _flop10.Q;
		public VoltageSignal Q11 => _flop11.Q;
		public VoltageSignal Q12 => _flop12.Q;
		public VoltageSignal Q13 => _flop13.Q;
		public VoltageSignal Q14 => _flop14.Q;
		public VoltageSignal Q15 => _flop15.Q;

		#endregion

		#region IOutput Members

		public void AddOutputHandler(Action<object> handler) => OutEvent += handler;

		#endregion

		#region Object Override Methods
		// this is a case where string.Format is clearer than an interpolated string
		public override string ToString() => string.Format(
					"{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}",
					_flop15.Q == VoltageSignal.HIGH ? 1 : 0,
					_flop14.Q == VoltageSignal.HIGH ? 1 : 0,
					_flop13.Q == VoltageSignal.HIGH ? 1 : 0,
					_flop12.Q == VoltageSignal.HIGH ? 1 : 0,
					_flop11.Q == VoltageSignal.HIGH ? 1 : 0,
					_flop10.Q == VoltageSignal.HIGH ? 1 : 0,
					_flop9.Q == VoltageSignal.HIGH ? 1 : 0,
					_flop8.Q == VoltageSignal.HIGH ? 1 : 0,
					_flop7.Q == VoltageSignal.HIGH ? 1 : 0,
					_flop6.Q == VoltageSignal.HIGH ? 1 : 0,
					_flop5.Q == VoltageSignal.HIGH ? 1 : 0,
					_flop4.Q == VoltageSignal.HIGH ? 1 : 0,
					_flop3.Q == VoltageSignal.HIGH ? 1 : 0,
					_flop2.Q == VoltageSignal.HIGH ? 1 : 0,
					_flop1.Q == VoltageSignal.HIGH ? 1 : 0,
					_flop0.Q == VoltageSignal.HIGH ? 1 : 0);
		#endregion

		#region Private Methods
		private void DoWireup()
		{
			((IOutput)_flop0).AddOutputHandler(Flop0Handler);
			((IOutput)_flop1).AddOutputHandler(Flop1Handler);
			((IOutput)_flop2).AddOutputHandler(Flop2Handler);
			((IOutput)_flop3).AddOutputHandler(Flop3Handler);
			((IOutput)_flop4).AddOutputHandler(Flop4Handler);
			((IOutput)_flop5).AddOutputHandler(Flop5Handler);
			((IOutput)_flop6).AddOutputHandler(Flop6Handler);
			((IOutput)_flop7).AddOutputHandler(Flop7Handler);
			((IOutput)_flop8).AddOutputHandler(Flop8Handler);
			((IOutput)_flop9).AddOutputHandler(Flop9Handler);
			((IOutput)_flop10).AddOutputHandler(Flop10Handler);
			((IOutput)_flop11).AddOutputHandler(Flop11Handler);
			((IOutput)_flop12).AddOutputHandler(Flop12Handler);
			((IOutput)_flop13).AddOutputHandler(Flop13Handler);
			((IOutput)_flop14).AddOutputHandler(Flop14Handler);
			((IOutput)_flop15).AddOutputHandler(Flop15Handler);
		}

		private void Flop0Handler(object o)
		{
			_flop0.D = _flop0.Qnot;
			_flop1.Clk = _flop0.Qnot;
			_flop1.D = _flop1.Qnot;
			_flop2.Clk = _flop1.Qnot;
			_flop2.D = _flop2.Qnot;
			_flop3.Clk = _flop2.Qnot;
			_flop3.D = _flop3.Qnot;
			_flop4.Clk = _flop3.Qnot;
			_flop4.D = _flop4.Qnot;
			_flop5.Clk = _flop4.Qnot;
			_flop5.D = _flop5.Qnot;
			_flop6.Clk = _flop5.Qnot;
			_flop6.D = _flop6.Qnot;
			_flop7.Clk = _flop6.Qnot;
			_flop7.D = _flop7.Qnot;
			_flop8.Clk = _flop7.Qnot;
			_flop8.D = _flop8.Qnot;
			_flop9.Clk = _flop8.Qnot;
			_flop9.D = _flop9.Qnot;
			_flop10.Clk = _flop9.Qnot;
			_flop10.D = _flop10.Qnot;
			_flop11.Clk = _flop10.Qnot;
			_flop11.D = _flop11.Qnot;
			_flop12.Clk = _flop11.Qnot;
			_flop12.D = _flop12.Qnot;
			_flop13.Clk = _flop12.Qnot;
			_flop13.D = _flop13.Qnot;
			_flop14.Clk = _flop13.Qnot;
			_flop14.D = _flop14.Qnot;
			_flop15.Clk = _flop14.Qnot;
			_flop15.D = _flop15.Qnot;
		}

		private void Flop1Handler(object o)
		{
			_flop1.D = _flop1.Qnot;
			_flop2.Clk = _flop1.Qnot;
			_flop2.D = _flop2.Qnot;
			_flop3.Clk = _flop2.Qnot;
			_flop3.D = _flop3.Qnot;
			_flop4.Clk = _flop3.Qnot;
			_flop4.D = _flop4.Qnot;
			_flop5.Clk = _flop4.Qnot;
			_flop5.D = _flop5.Qnot;
			_flop6.Clk = _flop5.Qnot;
			_flop6.D = _flop6.Qnot;
			_flop7.Clk = _flop6.Qnot;
			_flop7.D = _flop7.Qnot;
			_flop8.Clk = _flop7.Qnot;
			_flop8.D = _flop8.Qnot;
			_flop9.Clk = _flop8.Qnot;
			_flop9.D = _flop9.Qnot;
			_flop10.Clk = _flop9.Qnot;
			_flop10.D = _flop10.Qnot;
			_flop11.Clk = _flop10.Qnot;
			_flop11.D = _flop11.Qnot;
			_flop12.Clk = _flop11.Qnot;
			_flop12.D = _flop12.Qnot;
			_flop13.Clk = _flop12.Qnot;
			_flop13.D = _flop13.Qnot;
			_flop14.Clk = _flop13.Qnot;
			_flop14.D = _flop14.Qnot;
			_flop15.Clk = _flop14.Qnot;
			_flop15.D = _flop15.Qnot;
		}

		private void Flop2Handler(object o)
		{
			_flop2.D = _flop2.Qnot;
			_flop3.Clk = _flop2.Qnot;
			_flop3.D = _flop3.Qnot;
			_flop4.Clk = _flop3.Qnot;
			_flop4.D = _flop4.Qnot;
			_flop5.Clk = _flop4.Qnot;
			_flop5.D = _flop5.Qnot;
			_flop6.Clk = _flop5.Qnot;
			_flop6.D = _flop6.Qnot;
			_flop7.Clk = _flop6.Qnot;
			_flop7.D = _flop7.Qnot;
			_flop8.Clk = _flop7.Qnot;
			_flop8.D = _flop8.Qnot;
			_flop9.Clk = _flop8.Qnot;
			_flop9.D = _flop9.Qnot;
			_flop10.Clk = _flop9.Qnot;
			_flop10.D = _flop10.Qnot;
			_flop11.Clk = _flop10.Qnot;
			_flop11.D = _flop11.Qnot;
			_flop12.Clk = _flop11.Qnot;
			_flop12.D = _flop12.Qnot;
			_flop13.Clk = _flop12.Qnot;
			_flop13.D = _flop13.Qnot;
			_flop14.Clk = _flop13.Qnot;
			_flop14.D = _flop14.Qnot;
			_flop15.Clk = _flop14.Qnot;
			_flop15.D = _flop15.Qnot;
		}

		private void Flop3Handler(object o)
		{
			_flop3.D = _flop3.Qnot;
			_flop4.Clk = _flop3.Qnot;
			_flop4.D = _flop4.Qnot;
			_flop5.Clk = _flop4.Qnot;
			_flop5.D = _flop5.Qnot;
			_flop6.Clk = _flop5.Qnot;
			_flop6.D = _flop6.Qnot;
			_flop7.Clk = _flop6.Qnot;
			_flop7.D = _flop7.Qnot;
			_flop8.Clk = _flop7.Qnot;
			_flop8.D = _flop8.Qnot;
			_flop9.Clk = _flop8.Qnot;
			_flop9.D = _flop9.Qnot;
			_flop10.Clk = _flop9.Qnot;
			_flop10.D = _flop10.Qnot;
			_flop11.Clk = _flop10.Qnot;
			_flop11.D = _flop11.Qnot;
			_flop12.Clk = _flop11.Qnot;
			_flop12.D = _flop12.Qnot;
			_flop13.Clk = _flop12.Qnot;
			_flop13.D = _flop13.Qnot;
			_flop14.Clk = _flop13.Qnot;
			_flop14.D = _flop14.Qnot;
			_flop15.Clk = _flop14.Qnot;
			_flop15.D = _flop15.Qnot;
		}

		private void Flop4Handler(object o)
		{
			_flop4.D = _flop4.Qnot;
			_flop5.Clk = _flop4.Qnot;
			_flop5.D = _flop5.Qnot;
			_flop6.Clk = _flop5.Qnot;
			_flop6.D = _flop6.Qnot;
			_flop7.Clk = _flop6.Qnot;
			_flop7.D = _flop7.Qnot;
			_flop8.Clk = _flop7.Qnot;
			_flop8.D = _flop8.Qnot;
			_flop9.Clk = _flop8.Qnot;
			_flop9.D = _flop9.Qnot;
			_flop10.Clk = _flop9.Qnot;
			_flop10.D = _flop10.Qnot;
			_flop11.Clk = _flop10.Qnot;
			_flop11.D = _flop11.Qnot;
			_flop12.Clk = _flop11.Qnot;
			_flop12.D = _flop12.Qnot;
			_flop13.Clk = _flop12.Qnot;
			_flop13.D = _flop13.Qnot;
			_flop14.Clk = _flop13.Qnot;
			_flop14.D = _flop14.Qnot;
			_flop15.Clk = _flop14.Qnot;
			_flop15.D = _flop15.Qnot;
		}

		private void Flop5Handler(object o)
		{
			_flop5.D = _flop5.Qnot;
			_flop6.Clk = _flop5.Qnot;
			_flop6.D = _flop6.Qnot;
			_flop7.Clk = _flop6.Qnot;
			_flop7.D = _flop7.Qnot;
			_flop8.Clk = _flop7.Qnot;
			_flop8.D = _flop8.Qnot;
			_flop9.Clk = _flop8.Qnot;
			_flop9.D = _flop9.Qnot;
			_flop10.Clk = _flop9.Qnot;
			_flop10.D = _flop10.Qnot;
			_flop11.Clk = _flop10.Qnot;
			_flop11.D = _flop11.Qnot;
			_flop12.Clk = _flop11.Qnot;
			_flop12.D = _flop12.Qnot;
			_flop13.Clk = _flop12.Qnot;
			_flop13.D = _flop13.Qnot;
			_flop14.Clk = _flop13.Qnot;
			_flop14.D = _flop14.Qnot;
			_flop15.Clk = _flop14.Qnot;
			_flop15.D = _flop15.Qnot;
		}

		private void Flop6Handler(object o)
		{
			_flop6.D = _flop6.Qnot;
			_flop7.Clk = _flop6.Qnot;
			_flop7.D = _flop7.Qnot;
			_flop8.Clk = _flop7.Qnot;
			_flop8.D = _flop8.Qnot;
			_flop9.Clk = _flop8.Qnot;
			_flop9.D = _flop9.Qnot;
			_flop10.Clk = _flop9.Qnot;
			_flop10.D = _flop10.Qnot;
			_flop11.Clk = _flop10.Qnot;
			_flop11.D = _flop11.Qnot;
			_flop12.Clk = _flop11.Qnot;
			_flop12.D = _flop12.Qnot;
			_flop13.Clk = _flop12.Qnot;
			_flop13.D = _flop13.Qnot;
			_flop14.Clk = _flop13.Qnot;
			_flop14.D = _flop14.Qnot;
			_flop15.Clk = _flop14.Qnot;
			_flop15.D = _flop15.Qnot;
		}

		private void Flop7Handler(object o)
		{
			_flop7.D = _flop7.Qnot;
			_flop8.Clk = _flop7.Qnot;
			_flop8.D = _flop8.Qnot;
			_flop9.Clk = _flop8.Qnot;
			_flop9.D = _flop9.Qnot;
			_flop10.Clk = _flop9.Qnot;
			_flop10.D = _flop10.Qnot;
			_flop11.Clk = _flop10.Qnot;
			_flop11.D = _flop11.Qnot;
			_flop12.Clk = _flop11.Qnot;
			_flop12.D = _flop12.Qnot;
			_flop13.Clk = _flop12.Qnot;
			_flop13.D = _flop13.Qnot;
			_flop14.Clk = _flop13.Qnot;
			_flop14.D = _flop14.Qnot;
			_flop15.Clk = _flop14.Qnot;
			_flop15.D = _flop15.Qnot;
		}

		private void Flop8Handler(object o)
		{
			_flop8.D = _flop8.Qnot;
			_flop9.Clk = _flop8.Qnot;
			_flop9.D = _flop9.Qnot;
			_flop10.Clk = _flop9.Qnot;
			_flop10.D = _flop10.Qnot;
			_flop11.Clk = _flop10.Qnot;
			_flop11.D = _flop11.Qnot;
			_flop12.Clk = _flop11.Qnot;
			_flop12.D = _flop12.Qnot;
			_flop13.Clk = _flop12.Qnot;
			_flop13.D = _flop13.Qnot;
			_flop14.Clk = _flop13.Qnot;
			_flop14.D = _flop14.Qnot;
			_flop15.Clk = _flop14.Qnot;
			_flop15.D = _flop15.Qnot;
		}

		private void Flop9Handler(object o)
		{
			_flop9.D = _flop9.Qnot;
			_flop10.Clk = _flop9.Qnot;
			_flop10.D = _flop10.Qnot;
			_flop11.Clk = _flop10.Qnot;
			_flop11.D = _flop11.Qnot;
			_flop12.Clk = _flop11.Qnot;
			_flop12.D = _flop12.Qnot;
			_flop13.Clk = _flop12.Qnot;
			_flop13.D = _flop13.Qnot;
			_flop14.Clk = _flop13.Qnot;
			_flop14.D = _flop14.Qnot;
			_flop15.Clk = _flop14.Qnot;
			_flop15.D = _flop15.Qnot;
		}

		private void Flop10Handler(object o)
		{
			_flop10.D = _flop10.Qnot;
			_flop11.Clk = _flop10.Qnot;
			_flop11.D = _flop11.Qnot;
			_flop12.Clk = _flop11.Qnot;
			_flop12.D = _flop12.Qnot;
			_flop13.Clk = _flop12.Qnot;
			_flop13.D = _flop13.Qnot;
			_flop14.Clk = _flop13.Qnot;
			_flop14.D = _flop14.Qnot;
			_flop15.Clk = _flop14.Qnot;
			_flop15.D = _flop15.Qnot;
		}

		private void Flop11Handler(object o)
		{
			_flop11.D = _flop11.Qnot;
			_flop12.Clk = _flop11.Qnot;
			_flop12.D = _flop12.Qnot;
			_flop13.Clk = _flop12.Qnot;
			_flop13.D = _flop13.Qnot;
			_flop14.Clk = _flop13.Qnot;
			_flop14.D = _flop14.Qnot;
			_flop15.Clk = _flop14.Qnot;
			_flop15.D = _flop15.Qnot;
		}

		private void Flop12Handler(object o)
		{
			_flop12.D = _flop12.Qnot;
			_flop13.Clk = _flop12.Qnot;
			_flop13.D = _flop13.Qnot;
			_flop14.Clk = _flop13.Qnot;
			_flop14.D = _flop14.Qnot;
			_flop15.Clk = _flop14.Qnot;
			_flop15.D = _flop15.Qnot;
		}

		private void Flop13Handler(object o)
		{
			_flop13.D = _flop13.Qnot;
			_flop14.Clk = _flop13.Qnot;
			_flop14.D = _flop14.Qnot;
			_flop15.Clk = _flop14.Qnot;
			_flop15.D = _flop15.Qnot;
		}

		private void Flop14Handler(object o)
		{
			_flop14.D = _flop14.Qnot;
			_flop15.Clk = _flop14.Qnot;
			_flop15.D = _flop15.Qnot;
		}

		private void Flop15Handler(object o)
		{
			_flop15.D = _flop15.Qnot;
		}

		private void SetOutput()
		{
			_output = 0x0000;

			_output |= (_flop0.Q == VoltageSignal.HIGH ? (ushort)0x0001 : (ushort)0x0000);
			_output |= (_flop1.Q == VoltageSignal.HIGH ? (ushort)0x0002 : (ushort)0x0000);
			_output |= (_flop2.Q == VoltageSignal.HIGH ? (ushort)0x0004 : (ushort)0x0000);
			_output |= (_flop3.Q == VoltageSignal.HIGH ? (ushort)0x0008 : (ushort)0x0000);
			_output |= (_flop4.Q == VoltageSignal.HIGH ? (ushort)0x0010 : (ushort)0x0000);
			_output |= (_flop5.Q == VoltageSignal.HIGH ? (ushort)0x0020 : (ushort)0x0000);
			_output |= (_flop6.Q == VoltageSignal.HIGH ? (ushort)0x0040 : (ushort)0x0000);
			_output |= (_flop7.Q == VoltageSignal.HIGH ? (ushort)0x0080 : (ushort)0x0000);
			_output |= (_flop8.Q == VoltageSignal.HIGH ? (ushort)0x0100 : (ushort)0x0000);
			_output |= (_flop9.Q == VoltageSignal.HIGH ? (ushort)0x0200 : (ushort)0x0000);
			_output |= (_flop10.Q == VoltageSignal.HIGH ? (ushort)0x0400 : (ushort)0x0000);
			_output |= (_flop11.Q == VoltageSignal.HIGH ? (ushort)0x0800 : (ushort)0x0000);
			_output |= (_flop12.Q == VoltageSignal.HIGH ? (ushort)0x1000 : (ushort)0x0000);
			_output |= (_flop13.Q == VoltageSignal.HIGH ? (ushort)0x2000 : (ushort)0x0000);
			_output |= (_flop14.Q == VoltageSignal.HIGH ? (ushort)0x4000 : (ushort)0x0000);
			_output |= (_flop15.Q == VoltageSignal.HIGH ? (ushort)0x8000 : (ushort)0x0000);
		}

		private void HandleEvent(ushort oldOutput)
		{
			if (oldOutput != _output)
			{
				OutEvent?.Invoke(this);
			}
		}
		#endregion
	}

	public class CounterRipple16
	{
		public CounterRipple16(string name)
		{
			_v = new ConnectionPoint($"{name}-counter16.v");
			_clr = new ConnectionPoint($"{name}-counter16.clr");

			_flop0 = new DFlipFlopEdgeWithPresetAndClear_2($"{name}-counter16.q0");
			_flop1 = new DFlipFlopEdgeWithPresetAndClear_2($"{name}-counter16.q1");
			_flop2 = new DFlipFlopEdgeWithPresetAndClear_2($"{name}-counter16.q2");
			_flop3 = new DFlipFlopEdgeWithPresetAndClear_2($"{name}-counter16.q3");
			_flop4 = new DFlipFlopEdgeWithPresetAndClear_2($"{name}-counter16.q4");
			_flop5 = new DFlipFlopEdgeWithPresetAndClear_2($"{name}-counter16.q5");
			_flop6 = new DFlipFlopEdgeWithPresetAndClear_2($"{name}-counter16.q6");
			_flop7 = new DFlipFlopEdgeWithPresetAndClear_2($"{name}-counter16.q7");
			_flop8 = new DFlipFlopEdgeWithPresetAndClear_2($"{name}-counter16.q8");
			_flop9 = new DFlipFlopEdgeWithPresetAndClear_2($"{name}-counter16.q9");
			_flop10 = new DFlipFlopEdgeWithPresetAndClear_2($"{name}-counter16.q10");
			_flop11 = new DFlipFlopEdgeWithPresetAndClear_2($"{name}-counter16.q11");
			_flop12 = new DFlipFlopEdgeWithPresetAndClear_2($"{name}-counter16.q12");
			_flop13 = new DFlipFlopEdgeWithPresetAndClear_2($"{name}-counter16.q13");
			_flop14 = new DFlipFlopEdgeWithPresetAndClear_2($"{name}-counter16.q14");
			_flop15 = new DFlipFlopEdgeWithPresetAndClear_2($"{name}-counter16.q15");

			DoWireUp();
		}

		private readonly ConnectionPoint _v;
		private readonly ConnectionPoint _clr;

		private readonly DFlipFlopEdgeWithPresetAndClear_2 _flop0;
		private readonly DFlipFlopEdgeWithPresetAndClear_2 _flop1;
		private readonly DFlipFlopEdgeWithPresetAndClear_2 _flop2;
		private readonly DFlipFlopEdgeWithPresetAndClear_2 _flop3;
		private readonly DFlipFlopEdgeWithPresetAndClear_2 _flop4;
		private readonly DFlipFlopEdgeWithPresetAndClear_2 _flop5;
		private readonly DFlipFlopEdgeWithPresetAndClear_2 _flop6;
		private readonly DFlipFlopEdgeWithPresetAndClear_2 _flop7;
		private readonly DFlipFlopEdgeWithPresetAndClear_2 _flop8;
		private readonly DFlipFlopEdgeWithPresetAndClear_2 _flop9;
		private readonly DFlipFlopEdgeWithPresetAndClear_2 _flop10;
		private readonly DFlipFlopEdgeWithPresetAndClear_2 _flop11;
		private readonly DFlipFlopEdgeWithPresetAndClear_2 _flop12;
		private readonly DFlipFlopEdgeWithPresetAndClear_2 _flop13;
		private readonly DFlipFlopEdgeWithPresetAndClear_2 _flop14;
		private readonly DFlipFlopEdgeWithPresetAndClear_2 _flop15;

		public ConnectionPoint V => _v;
		public ConnectionPoint Clr => _clr;
		public ConnectionPoint Clk => _flop0.Clk;

		public ConnectionPoint Q0 => _flop0.Q;
		public ConnectionPoint Q1 => _flop1.Q;
		public ConnectionPoint Q2 => _flop2.Q;
		public ConnectionPoint Q3 => _flop3.Q;
		public ConnectionPoint Q4 => _flop4.Q;
		public ConnectionPoint Q5 => _flop5.Q;
		public ConnectionPoint Q6 => _flop6.Q;
		public ConnectionPoint Q7 => _flop7.Q;
		public ConnectionPoint Q8 => _flop8.Q;
		public ConnectionPoint Q9 => _flop9.Q;
		public ConnectionPoint Q10 => _flop10.Q;
		public ConnectionPoint Q11 => _flop11.Q;
		public ConnectionPoint Q12 => _flop12.Q;
		public ConnectionPoint Q13 => _flop13.Q;
		public ConnectionPoint Q14 => _flop14.Q;
		public ConnectionPoint Q15 => _flop15.Q;

		// this is a case where string.Format is clearer than an interpolated string
		public override string ToString() => string.Format(
					"{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}",
					_flop15.Q.V == VoltageSignal.HIGH ? 1 : 0,
					_flop14.Q.V == VoltageSignal.HIGH ? 1 : 0,
					_flop13.Q.V == VoltageSignal.HIGH ? 1 : 0,
					_flop12.Q.V == VoltageSignal.HIGH ? 1 : 0,
					_flop11.Q.V == VoltageSignal.HIGH ? 1 : 0,
					_flop10.Q.V == VoltageSignal.HIGH ? 1 : 0,
					_flop9.Q.V == VoltageSignal.HIGH ? 1 : 0,
					_flop8.Q.V == VoltageSignal.HIGH ? 1 : 0,
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
			_v.ConnectTo(_flop15.V)
				.ConnectTo(_flop14.V)
				.ConnectTo(_flop13.V)
				.ConnectTo(_flop12.V)
				.ConnectTo(_flop11.V)
				.ConnectTo(_flop10.V)
				.ConnectTo(_flop9.V)
				.ConnectTo(_flop8.V)
				.ConnectTo(_flop7.V)
				.ConnectTo(_flop6.V)
				.ConnectTo(_flop5.V)
				.ConnectTo(_flop4.V)
				.ConnectTo(_flop3.V)
				.ConnectTo(_flop2.V)
				.ConnectTo(_flop1.V)
				.ConnectTo(_flop0.V);

			_v.Changed += v =>
			{
				// a little bump to get things started
				if (v.V == VoltageSignal.HIGH)
				{
					_clr.V = VoltageSignal.HIGH;
					_clr.V = VoltageSignal.LOW;
					_flop0.D.V = _flop0.Qnot.V;
				}
			};

			_clr.ConnectTo(_flop15.Clr)
				 .ConnectTo(_flop14.Clr)
				 .ConnectTo(_flop13.Clr)
				 .ConnectTo(_flop12.Clr)
				 .ConnectTo(_flop11.Clr)
				 .ConnectTo(_flop10.Clr)
				 .ConnectTo(_flop9.Clr)
				 .ConnectTo(_flop8.Clr)
				 .ConnectTo(_flop7.Clr)
				 .ConnectTo(_flop6.Clr)
				 .ConnectTo(_flop5.Clr)
				 .ConnectTo(_flop4.Clr)
				 .ConnectTo(_flop3.Clr)
				 .ConnectTo(_flop2.Clr)
				 .ConnectTo(_flop1.Clr)
				 .ConnectTo(_flop0.Clr);

			_flop0.Qnot.ConnectTo(_flop0.D).ConnectTo(_flop1.Clk);
			_flop1.Qnot.ConnectTo(_flop1.D).ConnectTo(_flop2.Clk);
			_flop2.Qnot.ConnectTo(_flop2.D).ConnectTo(_flop3.Clk);
			_flop3.Qnot.ConnectTo(_flop3.D).ConnectTo(_flop4.Clk);
			_flop4.Qnot.ConnectTo(_flop4.D).ConnectTo(_flop5.Clk);
			_flop5.Qnot.ConnectTo(_flop5.D).ConnectTo(_flop6.Clk);
			_flop6.Qnot.ConnectTo(_flop6.D).ConnectTo(_flop7.Clk);
			_flop7.Qnot.ConnectTo(_flop7.D).ConnectTo(_flop8.Clk);
			_flop8.Qnot.ConnectTo(_flop8.D).ConnectTo(_flop9.Clk);
			_flop9.Qnot.ConnectTo(_flop9.D).ConnectTo(_flop10.Clk);
			_flop10.Qnot.ConnectTo(_flop10.D).ConnectTo(_flop11.Clk);
			_flop11.Qnot.ConnectTo(_flop11.D).ConnectTo(_flop12.Clk);
			_flop12.Qnot.ConnectTo(_flop12.D).ConnectTo(_flop13.Clk);
			_flop13.Qnot.ConnectTo(_flop13.D).ConnectTo(_flop14.Clk);
			_flop14.Qnot.ConnectTo(_flop14.D).ConnectTo(_flop15.Clk);
			_flop15.Qnot.ConnectTo(_flop15.D);
		}
	}
}
