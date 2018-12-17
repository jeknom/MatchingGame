using System;

namespace MatchingGame
{
	public class InvalidGridException : Exception
	{
		public InvalidGridException(string operation)
			: base(operation)
		{

		}
	}
}