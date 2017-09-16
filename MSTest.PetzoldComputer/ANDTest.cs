﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class ANDTest
	{
		/// <summary>
		///A test for AND Constructor
		///</summary>
		[TestMethod]
		public void ANDConstructor()
		{
			var and = new AND();

			Assert.AreEqual(and.Voltage, VoltageSignal.LOW, "AND Constructor: Voltage");
			Assert.AreEqual(and.A, VoltageSignal.LOW, "AND Constructor: A");
			Assert.AreEqual(and.B, VoltageSignal.LOW, "AND Constructor: B");
			Assert.AreEqual(and.O, VoltageSignal.LOW, "AND Constructor: O");
			Assert.AreEqual(and.ToString(), "LOW", "AND Constructor: ToString()");
		}

		[TestMethod]
		public void TestOutput()
		{
			var and = new AND();

			and.Voltage = VoltageSignal.HIGH;
			Assert.AreEqual(and.O, VoltageSignal.LOW, "Gate on -- A: L; B: L; O: L");
			and.A = VoltageSignal.HIGH;
			Assert.AreEqual(and.O, VoltageSignal.LOW, "Gate on -- A: H; B: L; O: L");
			and.B = VoltageSignal.HIGH;
			Assert.AreEqual(and.O, VoltageSignal.HIGH, "Gate on -- A: H; B: H; O: H");
			and.A = VoltageSignal.LOW;
			Assert.AreEqual(and.O, VoltageSignal.LOW, "Gate on -- A: L; B: H; O: L");

			and.Voltage = VoltageSignal.LOW;
			and.B = VoltageSignal.LOW;
			Assert.AreEqual(and.O, VoltageSignal.LOW, "Gate off -- A: L; B: L; O: L");
			and.A = VoltageSignal.HIGH;
			Assert.AreEqual(and.O, VoltageSignal.LOW, "Gate off -- A: H; B: L; O: L");
			and.B = VoltageSignal.HIGH;
			Assert.AreEqual(and.O, VoltageSignal.LOW, "Gate off -- A: H; B: H; O: L");
			and.A = VoltageSignal.LOW;
			Assert.AreEqual(and.O, VoltageSignal.LOW, "Gate off -- A: L; B: H; O: L");
		}

		[TestMethod]
		public void TestOutputEventsGateOn()
		{
			IAnd and2 = new AND();

			TestEventsHelper helper = new TestEventsHelper((IOutput)and2);

			// setup
			and2.Voltage = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			and2.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: ^; no event");
			and2.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: -->H; no event");
			and2.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: v; no event");
			and2.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: L; B: -->L; no event");
			and2.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: L; A: ^; no event");
			and2.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: L; A: -->H; no event");
			and2.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: L; A: v; no event");
			and2.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: L; A: -->L; no event");

			// setup
			and2.A = VoltageSignal.HIGH;
			and2.B = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			and2.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: H; B: ^; event");
			helper.ResetStatus();
			and2.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: -->H; no event");
			and2.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- A: H; B: v; event");
			helper.ResetStatus();
			and2.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- A: H; B: -->L; no event");

			// setup
			and2.A = VoltageSignal.LOW;
			and2.B = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			and2.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- B: H; A: ^; event");
			helper.ResetStatus();
			and2.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: H; A: -->H; no event");
			and2.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "fired", "Gate on -- B: H; A: v; event");
			helper.ResetStatus();
			and2.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate on -- B: H; A: -->L; no event");
		}

		[TestMethod]
		public void TestOutputEventsGateOff()
		{
			IAnd and2 = new AND();

			TestEventsHelper helper = new TestEventsHelper((IOutput)and2);

			and2.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- A: L; B: ^; no event");
			and2.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- A: L; B: -->H; no event");
			and2.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- A: L; B: v; no event");
			and2.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- A: L; B: -->L; no event");
			and2.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- B: L; A: ^; no event");
			and2.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- B: L; A: -->H; no event");
			and2.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- B: L; A: v; no event");
			and2.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- B: L; A: -->L; no event");

			// setup
			and2.A = VoltageSignal.HIGH;
			and2.B = VoltageSignal.LOW;
			helper.ResetStatus();
			// test
			and2.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- A: H; B: ^; no event");
			and2.B = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- A: H; B: -->H; no event");
			and2.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- A: H; B: v; no event");
			and2.B = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- A: H; B: -->L; no event");

			// setup
			and2.A = VoltageSignal.LOW;
			and2.B = VoltageSignal.HIGH;
			helper.ResetStatus();
			// test
			and2.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- B: H; A: ^; no event");
			and2.A = VoltageSignal.HIGH;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- B: H; A: -->H; no event");
			and2.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- B: H; A: v; no event");
			and2.A = VoltageSignal.LOW;
			Assert.AreEqual(helper.EventStatus, "not fired", "Gate off -- B: H; A: -->L; no event");
		}
	}
}