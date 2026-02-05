using Common.Item;
using UnityEngine;

namespace Common.Level.Child.Level_Possible_Items
{
    [CreateAssetMenu(menuName = "Minyinpop/Level/Child/Level Possible Items", fileName = "New Data")]
    public sealed class LevelPossibleItems : ScriptableObject
    {
        [field: SerializeField] private ItemSO[] ItemsData;
                                public ItemSO[] itemsData => ItemsData;
    }
}