﻿using Bazaro.Data.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Bazaro.Data.Models
{
    public record class Item : IdEntity
    {
        public Item NextItem { get; set; }
        public int? NextItemId { get; set; }

        [Required]
        public ContentType ContentType { get; set; }

        [Required]
        public byte[] Content { get; set; }
    }
}