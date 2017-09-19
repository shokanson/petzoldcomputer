using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetzoldComputer;
using System.Diagnostics;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class ControlPanelTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			IControlPanel panel = new ControlPanel { Voltage = VoltageSignal.HIGH };

			((IOutput)panel).AddOutputHandler(_ => Trace.TraceInformation("event fired"));

			panel.D0_in = panel.D2_in = VoltageSignal.HIGH;
			panel.A10_in = panel.A12_in = VoltageSignal.HIGH;
			panel.Write_in = VoltageSignal.HIGH;
			panel.Write_in = VoltageSignal.LOW;

			panel.Takeover = VoltageSignal.HIGH;
			panel.D1_sw = panel.D3_sw = VoltageSignal.HIGH;
			panel.A11_sw = panel.A13_sw = VoltageSignal.HIGH;
			panel.Write_sw = VoltageSignal.HIGH;

			panel.Takeover = VoltageSignal.LOW;
			panel.Takeover = VoltageSignal.HIGH;
			panel.Takeover = VoltageSignal.LOW;
		}
	}
}
