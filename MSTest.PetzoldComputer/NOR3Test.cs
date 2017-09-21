using Microsoft.VisualStudio.TestTools.UnitTesting;

using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class NOR3Test
	{
		[TestMethod]
		public void TestConstructor()
		{
			INor3 nor3 = new NOR3();

			Assert.AreEqual(VoltageSignal.LOW, nor3.Voltage, "Constructor: Voltage");
			Assert.AreEqual(VoltageSignal.LOW, nor3.A, "Constructor: A");
			Assert.AreEqual(VoltageSignal.LOW, nor3.B, "Constructor: B");
			Assert.AreEqual(VoltageSignal.LOW, nor3.C, "Constructor: B");
			Assert.AreEqual(VoltageSignal.LOW, nor3.O, "Constructor: O");
			Assert.AreEqual("LOW", nor3.ToString(), "Constructor: ToString()");
		}

		[TestMethod]
		public void TestOutput()
		{
			INor3 nor3 = new NOR3 { Voltage = VoltageSignal.HIGH };
			Assert.AreEqual(VoltageSignal.HIGH, nor3.O, "Gate on -- A: L; B: L; C: L; O: H");
			nor3.A = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, nor3.O, "Gate on -- A: H; B: L; C: L; O: L");
			nor3.B = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, nor3.O, "Gate on -- A: H; B: H; C: L; O: L");
			nor3.A = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, nor3.O, "Gate on -- A: L; B: H; C: L; O: L");
			nor3.C = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, nor3.O, "Gate on -- A: L; B: H; C: H; O: L");
			nor3.B = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, nor3.O, "Gate on -- A: L; B: L; C: H; O: L");
			nor3.A = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, nor3.O, "Gate on -- A: H; B: L; C: H; O: L");
			nor3.B = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, nor3.O, "Gate on -- A: H; B: H; C: H; O: L");

			nor3.Voltage = VoltageSignal.LOW;
			nor3.A = VoltageSignal.LOW;
			nor3.B = VoltageSignal.LOW;
			nor3.C = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, nor3.O, "Gate off -- A: L; B: L; C: L; O: L");
			nor3.A = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, nor3.O, "Gate off -- A: H; B: L; C: L; O: L");
			nor3.B = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, nor3.O, "Gate off -- A: H; B: H; C: L; O: L");
			nor3.A = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, nor3.O, "Gate off -- A: L; B: H; C: L; O: L");
			nor3.C = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, nor3.O, "Gate off -- A: L; B: H; C: H; O: L");
			nor3.B = VoltageSignal.LOW;
			Assert.AreEqual(VoltageSignal.LOW, nor3.O, "Gate off -- A: L; B: L; C: H; O: L");
			nor3.A = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, nor3.O, "Gate off -- A: H; B: L; C: H; O: L");
			nor3.B = VoltageSignal.HIGH;
			Assert.AreEqual(VoltageSignal.LOW, nor3.O, "Gate off -- A: H; B: H; C: H; O: L");
		}

		[TestMethod]
		public void TestOutputEventsGateOn()
		{
			INor3 nor3 = new NOR3();

			TestEventsHelper helper = new TestEventsHelper((IOutput)nor3);

			// setup
			nor3.Voltage = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			nor3.A = VoltageSignal.HIGH;
			Assert.IsTrue(helper.EventFired, "Gate on -- A: ^; B: L; C: L; event");
			helper.ResetStatus();
			nor3.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: -->H; B: L; C: L; no event");
			nor3.A = VoltageSignal.LOW;
			Assert.IsTrue(helper.EventFired, "Gate on -- A: v; B: L; C: L; event");
			helper.ResetStatus();
			nor3.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: -->L; B: L; C: L; no event");
			nor3.B = VoltageSignal.HIGH;
			Assert.IsTrue(helper.EventFired, "Gate on -- A: L; B: ^; C: L; event");
			helper.ResetStatus();
			nor3.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: -->H; C: L; no event");
			nor3.B = VoltageSignal.LOW;
			Assert.IsTrue(helper.EventFired, "Gate on -- A: L; B: v; C: L; event");
			helper.ResetStatus();
			nor3.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: -->L; C: L; no event");
			nor3.C = VoltageSignal.HIGH;
			Assert.IsTrue(helper.EventFired, "Gate on -- A: L; B: L; C: ^; event");
			helper.ResetStatus();
			nor3.C = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: L; C: -->H; no event");
			nor3.C = VoltageSignal.LOW;
			Assert.IsTrue(helper.EventFired, "Gate on -- A: L; B: L; C: v; event");
			helper.ResetStatus();
			nor3.C = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: L; C: -->L; no event");

			//
			// TEST A
			//
			// setup
			nor3.A = VoltageSignal.LOW;
			nor3.B = VoltageSignal.HIGH;
			nor3.C = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			nor3.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: ^; B: H; C: L; no event");
			nor3.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: -->H; B: H; C: L; no event");
			nor3.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: v; B: H; C: L; no event");
			nor3.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: -->L; B: H; C: L; no event");

			// setup
			nor3.A = VoltageSignal.LOW;
			nor3.B = VoltageSignal.HIGH;
			nor3.C = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			nor3.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: ^; B: H; C: H; no event");
			nor3.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: -->H; B: H; C: H; no event");
			nor3.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: v; B: H; C: H; no event");
			nor3.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: -->L; B: H; C: H; no event");

			// setup
			nor3.A = VoltageSignal.LOW;
			nor3.B = VoltageSignal.LOW;
			nor3.C = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			nor3.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: ^; B: L; C: H; no event");
			nor3.A = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: -->H; B: L; C: H; no event");
			nor3.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: v; B: L; C: H; no event");
			nor3.A = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: -->L; B: L; C: H; no event");

			//
			// TEST B
			//
			// setup
			nor3.A = VoltageSignal.HIGH;
			nor3.B = VoltageSignal.LOW;
			nor3.C = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			nor3.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: ^; C: L; no event");
			nor3.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: -->H; C: L; no event");
			nor3.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: v; C: L; no event");
			nor3.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: -->L; C: L; no event");

			// setup
			nor3.A = VoltageSignal.HIGH;
			nor3.B = VoltageSignal.LOW;
			nor3.C = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			nor3.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: ^; C: H; no event");
			nor3.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: -->H; C: H; no event");
			nor3.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: v; C: H; no event");
			nor3.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: -->L; C: H; no event");

			// setup
			nor3.A = VoltageSignal.LOW;
			nor3.B = VoltageSignal.LOW;
			nor3.C = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			nor3.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: ^; C: H; no event");
			nor3.B = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: -->H; C: H; no event");
			nor3.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: v; C: H; no event");
			nor3.B = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: -->L; C: H; no event");


			//
			// TEST C
			//
			// setup
			nor3.A = VoltageSignal.HIGH;
			nor3.B = VoltageSignal.HIGH;
			nor3.C = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			nor3.C = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: H; C: ^; no event");
			nor3.C = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: H; C: -->H; no event");
			nor3.C = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: H; C: v; no event");
			nor3.C = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: H; C: -->L; no event");

			// setup
			nor3.A = VoltageSignal.HIGH;
			nor3.B = VoltageSignal.LOW;
			nor3.C = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			nor3.C = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: L; C: ^; no event");
			nor3.C = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: L; C: -->H; no event");
			nor3.C = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: L; C: v; no event");
			nor3.C = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: H; B: L; C: -->L; no event");

			// setup
			nor3.A = VoltageSignal.LOW;
			nor3.B = VoltageSignal.HIGH;
			nor3.C = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			nor3.C = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: H; C: ^; no event");
			nor3.C = VoltageSignal.HIGH;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: H; C: -->H; no event");
			nor3.C = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: H; C: v; no event");
			nor3.C = VoltageSignal.LOW;
			Assert.IsFalse(helper.EventFired, "Gate on -- A: L; B: H; C: -->L; no event");
		}

		[TestMethod]
		public void NOR3_2_Constructor()
		{
			// arrange, act
			var nor3 = new NOR3_2();

			// assert
			Assert.AreEqual(VoltageSignal.LOW, nor3.V.V, "Constructor: Voltage");
			Assert.AreEqual(VoltageSignal.LOW, nor3.A.V, "Constructor: A");
			Assert.AreEqual(VoltageSignal.LOW, nor3.B.V, "Constructor: B");
			Assert.AreEqual(VoltageSignal.LOW, nor3.C.V, "Constructor: B");
			Assert.AreEqual(VoltageSignal.LOW, nor3.O.V, "Constructor: O");
			Assert.AreEqual("LOW", nor3.ToString(), "Constructor: ToString()");
		}

		[DataTestMethod]
		#region data
		[DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.HIGH)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW)]
		[DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW)]
		#endregion
		public void NOR3_2(VoltageSignal voltage, VoltageSignal a, VoltageSignal b, VoltageSignal c, VoltageSignal expected)
		{
			// arrage
			var nor3 = new NOR3_2();

			// act
			nor3.V.V = voltage;
			nor3.A.V = a;
			nor3.B.V = b;
			nor3.C.V = c;

			// assert
			Assert.AreEqual(expected, nor3.O.V, $"V:{nor3.V.V}; A:{nor3.A.V}; B:{nor3.B.V}; C:{nor3.C.V}");
		}

		[TestMethod]
		public void NOR3_2_Events_GateOn()
		{
			// arrange
			var nor3 = new NOR3_2();
			nor3.V.V = VoltageSignal.HIGH;
			bool fired = false;
			nor3.O.Changed += _ => fired = true;

			// act, assert
			nor3.A.V = VoltageSignal.HIGH;
			Assert.IsTrue(fired, "Gate on -- A: ^; B: L; C: L; event");
			fired = false;
			nor3.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: -->H; B: L; C: L; no event");
			nor3.A.V = VoltageSignal.LOW;
			Assert.IsTrue(fired, "Gate on -- A: v; B: L; C: L; event");
			fired = false;
			nor3.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: -->L; B: L; C: L; no event");
			nor3.B.V = VoltageSignal.HIGH;
			Assert.IsTrue(fired, "Gate on -- A: L; B: ^; C: L; event");
			fired = false;
			nor3.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: L; B: -->H; C: L; no event");
			nor3.B.V = VoltageSignal.LOW;
			Assert.IsTrue(fired, "Gate on -- A: L; B: v; C: L; event");
			fired = false;
			nor3.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: L; B: -->L; C: L; no event");
			nor3.C.V = VoltageSignal.HIGH;
			Assert.IsTrue(fired, "Gate on -- A: L; B: L; C: ^; event");
			fired = false;
			nor3.C.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: L; B: L; C: -->H; no event");
			nor3.C.V = VoltageSignal.LOW;
			Assert.IsTrue(fired, "Gate on -- A: L; B: L; C: v; event");
			fired = false;
			nor3.C.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: L; B: L; C: -->L; no event");

			//
			// TEST A
			//
			// setup
			nor3.A.V = VoltageSignal.LOW;
			nor3.B.V = VoltageSignal.HIGH;
			nor3.C.V = VoltageSignal.LOW;
			fired = false;
			// test
			nor3.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: ^; B: H; C: L; no event");
			nor3.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: -->H; B: H; C: L; no event");
			nor3.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: v; B: H; C: L; no event");
			nor3.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: -->L; B: H; C: L; no event");

			// setup
			nor3.A.V = VoltageSignal.LOW;
			nor3.B.V = VoltageSignal.HIGH;
			nor3.C.V = VoltageSignal.HIGH;
			fired = false;
			// test
			nor3.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: ^; B: H; C: H; no event");
			nor3.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: -->H; B: H; C: H; no event");
			nor3.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: v; B: H; C: H; no event");
			nor3.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: -->L; B: H; C: H; no event");

			// setup
			nor3.A.V = VoltageSignal.LOW;
			nor3.B.V = VoltageSignal.LOW;
			nor3.C.V = VoltageSignal.HIGH;
			fired = false;
			// test
			nor3.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: ^; B: L; C: H; no event");
			nor3.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: -->H; B: L; C: H; no event");
			nor3.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: v; B: L; C: H; no event");
			nor3.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: -->L; B: L; C: H; no event");

			//
			// TEST B
			//
			// setup
			nor3.A.V = VoltageSignal.HIGH;
			nor3.B.V = VoltageSignal.LOW;
			nor3.C.V = VoltageSignal.LOW;
			fired = false;
			// test
			nor3.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: H; B: ^; C: L; no event");
			nor3.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: H; B: -->H; C: L; no event");
			nor3.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: H; B: v; C: L; no event");
			nor3.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: H; B: -->L; C: L; no event");

			// setup
			nor3.A.V = VoltageSignal.HIGH;
			nor3.B.V = VoltageSignal.LOW;
			nor3.C.V = VoltageSignal.HIGH;
			fired = false;
			// test
			nor3.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: H; B: ^; C: H; no event");
			nor3.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: H; B: -->H; C: H; no event");
			nor3.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: H; B: v; C: H; no event");
			nor3.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: H; B: -->L; C: H; no event");

			// setup
			nor3.A.V = VoltageSignal.LOW;
			nor3.B.V = VoltageSignal.LOW;
			nor3.C.V = VoltageSignal.HIGH;
			fired = false;
			// test
			nor3.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: L; B: ^; C: H; no event");
			nor3.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: L; B: -->H; C: H; no event");
			nor3.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: L; B: v; C: H; no event");
			nor3.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: L; B: -->L; C: H; no event");


			//
			// TEST C
			//
			// setup
			nor3.A.V = VoltageSignal.HIGH;
			nor3.B.V = VoltageSignal.HIGH;
			nor3.C.V = VoltageSignal.LOW;
			fired = false;
			// test
			nor3.C.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: H; B: H; C: ^; no event");
			nor3.C.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: H; B: H; C: -->H; no event");
			nor3.C.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: H; B: H; C: v; no event");
			nor3.C.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: H; B: H; C: -->L; no event");

			// setup
			nor3.A.V = VoltageSignal.HIGH;
			nor3.B.V = VoltageSignal.LOW;
			nor3.C.V = VoltageSignal.LOW;
			fired = false;
			// test
			nor3.C.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: H; B: L; C: ^; no event");
			nor3.C.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: H; B: L; C: -->H; no event");
			nor3.C.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: H; B: L; C: v; no event");
			nor3.C.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: H; B: L; C: -->L; no event");

			// setup
			nor3.A.V = VoltageSignal.LOW;
			nor3.B.V = VoltageSignal.HIGH;
			nor3.C.V = VoltageSignal.LOW;
			fired = false;
			// test
			nor3.C.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: L; B: H; C: ^; no event");
			nor3.C.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: L; B: H; C: -->H; no event");
			nor3.C.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: L; B: H; C: v; no event");
			nor3.C.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: L; B: H; C: -->L; no event");
		}
	}
}
