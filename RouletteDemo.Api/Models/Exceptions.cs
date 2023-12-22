using System.Runtime.Serialization;

namespace RouletteDemo.Api.Models
{
	[Serializable]
	public class RouletteException : Exception
	{
		public RouletteException()
		{
		}

		public RouletteException(string message) : base(message)
		{
		}

		public RouletteException(string message, Exception inner) : base(message, inner)
		{
		}

		protected RouletteException(
		  SerializationInfo info,
		  StreamingContext context)
			: base(info, context) { }
	}

    [Serializable]
	public class RequestException : RouletteException
	{
		public RequestException()
		{
		}

		public RequestException(string message) : base(message)
		{
		}

		public RequestException(string message, Exception inner) : base(message, inner)
		{
		}

		protected RequestException(
		  SerializationInfo info,
		  StreamingContext context)
			: base(info, context) { }
	}
}