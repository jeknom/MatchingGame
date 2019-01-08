using System;

namespace MatchingGame
{
	public class InvalidEventException : Exception
	{
		public InvalidEventException(string operation)
			: base(operation)
		{

		}
	}
}