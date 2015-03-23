namespace Faseway.GameLibrary.Game.Handlers
{
    public interface ISortableUpdateHandler : IUpdateHandler
    {
        /// <summary>
        /// Gets the index of the tick. Default: 0.
        /// </summary>
        int TickIndex { get; }
    }
}
