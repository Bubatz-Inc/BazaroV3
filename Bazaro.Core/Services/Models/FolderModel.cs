namespace Bazaro.Core.Services.Models
{
    public record class FolderModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public List<FolderModel> SubFolders { get; set; }
    }
}
