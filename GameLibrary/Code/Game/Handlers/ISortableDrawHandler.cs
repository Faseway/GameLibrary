namespace Faseway.GameLibrary.Game.Handlers
{
    public interface ISortableDrawHandler : IDrawHandler
    {
        /// <summary>
        /// Gets the index of the render. Default: 0.
        /// </summary>
        int RenderIndex { get; }
    }
}
