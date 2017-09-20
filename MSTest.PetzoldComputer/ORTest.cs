using Microsoft.VisualStudio.TestTools.UnitTesting;

using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class ORTest
	{
		[TestMethod]
		public void TestConstructor()
		{
			IOr or = new OR();

			Assert.AreEqual(VoltageSignal.LOW, or.Voltage, "Constructor: Voltage");
			Assert.AreEqual(VoltageSignal.LOW, or.A, "Constructor: A");
			Assert.AreEqual(VoltageSignal.LOW, or.B, "Constructor: B");
			Assert.AreEqual(VoltageSignal.LOW, or.O, "Constructor: O");
			Assert.AreEqual("LOW", or.ToString(), "Constructor: ToString()");
		}

		[TestMethod]
		public void TestOutput()
		{
			IOr or2 = new OR { Voltage = VoltageSignal.HIGH };
			Assert.AreEqual(VoltageSignal.LOW, or2.O, "Gate on -- A: L; B: L; O: L");
			or2.A = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.HIGH, or2.O, "Gate on -- A: H; B: L; O: H");
			or2.B = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.HIGH, or2.O, "Gate on -- A: H; B: H; O: H");
			or2.A = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.HIGH, or2.O, "Gate on -- A: L; B: H; O: H");

			or2.Voltage = VoltageSignal.LOW;
			or2.B = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, or2.O, "Gate off -- A: L; B: L; O: L");
			or2.A = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, or2.O, "Gate off -- A: H; B: L; O: L");
			or2.B = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, or2.O, "Gate off -- A: H; B: H; O: L");
			or2.A = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, or2.O, "Gate off -- A: L; B: H; O: L");
		}

		[TestMethod]
		public void TestOutputEventsGateOn()
		{
			IOr or2 = new OR();

			TestEventsHelper helper = new TestEventsHelper((IOutput)or2);

			// setup
			or2.Voltage = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			or2.B = VoltageSignal.HIGH;
			Assert.IsTrue(helper.EventFired, "Gate on -- A: L; B: ^; event");
			helper.ResetStatus();
			or2.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: -->H; no event");
			or2.B = VoltageSignal.LOW;
			Assert.IsTrue(helper.EventFired, "Gate on -- A: L; B: v; event");
			helper.ResetStatus();
			or2.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: -->L; no event");
			or2.A = VoltageSignal.HIGH;
			Assert.IsTrue(helper.EventFired, "Gate on -- B: L; A: ^; event");
			helper.ResetStatus();
			or2.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: L; A: -->H; no event");
			or2.A = VoltageSignal.LOW;
			Assert.IsTrue(helper.EventFired, "Gate on -- B: L; A: v; event");
			helper.ResetStatus();
			or2.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: L; A: -->L; no event");

			// setup
			or2.A = VoltageSignal.HIGH;
			or2.B = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			or2.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: ^; no event");
			or2.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: -->H; no event");
			or2.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: v; no event");
			or2.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: -->L; no event");

			// setup
			or2.A = VoltageSignal.LOW;
			or2.B = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			or2.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: H; A: ^; no event");
			or2.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: H; A: -->H; no event");
			or2.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: H; A: v; no event");
			or2.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: H; A: -->L; no event");
		}

		[TestMethod]
		public void TestOutputEventsGateOff()
		{
			IOr or2 = new OR();

			TestEventsHelper helper = new TestEventsHelper((IOutput)or2);

			or2.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate off -- A: L; B: ^; no event");
			or2.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate off -- A: L; B: -->H; no event");
			or2.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate off -- A: L; B: v; no event");
			or2.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate off -- A: L; B: -->L; no event");
			or2.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate off -- B: L; A: ^; no event");
			or2.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate off -- B: L; A: -->H; no event");
			or2.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate off -- B: L; A: v; no event");
			or2.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate off -- B: L; A: -->L; no event");

			// setup
			or2.A = VoltageSignal.HIGH;
			or2.B = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			or2.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate off -- A: H; B: ^; no event");
			or2.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate off -- A: H; B: -->H; no event");
			or2.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate off -- A: H; B: v; no event");
			or2.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate off -- A: H; B: -->L; no event");

			// setup
			or2.A = VoltageSignal.LOW;
			or2.B = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			or2.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate off -- B: H; A: ^; no event");
			or2.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate off -- B: H; A: -->H; no event");
			or2.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate off -- B: H; A: v; no event");
			or2.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate off -- B: H; A: -->L; no event");
		}

		[TestMethod]
		public void OR_2_Constructor()
		{
			var or = new OR_2();

			Assert.AreEqual(VoltageSignal.LOW, or.V.V, "Constructor: Voltage");
			Assert.AreEqual(VoltageSignal.LOW, or.A.V, "Constructor: A");
			Assert.AreEqual(VoltageSignal.LOW, or.B.V, "Constructor: B");
			Assert.AreEqual(VoltageSignal.LOW, or.O.V, "Constructor: O");
			Assert.AreEqual("LOW", or.ToString(), "Constructor: ToString()");
		}

		[DataTestMethod]
		#region data
		[DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.HIGH)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.HIGH)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.HIGH)]
		#endregion
		public void OR_2(VoltageSignal voltage, VoltageSignal a, VoltageSignal b, VoltageSignal expected)
		{
			// arrage
			var or = new OR_2();

			// act
			or.V.V = voltage;
			or.A.V = a;
			or.B.V = b;

			// assert
			Assert.AreEqual(expected, or.O.V, $"V:{or.V.V}; A:{or.A.V}; B:{or.B.V}");
		}

		[TestMethod]
		public void OR_2_Events_On()
		{
			// arrage
			var or = new OR_2();
			or.V.V = VoltageSignal.HIGH;
			bool fired = false;
			or.O.Changed += _ => fired = true;

			// act, assert
			or.B.V = VoltageSignal.HIGH;
			Assert.IsTrue(fired, "Gate on -- A: L; B: ^; event");
			fired = false;
			or.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: L; B: -->H; no event");
			or.B.V = VoltageSignal.LOW;
			Assert.IsTrue(fired, "Gate on -- A: L; B: v; event");
			fired = false;
			or.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: L; B: -->L; no event");
			or.A.V = VoltageSignal.HIGH;
			Assert.IsTrue(fired, "Gate on -- B: L; A: ^; event");
			fired = false;
			or.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- B: L; A: -->H; no event");
			or.A.V = VoltageSignal.LOW;
			Assert.IsTrue(fired, "Gate on -- B: L; A: v; event");
			fired = false;
			or.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- B: L; A: -->L; no event");

			// reset
			or.A.V = VoltageSignal.HIGH;
			or.B.V = VoltageSignal.LOW;
			fired = false;
			
			or.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: H; B: ^; no event");
			or.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: H; B: -->H; no event");
			or.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: H; B: v; no event");
			or.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: H; B: -->L; no event");

			// reset
			or.A.V = VoltageSignal.LOW;
			or.B.V = VoltageSignal.HIGH;
			fired = false;

			or.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- B: H; A: ^; no event");
			or.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- B: H; A: -->H; no event");
			or.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- B: H; A: v; no event");
			or.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- B: H; A: -->L; no event");
		}
	}
}
