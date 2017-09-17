using Microsoft.VisualStudio.TestTools.UnitTesting;

using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class HalfAdderTest
	{
		[TestMethod]
		public void TestConstructor()
		{
			IHalfAdder halfAdder = new HalfAdder();

			Assert.AreEqual(halfAdder.Voltage, VoltageSignal.LOW, "Constructor: V");
			Assert.AreEqual(halfAdder.A, VoltageSignal.LOW, "Constructor: A");
			Assert.AreEqual(halfAdder.B, VoltageSignal.LOW, "Constructor: B");
			Assert.AreEqual(halfAdder.Sum, VoltageSignal.LOW, "Constructor: Sum");
			Assert.AreEqual(halfAdder.Carry, VoltageSignal.LOW, "Constructor: Carry");
			Assert.AreEqual(halfAdder.ToString(), "Sum: LOW; Carry: LOW", "Constructor: ToString()");
		}

		[TestMethod]
		public void TestSum()
		{
			IHalfAdder halfAdder = new HalfAdder { Voltage = VoltageSignal.HIGH };
			Assert.AreEqual(halfAdder.Sum, VoltageSignal.LOW, "Sum -- A: L; B: L; Sum: L");
			halfAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(halfAdder.Sum, VoltageSignal.HIGH, "Sum -- A: H; B: L; Sum: H");
			halfAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(halfAdder.Sum, VoltageSignal.LOW, "Sum -- A: H; B: H; Sum: L");
			halfAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(halfAdder.Sum, VoltageSignal.HIGH, "Sum -- A: L; B: H; Sum: H");
		}

		[TestMethod]
		public void TestCarry()
		{
			IHalfAdder halfAdder = new HalfAdder { Voltage = VoltageSignal.HIGH };
			Assert.AreEqual(halfAdder.Carry, VoltageSignal.LOW, "Carry -- A: L; B: L; Carry: L");
			halfAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(halfAdder.Carry, VoltageSignal.LOW, "Carry -- A: H; B: L; Carry: L");
			halfAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(halfAdder.Carry, VoltageSignal.HIGH, "Carry -- A: H; B: H; Carry: H");
			halfAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(halfAdder.Carry, VoltageSignal.LOW, "Carry -- A: L; B: H; Carry: L");
		}

		[TestMethod]
		public void TestSumEvent()
		{
			IHalfAdder halfAdder = new HalfAdder();

			TestEventsHelper helper = new TestEventsHelper((ISum)halfAdder);

			halfAdder.Voltage = VoltageSignal.HIGH;
			helper.ResetStatus();
			halfAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: L; B: ^; event");
			helper.ResetStatus();
			halfAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: -->H; no event");
			halfAdder.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: L; B: v; event");
			helper.ResetStatus();
			halfAdder.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: -->L; no event");
			halfAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- B: L; A: ^; event");
			helper.ResetStatus();
			halfAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: L; A: -->H; no event");
			halfAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- B: L; A: v; event");
			helper.ResetStatus();
			halfAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: L; A: -->L; no event");

			// setup
			halfAdder.A = VoltageSignal.HIGH;
			halfAdder.B = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			halfAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: H; B: ^; event");
			helper.ResetStatus();
			halfAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: -->H; no event");
			halfAdder.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: H; B: v; event");
			helper.ResetStatus();
			halfAdder.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: -->L; no event");

			// setup
			halfAdder.A = VoltageSignal.LOW;
			halfAdder.B = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			halfAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- B: H; A: ^; event");
			helper.ResetStatus();
			halfAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: H; A: -->H; no event");
			halfAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- B: H; A: v; event");
			helper.ResetStatus();
			halfAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: H; A: -->L; no event");
		}

		[TestMethod]
		public void TestCarryEvent()
		{
			IHalfAdder halfAdder = new HalfAdder();

			TestEventsHelper helper = new TestEventsHelper((ICarry)halfAdder);

			// setup
			halfAdder.Voltage = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			halfAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: ^; no event");
			halfAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: -->H; no event");
			halfAdder.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: v; no event");
			halfAdder.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: -->L; no event");
			halfAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: L; A: ^; no event");
			halfAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: L; A: -->H; no event");
			halfAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: L; A: v; no event");
			halfAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: L; A: -->L; no event");

			// setup
			halfAdder.A = VoltageSignal.HIGH;
			halfAdder.B = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			halfAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: H; B: ^; event");
			helper.ResetStatus();
			halfAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: -->H; no event");
			halfAdder.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: H; B: v; event");
			helper.ResetStatus();
			halfAdder.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: -->L; no event");

			// setup
			halfAdder.A = VoltageSignal.LOW;
			halfAdder.B = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			halfAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- B: H; A: ^; event");
			helper.ResetStatus();
			halfAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: H; A: -->H; no event");
			halfAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- B: H; A: v; event");
			helper.ResetStatus();
			halfAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: H; A: -->L; no event");
		}
	}
}
