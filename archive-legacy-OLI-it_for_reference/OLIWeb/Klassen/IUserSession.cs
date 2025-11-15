using OliEngine.OliMiddleTier.OLIs;

namespace OliWeb.Klassen
{
    /// <summary>
    ///     Represents the current user context for the application.
    /// </summary>
    public interface IUserSession
    {
        /// <summary>
        ///     Gets the current <see cref="OliUser" />.
        /// </summary>
        OliUser OliUser { get; }
    }
}

