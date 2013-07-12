using Sheepsteak.Echo.Core;
using System.Collections.Generic;

namespace Sheepsteak.Echo.Framework
{
    public class ArticleCache : Dictionary<int, Article>
    {
        public Article GetFromCache(int key)
        {
            if (this.ContainsKey(key))
            {
                return this[key];
            }

            return null;
        }
    }
}
