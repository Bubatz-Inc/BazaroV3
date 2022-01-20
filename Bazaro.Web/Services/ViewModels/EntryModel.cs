namespace Bazaro.Web.Services.ViewModels
{
    public record class EntryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? StartItemId { get; set; }
    }
}
