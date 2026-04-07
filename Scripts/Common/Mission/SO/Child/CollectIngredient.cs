using Common.Mission.Data;
using UnityEngine;

namespace Common.Mission.SO.Child
{
    [CreateAssetMenu(menuName = "Minyinpop/Mission/Child/Collect Ingredient", fileName = "Mew Data")]
    public sealed class CollectIngredient : MissionData
    {
        public override MissionType MissionType => MissionType.Collect_Ingredient;
    }
}