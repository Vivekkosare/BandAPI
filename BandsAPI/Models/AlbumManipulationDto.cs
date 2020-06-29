using BandsAPI.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BandsAPI.Models
{
    [TitleAndDescriptionAttribute(ErrorMessage = "Title should be different from Description")]
    public abstract class AlbumManipulationDto
    {
        [Required(ErrorMessage = "Title should not be empty")]
        [MaxLength(100, ErrorMessage = "Title length should not be more than 100")]
        public string Title { get; set; }

        [MaxLength(400, ErrorMessage = "Description length should not be more than 400")]
        public virtual string Description { get; set; }
    }
}
