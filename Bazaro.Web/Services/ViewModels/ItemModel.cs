using Bazaro.Web.Models;

namespace Bazaro.Web.Services.ViewModels
{
    public record class ItemModel
    {
        public int Id { get; set; }

        public ItemModel NextItem { get; set; }

        public ContentTypeModel ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}
