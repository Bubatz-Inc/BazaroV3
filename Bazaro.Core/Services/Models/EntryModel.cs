namespace Bazaro.Core.Services.Models
{
    public record class EntryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<EntryTagModel> Tags { get; set; }
        public ItemModel StartItem { get; set; }
    }
}
