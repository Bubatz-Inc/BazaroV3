using Bazaro.Data.Models.Base;

namespace Bazaro.Core.Models
{
    public record class ItemModel
    {
        public int Id { get; set; }
        public int NextItemId { get; set; }
        public ContentType ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}
