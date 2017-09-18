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

			Assert.AreEqual(VoltageSignal.LOW, not.Input, "Constructor: Input");
			Assert.AreEqual(VoltageSignal.LOW, not.Voltage, "Constructor: Voltage");
			Assert.AreEqual(VoltageSignal.LOW, not.Output, "Constructor: Output");
			Assert.AreEqual("LOW", not.ToString(), "Constructor: ToString()");
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
			Assert.AreEqual(VoltageSignal.HIGH, not.Output, "Input LOW, Voltage HIGH, Output HIGH");
			not.Voltage = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, not.Output, "Input LOW, Voltage LOW, Output LOW");

			// if Input is HIGH, Output should be LOW
			not.Input = VoltageSignal.HIGH;
			not.Voltage = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, not.Output, "Input HIGH, Voltage HIGH, Output LOW");
			not.Voltage = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, not.Output, "Input HIGH, Voltage LOW, Output LOW");
		}

		[TestMethod]
		public void TestEvents()
		{
			INot not = new NOT();

			// if Output is not going to change, an OutputEvent should not be fired
			TestEventsHelper helper = new TestEventsHelper((IOutput)not);

			not.Voltage = VoltageSignal.HIGH;	// I: L; V: goes H
			Assert.AreEqual("fired", helper.EventStatus, "Events: Input LOW, Voltage goes HIGH, event");
			helper.ResetStatus();
			not.Voltage = VoltageSignal.HIGH;	// I: L; V: stays H
			Assert.AreEqual("not fired", helper.EventStatus, "Events: Input LOW, Voltage stays HIGH, no event");
			not.Voltage = VoltageSignal.LOW;	// I: L; V: goes L
			Assert.AreEqual("fired", helper.EventStatus, "Events: Input LOW, Voltage goes LOW, event");
			helper.ResetStatus();
			not.Voltage = VoltageSignal.LOW;	// I: L; V: stays L
			Assert.AreEqual("not fired", helper.EventStatus, "Events: Input LOW, Voltage stays LOW, no event");

			not.Input = VoltageSignal.HIGH;	// V: L; I: goes H
			Assert.AreEqual("not fired", helper.EventStatus, "Events: Voltage LOW, Input goes HIGH, no event");
			not.Input = VoltageSignal.HIGH;	// V: L; I: stays H
			Assert.AreEqual("not fired", helper.EventStatus, "Events: Voltage LOW, Input stays HIGH, no event");
			not.Input = VoltageSignal.LOW;	// V: L; I: goes L
			Assert.AreEqual("not fired", helper.EventStatus, "Events: Voltage LOW, Input goes LOW, no event");
			not.Input = VoltageSignal.LOW;	// V: L; I: stays L
			Assert.AreEqual("not fired", helper.EventStatus, "Events: Voltage LOW, Input stays LOW, no event");

			not.Input = VoltageSignal.HIGH;
			helper.ResetStatus();
			not.Voltage = VoltageSignal.HIGH;	// I: H; V: goes H
			Assert.AreEqual("not fired", helper.EventStatus, "Events: Input HIGH, Voltage goes HIGH, no event");
			not.Voltage = VoltageSignal.HIGH;	// I: H; V: stays H
			Assert.AreEqual("not fired", helper.EventStatus, "Events: Input HIGH, Voltage stays HIGH, no event");
			not.Voltage = VoltageSignal.LOW;	// I: H; V: goes L
			Assert.AreEqual("not fired", helper.EventStatus, "Events: Input HIGH, Voltage goes LOW, no event");
			not.Voltage = VoltageSignal.LOW;	// I: H; V: stays L
			Assert.AreEqual("not fired", helper.EventStatus, "Events: Input HIGH, Voltage stays LOW, no event");

			not.Voltage = VoltageSignal.HIGH;
			not.Input = VoltageSignal.LOW;
			helper.ResetStatus();
			not.Input = VoltageSignal.HIGH;	// V: H; I: goes H
			Assert.AreEqual("fired", helper.EventStatus, "Events: Voltage HIGH, Input goes HIGH, event");
			helper.ResetStatus();
			not.Input = VoltageSignal.HIGH;	// V: H; I: stays H
			Assert.AreEqual("not fired", helper.EventStatus, "Events: Voltage HIGH, Input stays HIGH, no event");
			not.Input = VoltageSignal.LOW;	// V: H; I: goes L
			Assert.AreEqual("fired", helper.EventStatus, "Events: Voltage HIGH, Input goes LOW, event");
			helper.ResetStatus();
			not.Input = VoltageSignal.LOW;	// V: H; I: stays L
			Assert.AreEqual("not fired", helper.EventStatus, "Events: Voltage HIGH, Input stays LOW, no event");
		}
	}
}
