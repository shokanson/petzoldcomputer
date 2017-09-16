using Microsoft.VisualStudio.TestTools.UnitTesting;

using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class FullAdderTest
	{
		[TestMethod]
		public void TestConstructor()
		{
			IFullAdder fullAdder = new FullAdder();

			Assert.AreEqual(fullAdder.Voltage, VoltageSignal.LOW, "Constructor: V");
			Assert.AreEqual(fullAdder.A, VoltageSignal.LOW, "Constructor: A");
			Assert.AreEqual(fullAdder.B, VoltageSignal.LOW, "Constructor: B");
			Assert.AreEqual(fullAdder.CarryIn, VoltageSignal.LOW, "Constructor: CarryIn");
			Assert.AreEqual(fullAdder.Sum, VoltageSignal.LOW, "Constructor: Sum");
			Assert.AreEqual(fullAdder.Carry, VoltageSignal.LOW, "Constructor: Carry");
			Assert.AreEqual(fullAdder.ToString(), "Sum: LOW; Carry: LOW", "Constructor: ToString()");
		}

		[TestMethod]
		public void TestSumWithNoCarryin()
		{
			IFullAdder fullAdder = new FullAdder();

			fullAdder.Voltage = VoltageSignal.HIGH;
			Assert.AreEqual(fullAdder.Sum, VoltageSignal.LOW, "Sum -- A: L; B: L; Sum: L");
			fullAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(fullAdder.Sum, VoltageSignal.HIGH, "Sum -- A: H; B: L; Sum: H");
			fullAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(fullAdder.Sum, VoltageSignal.LOW, "Sum -- A: H; B: H; Sum: L");
			fullAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(fullAdder.Sum, VoltageSignal.HIGH, "Sum -- A: L; B: H; Sum: H");

		}

		[TestMethod]
		public void TestSumWithCarryin()
		{
			IFullAdder fullAdder = new FullAdder();

			fullAdder.Voltage = VoltageSignal.HIGH;
			fullAdder.CarryIn = VoltageSignal.HIGH;
			Assert.AreEqual(fullAdder.Sum, VoltageSignal.HIGH, "Sum -- A: L; B: L; Sum: H");
			fullAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(fullAdder.Sum, VoltageSignal.LOW, "Sum -- A: H; B: L; Sum: L");
			fullAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(fullAdder.Sum, VoltageSignal.HIGH, "Sum -- A: H; B: H; Sum: H");
			fullAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(fullAdder.Sum, VoltageSignal.LOW, "Sum -- A: L; B: H; Sum: L");

		}

		[TestMethod]
		public void TestCarryWithNoCarryin()
		{
			IFullAdder fullAdder = new FullAdder();

			fullAdder.Voltage = VoltageSignal.HIGH;
			Assert.AreEqual(fullAdder.Carry, VoltageSignal.LOW, "Carry -- A: L; B: L; Carry: L");
			fullAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(fullAdder.Carry, VoltageSignal.LOW, "Carry -- A: H; B: L; Carry: L");
			fullAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(fullAdder.Carry, VoltageSignal.HIGH, "Carry -- A: H; B: H; Carry: H");
			fullAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(fullAdder.Carry, VoltageSignal.LOW, "Carry -- A: L; B: H; Carry: L");

		}

		[TestMethod]
		public void TestCarryWithCarryin()
		{
			IFullAdder fullAdder = new FullAdder();

			fullAdder.Voltage = VoltageSignal.HIGH;
			fullAdder.CarryIn = VoltageSignal.HIGH;
			Assert.AreEqual(fullAdder.Carry, VoltageSignal.LOW, "Carry -- A: L; B: L; Carry: L");
			fullAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(fullAdder.Carry, VoltageSignal.HIGH, "Carry -- A: H; B: L; Carry: H");
			fullAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(fullAdder.Carry, VoltageSignal.HIGH, "Carry -- A: H; B: H; Carry: H");
			fullAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(fullAdder.Carry, VoltageSignal.HIGH, "Carry -- A: L; B: H; Carry: H");

		}

		[TestMethod]
		public void TestSumEventNoCarryin()
		{
			IFullAdder fullAdder = new FullAdder();

			TestEventsHelper helper = new TestEventsHelper((ISum)fullAdder);

			fullAdder.Voltage = VoltageSignal.HIGH;
			helper.ResetStatus();
			fullAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: L; B: ^; event");
			helper.ResetStatus();
			fullAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: -->H; no event");
			fullAdder.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: L; B: v; event");
			helper.ResetStatus();
			fullAdder.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: -->L; no event");
			fullAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- B: L; A: ^; event");
			helper.ResetStatus();
			fullAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: L; A: -->H; no event");
			fullAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- B: L; A: v; event");
			helper.ResetStatus();
			fullAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: L; A: -->L; no event");

			// setup
			fullAdder.A = VoltageSignal.HIGH;
			fullAdder.B = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			fullAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: H; B: ^; event");
			helper.ResetStatus();
			fullAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: -->H; no event");
			fullAdder.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: H; B: v; event");
			helper.ResetStatus();
			fullAdder.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: -->L; no event");

			// setup
			fullAdder.A = VoltageSignal.LOW;
			fullAdder.B = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			fullAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- B: H; A: ^; event");
			helper.ResetStatus();
			fullAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: H; A: -->H; no event");
			fullAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- B: H; A: v; event");
			helper.ResetStatus();
			fullAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: H; A: -->L; no event");
		}

		[TestMethod]
		public void TestCarryEventNoCarryin()
		{
			IFullAdder fullAdder = new FullAdder();

			TestEventsHelper helper = new TestEventsHelper((ICarry)fullAdder);

			// setup
			fullAdder.Voltage = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			fullAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: ^; no event");
			fullAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: -->H; no event");
			fullAdder.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: v; no event");
			fullAdder.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: -->L; no event");
			fullAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: L; A: ^; no event");
			fullAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: L; A: -->H; no event");
			fullAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: L; A: v; no event");
			fullAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: L; A: -->L; no event");

			// setup
			fullAdder.A = VoltageSignal.HIGH;
			fullAdder.B = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			fullAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: H; B: ^; event");
			helper.ResetStatus();
			fullAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: -->H; no event");
			fullAdder.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: H; B: v; event");
			helper.ResetStatus();
			fullAdder.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: -->L; no event");

			// setup
			fullAdder.A = VoltageSignal.LOW;
			fullAdder.B = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			fullAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- B: H; A: ^; event");
			helper.ResetStatus();
			fullAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: H; A: -->H; no event");
			fullAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- B: H; A: v; event");
			helper.ResetStatus();
			fullAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: H; A: -->L; no event");
		}

		[TestMethod]
		public void TestSumEventWithCarryin()
		{
			IFullAdder fullAdder = new FullAdder();

			TestEventsHelper helper = new TestEventsHelper((ISum)fullAdder);

			fullAdder.Voltage = VoltageSignal.HIGH;
			fullAdder.CarryIn = VoltageSignal.HIGH;
			helper.ResetStatus();
			fullAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: L; B: ^; event");
			helper.ResetStatus();
			fullAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: -->H; no event");
			fullAdder.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: L; B: v; event");
			helper.ResetStatus();
			fullAdder.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: -->L; no event");
			fullAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- B: L; A: ^; event");
			helper.ResetStatus();
			fullAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: L; A: -->H; no event");
			fullAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- B: L; A: v; event");
			helper.ResetStatus();
			fullAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: L; A: -->L; no event");

			// setup
			fullAdder.A = VoltageSignal.HIGH;
			fullAdder.B = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			fullAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: H; B: ^; event");
			helper.ResetStatus();
			fullAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: -->H; no event");
			fullAdder.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: H; B: v; event");
			helper.ResetStatus();
			fullAdder.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: -->L; no event");

			// setup
			fullAdder.A = VoltageSignal.LOW;
			fullAdder.B = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			fullAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- B: H; A: ^; event");
			helper.ResetStatus();
			fullAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: H; A: -->H; no event");
			fullAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- B: H; A: v; event");
			helper.ResetStatus();
			fullAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: H; A: -->L; no event");
		}

		[TestMethod]
		public void TestCarryEventWithCarryin()
		{
			IFullAdder fullAdder = new FullAdder();

			TestEventsHelper helper = new TestEventsHelper((ICarry)fullAdder);

			// setup
			fullAdder.Voltage = VoltageSignal.HIGH;
			fullAdder.CarryIn = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			fullAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: L; B: ^; event");
			helper.ResetStatus();
			fullAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: -->H; no event");
			fullAdder.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: L; B: v; event");
			helper.ResetStatus();
			fullAdder.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: -->L; no event");
			fullAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- B: L; A: ^; event");
			helper.ResetStatus();
			fullAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: L; A: -->H; no event");
			fullAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- B: L; A: v; event");
			helper.ResetStatus();
			fullAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: L; A: -->L; no event");

			// setup
			fullAdder.A = VoltageSignal.HIGH;
			fullAdder.B = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			fullAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: ^; no event");
			fullAdder.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: -->H; no event");
			fullAdder.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: v; no event");
			fullAdder.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: -->L; no event");

			// setup
			fullAdder.A = VoltageSignal.LOW;
			fullAdder.B = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			fullAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: H; A: ^; no event");
			fullAdder.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: H; A: -->H; no event");
			fullAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: H; A: v; no event");
			fullAdder.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: H; A: -->L; no event");
		}
	}
}
