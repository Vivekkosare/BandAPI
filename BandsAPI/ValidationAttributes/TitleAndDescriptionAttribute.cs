using BandsAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BandsAPI.ValidationAttributes
{
    public class TitleAndDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //return base.IsValid(value, validationContext);
            var album = (AlbumManipulationDto)validationContext.ObjectInstance;
            if(album.Title == album.Description)
            {
                return new ValidationResult("Title and Description should not be the same", new[] { "AlbumManipulationDto" });
            }
            return ValidationResult.Success;
        }
    }
}
