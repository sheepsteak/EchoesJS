using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sheepsteak.Echo.Model
{
    public class Article
    {
        public int Id { get; set; }

        public string Description
        {
            get { return this.UpVotes + " up and " + this.DownVotes + " down, posted by " + this.Username + " " + this.PostedAt.ToShortTimeString(); }
        }

        public int DownVotes { get; set; }

        public DateTime PostedAt { get; set; }

        public string Title { get; set; }

        public int UpVotes { get; set; }

        public string Url { get; set; }

        public string Username { get; set; }
    }
}
