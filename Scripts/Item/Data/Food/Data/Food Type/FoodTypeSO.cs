using Common.Value.Type;
using UnityEngine;

namespace Item.Data.Food.Data.Food_Type
{
    [CreateAssetMenu(menuName = "Minyinpop/Food/Food Type", fileName = "New Data")]
    internal sealed class FoodTypeSO : ScriptableObject
    {
        [field: Header("Information")]
        [field: SerializeField] private FoodType FoodType;
        [field: SerializeField] private string TypeName;
        [field: SerializeField] private Color TypeColor;
        
        public void GetValues(out FoodType foodType, out string typeName, out Color typeColor)
        {
            foodType = FoodType;
            typeName = TypeName;
            typeColor = TypeColor;
        }
    }
}