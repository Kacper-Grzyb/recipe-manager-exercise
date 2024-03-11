using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using System.Xml;

namespace recipe_manager_exercise
{
    public class JsonRecipeStorage : IRecipeStorage
    {
        public IRecipeManager LoadRecipes(string path)
        {
            var options = new JsonSerializerOptions
            {
                TypeInfoResolver = new DefaultJsonTypeInfoResolver
                {
                    Modifiers =
                    {
                        static typeInfo =>
                        {
                            if (typeInfo.Type == typeof(IRecipeManager))
                            {
                                typeInfo.CreateObject = () => new RecipeManager();
                            }
                            else if(typeInfo.Type == typeof(IRecipe))
                            {
                                typeInfo.CreateObject = () => new Recipe(new Guid(), "title", new List<string>(), "", "");
                            }
                        }      
                    }
                }
            };

            IRecipeManager? recipeManager;

            using(StreamReader inputFile = new StreamReader(path))
            {
                string jsonString = inputFile.ReadToEnd();
                recipeManager = JsonSerializer.Deserialize<IRecipeManager>(jsonString, options);
                if (recipeManager != null) return recipeManager;
            }

            return new RecipeManager();
        }
        public void SaveRecipes(string path, IRecipeManager rm)
        {
            string jsonString = JsonSerializer.Serialize(rm, new JsonSerializerOptions() { WriteIndented = true });
            using(StreamWriter outputFile = new StreamWriter(path))
            {
                outputFile.WriteLine(jsonString);
            }
        }
    }
}