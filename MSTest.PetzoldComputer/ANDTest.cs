using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
    [TestClass]
    public class ANDTest
    {
        [TestMethod]
        public void AND_Constructor()
        {
            // arrange, act
            var and = new AND("test");

            // assert
            Assert.AreEqual(VoltageSignal.LOW, and.V.V, "AND Constructor: Voltage");
            Assert.AreEqual(VoltageSignal.LOW, and.A.V, "AND Constructor: A");
            Assert.AreEqual(VoltageSignal.LOW, and.B.V, "AND Constructor: B");
            Assert.AreEqual(VoltageSignal.LOW, and.O.V, "AND Constructor: O");
            Assert.AreEqual("LOW", and.ToString(), "AND Constructor: ToString()");
        }

        [DataTestMethod]
        #region data
        [DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW)]
        [DataRow(VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW)]
        [DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW)]
        [DataRow(VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW)]
        [DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW, VoltageSignal.LOW)]
        [DataRow(VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.HIGH, VoltageSignal.LOW)]
        [DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.LOW, VoltageSignal.LOW)]
        [DataRow(VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.HIGH, VoltageSignal.HIGH)]
        #endregion
        public void AND(VoltageSignal voltage, VoltageSignal a, VoltageSignal b, VoltageSignal expected)
        {
            // arrage
            var and = new AND("test");

            // act
            and.V.V = voltage;
            and.A.V = a;
            and.B.V = b;

            // assert
            Assert.AreEqual(expected, and.O.V, $"V:{and.V}; A:{and.A}; B:{and.B}");
        }

        [TestMethod]
        public void AND_Events_GateOn()
        {
            // arrange
            var and = new AND("test");
            and.V.V = VoltageSignal.HIGH;
            bool fired = false;
            and.O.Changed += _ => fired = true;

            // act, assert
            and.B.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate on -- A: L; B: ^; no event");
            and.B.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate on -- A: L; B: -->H; no event");
            and.B.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate on -- A: L; B: v; no event");
            and.B.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate on -- A: L; B: -->L; no event");
            and.A.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate on -- B: L; A: ^; no event");
            and.A.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate on -- B: L; A: -->H; no event");
            and.A.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate on -- B: L; A: v; no event");
            and.A.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate on -- B: L; A: -->L; no event");

            // setup
            and.A.V = VoltageSignal.HIGH;
            and.B.V = VoltageSignal.LOW;
            fired = false;
            // test
            and.B.V = VoltageSignal.HIGH;
            Assert.IsTrue(fired, "Gate on -- A: H; B: ^; event");
            fired = false;
            and.B.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate on -- A: H; B: -->H; no event");
            and.B.V = VoltageSignal.LOW;
            Assert.IsTrue(fired, "Gate on -- A: H; B: v; event");
            fired = false;
            and.B.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate on -- A: H; B: -->L; no event");

            // setup
            and.A.V = VoltageSignal.LOW;
            and.B.V = VoltageSignal.HIGH;
            fired = false;
            // test
            and.A.V = VoltageSignal.HIGH;
            Assert.IsTrue(fired, "Gate on -- B: H; A: ^; event");
            fired = false;
            and.A.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate on -- B: H; A: -->H; no event");
            and.A.V = VoltageSignal.LOW;
            Assert.IsTrue(fired, "Gate on -- B: H; A: v; event");
            fired = false;
            and.A.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate on -- B: H; A: -->L; no event");
        }

        [TestMethod]
        public void AND_Events_GateOff()
        {
            // arrange
            var and = new AND("test");
            bool fired = false;
            and.O.Changed += _ => fired = true;

            // act, assert
            and.B.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate off -- A: L; B: ^; no event");
            and.B.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate off -- A: L; B: -->H; no event");
            and.B.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate off -- A: L; B: v; no event");
            and.B.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate off -- A: L; B: -->L; no event");
            and.A.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate off -- B: L; A: ^; no event");
            and.A.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate off -- B: L; A: -->H; no event");
            and.A.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate off -- B: L; A: v; no event");
            and.A.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate off -- B: L; A: -->L; no event");

            // reset
            and.A.V = VoltageSignal.HIGH;
            and.B.V = VoltageSignal.LOW;

            and.B.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate off -- A: H; B: ^; no event");
            and.B.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate off -- A: H; B: -->H; no event");
            and.B.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate off -- A: H; B: v; no event");
            and.B.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate off -- A: H; B: -->L; no event");

            // reset
            and.A.V = VoltageSignal.LOW;
            and.B.V = VoltageSignal.HIGH;

            and.A.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate off -- B: H; A: ^; no event");
            and.A.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "Gate off -- B: H; A: -->H; no event");
            and.A.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate off -- B: H; A: v; no event");
            and.A.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "Gate off -- B: H; A: -->L; no event");
        }

        [TestMethod]
        public void AND_Event_GateOnOff()
        {
            // arrange
            var and = new AND("test");
            bool fired = false;
            and.O.Changed += _ => fired = true;

            // act, assert
            and.V.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "A: L; B: L; V: ^; no event");
            and.V.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "A: L; B: L; V: --->H; no event");
            and.V.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "A: L; B: L; V: v; no event");
            and.V.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "A: L; B: L; V: --->L; no event");

            and.A.V = VoltageSignal.HIGH;
            and.V.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "A: H; B: L; V: ^; no event");
            and.V.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "A: H; B: L; V: --->H; no event");
            and.V.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "A: H; B: L; V: v; no event");
            and.V.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "A: H; B: L; V: --->L; no event");

            and.A.V = VoltageSignal.LOW;
            and.B.V = VoltageSignal.HIGH;
            and.V.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "A: L; B: H; V: ^; no event");
            and.V.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "A: L; B: H; V: --->H; no event");
            and.V.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "A: L; B: H; V: v; no event");
            and.V.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "A: L; B: H; V: --->L; no event");

            and.A.V = VoltageSignal.HIGH;
            and.V.V = VoltageSignal.HIGH;
            Assert.IsTrue(fired, "A: H; B: H; V: ^; event");
            fired = false;
            and.V.V = VoltageSignal.HIGH;
            Assert.IsFalse(fired, "A: H; B: H; V: --->H; no event");
            and.V.V = VoltageSignal.LOW;
            Assert.IsTrue(fired, "A: H; B: H; V: v; event");
            fired = false;
            and.V.V = VoltageSignal.LOW;
            Assert.IsFalse(fired, "A: H; B: H; V: --->L; no event");
        }
    }
}