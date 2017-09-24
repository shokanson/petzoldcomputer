using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class NOTTest
	{
		[TestMethod]
		public void Not2_Constructor()
		{
			// arrage, act
			var not = new NOT_2("test");

			// Assert
			Assert.AreEqual(VoltageSignal.LOW, not.V.V, "Voltage");
			Assert.AreEqual(VoltageSignal.LOW, not.Input.V, "Input");
			Assert.AreEqual(VoltageSignal.LOW, not.Output.V, "Output");
			Assert.AreEqual("LOW", not.ToString());
		}

		[DataTestMethod]
		[DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.HIGH)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW)]
		public void Not2(VoltageSignal voltage, VoltageSignal input, VoltageSignal expected)
		{
			// arrange
			var not = new NOT_2("test");

			// act
			not.V.V = voltage;
			not.Input.V = input;

			// assert
			Assert.AreEqual(expected, not.Output.V, $"V: {not.V}; I:{not.Input}");
		}

		[TestMethod]
		public void Not2_Events()
		{
			// arrage
			var not = new NOT_2("test");
			bool fired = false;
			not.Output.Changed += _ => fired = true;

			// act,assert
			not.V.V = VoltageSignal.HIGH;   // I: L; V: goes H
			Assert.IsTrue(fired, "Events: Input LOW, Voltage goes HIGH, event");
			fired = false;
			not.V.V = VoltageSignal.HIGH;   // I: L; V: stays H
			Assert.IsFalse(fired, "Events: Input LOW, Voltage stays HIGH, no event");
			not.V.V = VoltageSignal.LOW; // I: L; V: goes L
			Assert.IsTrue(fired, "Events: Input LOW, Voltage goes LOW, event");
			fired = false;
			not.V.V = VoltageSignal.LOW; // I: L; V: stays L
			Assert.IsFalse(fired, "Events: Input LOW, Voltage stays LOW, no event");

			not.Input.V = VoltageSignal.HIGH;  // V: L; I: goes H
			Assert.IsFalse(fired, "Events: Voltage LOW, Input goes HIGH, no event");
			not.Input.V = VoltageSignal.HIGH;  // V: L; I: stays H
			Assert.IsFalse(fired, "Events: Voltage LOW, Input stays HIGH, no event");
			not.Input.V = VoltageSignal.LOW;   // V: L; I: goes L
			Assert.IsFalse(fired, "Events: Voltage LOW, Input goes LOW, no event");
			not.Input.V = VoltageSignal.LOW;   // V: L; I: stays L
			Assert.IsFalse(fired, "Events: Voltage LOW, Input stays LOW, no event");

			not.Input.V = VoltageSignal.HIGH;
			fired = false;
			not.V.V = VoltageSignal.HIGH;   // I: H; V: goes H
			Assert.IsFalse(fired, "Events: Input HIGH, Voltage goes HIGH, no event");
			not.V.V = VoltageSignal.HIGH;   // I: H; V: stays H
			Assert.IsFalse(fired, "Events: Input HIGH, Voltage stays HIGH, no event");
			not.V.V = VoltageSignal.LOW; // I: H; V: goes L
			Assert.IsFalse(fired, "Events: Input HIGH, Voltage goes LOW, no event");
			not.V.V = VoltageSignal.LOW; // I: H; V: stays L
			Assert.IsFalse(fired, "Events: Input HIGH, Voltage stays LOW, no event");

			not.V.V = VoltageSignal.HIGH;
			not.Input.V = VoltageSignal.LOW;
			fired = false;
			not.Input.V = VoltageSignal.HIGH;  // V: H; I: goes H
			Assert.IsTrue(fired, "Events: Voltage HIGH, Input goes HIGH, event");
			fired = false;
			not.Input.V = VoltageSignal.HIGH;  // V: H; I: stays H
			Assert.IsFalse(fired, "Events: Voltage HIGH, Input stays HIGH, no event");
			not.Input.V = VoltageSignal.LOW;   // V: H; I: goes L
			Assert.IsTrue(fired, "Events: Voltage HIGH, Input goes LOW, event");
			fired = false;
			not.Input.V = VoltageSignal.LOW;   // V: H; I: stays L
			Assert.IsFalse(fired, "Events: Voltage HIGH, Input stays LOW, no event");
		}
	}
}
