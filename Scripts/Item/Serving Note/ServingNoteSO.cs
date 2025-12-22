using System;
using UI_System;
using UnityEngine;

namespace Item.Serving_Note
{
    [CreateAssetMenu(menuName = "Minyinpop/Item/Serving Note", fileName = "New Data")]
    public sealed class ServingNoteSO : ItemSO
    {
        [field: Header("Components")]
        [field: SerializeField] private GameObject servingNotePrefab;
        
        #region Interaction
            public override void Selected() { }
            public override void UnSelected() { }
            
            public static event Action<ServingNoteSO, GameObject> ServingNoteUIRequired;
            public override void Use() => ServingNoteUIRequired?.Invoke(this, servingNotePrefab);
        #endregion
    }
}