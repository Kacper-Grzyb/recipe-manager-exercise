using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipe_manager_exercise
{
    public class RecipeManager : IRecipeManager
    {
        public List<IRecipe> recipes { get; set; } = new List<IRecipe>();
        public List<string> categories { get; } = new List<string>() { "Default" };

        public void ViewRecipe(Guid recipeId)
        {
            foreach(IRecipe recipe in recipes)
            {
                if(recipe.recipeId ==  recipeId)
                {
                    recipe.DisplayRecipeInfo();
                }
            }
        }

        public IRecipe? GetRecipe(Guid recipeId)
        {
            foreach(var recipe in recipes)
            {
                if (recipe.recipeId == recipeId) return recipe;
            }
            return null;
        }

        public void UpdateCategories()
        {
            foreach(IRecipe recipe in recipes)
            {
                if(!categories.Contains(recipe.category)) categories.Add(recipe.category);
            }
        }

        public void AddRecipe(IRecipe recipe)
        {
            if (!categories.Contains(recipe.category)) categories.Add(recipe.category);
            recipes.Add(recipe);
        }

        public void CreateRecipe()
        {
            Guid guid = Guid.NewGuid();
            Console.WriteLine("Please input the new title for your recipe:");
            string title = InputValidator.GetStringInput();

            Console.WriteLine("Plese create the ingredient list for your recipe:");
            List<string> ingredients = CreateIngredients();

            Console.WriteLine("Please input the instructions for your recipe: ");
            string instructions = InputValidator.GetStringInput();

            IRecipe r = new Recipe(guid, title, ingredients, instructions, "default");
            AssignCategory(ref r);
            recipes.Add(r);
            Console.WriteLine("Recipe created succesfully!");
        }

        public void UpdateRecipe(Guid recipeId)
        {
            IRecipe? recipe =null;
            foreach(IRecipe r in recipes) if (r.recipeId == recipeId) recipe = r;
            if(recipe==null) throw new Exception("Provided recipeId does not exist within the recipes list!");

            while(true)
            {
                Console.WriteLine("Please choose the corresponding number to which part of the recipe you want to edit: [1-4]");
                Console.WriteLine("1. Title\n2. Ingredients\n3. Instructions\n4. Category\n5. Stop editing the recipe");
                int userInput = InputValidator.GetIntInputInRange(1, 4);
                switch(userInput)
                {
                    case 1:
                        Console.WriteLine("Please input the new title for your recipe:");
                        recipe.title = InputValidator.GetStringInput();
                        Console.WriteLine("Recipe name modified succesfully!");
                        break;
                    case 2:
                        Console.WriteLine("Here is the current ingredient list:");
                        foreach(string ingredient in recipe.ingredients) Console.WriteLine(ingredient);
                        Console.WriteLine("Please input the new ingredient list.");
                        recipe.ingredients = CreateIngredients();
                        break;
                    case 3:
                        Console.WriteLine($"Here are the current recipe instructions: {recipe.instructions}\n Please input the new instructions: ");
                        recipe.instructions = InputValidator.GetStringInput();
                        Console.WriteLine("Recipe instructions modified succesfully!");
                        break;
                    case 4:
                        AssignCategory(ref recipe);
                        break;
                    default:
                        return;
                }
            }
        }

        public void DeleteRecipe(Guid recipeId)
        {
            bool found = false;

            foreach (IRecipe recipe in recipes)
            {
                if (recipe.recipeId == recipeId)
                {
                    recipes.Remove(recipe);
                    found = true;
                    break;
                }
            }

            if (!found) throw new Exception($"Could not find recipe with recipeId {recipeId}");
        }

        private List<string> CreateIngredients()
        {
            List<string> ingredients = new List<string>();
            string ingredient = "";

            Console.WriteLine("Please start inputting the names of the ingredients. Press Enter in between each ingredient. To stop adding ingredients input -1");
            while(true)
            {
                ingredient = InputValidator.GetStringInput();
                if (ingredient == "-1" || ingredient == null) break;
                else ingredients.Add(ingredient.ToLower());
            }


            return ingredients;
        }


        private void AssignCategory(ref IRecipe recipe)
        {
            Console.WriteLine("Choose one of the categories below to assign to this recipe:");
            DisplayCategories();
            string userCategory = InputValidator.GetStringInput();
            if(categories.Contains(userCategory.ToLower()))
            {
                recipe.category = userCategory;
            }
            else
            {
                Console.WriteLine("The category provided does not exist. Would you like to create it?\n1. Yes\n2. No");
                if(InputValidator.GetIntInputInRange(1, 2) == 1)
                {
                    CreateCategory(userCategory);
                    recipe.category = userCategory;
                } 
            }
        }

        public void ChangeCategory(Guid recipeId, string category)
        {
            IRecipe? recipe = GetRecipe(recipeId);
            if(recipe != null)
            {
                if (!categories.Contains(category)) categories.Add(category);
                recipe.category = category;
            }
        }

        public void DisplayCategories()
        {
            foreach(string category in categories)
            {
                Console.WriteLine(category);
            }
        }

        public void CreateCategory(string categoryName)
        {
            if(categories.Contains(categoryName.ToLower()))
            {
                Console.WriteLine($"Category {categoryName} has already been created!");
            }
            else
            {
                categories.Add(categoryName.ToLower());
                Console.WriteLine($"Category {categoryName} added succesfully!");
            }
        }

        public void DeleteCategory(string categoryName)
        {
            if(categoryName.ToLower() == "default")
            {
                Console.WriteLine("Cannot delete the default category!");
                return;
            }

            if (categories.Contains(categoryName.ToLower()))
            {
                foreach(IRecipe recipe in recipes)
                {
                    if(recipe.category.ToLower() == categoryName.ToLower()) recipe.category = "default";
                }
                categories.Remove(categoryName.ToLower());
                Console.WriteLine($"Category {categoryName} deleted succesfully!");
            }
            else
            {
                Console.WriteLine($"Could not find {categoryName} in the exsiting category table!");
            }
        }

        public void UpdateCategory(string categoryName, string newCategoryName)
        {
            if(categories.Contains(categoryName))
            {
                categories[categories.IndexOf(categoryName)] = newCategoryName;
                for(int i=0; i<recipes.Count; i++)
                {
                    if (recipes[i].category == categoryName) recipes[i].category = newCategoryName;
                }
            }
        }

        public List<IRecipe> FilterByCategory(string categoryName)
        {
            List<IRecipe> filtered = new List<IRecipe>();

            foreach (IRecipe recipe in recipes)
            {
                if (recipe.category.ToLower() == categoryName.ToLower()) filtered.Add(recipe);
            }

            return filtered;
        }
        public List<IRecipe> FilterByIngredient(string ingredientName)
        {
            List<IRecipe> filtered = new List<IRecipe>();

            foreach (IRecipe recipe in recipes)
            {
                foreach(string ingredient in recipe.ingredients)
                {
                    if(ingredient.ToLower() == ingredientName.ToLower())
                    {
                        filtered.Add(recipe);
                        break;
                    }
                }
            }

            return filtered;
        }
    }
}