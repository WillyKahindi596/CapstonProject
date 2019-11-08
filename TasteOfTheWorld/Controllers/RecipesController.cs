using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TasteOfTheWorld.Models;
using TasteOfTheWorld.Models.ViewModel;

namespace TasteOfTheWorld.Controllers
{
    public class RecipesController : Controller
    {
        private CapstoneProjectEntities db = new CapstoneProjectEntities();

        // GET: Recipes
        public ActionResult Index(RecipeSearchViewModel model)
        {
            var recipes = db.Recipes.Include(r => r.Chef).Include(r => r.Country);
            if(model?.Continent_Id!=null )
            {
                recipes = recipes.Where(R => R.Country.Continent_Id == model.Continent_Id);

            }
            if (model?.Country_Id != null)
            {
                recipes = recipes.Where(R => R.Country_Id == model.Country_Id);

            }
            if (!string.IsNullOrWhiteSpace(model.Search_KeyWord))
            {
                recipes = recipes.Where(R => R.Dish_Description.Contains(model.Search_KeyWord) ||
                R.Ingredients.Contains(model.Search_KeyWord) || R.Directions.Contains(model.Search_KeyWord));

            }

            return View(recipes.ToList());
        }

        // GET: Recipes/Details/5 
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // GET: Recipes/Create
       [Authorize]
        public ActionResult Create()
        {
            var user_Id = User.Identity.GetUserId();
            var model = new RecipeViewModel();
            ViewBag.Continent = new SelectList(db.Continents, "Continent_Id", "Continent_Name");
            ViewBag.Country_Id = new SelectList(db.Countries, "Country_Id", "Country_Name");
            return View(model);
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecipeViewModel recipeVM)
        {
            if (ModelState.IsValid)
            {
                var recipe = new Recipe
                {
                    Directions = recipeVM.Directions,
                    Dish_Description = recipeVM.Dish_Description,
                    Image_URL = recipeVM.Image_URL,
                    Ingredients = recipeVM.Ingredients,
                    Name_of_Dish = recipeVM.Name_of_Dish,
                };
                if (recipeVM.Country_Id.HasValue)
                {
                    recipe.Country_Id = recipeVM.Country_Id.Value;
                }
                else
                {
                    var country = new Country { Country_Name = recipeVM.New_Country, Continent_Id = recipeVM.Continent.Value };
                    recipe.Country = country;
                }

                var user_Id = User.Identity.GetUserId();
                recipe.Chef_Id = user_Id;



                db.Recipes.Add(recipe);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            ViewBag.Continent = new SelectList(db.Continents, "Continent_Id", "Continent_Name", recipeVM.Continent);
            ViewBag.Country_Id = new SelectList(db.Countries, "Country_Id", "Country_Name", recipeVM.Country_Id);
            return View(recipeVM);
        }

        //GET: Recipes/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Recipe recipe = db.Recipes.Find(id);
        //    if (recipe == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.Chef_Id = new SelectList(db.Chefs, "Chef_Id", "First_Name", recipe.Chef_Id);
        //    ViewBag.Continent = new SelectList(db.Continents, "Continent_Id", "Continent_Name", recipe.Continent);
        //    ViewBag.Country_Id = new SelectList(db.Countries, "Country_Id", "Country_Name", recipe.Country_Id);
        //    return View(recipe);
        ////}

        //// POST: Recipes/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Recipe_Id,Name_of_Dish,Dish_Description,Image_URL,Ingredients,Directions,Chef_Id,Country_Id,Continent")] Recipe recipe)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(recipe).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.Chef_Id = new SelectList(db.Chefs, "Chef_Id", "First_Name", recipe.Chef_Id);
        //    ViewBag.Continent = new SelectList(db.Continents, "Continent_Id", "Continent_Name", recipe.Continent);
        //    ViewBag.Country_Id = new SelectList(db.Countries, "Country_Id", "Country_Name", recipe.Country_Id);
        //    return View(recipe);
        //}

        // GET: Recipes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }

            var user_id = User.Identity.GetUserId();
            if(recipe.Chef_Id == user_id)
            {
                ViewBag.CurrentChef = true;
            }
            else
            {
                ViewBag.CurrentChef = false;
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recipe recipe = db.Recipes.Find(id);
            var user_id = User.Identity.GetUserId();
            if (recipe.Chef_Id != user_id)
            {
                throw new Exception("Please TRY Another Recipe ");
            }

            db.Recipes.Remove(recipe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult SearchRecipe()
        {
            ViewBag.AllCountries = new SelectList(db.Countries.OrderBy(C => C.Country_Name).ToList(), "Country_Id", "Country_Name");
            ViewBag.AllContinent = new SelectList(db.Continents.OrderBy(C => C.Continent_Name).ToList(), "Continent_Id", "Continent_Name");

            return PartialView();

        }

    }
}
