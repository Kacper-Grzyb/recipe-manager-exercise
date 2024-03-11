using recipe_manager_exercise;

namespace recipe_manager_exercise_tests
{
    public class RecipeTests
    {
        [Fact]
        public void Constructor_AssignsCorrectValues()
        {
            Guid testGuid = Guid.NewGuid();
            List<string> ingredients = new List<string>() { "testIngredient" };
            Recipe recipe = new Recipe(testGuid, "test title", ingredients, "test instructions", "test category");
            bool conidtion = (recipe.recipeId == testGuid && recipe.title == "test title" && recipe.ingredients == ingredients && recipe.instructions == "test instructions" && recipe.category == "test category");
            Assert.True(conidtion);
        }

        [Fact]
        public void Property_SettersAndGetters()
        {
            // Testing getters

            Recipe testRecipe = new Recipe(new Guid("00000000-0000-0000-0000-000000000000"), "test title", new List<string>() { "test ingredient" }, "test instructions", "test category");
            Assert.True(testRecipe.recipeId is Guid);
            Assert.True(testRecipe.title is string);
            Assert.True(testRecipe.ingredients is List<string>);
            Assert.True(testRecipe.instructions is string);
            Assert.True(testRecipe.category is string);

            // Testing setters with different variables
            Recipe secondTestRecipeDuplicate = new Recipe(new Guid("00000000-0000-0000-0000-000000000000"), "test title", new List<string>() { "test ingredient" }, "test instructions", "test category");
            secondTestRecipeDuplicate.recipeId = new Guid("00000000-0000-0000-0000-000000000001");
            secondTestRecipeDuplicate.title = "new title";
            secondTestRecipeDuplicate.ingredients = new List<string>() { "new ingredient" };
            secondTestRecipeDuplicate.instructions = "new instructions";
            secondTestRecipeDuplicate.category = "new category";


            Assert.NotEqual(secondTestRecipeDuplicate.recipeId, testRecipe.recipeId);
            Assert.NotEqual(secondTestRecipeDuplicate.title, testRecipe.title);
            Assert.NotEqual(secondTestRecipeDuplicate.ingredients, testRecipe.ingredients);
            Assert.NotEqual(secondTestRecipeDuplicate.instructions, testRecipe.instructions);
            Assert.NotEqual(secondTestRecipeDuplicate.category, testRecipe.category);
        }
    }
}