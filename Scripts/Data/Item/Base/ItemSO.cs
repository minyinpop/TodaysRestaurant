using Data.General;
using UnityEngine;

namespace Data.Item.Base
{
    [CreateAssetMenu(menuName = "Minyinpop/Item Data", fileName = "New Data")]
    internal sealed class ItemSO : ScriptableObject, ITem
    {
        #region Information
            [field: Header("Information")]
            [field: SerializeField] private Sprite Sprite;
            
            public void GetInformationSettings(out Sprite sprite)
            {
                sprite = Sprite;
            }
        #endregion        
        
        #region Stack Settings
            [field: Header("Stack")]
            [field: SerializeField] private bool Stackable;
            [field: SerializeField] private Range StackRange;

            public void GetStackSettings(out bool stackable, out Range stackRange)
            {
                stackable = Stackable;
                stackRange = StackRange;
            }
        #endregion
    }
}