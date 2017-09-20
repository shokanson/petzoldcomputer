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
			Assert.IsTrue(helper.EventFired, "Events: Input LOW, Voltage goes HIGH, event");
			helper.ResetStatus();
			not.Voltage = VoltageSignal.HIGH;	// I: L; V: stays H
			Assert.IsFalse(helper.EventFired, "Events: Input LOW, Voltage stays HIGH, no event");
			not.Voltage = VoltageSignal.LOW;	// I: L; V: goes L
			Assert.IsTrue(helper.EventFired, "Events: Input LOW, Voltage goes LOW, event");
			helper.ResetStatus();
			not.Voltage = VoltageSignal.LOW;	// I: L; V: stays L
			Assert.IsFalse(helper.EventFired, "Events: Input LOW, Voltage stays LOW, no event");

			not.Input = VoltageSignal.HIGH;	// V: L; I: goes H
			Assert.IsFalse(helper.EventFired, "Events: Voltage LOW, Input goes HIGH, no event");
			not.Input = VoltageSignal.HIGH;	// V: L; I: stays H
			Assert.IsFalse(helper.EventFired, "Events: Voltage LOW, Input stays HIGH, no event");
			not.Input = VoltageSignal.LOW;	// V: L; I: goes L
			Assert.IsFalse(helper.EventFired, "Events: Voltage LOW, Input goes LOW, no event");
			not.Input = VoltageSignal.LOW;	// V: L; I: stays L
			Assert.IsFalse(helper.EventFired, "Events: Voltage LOW, Input stays LOW, no event");

			not.Input = VoltageSignal.HIGH;
			helper.ResetStatus();
			not.Voltage = VoltageSignal.HIGH;	// I: H; V: goes H
			Assert.IsFalse(helper.EventFired, "Events: Input HIGH, Voltage goes HIGH, no event");
			not.Voltage = VoltageSignal.HIGH;	// I: H; V: stays H
			Assert.IsFalse(helper.EventFired, "Events: Input HIGH, Voltage stays HIGH, no event");
			not.Voltage = VoltageSignal.LOW;	// I: H; V: goes L
			Assert.IsFalse(helper.EventFired, "Events: Input HIGH, Voltage goes LOW, no event");
			not.Voltage = VoltageSignal.LOW;	// I: H; V: stays L
			Assert.IsFalse(helper.EventFired, "Events: Input HIGH, Voltage stays LOW, no event");

			not.Voltage = VoltageSignal.HIGH;
			not.Input = VoltageSignal.LOW;
			helper.ResetStatus();
			not.Input = VoltageSignal.HIGH;	// V: H; I: goes H
			Assert.IsTrue(helper.EventFired, "Events: Voltage HIGH, Input goes HIGH, event");
			helper.ResetStatus();
			not.Input = VoltageSignal.HIGH;	// V: H; I: stays H
			Assert.IsFalse(helper.EventFired, "Events: Voltage HIGH, Input stays HIGH, no event");
			not.Input = VoltageSignal.LOW;	// V: H; I: goes L
			Assert.IsTrue(helper.EventFired, "Events: Voltage HIGH, Input goes LOW, event");
			helper.ResetStatus();
			not.Input = VoltageSignal.LOW;	// V: H; I: stays L
			Assert.IsFalse(helper.EventFired, "Events: Voltage HIGH, Input stays LOW, no event");
		}

		[TestMethod]
		public void Not2_Constructor()
		{
			// arrage, act
			var not = new NOT_2();

			// Assert
			Assert.AreEqual(VoltageSignal.LOW, not.Voltage.Voltage, "Voltage");
			Assert.AreEqual(VoltageSignal.LOW, not.Input.Voltage, "Input");
			Assert.AreEqual(VoltageSignal.LOW, not.Output.Voltage, "Output");
		}

		[DataTestMethod]
		[DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.HIGH)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW)]
		public void Not2(VoltageSignal voltage, VoltageSignal input, VoltageSignal expected)
		{
			// arrange
			var not = new NOT_2();

			// act
			not.Voltage.Voltage = voltage;
			not.Input.Voltage = input;

			// assert
			Assert.AreEqual(expected, not.Output.Voltage, $"V: {voltage}; I:{input}");
		}

		[TestMethod]
		public void Not2_Events()
		{
			// arrage
			var not = new NOT_2();

			// act,assert
			bool fired = false;
			not.Output.VoltageChanged += _ => fired = true;

			// w/ voltage L, output shouldn't change
			not.Input.Voltage = VoltageSignal.HIGH;
			Assert.IsFalse(fired);
			not.Input.Voltage = VoltageSignal.LOW;
			Assert.IsFalse(fired);

			// w/ input L, and voltage changed to H, output should change (to H)
			not.Voltage.Voltage = VoltageSignal.HIGH;
			Assert.IsTrue(fired);
			fired = false;

			// w/ voltage H, and input changed to H, output should change (to H)
			not.Input.Voltage = VoltageSignal.HIGH;
			Assert.IsTrue(fired);
			fired = false;

			// w/ voltage H, input H, and input set to H again, output shouldn't change
			not.Input.Voltage = VoltageSignal.HIGH;
			Assert.IsFalse(fired);

			// w/ voltage H, input L, and voltage set to L, output should change (to L)
			not.Input.Voltage = VoltageSignal.LOW;
			not.Voltage.Voltage = VoltageSignal.LOW;
			Assert.IsTrue(fired);
		}
	}
}
