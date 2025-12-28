using System.Collections.Generic;
using Item.Serving_Note;
using UnityEngine;

namespace UI_System.System.Child
{
    public sealed class ServingNoteUISystem : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private RectTransform parent;
        
        private readonly Dictionary<ServingNoteSO, ServingNote> _tempUIs = new();

        public void RequiresUI(ServingNoteSO servingNoteData, GameObject prefab)
        {
            if (_tempUIs.TryGetValue(servingNoteData, out var ui))
            {
                ui.gameObject.SetActive(!ui.gameObject.activeSelf);
            }
            else
            {
                var newUI = Instantiate(prefab, parent).GetComponent<ServingNote>();
                newUI.Initialize(servingNoteData);
                newUI.gameObject.SetActive(true);
                _tempUIs.Add(servingNoteData, newUI);
            }
        }
    }
}