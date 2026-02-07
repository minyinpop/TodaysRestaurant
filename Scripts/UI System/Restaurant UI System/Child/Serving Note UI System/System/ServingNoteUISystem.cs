using System.Collections.Generic;
using Common.Data.Item;
using Common.Data.Item.Serving_Note;
using UI_System.Restaurant_UI_System.Child.Serving_Note_UI_System.Object;
using UnityEngine;

namespace UI_System.Restaurant_UI_System.Child.Serving_Note_UI_System.System
{
    public sealed class ServingNoteUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private RectTransform servingNoteUIParent;

        private readonly Dictionary<ServingNoteSO, ServingNoteUI> _servingNoteUIs = new();

        private void Awake()
        {
            if (servingNoteUIParent == null)
            {
                Debug.Log($"{nameof(ServingNoteUISystem)} > {nameof(servingNoteUIParent)} cannot be null.");
            }
        }

        public bool TryInitialize(ServingNoteSO servingNoteData, GameObject servingNoteUIPrefab)
        {
            if (_servingNoteUIs.ContainsKey(servingNoteData)) return false;
            var newUI = Instantiate(servingNoteUIPrefab, servingNoteUIParent).GetComponent<ServingNoteUI>();
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