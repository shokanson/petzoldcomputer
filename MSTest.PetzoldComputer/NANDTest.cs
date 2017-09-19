using Microsoft.VisualStudio.TestTools.UnitTesting;

using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	/// <summary>
	/// Summary description for NANDTest
	/// </summary>
	[TestClass]
	public class NANDTest
	{
		[TestMethod]
		public void TestConstructor()
		{
			INand nand = new NAND();

			Assert.AreEqual(VoltageSignal.LOW, nand.Voltage, "NAND Constructor: Voltage");
			Assert.AreEqual(VoltageSignal.LOW, nand.A, "NAND Constructor: A");
			Assert.AreEqual(VoltageSignal.LOW, nand.B, "NAND Constructor: B");
			Assert.AreEqual(VoltageSignal.LOW, nand.O, "NAND Constructor: O");
			Assert.AreEqual("LOW", nand.ToString(), "NAND Constructor: ToString()");
		}

		[TestMethod]
		public void TestOutput()
		{
			INand nand = new NAND { Voltage = VoltageSignal.HIGH };
			Assert.AreEqual(VoltageSignal.HIGH, nand.O, "Gate on -- A: L; B: L; O: H");
			nand.A = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.HIGH, nand.O, "Gate on -- A: H; B: L; O: H");
			nand.B = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, nand.O, "Gate on -- A: H; B: H; O: L");
			nand.A = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.HIGH, nand.O, "Gate on -- A: L; B: H; O: H");

			nand.Voltage = VoltageSignal.LOW;
			nand.B = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, nand.O, "Gate off -- A: L; B: L; O: L");
			nand.A = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, nand.O, "Gate off -- A: H; B: L; O: L");
			nand.B = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, nand.O, "Gate off -- A: H; B: H; O: L");
			nand.A = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, nand.O, "Gate off -- A: L; B: H; O: L");
		}

		[TestMethod]
		public void TestOutputEventsGateOn()
		{
			INand nand = new NAND();

			TestEventsHelper helper = new TestEventsHelper((IOutput)nand);

			// setup
			nand.Voltage = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			nand.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: ^; no event");
			nand.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: -->H; no event");
			nand.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: v; no event");
			nand.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: -->L; no event");
			nand.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: L; A: ^; no event");
			nand.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: L; A: -->H; no event");
			nand.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: L; A: v; no event");
			nand.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: L; A: -->L; no event");

			// setup
			nand.A = VoltageSignal.HIGH;
			nand.B = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			nand.B = VoltageSignal.HIGH;
			Assert.IsTrue(helper.EventFired, "Gate on -- A: H; B: ^; event");
			helper.ResetStatus();
			nand.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: -->H; no event");
			nand.B = VoltageSignal.LOW;
			Assert.IsTrue(helper.EventFired, "Gate on -- A: H; B: v; event");
			helper.ResetStatus();
			nand.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: -->L; no event");

			// setup
			nand.A = VoltageSignal.LOW;
			nand.B = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			nand.A = VoltageSignal.HIGH;
			Assert.IsTrue(helper.EventFired, "Gate on -- B: H; A: ^; event");
			helper.ResetStatus();
			nand.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: H; A: -->H; no event");
			nand.A = VoltageSignal.LOW;
			Assert.IsTrue(helper.EventFired, "Gate on -- B: H; A: v; event");
			helper.ResetStatus();
			nand.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: H; A: -->L; no event");
		}

		[TestMethod]
		public void TestOutputEventsGateOff()
		{
			INand nand = new NAND();

			TestEventsHelper helper = new TestEventsHelper((IOutput)nand);

			nand.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate off -- A: L; B: ^; no event");
			nand.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate off -- A: L; B: -->H; no event");
			nand.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate off -- A: L; B: v; no event");
			nand.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate off -- A: L; B: -->L; no event");
			nand.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate off -- B: L; A: ^; no event");
			nand.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate off -- B: L; A: -->H; no event");
			nand.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate off -- B: L; A: v; no event");
			nand.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate off -- B: L; A: -->L; no event");

			// setup
			nand.A = VoltageSignal.HIGH;
			nand.B = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			nand.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate off -- A: H; B: ^; no event");
			nand.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate off -- A: H; B: -->H; no event");
			nand.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate off -- A: H; B: v; no event");
			nand.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate off -- A: H; B: -->L; no event");

			// setup
			nand.A = VoltageSignal.LOW;
			nand.B = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			nand.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate off -- B: H; A: ^; no event");
			nand.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate off -- B: H; A: -->H; no event");
			nand.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate off -- B: H; A: v; no event");
			nand.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate off -- B: H; A: -->L; no event");
		}
	}
}
