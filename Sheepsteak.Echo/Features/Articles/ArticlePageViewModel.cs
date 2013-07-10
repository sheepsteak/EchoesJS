using Caliburn.Micro;
using Sheepsteak.Echo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sheepsteak.Echo.Features.Articles
{
    public class ArticlePageViewModel:Screen
    {
        public ArticlePageViewModel()
        {

        }

        public Article Article { get; set; }

        protected override void OnActivate()
        {
            base.OnActivate();
        }
    }
}
