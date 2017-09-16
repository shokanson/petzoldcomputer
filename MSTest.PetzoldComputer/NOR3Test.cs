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

			Assert.AreEqual(nor3.Voltage, VoltageSignal.LOW, "Constructor: Voltage");
			Assert.AreEqual(nor3.A, VoltageSignal.LOW, "Constructor: A");
			Assert.AreEqual(nor3.B, VoltageSignal.LOW, "Constructor: B");
			Assert.AreEqual(nor3.C, VoltageSignal.LOW, "Constructor: B");
			Assert.AreEqual(nor3.O, VoltageSignal.LOW, "Constructor: O");
			Assert.AreEqual(nor3.ToString(), "LOW", "Constructor: ToString()");
		}

		[TestMethod]
		public void TestOutput()
		{
			INor3 nor3 = new NOR3();

			nor3.Voltage = VoltageSignal.HIGH;
			Assert.AreEqual(nor3.O, VoltageSignal.HIGH, "Gate on -- A: L; B: L; C: L; O: H");
			nor3.A = VoltageSignal.HIGH;
			Assert.AreEqual(nor3.O, VoltageSignal.LOW, "Gate on -- A: H; B: L; C: L; O: L");
			nor3.B = VoltageSignal.HIGH;
			Assert.AreEqual(nor3.O, VoltageSignal.LOW, "Gate on -- A: H; B: H; C: L; O: L");
			nor3.A = VoltageSignal.LOW;
			Assert.AreEqual(nor3.O, VoltageSignal.LOW, "Gate on -- A: L; B: H; C: L; O: L");
			nor3.C = VoltageSignal.HIGH;
			Assert.AreEqual(nor3.O, VoltageSignal.LOW, "Gate on -- A: L; B: H; C: H; O: L");
			nor3.B = VoltageSignal.LOW;
			Assert.AreEqual(nor3.O, VoltageSignal.LOW, "Gate on -- A: L; B: L; C: H; O: L");
			nor3.A = VoltageSignal.HIGH;
			Assert.AreEqual(nor3.O, VoltageSignal.LOW, "Gate on -- A: H; B: L; C: H; O: L");
			nor3.B = VoltageSignal.HIGH;
			Assert.AreEqual(nor3.O, VoltageSignal.LOW, "Gate on -- A: H; B: H; C: H; O: L");

			nor3.Voltage = VoltageSignal.LOW;
			nor3.A = VoltageSignal.LOW;
			nor3.B = VoltageSignal.LOW;
			nor3.C = VoltageSignal.LOW;
			Assert.AreEqual(nor3.O, VoltageSignal.LOW, "Gate off -- A: L; B: L; C: L; O: L");
			nor3.A = VoltageSignal.HIGH;
			Assert.AreEqual(nor3.O, VoltageSignal.LOW, "Gate off -- A: H; B: L; C: L; O: L");
			nor3.B = VoltageSignal.HIGH;
			Assert.AreEqual(nor3.O, VoltageSignal.LOW, "Gate off -- A: H; B: H; C: L; O: L");
			nor3.A = VoltageSignal.LOW;
			Assert.AreEqual(nor3.O, VoltageSignal.LOW, "Gate off -- A: L; B: H; C: L; O: L");
			nor3.C = VoltageSignal.HIGH;
			Assert.AreEqual(nor3.O, VoltageSignal.LOW, "Gate off -- A: L; B: H; C: H; O: L");
			nor3.B = VoltageSignal.LOW;
			Assert.AreEqual(nor3.O, VoltageSignal.LOW, "Gate off -- A: L; B: L; C: H; O: L");
			nor3.A = VoltageSignal.HIGH;
			Assert.AreEqual(nor3.O, VoltageSignal.LOW, "Gate off -- A: H; B: L; C: H; O: L");
			nor3.B = VoltageSignal.HIGH;
			Assert.AreEqual(nor3.O, VoltageSignal.LOW, "Gate off -- A: H; B: H; C: H; O: L");
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
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: ^; B: L; C: L; event");
			helper.ResetStatus();
			nor3.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: -->H; B: L; C: L; no event");
			nor3.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: v; B: L; C: L; event");
			helper.ResetStatus();
			nor3.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: -->L; B: L; C: L; no event");
			nor3.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: L; B: ^; C: L; event");
			helper.ResetStatus();
			nor3.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: -->H; C: L; no event");
			nor3.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: L; B: v; C: L; event");
			helper.ResetStatus();
			nor3.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: -->L; C: L; no event");
			nor3.C = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: L; B: L; C: ^; event");
			helper.ResetStatus();
			nor3.C = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: L; C: -->H; no event");
			nor3.C = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: L; B: L; C: v; event");
			helper.ResetStatus();
			nor3.C = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: L; C: -->L; no event");

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
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: ^; B: H; C: L; no event");
			nor3.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: -->H; B: H; C: L; no event");
			nor3.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: v; B: H; C: L; no event");
			nor3.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: -->L; B: H; C: L; no event");

			// setup
			nor3.A = VoltageSignal.LOW;
			nor3.B = VoltageSignal.HIGH;
			nor3.C = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			nor3.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: ^; B: H; C: H; no event");
			nor3.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: -->H; B: H; C: H; no event");
			nor3.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: v; B: H; C: H; no event");
			nor3.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: -->L; B: H; C: H; no event");

			// setup
			nor3.A = VoltageSignal.LOW;
			nor3.B = VoltageSignal.LOW;
			nor3.C = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			nor3.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: ^; B: L; C: H; no event");
			nor3.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: -->H; B: L; C: H; no event");
			nor3.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: v; B: L; C: H; no event");
			nor3.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: -->L; B: L; C: H; no event");

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
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: ^; C: L; no event");
			nor3.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: -->H; C: L; no event");
			nor3.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: v; C: L; no event");
			nor3.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: -->L; C: L; no event");

			// setup
			nor3.A = VoltageSignal.HIGH;
			nor3.B = VoltageSignal.LOW;
			nor3.C = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			nor3.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: ^; C: H; no event");
			nor3.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: -->H; C: H; no event");
			nor3.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: v; C: H; no event");
			nor3.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: -->L; C: H; no event");

			// setup
			nor3.A = VoltageSignal.LOW;
			nor3.B = VoltageSignal.LOW;
			nor3.C = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			nor3.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: ^; C: H; no event");
			nor3.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: -->H; C: H; no event");
			nor3.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: v; C: H; no event");
			nor3.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: -->L; C: H; no event");


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
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: H; C: ^; no event");
			nor3.C = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: H; C: -->H; no event");
			nor3.C = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: H; C: v; no event");
			nor3.C = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: H; C: -->L; no event");

			// setup
			nor3.A = VoltageSignal.HIGH;
			nor3.B = VoltageSignal.LOW;
			nor3.C = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			nor3.C = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: L; C: ^; no event");
			nor3.C = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: L; C: -->H; no event");
			nor3.C = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: L; C: v; no event");
			nor3.C = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: L; C: -->L; no event");

			// setup
			nor3.A = VoltageSignal.LOW;
			nor3.B = VoltageSignal.HIGH;
			nor3.C = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			nor3.C = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: H; C: ^; no event");
			nor3.C = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: H; C: -->H; no event");
			nor3.C = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: H; C: v; no event");
			nor3.C = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: H; C: -->L; no event");
		}
	}
}
