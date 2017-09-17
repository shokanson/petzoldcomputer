using Microsoft.VisualStudio.TestTools.UnitTesting;

using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class NOTTest
	{
		[TestMethod]
		public void TestConstructor()
		{
			INot not = new NOT();

			Assert.AreEqual(not.Input, VoltageSignal.LOW, "Constructor: Input");
			Assert.AreEqual(not.Voltage, VoltageSignal.LOW, "Constructor: Voltage");
			Assert.AreEqual(not.Output, VoltageSignal.LOW, "Constructor: Output");
			Assert.AreEqual(not.ToString(), "LOW", "Constructor: ToString()");
		}

		[TestMethod]
		public void TestOutput()
		{
			INot not = new NOT
			{
				// if Input is LOW, Output should follow Voltage
				Input = VoltageSignal.LOW,
				Voltage = VoltageSignal.HIGH
			};
			Assert.AreEqual(not.Output, VoltageSignal.HIGH, "Input LOW, Voltage HIGH, Output HIGH");
			not.Voltage = VoltageSignal.LOW;
			Assert.AreEqual(not.Output, VoltageSignal.LOW, "Input LOW, Voltage LOW, Output LOW");

			// if Input is HIGH, Output should be LOW
			not.Input = VoltageSignal.HIGH;
			not.Voltage = VoltageSignal.HIGH;
			Assert.AreEqual(not.Output, VoltageSignal.LOW, "Input HIGH, Voltage HIGH, Output LOW");
			not.Voltage = VoltageSignal.LOW;
			Assert.AreEqual(not.Output, VoltageSignal.LOW, "Input HIGH, Voltage LOW, Output LOW");
		}

		[TestMethod]
		public void TestEvents()
		{
			INot not = new NOT();

			// if Output is not going to change, an OutputEvent should not be fired
			TestEventsHelper helper = new TestEventsHelper((IOutput)not);

			not.Voltage = VoltageSignal.HIGH;	// I: L; V: goes H
			Assert.AreEqual(helper.EventStatus, "fired", "Events: Input LOW, Voltage goes HIGH, event");
			helper.ResetStatus();
			not.Voltage = VoltageSignal.HIGH;	// I: L; V: stays H
			Assert.AreEqual(helper.EventStatus, "not fired", "Events: Input LOW, Voltage stays HIGH, no event");
			not.Voltage = VoltageSignal.LOW;	// I: L; V: goes L
			Assert.AreEqual(helper.EventStatus, "fired", "Events: Input LOW, Voltage goes LOW, event");
			helper.ResetStatus();
			not.Voltage = VoltageSignal.LOW;	// I: L; V: stays L
			Assert.AreEqual(helper.EventStatus, "not fired", "Events: Input LOW, Voltage stays LOW, no event");

			not.Input = VoltageSignal.HIGH;	// V: L; I: goes H
			Assert.AreEqual(helper.EventStatus, "not fired", "Events: Voltage LOW, Input goes HIGH, no event");
			not.Input = VoltageSignal.HIGH;	// V: L; I: stays H
			Assert.AreEqual(helper.EventStatus, "not fired", "Events: Voltage LOW, Input stays HIGH, no event");
			not.Input = VoltageSignal.LOW;	// V: L; I: goes L
			Assert.AreEqual(helper.EventStatus, "not fired", "Events: Voltage LOW, Input goes LOW, no event");
			not.Input = VoltageSignal.LOW;	// V: L; I: stays L
			Assert.AreEqual(helper.EventStatus, "not fired", "Events: Voltage LOW, Input stays LOW, no event");

			not.Input = VoltageSignal.HIGH;
			helper.ResetStatus();
			not.Voltage = VoltageSignal.HIGH;	// I: H; V: goes H
			Assert.AreEqual(helper.EventStatus, "not fired", "Events: Input HIGH, Voltage goes HIGH, no event");
			not.Voltage = VoltageSignal.HIGH;	// I: H; V: stays H
			Assert.AreEqual(helper.EventStatus, "not fired", "Events: Input HIGH, Voltage stays HIGH, no event");
			not.Voltage = VoltageSignal.LOW;	// I: H; V: goes L
			Assert.AreEqual(helper.EventStatus, "not fired", "Events: Input HIGH, Voltage goes LOW, no event");
			not.Voltage = VoltageSignal.LOW;	// I: H; V: stays L
			Assert.AreEqual(helper.EventStatus, "not fired", "Events: Input HIGH, Voltage stays LOW, no event");

			not.Voltage = VoltageSignal.HIGH;
			not.Input = VoltageSignal.LOW;
			helper.ResetStatus();
			not.Input = VoltageSignal.HIGH;	// V: H; I: goes H
			Assert.AreEqual(helper.EventStatus, "fired", "Events: Voltage HIGH, Input goes HIGH, event");
			helper.ResetStatus();
			not.Input = VoltageSignal.HIGH;	// V: H; I: stays H
			Assert.AreEqual(helper.EventStatus, "not fired", "Events: Voltage HIGH, Input stays HIGH, no event");
			not.Input = VoltageSignal.LOW;	// V: H; I: goes L
			Assert.AreEqual(helper.EventStatus, "fired", "Events: Voltage HIGH, Input goes LOW, event");
			helper.ResetStatus();
			not.Input = VoltageSignal.LOW;	// V: H; I: stays L
			Assert.AreEqual(helper.EventStatus, "not fired", "Events: Voltage HIGH, Input stays LOW, no event");
		}
	}
}
