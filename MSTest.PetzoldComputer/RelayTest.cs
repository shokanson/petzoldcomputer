using Microsoft.VisualStudio.TestTools.UnitTesting;

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
				Input = VoltageSignal.LOW,
				Voltage = VoltageSignal.HIGH
			};
			// if Input is LOW, Output should be LOW
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

			relay.Voltage = VoltageSignal.HIGH; // I: L; V: goes H
			Assert.IsFalse(helper.EventFired, "Events: Input LOW, Voltage goes HIGH, no event");
			relay.Voltage = VoltageSignal.HIGH; // I: L; V: stays H
			Assert.IsFalse(helper.EventFired, "Events: Input LOW, Voltage stays HIGH, no event");
			relay.Voltage = VoltageSignal.LOW;  // I: L; V: goes L
			Assert.IsFalse(helper.EventFired, "Events: Input LOW, Voltage goes LOW, no event");
			relay.Voltage = VoltageSignal.LOW;  // I: L; V: stays L
			Assert.IsFalse(helper.EventFired, "Events: Input LOW, Voltage stays LOW, no event");

			relay.Input = VoltageSignal.HIGH;   // V: L; I: goes H
			Assert.IsFalse(helper.EventFired, "Events: Voltage LOW, Input goes HIGH, no event");
			relay.Input = VoltageSignal.HIGH;   // V: L; I: stays H
			Assert.IsFalse(helper.EventFired, "Events: Voltage LOW, Input stays HIGH, no event");
			relay.Input = VoltageSignal.LOW; // V: L; I: goes L
			Assert.IsFalse(helper.EventFired, "Events: Voltage LOW, Input goes LOW, no event");
			relay.Input = VoltageSignal.LOW; // V: L; I: stays L
			Assert.IsFalse(helper.EventFired, "Events: Voltage LOW, Input stays LOW, no event");

			relay.Input = VoltageSignal.HIGH;
			helper.ResetStatus();
			relay.Voltage = VoltageSignal.HIGH; // I: H; V: goes H
			Assert.IsTrue(helper.EventFired, "Events: Input HIGH, Voltage goes HIGH, event");
			helper.ResetStatus();
			relay.Voltage = VoltageSignal.HIGH; // I: H; V: stays H
			Assert.IsFalse(helper.EventFired, "Events: Input HIGH, Voltage stays HIGH, no event");
			relay.Voltage = VoltageSignal.LOW;  // I: H; V: goes L
			Assert.IsTrue(helper.EventFired, "Events: Input HIGH, Voltage goes LOW, event");
			helper.ResetStatus();
			relay.Voltage = VoltageSignal.LOW;  // I: H; V: stays L
			Assert.IsFalse(helper.EventFired, "Events: Input HIGH, Voltage stays LOW, no event");

			relay.Voltage = VoltageSignal.HIGH;
			relay.Input = VoltageSignal.LOW;
			helper.ResetStatus();
			relay.Input = VoltageSignal.HIGH;   // V: H; I: goes H
			Assert.IsTrue(helper.EventFired, "Events: Voltage HIGH, Input goes HIGH, event");
			helper.ResetStatus();
			relay.Input = VoltageSignal.HIGH;   // V: H; I: stays H
			Assert.IsFalse(helper.EventFired, "Events: Voltage HIGH, Input stays HIGH, no event");
			relay.Input = VoltageSignal.LOW; // V: H; I: goes L
			Assert.IsTrue(helper.EventFired, "Events: Voltage HIGH, Input goes LOW, event");
			helper.ResetStatus();
			relay.Input = VoltageSignal.LOW; // V: H; I: stays L
			Assert.IsFalse(helper.EventFired, "Events: Voltage HIGH, Input stays LOW, no event");
		}

		[TestMethod]
		public void Relay_2_Constructor_NormallyOpenSwitch()
		{
			// arrage, act
			var relay = new Relay_2();

			// assert
			Assert.AreEqual(VoltageSignal.LOW, relay.Voltage.Voltage, "Coil Voltage");
			Assert.AreEqual(VoltageSignal.LOW, relay.Input.Voltage, "Switch input");
			Assert.AreEqual(VoltageSignal.LOW, relay.Output.Voltage, "Switch Output");
		}

		[TestMethod]
		public void Relay_2_Constructor_NormallyClosedSwitch()
		{
			// arrage, act
			var relay = new Relay_2(true);

			// assert
			Assert.AreEqual(VoltageSignal.LOW, relay.Voltage.Voltage, "Coil Voltage");
			Assert.AreEqual(VoltageSignal.LOW, relay.Input.Voltage, "Switch input");
			Assert.AreEqual(VoltageSignal.LOW, relay.Output.Voltage, "Switch Output");
		}

		[DataTestMethod]
		[DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.HIGH)]
		public void Relay_2_NormallyOpenSwitch(VoltageSignal voltage, VoltageSignal input, VoltageSignal expected)
		{
			// arrage
			var relay = new Relay_2();

			// act
			relay.Voltage.Voltage = voltage;
			relay.Input.Voltage = input;

			// assert
			Assert.AreEqual(expected, relay.Output.Voltage, $"V: {voltage}; I:{input}");
		}

		[DataTestMethod]
		[DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.HIGH)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW)]
		public void Relay_2_NormallyClosedSwitch(VoltageSignal voltage, VoltageSignal input, VoltageSignal expected)
		{
			// arrage
			var relay = new Relay_2(true);

			// act
			relay.Voltage.Voltage = voltage;
			relay.Input.Voltage = input;

			// assert
			Assert.AreEqual(expected, relay.Output.Voltage, $"V: {voltage}; I:{input}");
		}

		[TestMethod]
		public void Relay_2_NormallyOpenSwitch_Events()
		{
			// arrage
			var relay = new Relay_2();

			// act,assert
			bool fired = false;
			relay.Output.VoltageChanged += _ => fired = true;

			// w/ voltage L, output shouldn't change
			relay.Input.Voltage = VoltageSignal.HIGH;
			Assert.IsFalse(fired);
			relay.Input.Voltage = VoltageSignal.LOW;
			Assert.IsFalse(fired);

			// w/ input L, and voltage changed to H, output shouldn't change
			relay.Voltage.Voltage = VoltageSignal.HIGH;
			Assert.IsFalse(fired);

			// w/ voltage H, and input changed to H, output should change (to H)
			relay.Input.Voltage = VoltageSignal.HIGH;
			Assert.IsTrue(fired);
			fired = false;

			// w/ voltage H, input H, and input set to H again, output shouldn't change
			relay.Input.Voltage = VoltageSignal.HIGH;
			Assert.IsFalse(fired);

			// w/ voltage H, input H, and input set to L, output should change (to L)
			relay.Input.Voltage = VoltageSignal.LOW;
			Assert.IsTrue(fired);
			fired = false;

			// w/ voltage H, input L, and voltage set to L, output shouldn't change
			relay.Voltage.Voltage = VoltageSignal.LOW;
			Assert.IsFalse(fired);
		}

		[TestMethod]
		public void Relay_2_NormallyClosedSwitch_Events()
		{
			// arrage
			var relay = new Relay_2(true);

			// act,assert
			bool fired = false;
			relay.Output.VoltageChanged += _ => fired = true;

			// w/ voltage L, output shouldn't change
			relay.Input.Voltage = VoltageSignal.HIGH;
			Assert.IsFalse(fired);
			relay.Input.Voltage = VoltageSignal.LOW;
			Assert.IsFalse(fired);

			// w/ input L, and voltage changed to H, output should change (to H)
			relay.Voltage.Voltage = VoltageSignal.HIGH;
			Assert.IsTrue(fired);
			fired = false;

			// w/ voltage H, and input changed to H, output should change (to H)
			relay.Input.Voltage = VoltageSignal.HIGH;
			Assert.IsTrue(fired);
			fired = false;

			// w/ voltage H, input H, and input set to H again, output shouldn't change
			relay.Input.Voltage = VoltageSignal.HIGH;
			Assert.IsFalse(fired);

			// w/ voltage H, input L, and voltage set to L, output should change (to L)
			relay.Input.Voltage = VoltageSignal.LOW;
			relay.Voltage.Voltage = VoltageSignal.LOW;
			Assert.IsTrue(fired);
		}
	}
}