using Microsoft.VisualStudio.TestTools.UnitTesting;

using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class InvertedRelayTest
	{
		[TestMethod]
		public void TestConstructor()
		{
			IRelay invRelay = new InvertedRelay();

			Assert.AreEqual(VoltageSignal.LOW, invRelay.Input, "Constructor: Input");
			Assert.AreEqual(VoltageSignal.LOW, invRelay.Voltage, "Constructor: Voltage");
			Assert.AreEqual(VoltageSignal.LOW, invRelay.Output, "Constructor: Output");
			Assert.AreEqual("LOW", invRelay.ToString(), "Constructor: ToString()");
		}

		[TestMethod]
		public void TestOutput()
		{
			IRelay invRelay = new InvertedRelay
			{

				// if Input is LOW, Output should follow Voltage
				Input = VoltageSignal.LOW,
				Voltage = VoltageSignal.HIGH
			};
			Assert.AreEqual(VoltageSignal.HIGH, invRelay.Output, "Input LOW, Voltage HIGH, Output HIGH");
			invRelay.Voltage = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, invRelay.Output, "Input LOW, Voltage LOW, Output LOW");

			// if Input is HIGH, Output should be LOW
			invRelay.Input = VoltageSignal.HIGH;
			invRelay.Voltage = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, invRelay.Output, "Input HIGH, Voltage HIGH, Output LOW");
			invRelay.Voltage = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, invRelay.Output, "Input HIGH, Voltage LOW, Output LOW");
		}

		[TestMethod]
		public void TestEvents()
		{
			IRelay invRelay = new InvertedRelay();

			// if Output is not going to change, an OutputEvent should not be fired
			TestEventsHelper helper = new TestEventsHelper((IOutput)invRelay);

			invRelay.Voltage = VoltageSignal.HIGH;	// I: L; V: goes H
			Assert.AreEqual("fired", helper.EventStatus, "Events: Input LOW, Voltage goes HIGH, event");
			helper.ResetStatus();
			invRelay.Voltage = VoltageSignal.HIGH;	// I: L; V: stays H
			Assert.AreEqual("not fired", helper.EventStatus, "Events: Input LOW, Voltage stays HIGH, no event");
			invRelay.Voltage = VoltageSignal.LOW;	// I: L; V: goes L
			Assert.AreEqual("fired", helper.EventStatus, "Events: Input LOW, Voltage goes LOW, event");
			helper.ResetStatus();
			invRelay.Voltage = VoltageSignal.LOW;	// I: L; V: stays L
			Assert.AreEqual("not fired", helper.EventStatus, "Events: Input LOW, Voltage stays LOW, no event");

			invRelay.Input = VoltageSignal.HIGH;	// V: L; I: goes H
			Assert.AreEqual("not fired", helper.EventStatus, "Events: Voltage LOW, Input goes HIGH, no event");
			invRelay.Input = VoltageSignal.HIGH;	// V: L; I: stays H
			Assert.AreEqual("not fired", helper.EventStatus, "Events: Voltage LOW, Input stays HIGH, no event");
			invRelay.Input = VoltageSignal.LOW;	// V: L; I: goes L
			Assert.AreEqual("not fired", helper.EventStatus, "Events: Voltage LOW, Input goes LOW, no event");
			invRelay.Input = VoltageSignal.LOW;	// V: L; I: stays L
			Assert.AreEqual("not fired", helper.EventStatus, "Events: Voltage LOW, Input stays LOW, no event");

			invRelay.Input = VoltageSignal.HIGH;
			helper.ResetStatus();
			invRelay.Voltage = VoltageSignal.HIGH;	// I: H; V: goes H
			Assert.AreEqual("not fired", helper.EventStatus, "Events: Input HIGH, Voltage goes HIGH, no event");
			invRelay.Voltage = VoltageSignal.HIGH;	// I: H; V: stays H
			Assert.AreEqual("not fired", helper.EventStatus, "Events: Input HIGH, Voltage stays HIGH, no event");
			invRelay.Voltage = VoltageSignal.LOW;	// I: H; V: goes L
			Assert.AreEqual("not fired", helper.EventStatus, "Events: Input HIGH, Voltage goes LOW, no event");
			invRelay.Voltage = VoltageSignal.LOW;	// I: H; V: stays L
			Assert.AreEqual("not fired", helper.EventStatus, "Events: Input HIGH, Voltage stays LOW, no event");

			invRelay.Voltage = VoltageSignal.HIGH;
			invRelay.Input = VoltageSignal.LOW;
			helper.ResetStatus();
			invRelay.Input = VoltageSignal.HIGH;	// V: H; I: goes H
			Assert.AreEqual("fired", helper.EventStatus, "Events: Voltage HIGH, Input goes HIGH, event");
			helper.ResetStatus();
			invRelay.Input = VoltageSignal.HIGH;	// V: H; I: stays H
			Assert.AreEqual("not fired", helper.EventStatus, "Events: Voltage HIGH, Input stays HIGH, no event");
			invRelay.Input = VoltageSignal.LOW;	// V: H; I: goes L
			Assert.AreEqual("fired", helper.EventStatus, "Events: Voltage HIGH, Input goes LOW, event");
			helper.ResetStatus();
			invRelay.Input = VoltageSignal.LOW;	// V: H; I: stays L
			Assert.AreEqual("not fired", helper.EventStatus, "Events: Voltage HIGH, Input stays LOW, no event");
		}
	}
}
