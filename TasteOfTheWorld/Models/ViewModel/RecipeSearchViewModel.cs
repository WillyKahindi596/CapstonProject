using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TasteOfTheWorld.Models.ViewModel
{
    public class RecipeSearchViewModel
    {
        public int? Country_Id { get; set; }
        public int? Continent_Id { get; set; }
        public string Search_KeyWord { get; set; }


    }
}