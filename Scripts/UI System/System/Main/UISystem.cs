using System;
using System.Collections.Generic;
using Item;
using Item.Serving_Note;
using Player_System.System.Child.Mouse_System.Child;
using UI_System.System.Child;
using UnityEngine;

namespace UI_System.System.Main
{
    public sealed class UISystem : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private ItemDragUISystem itemDragUISystem;
        [field: SerializeField] private ServingNoteUISystem servingNoteUISystem;
        
        private readonly Queue<Action> _activeActions = new();

        private void OnEnable()
        {
            ItemDragSystem.RequiresUI += RequiresItemDragUI;
            _activeActions.Enqueue(() => ItemDragSystem.RequiresUI -= RequiresItemDragUI);
            
            ServingNoteSO.RequiresUI += RequiresServingNoteUI;
            _activeActions.Enqueue(() => ServingNoteSO.RequiresUI -= RequiresServingNoteUI);
        }
        
        private void OnDisable()
        {
            while (_activeActions.Count > 0) _activeActions.Dequeue()?.Invoke();
        }

        private void RequiresItemDragUI(bool isDragging, ItemSO item)
        {
            itemDragUISystem.RequiresUI(isDragging, item);
        }
        
        private void RequiresServingNoteUI(ServingNoteSO servingNoteSO, GameObject prefab)
        {
            servingNoteUISystem.RequiresUI(servingNoteSO, prefab);
        }
    }
}