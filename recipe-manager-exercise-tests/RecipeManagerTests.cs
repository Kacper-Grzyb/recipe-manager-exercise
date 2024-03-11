using recipe_manager_exercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipe_manager_exercise_tests
{
    public class RecipeManagerTests
    {
        [Fact]
        public void AddRecipe_AddsRecipeToList()
        {
            RecipeManager recipeManager = new RecipeManager();
            IRecipe recipe = new Recipe(new Guid(), "title", new List<string>() { "ingredient" }, "instructions", "category");
            recipeManager.AddRecipe(recipe);
            Assert.Contains(recipe, recipeManager.recipes);
        }

        [Fact]
        public void UpdateRecipe_UpdatesExistingRecipe()
        {
            RecipeManager recipeManager = new RecipeManager();
            IRecipe recipe = new Recipe(new Guid(), "title", new List<string>() { "ingredient" }, "instructions", "category");
            IRecipe clone = recipe;
            recipeManager.AddRecipe(recipe);
            recipeManager.UpdateRecipe(recipe.recipeId);
            Assert.NotEqual(clone, recipeManager.GetRecipe(recipe.recipeId));
        }

        [Fact]
        public void DeleteRecipe_RemovesRecipeFromList()
        {
            RecipeManager recipeManager = new RecipeManager();
            IRecipe recipe = new Recipe(new Guid(), "title", new List<string>() { "ingredient" }, "instructions", "category");
            recipeManager.AddRecipe(recipe);
            recipeManager.DeleteRecipe(recipe.recipeId);
            Assert.DoesNotContain(recipe, recipeManager.recipes);
        }

        [Fact]
        public void GetRecipe_ReturnsCorrectRecipe()
        {
            RecipeManager recipeManager = new RecipeManager();
            IRecipe? recipe = new Recipe(new Guid(), "title", new List<string>() { "ingredient" }, "instructions", "category");
            recipeManager.AddRecipe(recipe);
            IRecipe? returnedRecipe = recipeManager.GetRecipe(recipe.recipeId);
            Assert.Equal(recipe, returnedRecipe);
        }

        [Fact]
        public void SearchRecipes_FiltersByCategory()
        {
            RecipeManager recipeManager = new RecipeManager();
            IRecipe? recipe1 = new Recipe(new Guid(), "title", new List<string>() { "ingredient" }, "instructions", "category");
            IRecipe? recipe2 = new Recipe(new Guid(), "title", new List<string>() { "ingredient" }, "instructions", "different");
            recipeManager.AddRecipe(recipe1);
            recipeManager.AddRecipe(recipe2);

            List<IRecipe> prefilteredList = new List<IRecipe>() { recipe1 };
            Assert.Equal(prefilteredList, recipeManager.FilterByCategory("category"));
        }

        [Fact]
        public void SearchRecipes_FiltersByIngredient()
        {
            RecipeManager recipeManager = new RecipeManager();
            IRecipe? recipe1 = new Recipe(new Guid(), "title", new List<string>() { "ingredient" }, "instructions", "category");
            IRecipe? recipe2 = new Recipe(new Guid(), "title", new List<string>() { "different" }, "instructions", "category");
            recipeManager.AddRecipe(recipe1);
            recipeManager.AddRecipe(recipe2);

            List<IRecipe> prefilteredList = new List<IRecipe>() { recipe1 };
            Assert.Equal(prefilteredList, recipeManager.FilterByIngredient("ingredient"));
        }

        [Fact]
        public void CategorizeRecipes_AssignsCorrectCategory()
        {
            RecipeManager recipeManager = new RecipeManager();
            IRecipe? recipe1 = new Recipe(new Guid(), "title", new List<string>() { "ingredient" }, "instructions", "category");
            recipeManager.AddRecipe(recipe1);
            recipeManager.ChangeCategory(recipe1.recipeId, "different category");
            Assert.Equal("different category", recipeManager.GetRecipe(recipe1.recipeId).category);
        }

        [Fact]
        public void UpdateCategory_UpdatesExistingCategory()
        {
            RecipeManager recipeManager = new RecipeManager();
            IRecipe recipe1 = new Recipe(new Guid(), "title", new List<string>() { "ingredient" }, "instructions", "category");
            recipeManager.AddRecipe(recipe1);
            recipeManager.UpdateCategory("category", "new category");
            Assert.Equal("new category", recipeManager.GetRecipe(recipe1.recipeId).category);
        }

        [Fact]
        public void DeleteCategory_RemovesCategoryAndReassignsRecipes()
        {
            RecipeManager recipeManager = new RecipeManager();
            IRecipe recipe1 = new Recipe(new Guid(), "title", new List<string>() { "ingredient" }, "instructions", "category");
            recipeManager.AddRecipe(recipe1);
            recipeManager.DeleteCategory("category");
            Assert.Equal("default", recipeManager.GetRecipe(recipe1.recipeId).category);
        }
    }
}
