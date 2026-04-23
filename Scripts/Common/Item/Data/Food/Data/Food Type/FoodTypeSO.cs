using Common.Value.Type;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Item.Data.Food.Data.Food_Type
{
    [CreateAssetMenu(menuName = "Minyinpop/Food/Food Type", fileName = "New Data")]
    internal sealed class FoodTypeSO : ScriptableObject
    {
        [field: Header("Information")]
        [field: SerializeField, FormerlySerializedAs("FoodType")] private FoodType foodType;
                                                                          public FoodType FoodType => foodType;
        [field: SerializeField, FormerlySerializedAs("TypeName")] private string typeName;
                                                                          public string TypeName => typeName;
        [field: SerializeField, FormerlySerializedAs("TypeColor")] private Color typeColor;
                                                                           public Color TypeColor => typeColor;
    }
}