using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipe_manager_exercise
{
    public class Program
    {
        static void Main()
        {
            IRecipeManager recipeManager = new RecipeManager();
            IRecipeStorage recipeStorage = new JsonRecipeStorage();
            bool quit = false;

            Console.WriteLine("Welcome to your Recipe Manager App!");
            Console.WriteLine("Do you want to load a pre-existing recipe list?\n1. Yes\n2. No");
            if(InputValidator.GetIntInputInRange(1, 2) == 1)
            {
                Console.WriteLine("Please specify a path from which to load the recipe list:");
                string path = InputValidator.GetStringInput();
                recipeManager = recipeStorage.LoadRecipes(path);
                recipeManager.UpdateCategories();
            }

            while(!quit)
            {
                Console.WriteLine("\nPlease pick an action\n1. Add a new recipe\n2. View all recipes\n3. Update an existing recipe\n4. Search for recipes\n5. Manage recipe categories\n6. Exit Application");
                int userInput = InputValidator.GetIntInputInRange(1, 6);
                switch(userInput)
                {
                    case 1:
                        recipeManager.CreateRecipe();
                        break;
                    case 2:
                        if(recipeManager.recipes.Count==0)
                        {
                            Console.WriteLine("There are no recipes to view!");
                            break;
                        }
                        foreach(IRecipe recipe in recipeManager.recipes) recipe.DisplayRecipeInfo();
                        break;
                    case 3:
                        UpdateRecipes(recipeManager);
                        break;
                    case 4:
                        Search(recipeManager);
                        break;
                    case 5:
                        ManageCategories(recipeManager);
                        break;
                    case 6:
                        quit = true;
                        break;
                }
            }

            Console.WriteLine("Do you wish to save your recipes into a file?\n1. Yes\n2. No");
            if(InputValidator.GetIntInputInRange(1, 2) == 1)
            {
                Console.WriteLine("Please give a path to a json file to save the recipes into:");
                string filePath = InputValidator.GetStringInput();
                try
                {
                    recipeStorage.SaveRecipes(filePath, recipeManager);
                }
                catch
                {
                    Console.WriteLine("Failed to save to the file!");
                }
            }
            Console.WriteLine("Thank you for using the application!");
        }

        static void UpdateRecipes(IRecipeManager recipeManager)
        {
            if(recipeManager.recipes.Count==0)
            {
                Console.WriteLine("There are no recipes to update!");
                return;
            }

            Console.WriteLine("Which recipe would you like to update?");
            int counter = 1;
            foreach(IRecipe recipe in recipeManager.recipes)
            {
                Console.WriteLine($"{counter}. {recipe.title}");
                counter++;
            }
            int userInput = InputValidator.GetIntInputInRange(1, counter);

            recipeManager.UpdateRecipe(recipeManager.recipes[userInput].recipeId);
        }

        static void Search(IRecipeManager recipeManager)
        {
            if (recipeManager.recipes.Count == 0)
            {
                Console.WriteLine("There are no recipes to search for!");
                return;
            }

            bool resultsFound = false;
            string userSearch = "";

            Console.WriteLine("Which parameter do you want to search by?\n1. Ingredients\n2. Categories");
            int ans = InputValidator.GetIntInputInRange(1, 2);
            if(ans == 1)
            {
                Console.WriteLine("Input the ingredient by which you would like to search the recipes: ");
                userSearch = InputValidator.GetStringInput();
                var filteredList = recipeManager.FilterByIngredient(userSearch);
                foreach(IRecipe recipe in filteredList)
                {
                    if (recipe.ingredients.Contains(userSearch.ToLower()))
                    {
                        recipe.DisplayRecipeInfo();
                        resultsFound = true;
                    }
                }
            }
            else
            {
                Console.WriteLine("Input the category by which you would like to search the recipes: ");
                userSearch = InputValidator.GetStringInput();
                var filteredList = recipeManager.FilterByCategory(userSearch);
                foreach (IRecipe recipe in filteredList)
                {
                    if (recipe.category.ToLower() == userSearch.ToLower())
                    {
                        recipe.DisplayRecipeInfo();
                        resultsFound = true;
                    }
                }
            }

            if (!resultsFound) Console.WriteLine("No results found...");
        }

        static void ManageCategories(IRecipeManager recipeManager)
        {

            Console.WriteLine("These are the current existing categories:");
            foreach (string category in recipeManager.categories) Console.WriteLine(category);
            Console.WriteLine("Please pick an action:\n1. Add Category\n2. Delete Category\n3. Update Category");
            int userInput = InputValidator.GetIntInputInRange(1, 3);
            switch(userInput)
            {
                case 1:
                    Console.WriteLine("Please input the name of the category you would like to create:");
                    recipeManager.CreateCategory(InputValidator.GetStringInput());
                    break;
                case 2:
                    Console.WriteLine("Please input the name of the category you would like to delete:");
                    recipeManager.DeleteCategory(InputValidator.GetStringInput());
                    break;
                case 3:
                    // Update category
                    break;
            }
        }
    }
}