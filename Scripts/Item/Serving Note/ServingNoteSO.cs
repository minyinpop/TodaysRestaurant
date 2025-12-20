using System;
using UI_System;
using UnityEngine;

namespace Item.Serving_Note
{
    [CreateAssetMenu(menuName = "Minyinpop/Item/Serving Note", fileName = "New Data")]
    internal sealed class ServingNoteSO : ItemSO, IUIHandler
    {
        [field: SerializeField] private GameObject UIObject;
        
        #region Interaction
            public override void Selected() { }
            public override void UnSelected() { }
            
            public static event Action<IUIHandler, GameObject> OnUseItem;
            public override void Use() => OnUseItem?.Invoke(this, UIObject);
        #endregion
    }
}