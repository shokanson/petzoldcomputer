using System;
namespace PetzoldComputer
{
	public interface IOutput
	{
		void AddOutputHandler(Action<object> handler);
	}

	public interface ISum
	{
		void AddSumHandler(Action<object> handler);
	}

	public interface ICarry
	{
		void AddCarryHandler(Action<object> handler);
	}

	public interface IOverUnderFlow
	{
		void AddOverUnderFlowHandler(Action<object> handler);
	}
}

/*
$Log: /PetzoldComputer/Events.cs $ $NoKeyWords:$
 * 
 * 3     1/21/07 11:58p Sean
 * results of ReSharper analysis
*/
