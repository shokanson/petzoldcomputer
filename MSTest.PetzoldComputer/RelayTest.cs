﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class RelayTest
	{
		[TestMethod]
		public void TestConstructor()
		{
			IRelay relay = new Relay();

			Assert.AreEqual(VoltageSignal.LOW, relay.Input, "Constructor: Input");
			Assert.AreEqual(VoltageSignal.LOW, relay.Voltage, "Constructor: Voltage");
			Assert.AreEqual(VoltageSignal.LOW, relay.Output, "Constructor: Output");
			Assert.AreEqual("LOW", relay.ToString(), "Constructor: ToString()");
		}

		[TestMethod]
		public void TestOutput()
		{
			IRelay relay = new Relay
			{
				// if Input is LOW, Output should be LOW
				Input = VoltageSignal.LOW,
				Voltage = VoltageSignal.HIGH
			};
			Assert.AreEqual(VoltageSignal.LOW, relay.Output, "Input LOW, Voltage HIGH, Output LOW");
			relay.Voltage = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, relay.Output, "Input LOW, Voltage LOW, Output LOW");

			// if Input is HIGH, Output should follow Voltage
			relay.Input = VoltageSignal.HIGH;
			relay.Voltage = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.HIGH, relay.Output, "Input HIGH, Voltage HIGH, Output HIGH");
			relay.Voltage = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, relay.Output, "Input HIGH, Voltage LOW, Output LOW");
		}

		[TestMethod]
		public void TestEvents()
		{
			IRelay relay = new Relay();

			// if Output is not going to change, an OutputEvent should not be fired
			TestEventsHelper helper = new TestEventsHelper((IOutput)relay);

			relay.Voltage = VoltageSignal.HIGH;	// I: L; V: goes H
			Assert.IsFalse(helper.EventFired, "Events: Input LOW, Voltage goes HIGH, no event");
			relay.Voltage = VoltageSignal.HIGH;	// I: L; V: stays H
			Assert.IsFalse(helper.EventFired, "Events: Input LOW, Voltage stays HIGH, no event");
			relay.Voltage = VoltageSignal.LOW;	// I: L; V: goes L
			Assert.IsFalse(helper.EventFired, "Events: Input LOW, Voltage goes LOW, no event");
			relay.Voltage = VoltageSignal.LOW;	// I: L; V: stays L
			Assert.IsFalse(helper.EventFired, "Events: Input LOW, Voltage stays LOW, no event");

			relay.Input = VoltageSignal.HIGH;	// V: L; I: goes H
			Assert.IsFalse(helper.EventFired, "Events: Voltage LOW, Input goes HIGH, no event");
			relay.Input = VoltageSignal.HIGH;	// V: L; I: stays H
			Assert.IsFalse(helper.EventFired, "Events: Voltage LOW, Input stays HIGH, no event");
			relay.Input = VoltageSignal.LOW;	// V: L; I: goes L
			Assert.IsFalse(helper.EventFired, "Events: Voltage LOW, Input goes LOW, no event");
			relay.Input = VoltageSignal.LOW;	// V: L; I: stays L
			Assert.IsFalse(helper.EventFired, "Events: Voltage LOW, Input stays LOW, no event");

			relay.Input = VoltageSignal.HIGH;
			helper.ResetStatus();
			relay.Voltage = VoltageSignal.HIGH;	// I: H; V: goes H
			Assert.IsTrue(helper.EventFired, "Events: Input HIGH, Voltage goes HIGH, event");
			helper.ResetStatus();
			relay.Voltage = VoltageSignal.HIGH;	// I: H; V: stays H
			Assert.IsFalse(helper.EventFired, "Events: Input HIGH, Voltage stays HIGH, no event");
			relay.Voltage = VoltageSignal.LOW;	// I: H; V: goes L
			Assert.IsTrue(helper.EventFired, "Events: Input HIGH, Voltage goes LOW, event");
			helper.ResetStatus();
			relay.Voltage = VoltageSignal.LOW;	// I: H; V: stays L
			Assert.IsFalse(helper.EventFired, "Events: Input HIGH, Voltage stays LOW, no event");

			relay.Voltage = VoltageSignal.HIGH;
			relay.Input = VoltageSignal.LOW;
			helper.ResetStatus();
			relay.Input = VoltageSignal.HIGH;	// V: H; I: goes H
			Assert.IsTrue(helper.EventFired, "Events: Voltage HIGH, Input goes HIGH, event");
			helper.ResetStatus();
			relay.Input = VoltageSignal.HIGH;	// V: H; I: stays H
			Assert.IsFalse(helper.EventFired, "Events: Voltage HIGH, Input stays HIGH, no event");
			relay.Input = VoltageSignal.LOW;	// V: H; I: goes L
			Assert.IsTrue(helper.EventFired, "Events: Voltage HIGH, Input goes LOW, event");
			helper.ResetStatus();
			relay.Input = VoltageSignal.LOW;	// V: H; I: stays L
			Assert.IsFalse(helper.EventFired, "Events: Voltage HIGH, Input stays LOW, no event");
		}
	}
}
