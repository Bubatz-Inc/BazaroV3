namespace Bazaro.Web.Services.ViewModels
{
    public record class FolderModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsShared { get; set; }

        public List<FolderModel> SubFolders { get; set; }
        public List<EntryModel> Entries { get; set; }
    }
}
