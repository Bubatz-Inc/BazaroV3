using Bazaro.Data.Models.Base;

namespace Bazaro.Core.Models
{
    public class HeaderModel
    {
        public int Id { get; set; }
        public HeaderType Type { get; set; }
        public byte[] Content { get; set; }
    }
}
