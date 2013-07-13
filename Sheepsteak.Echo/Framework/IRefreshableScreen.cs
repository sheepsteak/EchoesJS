using Caliburn.Micro;
using System.Threading.Tasks;

namespace Sheepsteak.Echo.Framework
{
    public interface IRefreshableScreen : IScreen
    {
        bool IsRefreshing { get; }

        Task RefreshArticles();
    }
}
