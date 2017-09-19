using System;

namespace PetzoldComputer
{
	public class ControlPanel : IControlPanel, IOutput
	{
		#region Construction
		public ControlPanel()
		{
			_d0 = new Selector2to1();
			_d1 = new Selector2to1();
			_d2 = new Selector2to1();
			_d3 = new Selector2to1();
			_d4 = new Selector2to1();
			_d5 = new Selector2to1();
			_d6 = new Selector2to1();
			_d7 = new Selector2to1();

			_a0 = new Selector2to1();
			_a1 = new Selector2to1();
			_a2 = new Selector2to1();
			_a3 = new Selector2to1();
			_a4 = new Selector2to1();
			_a5 = new Selector2to1();
			_a6 = new Selector2to1();
			_a7 = new Selector2to1();
			_a8 = new Selector2to1();
			_a9 = new Selector2to1();
			_a10 = new Selector2to1();
			_a11 = new Selector2to1();
			_a12 = new Selector2to1();
			_a13 = new Selector2to1();
			_a14 = new Selector2to1();
			_a15 = new Selector2to1();

			_write = new Selector2to1();
		}
		#endregion

		#region Implementation
		private ISelector2to1 _d0;
		private ISelector2to1 _d1;
		private ISelector2to1 _d2;
		private ISelector2to1 _d3;
		private ISelector2to1 _d4;
		private ISelector2to1 _d5;
		private ISelector2to1 _d6;
		private ISelector2to1 _d7;

		private ISelector2to1 _a0;
		private ISelector2to1 _a1;
		private ISelector2to1 _a2;
		private ISelector2to1 _a3;
		private ISelector2to1 _a4;
		private ISelector2to1 _a5;
		private ISelector2to1 _a6;
		private ISelector2to1 _a7;
		private ISelector2to1 _a8;
		private ISelector2to1 _a9;
		private ISelector2to1 _a10;
		private ISelector2to1 _a11;
		private ISelector2to1 _a12;
		private ISelector2to1 _a13;
		private ISelector2to1 _a14;
		private ISelector2to1 _a15;

		private ISelector2to1 _write;

		private Action<object> OutputChanged;
		#endregion

		public VoltageSignal Voltage
		{
			get => _write.Voltage;
			set
			{
				string oldOutput = ToString();
				_d0.Voltage =
					_d1.Voltage =
					_d2.Voltage =
					_d3.Voltage =
					_d4.Voltage =
					_d5.Voltage =
					_d6.Voltage =
					_d7.Voltage =
					_a0.Voltage =
					_a1.Voltage =
					_a2.Voltage =
					_a3.Voltage =
					_a4.Voltage =
					_a5.Voltage =
					_a6.Voltage =
					_a7.Voltage =
					_a8.Voltage =
					_a9.Voltage =
					_a10.Voltage =
					_a11.Voltage =
					_a12.Voltage =
					_a13.Voltage =
					_a14.Voltage =
					_a15.Voltage =
					_write.Voltage = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}

		public VoltageSignal Takeover
		{
			get => _write.Select;
			set
			{
				string oldOutput = ToString();
				_d0.Select =
					_d1.Select =
					_d2.Select =
					_d3.Select =
					_d4.Select =
					_d5.Select =
					_d6.Select =
					_d7.Select =
					_a0.Select =
					_a1.Select =
					_a2.Select =
					_a3.Select =
					_a4.Select =
					_a5.Select =
					_a6.Select =
					_a7.Select =
					_a8.Select =
					_a9.Select =
					_a10.Select =
					_a11.Select =
					_a12.Select =
					_a13.Select =
					_a14.Select =
					_a15.Select =
					_write.Select = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}

		public VoltageSignal D0_in
		{
			get => _d0.A;
			set
			{
				string oldOutput = ToString();
				_d0.A = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal D1_in
		{
			get => _d1.A;
			set
			{
				string oldOutput = ToString();
				_d1.A = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal D2_in
		{
			get => _d2.A;
			set
			{
				string oldOutput = ToString();
				_d2.A = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal D3_in
		{
			get => _d3.A;
			set
			{
				string oldOutput = ToString();
				_d3.A = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal D4_in
		{
			get => _d4.A;
			set
			{
				string oldOutput = ToString();
				_d4.A = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal D5_in
		{
			get => _d5.A;
			set
			{
				string oldOutput = ToString();
				_d5.A = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal D6_in
		{
			get => _d6.A;
			set
			{
				string oldOutput = ToString();
				_d6.A = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal D7_in
		{
			get => _d7.A;
			set
			{
				string oldOutput = ToString();
				_d7.A = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}

		public VoltageSignal A0_in
		{
			get => _a0.A;
			set
			{
				string oldOutput = ToString();
				_a0.A = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A1_in
		{
			get => _a1.A;
			set
			{
				string oldOutput = ToString();
				_a1.A = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A2_in
		{
			get => _a2.A;
			set
			{
				string oldOutput = ToString();
				_a2.A = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A3_in
		{
			get => _a3.A;
			set
			{
				string oldOutput = ToString();
				_a3.A = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A4_in
		{
			get => _a4.A;
			set
			{
				string oldOutput = ToString();
				_a4.A = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A5_in
		{
			get => _a5.A;
			set
			{
				string oldOutput = ToString();
				_a5.A = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A6_in
		{
			get => _a6.A;
			set
			{
				string oldOutput = ToString();
				_a6.A = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A7_in
		{
			get => _a7.A;
			set
			{
				string oldOutput = ToString();
				_a7.A = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A8_in
		{
			get => _a8.A;
			set
			{
				string oldOutput = ToString();
				_a8.A = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A9_in
		{
			get => _a9.A;
			set
			{
				string oldOutput = ToString();
				_a9.A = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A10_in
		{
			get => _a10.A;
			set
			{
				string oldOutput = ToString();
				_a10.A = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A11_in
		{
			get => _a11.A;
			set
			{
				string oldOutput = ToString();
				_a11.A = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A12_in
		{
			get => _a12.A;
			set
			{
				string oldOutput = ToString();
				_a12.A = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A13_in
		{
			get => _a13.A;
			set
			{
				string oldOutput = ToString();
				_a13.A = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A14_in
		{
			get => _a14.A;
			set
			{
				string oldOutput = ToString();
				_a14.A = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A15_in
		{
			get => _a15.A;
			set
			{
				string oldOutput = ToString();
				_a15.A = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}

		public VoltageSignal Write_in
		{
			get => _write.A;
			set
			{
				string oldOutput = ToString();
				_write.A = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}

		public VoltageSignal D0_sw
		{
			get => _d0.B;
			set
			{
				string oldOutput = ToString();
				_d0.B = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal D1_sw
		{
			get => _d1.B;
			set
			{
				string oldOutput = ToString();
				_d1.B = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal D2_sw
		{
			get => _d2.B;
			set
			{
				string oldOutput = ToString();
				_d2.B = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal D3_sw
		{
			get => _d3.B;
			set
			{
				string oldOutput = ToString();
				_d3.B = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal D4_sw
		{
			get => _d4.B;
			set
			{
				string oldOutput = ToString();
				_d4.B = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal D5_sw
		{
			get => _d5.B;
			set
			{
				string oldOutput = ToString();
				_d5.B = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal D6_sw
		{
			get => _d6.B;
			set
			{
				string oldOutput = ToString();
				_d6.B = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal D7_sw
		{
			get => _d7.B;
			set
			{
				string oldOutput = ToString();
				_d7.B = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}

		public VoltageSignal A0_sw
		{
			get => _a0.B;
			set
			{
				string oldOutput = ToString();
				_a0.B = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A1_sw
		{
			get => _a1.B;
			set
			{
				string oldOutput = ToString();
				_a1.B = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A2_sw
		{
			get => _a2.B;
			set
			{
				string oldOutput = ToString();
				_a2.B = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A3_sw
		{
			get => _a3.B;
			set
			{
				string oldOutput = ToString();
				_a3.B = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A4_sw
		{
			get => _a4.B;
			set
			{
				string oldOutput = ToString();
				_a4.B = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A5_sw
		{
			get => _a5.B;
			set
			{
				string oldOutput = ToString();
				_a5.B = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A6_sw
		{
			get => _a6.B;
			set
			{
				string oldOutput = ToString();
				_a6.B = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A7_sw
		{
			get => _a7.B;
			set
			{
				string oldOutput = ToString();
				_a7.B = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A8_sw
		{
			get => _a8.B;
			set
			{
				string oldOutput = ToString();
				_a8.B = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A9_sw
		{
			get => _a9.B;
			set
			{
				string oldOutput = ToString();
				_a9.B = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A10_sw
		{
			get => _a10.B;
			set
			{
				string oldOutput = ToString();
				_a10.B = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A11_sw
		{
			get => _a11.B;
			set
			{
				string oldOutput = ToString();
				_a11.B = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A12_sw
		{
			get => _a12.B;
			set
			{
				string oldOutput = ToString();
				_a12.B = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A13_sw
		{
			get => _a13.B;
			set
			{
				string oldOutput = ToString();
				_a13.B = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A14_sw
		{
			get => _a14.B;
			set
			{
				string oldOutput = ToString();
				_a14.B = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}
		public VoltageSignal A15_sw
		{
			get => _a15.B;
			set
			{
				string oldOutput = ToString();
				_a15.B = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}

		public VoltageSignal Write_sw
		{
			get => _write.B;
			set
			{
				string oldOutput = ToString();
				_write.B = value;
				if (ToString() != oldOutput) OutputChanged?.Invoke(this);
			}
		}

		public VoltageSignal D0 { get => _d0.O; }
		public VoltageSignal D1 { get => _d1.O; }
		public VoltageSignal D2 { get => _d2.O; }
		public VoltageSignal D3 { get => _d3.O; }
		public VoltageSignal D4 { get => _d4.O; }
		public VoltageSignal D5 { get => _d5.O; }
		public VoltageSignal D6 { get => _d6.O; }
		public VoltageSignal D7 { get => _d7.O; }

		public VoltageSignal A0 { get => _a0.O; }
		public VoltageSignal A1 { get => _a1.O; }
		public VoltageSignal A2 { get => _a2.O; }
		public VoltageSignal A3 { get => _a3.O; }
		public VoltageSignal A4 { get => _a4.O; }
		public VoltageSignal A5 { get => _a5.O; }
		public VoltageSignal A6 { get => _a6.O; }
		public VoltageSignal A7 { get => _a7.O; }
		public VoltageSignal A8 { get => _a8.O; }
		public VoltageSignal A9 { get => _a9.O; }
		public VoltageSignal A10 { get => _a10.O; }
		public VoltageSignal A11 { get => _a11.O; }
		public VoltageSignal A12 { get => _a12.O; }
		public VoltageSignal A13 { get => _a13.O; }
		public VoltageSignal A14 { get => _a14.O; }
		public VoltageSignal A15 { get => _a15.O; }

		public VoltageSignal Write { get => _write.O; }

		public VoltageSignal B0 { get; set; }
		public VoltageSignal B1 { get; set; }
		public VoltageSignal B2 { get; set; }
		public VoltageSignal B3 { get; set; }
		public VoltageSignal B4 { get; set; }
		public VoltageSignal B5 { get; set; }
		public VoltageSignal B6 { get; set; }
		public VoltageSignal B7 { get; set; }

		public string Bulbs => $"{bulb(B7)}{bulb(B6)}{bulb(B5)}{bulb(B4)}{bulb(B3)}{bulb(B2)}{bulb(B1)}{bulb(B0)}";
		private char bulb(VoltageSignal voltage) => voltage == VoltageSignal.HIGH ? 'Ȳ' : '.';

		#region IOutput Members

		public void AddOutputHandler(Action<object> handler) => OutputChanged += handler;

		#endregion

		#region Object Override Methods
		// this is a case where string.Format is clearer than an interpolated string
		public override string ToString() => string.Format(
			//      W   A15 ---------------------------------------------- A0   D7 ------------------------ D0
					"{0}:{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}:{17}{18}{19}{20}{21}{22}{23}{24}",
					_write.O == VoltageSignal.HIGH ? 1 : 0,
					_a15.O == VoltageSignal.HIGH ? 1 : 0,
					_a14.O == VoltageSignal.HIGH ? 1 : 0,
					_a13.O == VoltageSignal.HIGH ? 1 : 0,
					_a12.O == VoltageSignal.HIGH ? 1 : 0,
					_a11.O == VoltageSignal.HIGH ? 1 : 0,
					_a10.O == VoltageSignal.HIGH ? 1 : 0,
					_a9.O == VoltageSignal.HIGH ? 1 : 0,
					_a8.O == VoltageSignal.HIGH ? 1 : 0,
					_a7.O == VoltageSignal.HIGH ? 1 : 0,
					_a6.O == VoltageSignal.HIGH ? 1 : 0,
					_a5.O == VoltageSignal.HIGH ? 1 : 0,
					_a4.O == VoltageSignal.HIGH ? 1 : 0,
					_a3.O == VoltageSignal.HIGH ? 1 : 0,
					_a2.O == VoltageSignal.HIGH ? 1 : 0,
					_a1.O == VoltageSignal.HIGH ? 1 : 0,
					_a0.O == VoltageSignal.HIGH ? 1 : 0,
					_d7.O == VoltageSignal.HIGH ? 1 : 0,
					_d6.O == VoltageSignal.HIGH ? 1 : 0,
					_d5.O == VoltageSignal.HIGH ? 1 : 0,
					_d4.O == VoltageSignal.HIGH ? 1 : 0,
					_d3.O == VoltageSignal.HIGH ? 1 : 0,
					_d2.O == VoltageSignal.HIGH ? 1 : 0,
					_d1.O == VoltageSignal.HIGH ? 1 : 0,
					_d0.O == VoltageSignal.HIGH ? 1 : 0);
		#endregion
	}
}
