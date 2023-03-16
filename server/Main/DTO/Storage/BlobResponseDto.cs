using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Main.DTO
{
    public class BlobResponseDto
    {
        public string? Status { get; set; }
        public bool Error { get; set; }
        public BlobDto Blob { get; set; }

        public BlobResponseDto()
        {
            Blob = new BlobDto();
        }
    }
}