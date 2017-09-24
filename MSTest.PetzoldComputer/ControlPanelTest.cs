using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetzoldComputer;
using System.Diagnostics;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class ControlPanelTest
	{
		[TestMethod]
		public void ControlPanel()
		{
			var panel = new ControlPanel("test");
			panel.V.V = VoltageSignal.HIGH;

			WireUpEventHandler(panel);

			panel.D0_in.V = panel.D2_in.V = VoltageSignal.HIGH;
			panel.A10_in.V = panel.A12_in.V = VoltageSignal.HIGH;
			panel.Write_in.V = VoltageSignal.HIGH;
			panel.Write_in.V = VoltageSignal.LOW;

			panel.Takeover.V = VoltageSignal.HIGH;
			panel.D1_sw.V = panel.D3_sw.V = VoltageSignal.HIGH;
			panel.A11_sw.V = panel.A13_sw.V = VoltageSignal.HIGH;
			panel.Write_sw.V = VoltageSignal.HIGH;

			panel.Takeover.V = VoltageSignal.LOW;
			panel.Takeover.V = VoltageSignal.HIGH;
			panel.Takeover.V = VoltageSignal.LOW;
		}

		private static void WireUpEventHandler(ControlPanel panel)
		{
			panel.D0.Changed += EventHandler;
			panel.D1.Changed += EventHandler;
			panel.D2.Changed += EventHandler;
			panel.D3.Changed += EventHandler;
			panel.D4.Changed += EventHandler;
			panel.D5.Changed += EventHandler;
			panel.D6.Changed += EventHandler;
			panel.D7.Changed += EventHandler;

			panel.A0.Changed += EventHandler;
			panel.A1.Changed += EventHandler;
			panel.A2.Changed += EventHandler;
			panel.A3.Changed += EventHandler;
			panel.A4.Changed += EventHandler;
			panel.A5.Changed += EventHandler;
			panel.A6.Changed += EventHandler;
			panel.A7.Changed += EventHandler;
			panel.A8.Changed += EventHandler;
			panel.A9.Changed += EventHandler;
			panel.A10.Changed += EventHandler;
			panel.A11.Changed += EventHandler;
			panel.A12.Changed += EventHandler;
			panel.A13.Changed += EventHandler;
			panel.A14.Changed += EventHandler;
			panel.A15.Changed += EventHandler;

			panel.Write.Changed += EventHandler;
		}

		private static void EventHandler(ConnectionPoint cp)
		{
			Trace.TraceInformation("event fired");
		}
	}
}
