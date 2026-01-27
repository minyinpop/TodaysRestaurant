using System.Collections.Generic;
using Common.Item;
using Common.Item.Serving_Note;
using UI_System.System.Main;
using UnityEngine;

namespace UI_System.System.Child.Serving_Note_UI_System
{
    public sealed class ServingNoteUISystem : MonoBehaviour
    {
        [field: Header("Components")] [field: SerializeField]
        private RectTransform parent;

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

        public void GetServingNoteItems(ServingNoteSO servingNoteData, out List<ItemSO> servingNoteItems)
        {
            _servingNoteUIs.TryGetValue(servingNoteData, out var servingNote);
            servingNote.GetSlotItems(out servingNoteItems);
        }

        public void RemoveServingNoteUI(ServingNoteSO servingNoteData)
        {
            if (_servingNoteUIs.TryGetValue(servingNoteData, out var ui))
                Destroy(ui.gameObject);
            _servingNoteUIs.Remove(servingNoteData);
        }
    }
}