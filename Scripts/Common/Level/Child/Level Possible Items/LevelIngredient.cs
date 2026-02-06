using Common.Item;
using Common.Item.Ingredient;
using UnityEngine;

namespace Common.Level.Child.Level_Possible_Items
{
    [CreateAssetMenu(menuName = "Minyinpop/Level/Child/Level Ingredient", fileName = "New Data")]
    public sealed class LevelIngredient : ScriptableObject
    {
        [field: SerializeField] private IngredientSO[] IngredientsData;
                                public IngredientSO[] ingredientsData => IngredientsData;
    }
}