using System;

namespace PetzoldComputer
{
	public class NAND : INand, IOutput
	{
		#region Construction
		public NAND()
		{
			_relay1 = new InvertedRelay();
			_relay2 = new InvertedRelay();
			SetOutput();
		}
		#endregion

		#region Implementation
		private IRelay _relay1;
		private IRelay _relay2;
		private VoltageSignal _output;

		private Action<object> OutputChanged;
		#endregion

		#region INand Members

		public VoltageSignal Voltage
		{
			get => _relay1.Voltage;
			set
			{
				VoltageSignal oldOutput = _output;

				_relay1.Voltage = value;
				_relay2.Voltage = value;
				SetOutput();
				FireEvent(oldOutput);
			}
		}

		public VoltageSignal A
		{
			get => _relay1.Input;
			set
			{
				VoltageSignal oldOutput = _output;

				_relay1.Input = value;
				SetOutput();
				FireEvent(oldOutput);
			}
		}

		public VoltageSignal B
		{
			get => _relay2.Input;
			set
			{
				VoltageSignal oldOutput = _output;

				_relay2.Input = value;
				SetOutput();
				FireEvent(oldOutput);
			}
		}

		public VoltageSignal O => _output;

		#endregion

		#region IOutput Members

		public void AddOutputHandler(Action<object> handler) => OutputChanged += handler;

		#endregion

		#region Object Override Members
		public override string ToString() => _output.ToString();
		#endregion

		#region Private Members
		private void SetOutput() =>
			_output = (_relay1.Output == VoltageSignal.HIGH || _relay2.Output == VoltageSignal.HIGH)
				? VoltageSignal.HIGH
				: VoltageSignal.LOW;

		private void FireEvent(VoltageSignal oldOutput)
		{
			if (_output != oldOutput)
			{
				OutputChanged?.Invoke(this);
			}
		}
		#endregion
	}

	public class NAND_2
	{
		public NAND_2()
		{
			DoWireUp();
		}

		private readonly NOT_2 _not1 = new NOT_2();
		private readonly NOT_2 _not2 = new NOT_2();
		private readonly ConnectionPoint _output = new ConnectionPoint();

		public ConnectionPoint V => _not1.V;
		public ConnectionPoint A => _not1.Input;
		public ConnectionPoint B => _not2.Input;
		public ConnectionPoint O => _output;

		public override string ToString() => O.ToString();

		private void DoWireUp()
		{
			_not1.V.ConnectTo(_not2.V);
			// wiring it up this way doesn't really make sense
			//_not1.Output.ConnectTo(_output);
			//_not2.Output.ConnectTo(_output);
			// ...so we'll do some manual wiring
			_not1.Output.Changed += OnNotOutputChanged;
			_not2.Output.Changed += OnNotOutputChanged;
		}

		private void OnNotOutputChanged(ConnectionPoint cp)
		{
			_output.V = _not1.Output.V == VoltageSignal.HIGH || _not2.Output.V == VoltageSignal.HIGH
				? VoltageSignal.HIGH
				: VoltageSignal.LOW;
		}
	}
}