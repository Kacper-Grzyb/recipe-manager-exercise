using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace recipe_manager_exercise
{
    public interface IRecipe
    {
        public Guid recipeId { get; set; }
        public string title { get; set; }
        public List<string> ingredients { get; set; }
        public string instructions { get; set; }
        public string category { get; set; }
        public void DisplayRecipeInfo();
    }
}
