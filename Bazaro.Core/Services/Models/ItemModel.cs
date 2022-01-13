using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazaro.Core.Services.Models
{
    public record class ItemModel
    {
        public ItemModel? NextItem { get; set; }

        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}
