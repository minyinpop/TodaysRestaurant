using System.Collections.Generic;
using Item;
using Item.Serving_Note;
using UnityEngine;

namespace UI_System.System.Child.Serving_Note_UI_System
{
    public sealed class ServingNoteUISystem : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private RectTransform parent;
        
        private readonly Dictionary<ServingNoteSO, ServingNoteUI> _servingNoteUIs = new();

        public bool TryInitialize(ServingNoteSO servingNoteData, GameObject prefab)
        {
            if (_servingNoteUIs.ContainsKey(servingNoteData)) return false;
            var newUI = Instantiate(prefab, parent).GetComponent<ServingNoteUI>();
            newUI.Initialize(servingNoteData);
            newUI.gameObject.SetActive(true);
            _servingNoteUIs.Add(servingNoteData, newUI);
            return true;
        }

        public void ToggleUI(ServingNoteSO servingNoteData)
        {
            if (_servingNoteUIs.TryGetValue(servingNoteData, out var ui))
                ui.gameObject.SetActive(!ui.gameObject.activeSelf);
        }

        public void GetServingNoteItems(ServingNoteSO servingNoteData, out Queue<ItemSO> servingNoteItems)
        {
            _servingNoteUIs.TryGetValue(servingNoteData, out var servingNote);
            
            if (servingNote is null)
                throw new KeyNotFoundException($"Serving note UI not found for {servingNoteData.name}");
            
            servingNote.GetServingNoteSlotItems(out servingNoteItems);
        }
    }
}