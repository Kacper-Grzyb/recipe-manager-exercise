using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipe_manager_exercise
{
    public interface IRecipeManager
    {
        public List<IRecipe> recipes { get; set; }
        public List<string> categories { get; }
        public void CreateRecipe();
        public void AddRecipe(IRecipe recipe);
        public IRecipe? GetRecipe(Guid recipeId);
        public void ViewRecipe(Guid recipeId);
        public void UpdateCategories();
        public void UpdateRecipe(Guid recipeId);
        public void DeleteRecipe(Guid recipeId);
        public void DisplayCategories();
        public void CreateCategory(string categoryName);
        public void DeleteCategory(string categoryName);
        public void ChangeCategory(Guid recipeId, string categoryName);
        public void UpdateCategory(string categoryName, string newCategoryName);
        public List<IRecipe> FilterByCategory(string categoryName);
        public List<IRecipe> FilterByIngredient(string ingredientName);
    }
}