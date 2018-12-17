using System;

namespace MatchingGame
{
	public class InvalidBlockException : Exception
	{
		public InvalidBlockException(string operation)
			: base(operation)
		{

		}
	}
}