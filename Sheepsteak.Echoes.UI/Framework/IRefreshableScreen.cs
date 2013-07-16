using Caliburn.Micro;
using System.Threading.Tasks;

namespace Sheepsteak.Echoes.UI.Framework
{
    public interface IRefreshableScreen : IScreen
    {
        bool IsRefreshing { get; }

        Task RefreshArticles();
    }
}
