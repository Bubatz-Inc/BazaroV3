namespace Bazaro.Core.Models
{
    public record class NoteModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? StartItemId { get; set; }
    }
}
