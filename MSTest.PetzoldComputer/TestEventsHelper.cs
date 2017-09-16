using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetzoldComputer;

namespace MSTest.PetzoldComputer
{
	class TestEventsHelper
	{
		public TestEventsHelper(IOutput oe)
		{
			ResetStatus();
			oe.AddOutputHandler(_ => { EventStatus = "fired"; });
		}

		public TestEventsHelper(ISum se)
		{
			ResetStatus();
			se.AddSumHandler(_ => { EventStatus = "fired"; });
		}

		public TestEventsHelper(ICarry ce)
		{
			ResetStatus();
			ce.AddCarryHandler(_ => { EventStatus = "fired"; });
		}

		public string EventStatus { get; private set; }

		public void ResetStatus()
		{
			EventStatus = "not fired";
		}
	}
}
