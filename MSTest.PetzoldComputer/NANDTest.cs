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
        public void NAND_Constructor()
        {
            var nand = new NAND("test");

            Assert.AreEqual(VoltageSignal.LOW, nand.V.V, "NAND Constructor: Voltage");
            Assert.AreEqual(VoltageSignal.LOW, nand.A.V, "NAND Constructor: A");
            Assert.AreEqual(VoltageSignal.LOW, nand.B.V, "NAND Constructor: B");
            Assert.AreEqual(VoltageSignal.LOW, nand.O.V, "NAND Constructor: O");
            Assert.AreEqual("LOW", nand.ToString(), "NAND Constructor: ToString()");
        }

        [DataTestMethod]
        #region data
        [DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW)]
        [DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW)]
        [DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW)]
        [DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW)]
        [DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.HIGH)]
        [DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.HIGH)]
        [DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.HIGH)]
        [DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW)]
        #endregion
        public void NAND(VoltageSignal voltage, VoltageSignal a, VoltageSignal b, VoltageSignal expected)
        {
            // arrage
            var nand = new NAND("test");

            // act
            nand.V.V = voltage;
            nand.A.V = a;
            nand.B.V = b;

            // assert
            Assert.AreEqual(expected, nand.O.V, $"V:{nand.V}; A:{nand.A}; B:{nand.B}");
        }

        [TestMethod]
        public void NAND_Events_GateOn()
        {
            // arrange
            var nand = new NAND("test");
            nand.V.V = VoltageSignal.HIGH;
            bool fired = false;
            nand.O.Changed += _ => fired = true;

            // act, assert
            nand.B.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate on -- A: L; B: ^; no event");
            nand.B.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate on -- A: L; B: -->H; no event");
            nand.B.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate on -- A: L; B: v; no event");
            nand.B.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate on -- A: L; B: -->L; no event");
            nand.A.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate on -- B: L; A: ^; no event");
            nand.A.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate on -- B: L; A: -->H; no event");
            nand.A.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate on -- B: L; A: v; no event");
            nand.A.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate on -- B: L; A: -->L; no event");

            // setup
            nand.A.V = VoltageSignal.HIGH;
            nand.B.V = VoltageSignal.LOW;
            fired = false;
            // test
            nand.B.V = VoltageSignal.HIGH;
            Assert.IsTrue(fired, "Gate on -- A: H; B: ^; event");
            fired = false;
            nand.B.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate on -- A: H; B: -->H; no event");
            nand.B.V = VoltageSignal.LOW;
            Assert.IsTrue(fired, "Gate on -- A: H; B: v; event");
            fired = false;
            nand.B.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate on -- A: H; B: -->L; no event");

            // setup
            nand.A.V = VoltageSignal.LOW;
            nand.B.V = VoltageSignal.HIGH;
            fired = false;
            // test
            nand.A.V = VoltageSignal.HIGH;
            Assert.IsTrue(fired, "Gate on -- B: H; A: ^; event");
            fired = false;
            nand.A.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate on -- B: H; A: -->H; no event");
            nand.A.V = VoltageSignal.LOW;
            Assert.IsTrue(fired, "Gate on -- B: H; A: v; event");
            fired = false;
            nand.A.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate on -- B: H; A: -->L; no event");
        }

        [TestMethod]
        public void AND_Events_GateOff()
        {
            // arrange
            var nand = new NAND("test");
            bool fired = false;
            nand.O.Changed += _ => fired = true;

            // act, assert
            nand.B.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate off -- A: L; B: ^; no event");
            nand.B.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate off -- A: L; B: -->H; no event");
            nand.B.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate off -- A: L; B: v; no event");
            nand.B.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate off -- A: L; B: -->L; no event");
            nand.A.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate off -- B: L; A: ^; no event");
            nand.A.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate off -- B: L; A: -->H; no event");
            nand.A.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate off -- B: L; A: v; no event");
            nand.A.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate off -- B: L; A: -->L; no event");

            // reset
            nand.A.V = VoltageSignal.HIGH;
            nand.B.V = VoltageSignal.LOW;

            nand.B.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate off -- A: H; B: ^; no event");
            nand.B.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate off -- A: H; B: -->H; no event");
            nand.B.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate off -- A: H; B: v; no event");
            nand.B.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate off -- A: H; B: -->L; no event");

            // reset
            nand.A.V = VoltageSignal.LOW;
            nand.B.V = VoltageSignal.HIGH;

            nand.A.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate off -- B: H; A: ^; no event");
            nand.A.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate off -- B: H; A: -->H; no event");
            nand.A.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate off -- B: H; A: v; no event");
            nand.A.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate off -- B: H; A: -->L; no event");
        }

        [TestMethod]
        public void NAND_Event_GateOnOff()
        {
            // arrange
            var nand = new NAND("test");
            bool fired = false;
            nand.O.Changed += _ => fired = true;

            // act, assert
            nand.V.V = VoltageSignal.HIGH;
            Assert.IsTrue(fired, "A: L; B: L; V: ^; event");
            fired = false;
            nand.V.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "A: L; B: L; V: --->H; no event");
            nand.V.V = VoltageSignal.LOW;
            Assert.IsTrue(fired, "A: L; B: L; V: v; event");
            fired = false;
            nand.V.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "A: L; B: L; V: --->L; no event");

            nand.A.V = VoltageSignal.HIGH;
            fired = false;
            nand.V.V = VoltageSignal.HIGH;
            Assert.IsTrue(fired, "A: H; B: L; V: ^; event");
            fired = false;
            nand.V.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "A: H; B: L; V: --->H; no event");
            nand.V.V = VoltageSignal.LOW;
            Assert.IsTrue(fired, "A: H; B: L; V: v; event");
            fired = false;
            nand.V.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "A: H; B: L; V: --->L; no event");

            nand.A.V = VoltageSignal.LOW;
            nand.B.V = VoltageSignal.HIGH;
            fired = false;
            nand.V.V = VoltageSignal.HIGH;
            Assert.IsTrue(fired, "A: L; B: H; V: ^; event");
            fired = false;
            nand.V.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "A: L; B: H; V: --->H; no event");
            nand.V.V = VoltageSignal.LOW;
            Assert.IsTrue(fired, "A: L; B: H; V: v; event");
            fired = false;
            nand.V.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "A: L; B: H; V: --->L; no event");

            nand.A.V = VoltageSignal.HIGH;
            nand.V.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "A: H; B: H; V: ^; no event");
            nand.V.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "A: H; B: H; V: --->H; no event");
            nand.V.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "A: H; B: H; V: v; no event");
            nand.V.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "A: H; B: H; V: --->L; no event");
        }
    }
}
