using Caliburn.Micro;
using System.Threading.Tasks;

namespace Sheepsteak.EchoesJS.UI.Framework
{
    public interface IRefreshableScreen : IScreen
    {
        bool IsRefreshing { get; }

        Task RefreshArticles();
    }
}
