using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipe_manager_exercise
{
    public class Recipe : IRecipe
    {
        public Guid recipeId { get; set; }
        public string title { get; set; }
        public List<string> ingredients { get; set; }
        public string instructions { get; set; }
        public string category { get; set; }

        public Recipe(Guid recipeId, string title, List<string> ingredients, string instructions, string category)
        {
            this.recipeId = recipeId;
            this.title = title;
            this.ingredients = ingredients;
            this.instructions = instructions;
            this.category = category;
        }

        public void DisplayRecipeInfo()
        {
            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Recipe Category: {category}");
            Console.WriteLine($"Ingredients needed:");
            foreach (string ingredient in ingredients) { Console.WriteLine($" - {ingredient}"); }
            Console.WriteLine($"Instructions: {instructions}");
        }
    }
}