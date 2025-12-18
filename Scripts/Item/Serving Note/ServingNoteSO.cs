using System;
using Item.Data;
using UnityEngine;

namespace Item.Serving_Note
{
    [CreateAssetMenu(menuName = "Minyinpop/Item/Serving Note", fileName = "New Data")]
    internal sealed class ServingNoteSO : ItemSO
    {
        [field: SerializeField] private GameObject UIObject;
        
        #region Interaction
            public override void Selected() { }
            public override void UnSelected() { }
            
            public static event Action<GameObject> OnUseItem;
            public override void Use() => OnUseItem?.Invoke(UIObject);
        #endregion
    }
}