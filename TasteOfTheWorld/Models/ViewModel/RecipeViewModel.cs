using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TasteOfTheWorld.Models.ViewModel
{
    public class RecipeViewModel:IValidatableObject
    {
        [Required]
        [Display(Name ="Dish")]
        public string Name_of_Dish { get; set; }

        [Required]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]

        public string Dish_Description { get; set; }
        [Display(Name = "Image")]
        [DataType(DataType.MultilineText)]
        [Required]


        public string Image_URL { get; set; }
        [Display(Name = "Ingredients")]
        [DataType(DataType.MultilineText)]
        [Required]


        public string Ingredients { get; set; }
        [Display(Name = "Directions")]
        [DataType(DataType.MultilineText)]



        public string Directions { get; set; }
        [Display(Name = "Country")]



        public int?  Country_Id { get; set; }
        [Display(Name = "Continent")]

        public int? Continent { get; set; }
        [Display(Name = "Country(Not found in List)")]

        public string New_Country { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Country_Id.HasValue && !string.IsNullOrEmpty(New_Country))
            {
                yield return new ValidationResult("Choose exsiting Counrty", new[] { nameof(New_Country) });
            }
            if(Country_Id== null&& string.IsNullOrWhiteSpace(New_Country))
            {
                yield return new ValidationResult("Choose exsiting Counrty or New country", new[] { nameof(New_Country) });

            }
            if (!string.IsNullOrEmpty(New_Country)&& Continent==null)
            {
                yield return new ValidationResult("Continent Is Required", new[] { nameof(Continent) });
            }
        }
    }
}