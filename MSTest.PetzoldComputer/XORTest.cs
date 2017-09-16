using Microsoft.VisualStudio.TestTools.UnitTesting;

using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class XORTest
	{
		[TestMethod]
		public void TestConstructor()
		{
			IXor xor = new XOR();

			Assert.AreEqual(xor.Voltage, VoltageSignal.LOW, "Constructor: Voltage");
			Assert.AreEqual(xor.A, VoltageSignal.LOW, "Constructor: A");
			Assert.AreEqual(xor.B, VoltageSignal.LOW, "Constructor: B");
			Assert.AreEqual(xor.O, VoltageSignal.LOW, "Constructor: O");
			Assert.AreEqual(xor.ToString(), "LOW", "Constructor: ToString()");
		}

		[TestMethod]
		public void TestOutput()
		{
			IXor xor = new XOR();

			xor.Voltage = VoltageSignal.HIGH;
			Assert.AreEqual(xor.O, VoltageSignal.LOW, "Gate on -- A: L; B: L; O: L");
			xor.A = VoltageSignal.HIGH;
			Assert.AreEqual(xor.O, VoltageSignal.HIGH, "Gate on -- A: H; B: L; O: H");
			xor.B = VoltageSignal.HIGH;
			Assert.AreEqual(xor.O, VoltageSignal.LOW, "Gate on -- A: H; B: H; O: L");
			xor.A = VoltageSignal.LOW;
			Assert.AreEqual(xor.O, VoltageSignal.HIGH, "Gate on -- A: L; B: H; O: H");

			xor.Voltage = VoltageSignal.LOW;
			xor.B = VoltageSignal.LOW;
			Assert.AreEqual(xor.O, VoltageSignal.LOW, "Gate off -- A: L; B: L; O: L");
			xor.A = VoltageSignal.HIGH;
			Assert.AreEqual(xor.O, VoltageSignal.LOW, "Gate off -- A: H; B: L; O: L");
			xor.B = VoltageSignal.HIGH;
			Assert.AreEqual(xor.O, VoltageSignal.LOW, "Gate off -- A: H; B: H; O: L");
			xor.A = VoltageSignal.LOW;
			Assert.AreEqual(xor.O, VoltageSignal.LOW, "Gate off -- A: L; B: H; O: L");
		}

		[TestMethod]
		public void TestOutputEventsGateOn()
		{
			IXor xor = new XOR();

			TestEventsHelper helper = new TestEventsHelper((IOutput)xor);

			// setup
			xor.Voltage = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			xor.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: L; B: ^; event");
			helper.ResetStatus();
			xor.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: -->H; no event");
			xor.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: L; B: v; event");
			helper.ResetStatus();
			xor.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: -->L; no event");
			xor.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- B: L; A: ^; event");
			helper.ResetStatus();
			xor.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: L; A: -->H; no event");
			xor.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- B: L; A: v; event");
			helper.ResetStatus();
			xor.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: L; A: -->L; no event");

			// setup
			xor.A = VoltageSignal.HIGH;
			xor.B = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			xor.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: H; B: ^; event");
			helper.ResetStatus();
			xor.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: -->H; no event");
			xor.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: H; B: v; event");
			helper.ResetStatus();
			xor.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: -->L; no event");

			// setup
			xor.A = VoltageSignal.LOW;
			xor.B = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			xor.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- B: H; A: ^; event");
			helper.ResetStatus();
			xor.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: H; A: -->H; no event");
			xor.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- B: H; A: v; event");
			helper.ResetStatus();
			xor.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: H; A: -->L; no event");
		}

		[TestMethod]
		public void TestOutputEventsGateOff()
		{
			IOr or2 = new OR();

			TestEventsHelper helper = new TestEventsHelper((IOutput)or2);

			or2.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- A: L; B: ^; no event");
			or2.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- A: L; B: -->H; no event");
			or2.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- A: L; B: v; no event");
			or2.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- A: L; B: -->L; no event");
			or2.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- B: L; A: ^; no event");
			or2.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- B: L; A: -->H; no event");
			or2.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- B: L; A: v; no event");
			or2.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- B: L; A: -->L; no event");

			// setup
			or2.A = VoltageSignal.HIGH;
			or2.B = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			or2.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- A: H; B: ^; no event");
			or2.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- A: H; B: -->H; no event");
			or2.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- A: H; B: v; no event");
			or2.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- A: H; B: -->L; no event");

			// setup
			or2.A = VoltageSignal.LOW;
			or2.B = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			or2.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- B: H; A: ^; no event");
			or2.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- B: H; A: -->H; no event");
			or2.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- B: H; A: v; no event");
			or2.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- B: H; A: -->L; no event");
		}
	}
}
