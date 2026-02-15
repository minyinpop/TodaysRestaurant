using Common.Item.Data.Ingredient;
using UnityEngine;

namespace Common.Level.Child.Level_Possible_Items
{
    [CreateAssetMenu(menuName = "Minyinpop/Level/Child/Level Ingredient", fileName = "New Data")]
    public sealed class LevelIngredient : ScriptableObject
    {
        [field: Header("Data")]
        [field: SerializeField] private IngredientSO[] ingredientsData;
                                public IngredientSO[] IngredientsData => ingredientsData;
    }
}