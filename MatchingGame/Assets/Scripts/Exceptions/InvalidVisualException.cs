using System;

namespace MatchingGame
{
	public class InvalidVisualException : Exception
	{
		public InvalidVisualException(string operation)
			: base(operation)
		{

		}
	}
}