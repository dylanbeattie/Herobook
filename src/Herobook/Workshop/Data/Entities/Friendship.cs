using System;
using System.Linq;
using Newtonsoft.Json;

namespace Herobook.Workshop.Data.Entities {
    public class Friendship {
        public Friendship() { }

        public Friendship(params string[] names) {
            Names = names;
        }

        public string[] Names { get; set; }
    }
}
