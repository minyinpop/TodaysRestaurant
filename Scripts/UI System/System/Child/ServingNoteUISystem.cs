using System.Collections.Generic;
using Item.Serving_Note;
using UnityEngine;

namespace UI_System.System.Child
{
    public sealed class ServingNoteUISystem : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private RectTransform parent;
        
        private readonly Dictionary<ServingNoteSO, GameObject> _tempUIs = new();

        public void RequiresUI(ServingNoteSO servingNoteSO, GameObject prefab)
        {
            if (_tempUIs.TryGetValue(servingNoteSO, out var ui))
            {
                ui.SetActive(!ui.activeSelf);
            }
            else
            {
                var newUI = Instantiate(prefab, parent);
                newUI.SetActive(true);
                _tempUIs.Add(servingNoteSO, newUI);
            }
        }
    }
}