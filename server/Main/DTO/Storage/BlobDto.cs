using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Main.DTO
{
    public class BlobDto
    {
        public string? Uri { get; set; }
        public string? Name { get; set; }
        public string? ContentType { get; set; }
        public Stream? Content { get; set; }
    }
}