using Socius.Dto.Views.UserInteraction;

namespace Socius.Extensions;

public static class SociusExtensions
{
	public static T ThrowIfNull<T>(this T source, string errorMessage)
	{
		if (source != null)
		{
			return source;
		}

		throw new ArgumentNullException(nameof(source), errorMessage);
	}

}
