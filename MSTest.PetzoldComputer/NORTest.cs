﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
    [TestClass]
    public class NORTest
    {
        [TestMethod]
        public void NOR_Constructor()
        {
            // arrange, act
            var nor = new NOR("test");

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
        public void NOR(VoltageSignal voltage, VoltageSignal a, VoltageSignal b, VoltageSignal expected)
        {
            // arrage
            var nor = new NOR("test");

            // act
            nor.V.V = voltage;
            nor.A.V = a;
            nor.B.V = b;

            // assert
            Assert.AreEqual(expected, nor.O.V, $"V:{nor.V}; A:{nor.A}; B:{nor.B}");
        }

        [TestMethod]
        public void NOR_Events_GateOn()
        {
            // arrange
            var nor = new NOR("test");
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
        public void NOR_Events_GateOff()
        {
            // arrange
            var nor = new NOR("test");
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
        public void NOR_Event_GateOnOff()
        {
            // arrange
            var nor = new NOR("test");
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
