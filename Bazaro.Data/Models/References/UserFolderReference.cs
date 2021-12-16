using System.ComponentModel.DataAnnotations;

namespace Bazaro.Data.Models.References
{
    public record class UserFolderReference
    {
        [Required]
        public User User { get; set; }
        public int UserId { get; set; }

        [Required]
        public Folder Folder { get; set; }
        public int FolderId { get; set; }
    }
}
