using Microsoft.VisualStudio.TestTools.UnitTesting;

using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class NORTest
	{
		[TestMethod]
		public void TestConstructor()
		{
			INor or = new NOR();

			Assert.AreEqual(VoltageSignal.LOW, or.Voltage, "Constructor: Voltage");
			Assert.AreEqual(VoltageSignal.LOW, or.A, "Constructor: A");
			Assert.AreEqual(VoltageSignal.LOW, or.B, "Constructor: B");
			Assert.AreEqual(VoltageSignal.LOW, or.O, "Constructor: O");
			Assert.AreEqual("LOW", or.ToString(), "Constructor: ToString()");
		}

		[TestMethod]
		public void TestOutput()
		{
			INor or = new NOR { Voltage = VoltageSignal.HIGH };
			Assert.AreEqual(VoltageSignal.HIGH, or.O, "Gate on -- A: L; B: L; O: H");
			or.A = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, or.O, "Gate on -- A: H; B: L; O: L");
			or.B = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, or.O, "Gate on -- A: H; B: H; O: L");
			or.A = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, or.O, "Gate on -- A: L; B: H; O: L");

			or.Voltage = VoltageSignal.LOW;
			or.B = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, or.O, "Gate off -- A: L; B: L; O: L");
			or.A = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, or.O, "Gate off -- A: H; B: L; O: L");
			or.B = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, or.O, "Gate off -- A: H; B: H; O: L");
			or.A = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, or.O, "Gate off -- A: L; B: H; O: L");
		}

		[TestMethod]
		public void TestOutputEventsGateOn()
		{
			INor or = new NOR();

			TestEventsHelper helper = new TestEventsHelper((IOutput)or);

			// setup
			or.Voltage = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			or.B = VoltageSignal.HIGH;
			Assert.AreEqual("fired", helper.EventStatus, "Gate on -- A: L; B: ^; event");
			helper.ResetStatus();
			or.B = VoltageSignal.HIGH;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate on -- A: L; B: -->H; no event");
			or.B = VoltageSignal.LOW;
			Assert.AreEqual("fired", helper.EventStatus, "Gate on -- A: L; B: v; event");
			helper.ResetStatus();
			or.B = VoltageSignal.LOW;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate on -- A: L; B: -->L; no event");
			or.A = VoltageSignal.HIGH;
			Assert.AreEqual("fired", helper.EventStatus, "Gate on -- B: L; A: ^; event");
			helper.ResetStatus();
			or.A = VoltageSignal.HIGH;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate on -- B: L; A: -->H; no event");
			or.A = VoltageSignal.LOW;
			Assert.AreEqual("fired", helper.EventStatus, "Gate on -- B: L; A: v; event");
			helper.ResetStatus();
			or.A = VoltageSignal.LOW;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate on -- B: L; A: -->L; no event");

			// setup
			or.A = VoltageSignal.HIGH;
			or.B = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			or.B = VoltageSignal.HIGH;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate on -- A: H; B: ^; no event");
			or.B = VoltageSignal.HIGH;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate on -- A: H; B: -->H; no event");
			or.B = VoltageSignal.LOW;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate on -- A: H; B: v; no event");
			or.B = VoltageSignal.LOW;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate on -- A: H; B: -->L; no event");

			// setup
			or.A = VoltageSignal.LOW;
			or.B = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			or.A = VoltageSignal.HIGH;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate on -- B: H; A: ^; no event");
			or.A = VoltageSignal.HIGH;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate on -- B: H; A: -->H; no event");
			or.A = VoltageSignal.LOW;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate on -- B: H; A: v; no event");
			or.A = VoltageSignal.LOW;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate on -- B: H; A: -->L; no event");
		}

		[TestMethod]
		public void TestOutputEventsGateOff()
		{
			IOr or2 = new OR();

			TestEventsHelper helper = new TestEventsHelper((IOutput)or2);

			or2.B = VoltageSignal.HIGH;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate off -- A: L; B: ^; no event");
			or2.B = VoltageSignal.HIGH;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate off -- A: L; B: -->H; no event");
			or2.B = VoltageSignal.LOW;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate off -- A: L; B: v; no event");
			or2.B = VoltageSignal.LOW;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate off -- A: L; B: -->L; no event");
			or2.A = VoltageSignal.HIGH;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate off -- B: L; A: ^; no event");
			or2.A = VoltageSignal.HIGH;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate off -- B: L; A: -->H; no event");
			or2.A = VoltageSignal.LOW;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate off -- B: L; A: v; no event");
			or2.A = VoltageSignal.LOW;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate off -- B: L; A: -->L; no event");

			// setup
			or2.A = VoltageSignal.HIGH;
			or2.B = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			or2.B = VoltageSignal.HIGH;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate off -- A: H; B: ^; no event");
			or2.B = VoltageSignal.HIGH;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate off -- A: H; B: -->H; no event");
			or2.B = VoltageSignal.LOW;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate off -- A: H; B: v; no event");
			or2.B = VoltageSignal.LOW;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate off -- A: H; B: -->L; no event");

			// setup
			or2.A = VoltageSignal.LOW;
			or2.B = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			or2.A = VoltageSignal.HIGH;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate off -- B: H; A: ^; no event");
			or2.A = VoltageSignal.HIGH;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate off -- B: H; A: -->H; no event");
			or2.A = VoltageSignal.LOW;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate off -- B: H; A: v; no event");
			or2.A = VoltageSignal.LOW;
			Assert.AreEqual("not fired", helper.EventStatus, "Gate off -- B: H; A: -->L; no event");
		}
	}
}
