using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class RelayTest
	{
		[TestMethod]
		public void Relay_Constructor()
		{
			// arrage, act
			var relay = new Relay("test");

			// assert
			Assert.AreEqual(VoltageSignal.LOW, relay.Voltage.V, "Coil Voltage");
			Assert.AreEqual(VoltageSignal.LOW, relay.Input.V, "Switch input");
			Assert.AreEqual(VoltageSignal.LOW, relay.Output.V, "Switch Output");
			Assert.AreEqual("LOW", relay.ToString());
		}

		[TestMethod]
		public void Relay_Constructor_Inverted()
		{
			// arrage, act
			var relay = new Relay("test", true);

			// assert
			Assert.AreEqual(VoltageSignal.LOW, relay.Voltage.V, "Coil Voltage");
			Assert.AreEqual(VoltageSignal.LOW, relay.Input.V, "Switch input");
			Assert.AreEqual(VoltageSignal.LOW, relay.Output.V, "Switch Output");
			Assert.AreEqual("LOW", relay.ToString());
		}

		[DataTestMethod]
		[DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.HIGH)]
		public void Relay(VoltageSignal voltage, VoltageSignal input, VoltageSignal expected)
		{
			// arrage
			var relay = new Relay("test");

			// act
			relay.Voltage.V = voltage;
			relay.Input.V = input;

			// assert
			Assert.AreEqual(expected, relay.Output.V, $"V: {relay.Voltage}; I:{relay.Input}");
		}

		[DataTestMethod]
		[DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.HIGH)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW)]
		public void Relay_Inverted(VoltageSignal voltage, VoltageSignal input, VoltageSignal expected)
		{
			// arrage
			var relay = new Relay("test", true);

			// act
			relay.Voltage.V = voltage;
			relay.Input.V = input;

			// assert
			Assert.AreEqual(expected, relay.Output.V, $"V: {voltage}; I:{input}");
		}

		[TestMethod]
		public void Relay_Events()
		{
			// arrage
			var relay = new Relay("test");

			// act,assert
			bool fired = false;
			relay.Output.Changed += _ => fired = true;


			relay.Voltage.V = VoltageSignal.HIGH; // I: L; V: goes H
			Assert.IsFalse(fired, "Events: Input LOW, Voltage goes HIGH, no event");
			relay.Voltage.V = VoltageSignal.HIGH; // I: L; V: stays H
			Assert.IsFalse(fired, "Events: Input LOW, Voltage stays HIGH, no event");
			relay.Voltage.V = VoltageSignal.LOW;  // I: L; V: goes L
			Assert.IsFalse(fired, "Events: Input LOW, Voltage goes LOW, no event");
			relay.Voltage.V = VoltageSignal.LOW;  // I: L; V: stays L
			Assert.IsFalse(fired, "Events: Input LOW, Voltage stays LOW, no event");

			relay.Input.V = VoltageSignal.HIGH;   // V: L; I: goes H
			Assert.IsFalse(fired, "Events: Voltage LOW, Input goes HIGH, no event");
			relay.Input.V = VoltageSignal.HIGH;   // V: L; I: stays H
			Assert.IsFalse(false, "Events: Voltage LOW, Input stays HIGH, no event");
			relay.Input.V = VoltageSignal.LOW; // V: L; I: goes L
			Assert.IsFalse(false, "Events: Voltage LOW, Input goes LOW, no event");
			relay.Input.V = VoltageSignal.LOW; // V: L; I: stays L
			Assert.IsFalse(fired, "Events: Voltage LOW, Input stays LOW, no event");

			relay.Input.V = VoltageSignal.HIGH;
			fired = false;
			relay.Voltage.V = VoltageSignal.HIGH; // I: H; V: goes H
			Assert.IsTrue(true, "Events: Input HIGH, Voltage goes HIGH, event");
			fired = false;
			relay.Voltage.V = VoltageSignal.HIGH; // I: H; V: stays H
			Assert.IsFalse(false, "Events: Input HIGH, Voltage stays HIGH, no event");
			relay.Voltage.V = VoltageSignal.LOW;  // I: H; V: goes L
			Assert.IsTrue(fired, "Events: Input HIGH, Voltage goes LOW, event");
			fired = false;
			relay.Voltage.V = VoltageSignal.LOW;  // I: H; V: stays L
			Assert.IsFalse(fired, "Events: Input HIGH, Voltage stays LOW, no event");

			relay.Voltage.V = VoltageSignal.HIGH;
			relay.Input.V = VoltageSignal.LOW;
			fired = false;
			relay.Input.V = VoltageSignal.HIGH;   // V: H; I: goes H
			Assert.IsTrue(fired, "Events: Voltage HIGH, Input goes HIGH, event");
			fired = false;
			relay.Input.V = VoltageSignal.HIGH;   // V: H; I: stays H
			Assert.IsFalse(fired, "Events: Voltage HIGH, Input stays HIGH, no event");
			relay.Input.V = VoltageSignal.LOW; // V: H; I: goes L
			Assert.IsTrue(fired, "Events: Voltage HIGH, Input goes LOW, event");
			fired = false;
			relay.Input.V = VoltageSignal.LOW; // V: H; I: stays L
			Assert.IsFalse(fired, "Events: Voltage HIGH, Input stays LOW, no event");
		}

		[TestMethod]
		public void Relay_Inverted_Events()
		{
			// arrage
			var relay = new Relay("test", true);
			bool fired = false;
			relay.Output.Changed += _ => fired = true;

			// act,assert
			relay.Voltage.V = VoltageSignal.HIGH;   // I: L; V: goes H
			Assert.IsTrue(fired, "Events: Input LOW, Voltage goes HIGH, event");
			fired = false;
			relay.Voltage.V = VoltageSignal.HIGH;   // I: L; V: stays H
			Assert.IsFalse(fired, "Events: Input LOW, Voltage stays HIGH, no event");
			relay.Voltage.V = VoltageSignal.LOW; // I: L; V: goes L
			Assert.IsTrue(fired, "Events: Input LOW, Voltage goes LOW, event");
			fired = false;
			relay.Voltage.V = VoltageSignal.LOW; // I: L; V: stays L
			Assert.IsFalse(fired, "Events: Input LOW, Voltage stays LOW, no event");

			relay.Input.V = VoltageSignal.HIGH;  // V: L; I: goes H
			Assert.IsFalse(fired, "Events: Voltage LOW, Input goes HIGH, no event");
			relay.Input.V = VoltageSignal.HIGH;  // V: L; I: stays H
			Assert.IsFalse(fired, "Events: Voltage LOW, Input stays HIGH, no event");
			relay.Input.V = VoltageSignal.LOW;   // V: L; I: goes L
			Assert.IsFalse(fired, "Events: Voltage LOW, Input goes LOW, no event");
			relay.Input.V = VoltageSignal.LOW;   // V: L; I: stays L
			Assert.IsFalse(fired, "Events: Voltage LOW, Input stays LOW, no event");

			relay.Input.V = VoltageSignal.HIGH;
			fired = false;
			relay.Voltage.V = VoltageSignal.HIGH;   // I: H; V: goes H
			Assert.IsFalse(fired, "Events: Input HIGH, Voltage goes HIGH, no event");
			relay.Voltage.V = VoltageSignal.HIGH;   // I: H; V: stays H
			Assert.IsFalse(fired, "Events: Input HIGH, Voltage stays HIGH, no event");
			relay.Voltage.V = VoltageSignal.LOW; // I: H; V: goes L
			Assert.IsFalse(fired, "Events: Input HIGH, Voltage goes LOW, no event");
			relay.Voltage.V = VoltageSignal.LOW; // I: H; V: stays L
			Assert.IsFalse(fired, "Events: Input HIGH, Voltage stays LOW, no event");

			relay.Voltage.V = VoltageSignal.HIGH;
			relay.Input.V = VoltageSignal.LOW;
			fired = false;
			relay.Input.V = VoltageSignal.HIGH;  // V: H; I: goes H
			Assert.IsTrue(fired, "Events: Voltage HIGH, Input goes HIGH, event");
			fired = false;
			relay.Input.V = VoltageSignal.HIGH;  // V: H; I: stays H
			Assert.IsFalse(fired, "Events: Voltage HIGH, Input stays HIGH, no event");
			relay.Input.V = VoltageSignal.LOW;   // V: H; I: goes L
			Assert.IsTrue(fired, "Events: Voltage HIGH, Input goes LOW, event");
			fired = false;
			relay.Input.V = VoltageSignal.LOW;   // V: H; I: stays L
			Assert.IsFalse(fired, "Events: Voltage HIGH, Input stays LOW, no event");
		}
	}
}