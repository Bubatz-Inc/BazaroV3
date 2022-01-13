namespace Bazaro.Web.Services.Models
{
    public record class ItemModel
    {
        public ItemModel NextItem { get; set; }

        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}
