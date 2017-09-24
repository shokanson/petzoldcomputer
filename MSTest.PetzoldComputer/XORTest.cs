using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	[TestClass]
	public class XORTest
	{
		[TestMethod]
		public void XOR_2_Constructor()
		{
			var xor = new XOR_2("test");

			Assert.AreEqual(VoltageSignal.LOW, xor.V.V, "Constructor: Voltage");
			Assert.AreEqual(VoltageSignal.LOW, xor.A.V, "Constructor: A");
			Assert.AreEqual(VoltageSignal.LOW, xor.B.V, "Constructor: B");
			Assert.AreEqual(VoltageSignal.LOW, xor.O.V, "Constructor: O");
			Assert.AreEqual("LOW", xor.ToString(), "Constructor: ToString()");
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
		[DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW)]
		#endregion
		public void XOR_2(VoltageSignal voltage, VoltageSignal a, VoltageSignal b, VoltageSignal expected)
		{
			// arrage
			var xor = new XOR_2("test");

			// act
			xor.V.V = voltage;
			xor.A.V = a;
			xor.B.V = b;

			// assert
			Assert.AreEqual(expected, xor.O.V, $"V:{xor.V}; A:{xor.A}; B:{xor.B}");
		}

		[TestMethod]
		public void XOR_2_Events_GateOn()
		{
			// arrange
			var xor = new XOR_2("test");
			xor.V.V = VoltageSignal.HIGH;
			bool fired = false;
			xor.O.Changed += _ => fired = true;

			// act, assert
			xor.B.V = VoltageSignal.HIGH;
			Assert.IsTrue(fired, "Gate on -- A: L; B: ^; event");
			fired = false;
			xor.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: L; B: -->H; no event");
			xor.B.V = VoltageSignal.LOW;
			Assert.IsTrue(fired, "Gate on -- A: L; B: v; event");
			fired = false;
			xor.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: L; B: -->L; no event");
			xor.A.V = VoltageSignal.HIGH;
			Assert.IsTrue(fired, "Gate on -- B: L; A: ^; event");
			fired = false;
			xor.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- B: L; A: -->H; no event");
			xor.A.V = VoltageSignal.LOW;
			Assert.IsTrue(fired, "Gate on -- B: L; A: v; event");
			fired = false;
			xor.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- B: L; A: -->L; no event");

			// setup
			xor.A.V = VoltageSignal.HIGH;
			xor.B.V = VoltageSignal.LOW;
			fired = false;
			// test
			xor.B.V = VoltageSignal.HIGH;
			Assert.IsTrue(fired, "Gate on -- A: H; B: ^; event");
			fired = false;
			xor.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- A: H; B: -->H; no event");
			xor.B.V = VoltageSignal.LOW;
			Assert.IsTrue(fired, "Gate on -- A: H; B: v; event");
			fired = false;
			xor.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- A: H; B: -->L; no event");

			// setup
			xor.A.V = VoltageSignal.LOW;
			xor.B.V = VoltageSignal.HIGH;
			fired = false;
			// test
			xor.A.V = VoltageSignal.HIGH;
			Assert.IsTrue(fired, "Gate on -- B: H; A: ^; event");
			fired = false;
			xor.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate on -- B: H; A: -->H; no event");
			xor.A.V = VoltageSignal.LOW;
			Assert.IsTrue(fired, "Gate on -- B: H; A: v; event");
			fired = false;
			xor.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate on -- B: H; A: -->L; no event");
		}

		[TestMethod]
		public void XOR_2_Events_GateOff()
		{
			// arrange
			var xor = new XOR_2("test");
			bool fired = false;
			xor.O.Changed += _ => fired = true;

			// act, assert
			xor.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate off -- A: L; B: ^; no event");
			xor.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate off -- A: L; B: -->H; no event");
			xor.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate off -- A: L; B: v; no event");
			xor.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate off -- A: L; B: -->L; no event");
			xor.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate off -- B: L; A: ^; no event");
			xor.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate off -- B: L; A: -->H; no event");
			xor.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate off -- B: L; A: v; no event");
			xor.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate off -- B: L; A: -->L; no event");

			// setup
			xor.A.V = VoltageSignal.HIGH;
			xor.B.V = VoltageSignal.LOW;
			// test
			xor.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate off -- A: H; B: ^; no event");
			xor.B.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate off -- A: H; B: -->H; no event");
			xor.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate off -- A: H; B: v; no event");
			xor.B.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate off -- A: H; B: -->L; no event");

			// setup
			xor.A.V = VoltageSignal.LOW;
			xor.B.V = VoltageSignal.HIGH;
			// test
			xor.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate off -- B: H; A: ^; no event");
			xor.A.V = VoltageSignal.HIGH;
			Assert.IsFalse(fired, "Gate off -- B: H; A: -->H; no event");
			xor.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate off -- B: H; A: v; no event");
			xor.A.V = VoltageSignal.LOW;
			Assert.IsFalse(fired, "Gate off -- B: H; A: -->L; no event");
		}
	}
}
