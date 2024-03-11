using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipe_manager_exercise
{
    public interface IRecipeStorage
    {
        public IRecipeManager LoadRecipes(string path);
        public void SaveRecipes(string path, IRecipeManager rm);
    }
}