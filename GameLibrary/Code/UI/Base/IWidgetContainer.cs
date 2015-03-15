using System.Collections.Generic;

using Faseway.GameLibrary.UI;
using Faseway.GameLibrary.Game.Handlers;

namespace Faseway.GameLibrary.UI.Base
{
    public interface IWidgetContainer : IGameHandler
    {
        List<Widget> Widgets { get; }
    }
}
