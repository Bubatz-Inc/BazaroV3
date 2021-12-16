using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazaro.Core.Services.Models
{
    public record class EntryTagModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
