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
			get { return _flop0.Voltage; }
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
			get { return _flop0.Clk; }
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
			get { return ((IPresetAndClear)_flop0).Clr; }
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

		public VoltageSignal Q8
		{
			get { return _flop8.Q; }
		}

		public VoltageSignal Q9
		{
			get { return _flop9.Q; }
		}

		public VoltageSignal Q10
		{
			get { return _flop10.Q; }
		}

		public VoltageSignal Q11
		{
			get { return _flop11.Q; }
		}

		public VoltageSignal Q12
		{
			get { return _flop12.Q; }
		}

		public VoltageSignal Q13
		{
			get { return _flop13.Q; }
		}

		public VoltageSignal Q14
		{
			get { return _flop14.Q; }
		}

		public VoltageSignal Q15
		{
			get { return _flop15.Q; }
		}

		#endregion

		#region IOutput Members

		public void AddOutputHandler(Action<object> handler)
		{
			OutEvent += handler;
		}

		#endregion

		#region Object Override Methods
		public override string ToString()
		{
			return
				string.Format(
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
		}
		#endregion

		#region Private Methods
		private void DoWireup()
		{
			((IOutput)_flop0).AddOutputHandler(InternalEventHandler);
			((IOutput)_flop1).AddOutputHandler(InternalEventHandler);
			((IOutput)_flop2).AddOutputHandler(InternalEventHandler);
			((IOutput)_flop3).AddOutputHandler(InternalEventHandler);
			((IOutput)_flop4).AddOutputHandler(InternalEventHandler);
			((IOutput)_flop5).AddOutputHandler(InternalEventHandler);
			((IOutput)_flop6).AddOutputHandler(InternalEventHandler);
			((IOutput)_flop7).AddOutputHandler(InternalEventHandler);
			((IOutput)_flop8).AddOutputHandler(InternalEventHandler);
			((IOutput)_flop9).AddOutputHandler(InternalEventHandler);
			((IOutput)_flop10).AddOutputHandler(InternalEventHandler);
			((IOutput)_flop11).AddOutputHandler(InternalEventHandler);
			((IOutput)_flop12).AddOutputHandler(InternalEventHandler);
			((IOutput)_flop13).AddOutputHandler(InternalEventHandler);
			((IOutput)_flop14).AddOutputHandler(InternalEventHandler);
			((IOutput)_flop15).AddOutputHandler(InternalEventHandler);
		}

		private void InternalEventHandler(object o)
		{
			_flop0.D		= _flop0.Qnot;
			_flop1.Clk	= _flop0.Qnot;
			_flop1.D		= _flop1.Qnot;
			_flop2.Clk	= _flop1.Qnot;
			_flop2.D		= _flop2.Qnot;
			_flop3.Clk	= _flop2.Qnot;
			_flop3.D		= _flop3.Qnot;
			_flop4.Clk	= _flop3.Qnot;
			_flop4.D		= _flop4.Qnot;
			_flop5.Clk	= _flop4.Qnot;
			_flop5.D		= _flop5.Qnot;
			_flop6.Clk	= _flop5.Qnot;
			_flop6.D		= _flop6.Qnot;
			_flop7.Clk	= _flop6.Qnot;
			_flop7.D		= _flop7.Qnot;
			_flop8.Clk	= _flop7.Qnot;
			_flop8.D		= _flop8.Qnot;
			_flop9.Clk	= _flop8.Qnot;
			_flop9.D		= _flop9.Qnot;
			_flop10.Clk	= _flop9.Qnot;
			_flop10.D	= _flop10.Qnot;
			_flop11.Clk	= _flop10.Qnot;
			_flop11.D	= _flop11.Qnot;
			_flop12.Clk	= _flop11.Qnot;
			_flop12.D	= _flop12.Qnot;
			_flop13.Clk	= _flop12.Qnot;
			_flop13.D	= _flop13.Qnot;
			_flop14.Clk	= _flop13.Qnot;
			_flop14.D	= _flop14.Qnot;
			_flop15.Clk	= _flop14.Qnot;
			_flop15.D	= _flop15.Qnot;
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
$Log: /PetzoldComputer/CounterSynchronous16.cs $ $NoKeyWords:$
 * 
 * 2     1/21/07 11:58p Sean
 * results of ReSharper analysis
*/
