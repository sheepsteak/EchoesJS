using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sheepsteak.Echoes.UI.Framework
{
    public class CacheService : ICacheService
    {
        public CacheService()
        {

        }

        private ArticleCache articleCache;

        public ArticleCache Articles
        {
            get
            {
                if (articleCache == null)
                {
                    articleCache = new ArticleCache();
                }

                return articleCache;
            }
        }
    }

}
