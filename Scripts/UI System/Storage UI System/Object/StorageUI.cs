using System;
using System.Collections.Generic;
using Common.Button;
using Common.Item_Slot.New.Child;
using Common.Item.Data;
using UnityEngine;

namespace UI_System.Storage_UI_System.Object
{
    public sealed class StorageUI : MonoBehaviour
    {
        [field: Header("關閉按鈕")]
        [field: SerializeField] private Button closeButton;
        
        [field: Header("儲物格")]
        [field: SerializeField] private List<TakeOnlySlot> itemSlots;
        
        private bool _initialized;

        /// <summary>
        /// 當 ItemSlot 被點擊後，就會觸發這個 Action
        /// </summary>
        /// <param name="int">
        /// ItemSlot 在 UI 上的 index
        /// </param>
        public event Action<int> OnTake;
        public event Action OnClickCloseButton;

        public void Awake()
        {
            foreach (var itemSlot in itemSlots)
            {
                itemSlot.OnTake += InvokeOnTake;
            }
            
            closeButton.OnClick += InvokeOnClickCloseButton;
        }

        private void OnDestroy()
        {
            foreach (var itemSlot in itemSlots)
            {
                itemSlot.OnTake -= InvokeOnTake;
            }

            closeButton.OnClick -= InvokeOnClickCloseButton;
        }

        public void Initialize(IReadOnlyList<IItem> items)
        {
            if (_initialized)
            {
                Debug.Log($"{name} 已經初始化過了！");
                return;
            }

            _initialized = true;

            for (var i = 0; i < itemSlots.Count; i++)
            {
                if (i >= items.Count)
                {
                    itemSlots[i].Initialize(null);
                }
                else if (i < items.Count)
                {
                    /*【足立レイENGLISH】Goodbye【UTAUカバー】*/
                    itemSlots[i].Initialize(items[i]);
                }
            }
        }

        public void SetInteractable(bool interactable)
        {
            if (!_initialized)
            {
                Debug.Log($"請先將 {name} 初始化！");
                return;
            }

            foreach (var itemSlot in itemSlots)
            {
                itemSlot.SetInteractable(interactable);
            }
            
            closeButton.SetInteractable(interactable);
        }

        private void InvokeOnTake(TakeOnlySlot itemSlot)
        {
            if (itemSlot is null)
            {
                throw new ArgumentNullException(nameof(itemSlot), "不能是空值。");
            }

            if (OnTake is null)
            {
                throw new InvalidOperationException($"{nameof(OnTake)} 沒有被訂閱。");
            }

            OnTake.Invoke(itemSlots.IndexOf(itemSlot));
        }

        private void InvokeOnClickCloseButton()
        {
            if (OnClickCloseButton is null)
            {
                throw new InvalidOperationException($"{nameof(OnClickCloseButton)} 沒有被訂閱。");
            }

            OnClickCloseButton.Invoke();
        }
    }
}