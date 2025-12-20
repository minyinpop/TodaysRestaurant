using System;
using System.Collections.Generic;
using System.Linq;
using Item.Serving_Note;
using UnityEngine;

namespace UI_System
{
    public sealed class UISystem : MonoBehaviour
    {
        [field: Header("RectTransform")]
        [field: SerializeField] private RectTransform BottomLeft;

        private readonly Dictionary<IUIHandler, GameObject> UIs = new();
        private readonly Queue<Action> ActiveActions = new();

        private void OnEnable()
        {
            ServingNoteSO.OnUseItem += InstantiateUI;
            ActiveActions.Enqueue(() => ServingNoteSO.OnUseItem -= InstantiateUI);
        }
        
        private void OnDisable()
        {
            while (ActiveActions.Count > 0) ActiveActions.Dequeue()?.Invoke();
        }
        
        private void InstantiateUI(IUIHandler handler, GameObject prefab)
        {
            if (UIs.ContainsKey(handler))
            {
            }
            else
            {
            }

            Instantiate(prefab, BottomLeft);
        }
    }
}