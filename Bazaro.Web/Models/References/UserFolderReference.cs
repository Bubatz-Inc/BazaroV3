using Bazaro.Web.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Bazaro.Web.Models.References
{
    public record class UserFolderReference : IEntity
    {
        [Required]
        public User User { get; set; }
        public string UserId { get; set; }

        [Required]
        public Folder Folder { get; set; }
        public int FolderId { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public bool IsShared { get; set; }
    }
}
