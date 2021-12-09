using System.ComponentModel.DataAnnotations;

namespace Bazaro.Data.Models.Base
{
    public interface IEntity
    {
    }

    public abstract record class BaseEntity<T> : IEntity
    {
        [Key]
        public T Id { get; set; }

        [Required]
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }

    public abstract record class IdEntity : BaseEntity<int>
    {
    }
}
