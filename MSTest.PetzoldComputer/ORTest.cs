using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class ORTest
	{
		[TestMethod]
		public void OR_2_Constructor()
		{
			var or = new OR_2("test");

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
			var or = new OR_2("test");

			// act
			or.V.V = voltage;
			or.A.V = a;
			or.B.V = b;

			// assert
			Assert.AreEqual(expected, or.O.V, $"V:{or.V}; A:{or.A}; B:{or.B}");
		}

		[TestMethod]
		public void OR_2_Events_GateOn()
		{
			// arrage
			var or = new OR_2("test");
			or.V.V = VoltageSignal.HIGH;	// turn on the gate
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

		[TestMethod]
		public void OR_2_Events_GateOff()
		{
			// arrange
			var or = new OR_2("test");
			bool fired = false;
			or.O.Changed += _ => fired = true;

			// act, assert
			or.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate off -- A: L; B: ^; no event");
			or.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate off -- A: L; B: -->H; no event");
			or.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate off -- A: L; B: v; no event");
			or.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate off -- A: L; B: -->L; no event");
			or.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate off -- B: L; A: ^; no event");
			or.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate off -- B: L; A: -->H; no event");
			or.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate off -- B: L; A: v; no event");
			or.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate off -- B: L; A: -->L; no event");

			// reset
			or.A.V = VoltageSignal.HIGH;
			or.B.V = VoltageSignal.LOW;
			
			or.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate off -- A: H; B: ^; no event");
			or.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate off -- A: H; B: -->H; no event");
			or.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate off -- A: H; B: v; no event");
			or.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate off -- A: H; B: -->L; no event");

			// reset
			or.A.V = VoltageSignal.LOW;
			or.B.V = VoltageSignal.HIGH;

			or.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate off -- B: H; A: ^; no event");
			or.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate off -- B: H; A: -->H; no event");
			or.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate off -- B: H; A: v; no event");
			or.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate off -- B: H; A: -->L; no event");
		}

		[TestMethod]
		public void OR_2_Event_GateOnOff()
		{
			// arrange
			var or = new OR_2("test");
			bool fired = false;
			or.O.Changed += _ => fired = true;

			// act, assert
			or.V.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "A: L; B: L; V: ^; no event");
			or.V.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "A: L; B: L; V: --->H; no event");
			or.V.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "A: L; B: L; V: v; no event");
			or.V.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "A: L; B: L; V: --->L; no event");

			or.A.V = VoltageSignal.HIGH;
			or.V.V = VoltageSignal.HIGH;
			Assert.IsTrue(fired, "A: H; B: L; V: ^; event");
			fired = false;
			or.V.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "A: H; B: L; V: --->H; no event");
			or.V.V = VoltageSignal.LOW;
			Assert.IsTrue(fired, "A: H; B: L; V: v; event");
			fired = false;
			or.V.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "A: H; B: L; V: --->L; no event");

			or.A.V = VoltageSignal.LOW;
			or.B.V = VoltageSignal.HIGH;
			or.V.V = VoltageSignal.HIGH;
			Assert.IsTrue(fired, "A: L; B: H; V: ^; event");
			fired = false;
			or.V.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "A: L; B: H; V: --->H; no event");
			or.V.V = VoltageSignal.LOW;
			Assert.IsTrue(fired, "A: L; B: H; V: v; event");
			fired = false;
			or.V.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "A: L; B: H; V: --->L; no event");

			or.A.V = VoltageSignal.HIGH;
			or.V.V = VoltageSignal.HIGH;
			Assert.IsTrue(fired, "A: H; B: H; V: ^; event");
			fired = false;
			or.V.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "A: H; B: H; V: --->H; no event");
			or.V.V = VoltageSignal.LOW;
			Assert.IsTrue(fired, "A: H; B: H; V: v; event");
			fired = false;
			or.V.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "A: H; B: H; V: --->L; no event");
		}
	}
}
