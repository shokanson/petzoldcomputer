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
			Assert.IsTrue(helper.EventFired, "Gate on -- A: L; B: ^; event");
			helper.ResetStatus();
			or.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: -->H; no event");
			or.B = VoltageSignal.LOW;
			Assert.IsTrue(helper.EventFired, "Gate on -- A: L; B: v; event");
			helper.ResetStatus();
			or.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: -->L; no event");
			or.A = VoltageSignal.HIGH;
			Assert.IsTrue(helper.EventFired, "Gate on -- B: L; A: ^; event");
			helper.ResetStatus();
			or.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: L; A: -->H; no event");
			or.A = VoltageSignal.LOW;
			Assert.IsTrue(helper.EventFired, "Gate on -- B: L; A: v; event");
			helper.ResetStatus();
			or.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: L; A: -->L; no event");

			// setup
			or.A = VoltageSignal.HIGH;
			or.B = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			or.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: ^; no event");
			or.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: -->H; no event");
			or.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: v; no event");
			or.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: -->L; no event");

			// setup
			or.A = VoltageSignal.LOW;
			or.B = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			or.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: H; A: ^; no event");
			or.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: H; A: -->H; no event");
			or.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- B: H; A: v; no event");
			or.A = VoltageSignal.LOW;
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
		public void NOR_2_Constructor()
		{
			// arrange, act
			var nor = new NOR_2();

			// assert
			Assert.AreEqual(VoltageSignal.LOW, nor.V.V, "Constructor: Voltage");
			Assert.AreEqual(VoltageSignal.LOW, nor.A.V, "Constructor: A");
			Assert.AreEqual(VoltageSignal.LOW, nor.B.V, "Constructor: B");
			Assert.AreEqual(VoltageSignal.LOW, nor.O.V, "Constructor: O");
			Assert.AreEqual("LOW", nor.ToString(), "Constructor: ToString()");
		}

		[DataTestMethod]
		#region data
		[DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.HIGH)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW)]
		#endregion
		public void NOR_2(VoltageSignal voltage, VoltageSignal a, VoltageSignal b, VoltageSignal expected)
		{
			// arrage
			var nor = new NOR_2();

			// act
			nor.V.V = voltage;
			nor.A.V = a;
			nor.B.V = b;

			// assert
			Assert.AreEqual(expected, nor.O.V, $"V:{nor.V}; A:{nor.A}; B:{nor.B}");
		}

		[TestMethod]
		public void NOR_2_Events_GateOn()
		{
			// arrange
			var nor = new NOR_2();
			nor.V.V = VoltageSignal.HIGH;
			bool fired = false;
			nor.O.Changed += _ => fired = true;

			// act, assert
			nor.B.V = VoltageSignal.HIGH;
			Assert.IsTrue(fired, "Gate on -- A: L; B: ^; event");
			fired = false;
			nor.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: L; B: -->H; no event");
			nor.B.V = VoltageSignal.LOW;
			Assert.IsTrue(fired, "Gate on -- A: L; B: v; event");
			fired = false;
			nor.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: L; B: -->L; no event");
			nor.A.V = VoltageSignal.HIGH;
			Assert.IsTrue(fired, "Gate on -- B: L; A: ^; event");
			fired = false;
			nor.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- B: L; A: -->H; no event");
			nor.A.V = VoltageSignal.LOW;
			Assert.IsTrue(fired, "Gate on -- B: L; A: v; event");
			fired = false;
			nor.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- B: L; A: -->L; no event");

			// setup
			nor.A.V = VoltageSignal.HIGH;
			nor.B.V = VoltageSignal.LOW;
			fired = false;
			// test
			nor.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: H; B: ^; no event");
			nor.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: H; B: -->H; no event");
			nor.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: H; B: v; no event");
			nor.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: H; B: -->L; no event");

			// setup
			nor.A.V = VoltageSignal.LOW;
			nor.B.V = VoltageSignal.HIGH;
			fired = false;
			// test
			nor.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- B: H; A: ^; no event");
			nor.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- B: H; A: -->H; no event");
			nor.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- B: H; A: v; no event");
			nor.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- B: H; A: -->L; no event");
		}

		[TestMethod]
		public void NOR_2_Events_GateOff()
		{
			// arrange
			var nor = new NOR_2();
			bool fired = false;
			nor.O.Changed += _ => fired = true;

			// act, assert
			nor.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate off -- A: L; B: ^; no event");
			nor.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate off -- A: L; B: -->H; no event");
			nor.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate off -- A: L; B: v; no event");
			nor.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate off -- A: L; B: -->L; no event");
			nor.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate off -- B: L; A: ^; no event");
			nor.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate off -- B: L; A: -->H; no event");
			nor.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate off -- B: L; A: v; no event");
			nor.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate off -- B: L; A: -->L; no event");

			// reset
			nor.A.V = VoltageSignal.HIGH;
			nor.B.V = VoltageSignal.LOW;

			nor.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate off -- A: H; B: ^; no event");
			nor.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate off -- A: H; B: -->H; no event");
			nor.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate off -- A: H; B: v; no event");
			nor.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate off -- A: H; B: -->L; no event");

			// reset
			nor.A.V = VoltageSignal.LOW;
			nor.B.V = VoltageSignal.HIGH;

			nor.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate off -- B: H; A: ^; no event");
			nor.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate off -- B: H; A: -->H; no event");
			nor.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate off -- B: H; A: v; no event");
			nor.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate off -- B: H; A: -->L; no event");
		}

		[TestMethod]
		public void NOR_2_Event_GateOnOff()
		{
			// arrange
			var nor = new NOR_2();
			bool fired = false;
			nor.O.Changed += _ => fired = true;

			// act, assert
			nor.V.V = VoltageSignal.HIGH;
			Assert.IsTrue(fired, "A: L; B: L; V: ^; event");
			fired = false;
			nor.V.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "A: L; B: L; V: --->H; no event");
			nor.V.V = VoltageSignal.LOW;
			Assert.IsTrue(fired, "A: L; B: L; V: v; event");
			fired = false;
			nor.V.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "A: L; B: L; V: --->L; no event");

			nor.A.V = VoltageSignal.HIGH;
			fired = false;
			nor.V.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "A: H; B: L; V: ^; no event");
			nor.V.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "A: H; B: L; V: --->H; no event");
			nor.V.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "A: H; B: L; V: v; no event");
			nor.V.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "A: H; B: L; V: --->L; no event");

			nor.A.V = VoltageSignal.LOW;
			nor.B.V = VoltageSignal.HIGH;
			fired = false;
			nor.V.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "A: L; B: H; V: ^; no event");
			nor.V.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "A: L; B: H; V: --->H; no event");
			nor.V.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "A: L; B: H; V: v; no event");
			nor.V.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "A: L; B: H; V: --->L; no event");

			nor.A.V = VoltageSignal.HIGH;
			fired = false;
			nor.V.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "A: H; B: H; V: ^; no event");
			nor.V.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "A: H; B: H; V: --->H; no event");
			nor.V.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "A: H; B: H; V: v; no event");
			nor.V.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "A: H; B: H; V: --->L; no event");
		}
	}
}
