using System;

namespace Herobook.Workshop.Data.Entities {
    public class Photo {
        public Guid PhotoId { get; set; }
        public string Username { get; set; }
        public string Caption { get; set; }
        public DateTimeOffset PostedAt { get; set; }
    }
}
