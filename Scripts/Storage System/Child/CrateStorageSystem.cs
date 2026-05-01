using System.Collections.Generic;
using Common.Item.Data;
using Player_System.Object;
using Storage_System.Main;
using UI_System.Storage_UI_System.System;
using UnityEngine;

namespace Storage_System.Child
{
    public sealed class CrateStorageSystem : StorageSystem
    {
        [field: Header("狀態")]
        [field: SerializeField] private bool interactable;
                                public override bool Interactable => interactable;
        
        [field: Header("高亮顯示")]
        [field: SerializeField] private MeshRenderer meshRenderer;
        [field: SerializeField] private RenderingLayerMask enableInteractLayerMask;
        [field: SerializeField] private RenderingLayerMask enterDetectLayerMask;
        [field: SerializeField] private RenderingLayerMask disableInteractLayerMask;
        
        [field: Header("介面")]
        [field: SerializeField] private StorageUISystem storageUISystem;
        [field: SerializeField] private GameObject storageUIPrefab;
                                public override GameObject StorageUIPrefab => storageUIPrefab;

        [field: Header("內容物")]
        [field: SerializeField] private List<ItemSO> itemsData;
                                public override IReadOnlyList<ItemSO> ItemsData => itemsData;
        
        private void Awake()
        {
            storageUISystem.OnTake += InvokeOnTake;
            storageUISystem.OnClose += InvokeOnClose;
        }

        private void OnEnable()
        {
            if (interactable)
            {
                meshRenderer.renderingLayerMask = enableInteractLayerMask;
            }
        }

        private void OnDestroy()
        {
            storageUISystem.OnTake -= InvokeOnTake;
            storageUISystem.OnClose -= InvokeOnClose;
        }

        public override void OnEnterDetect(PlayerObject playerObject)
        {
            if (interactable)
            {
                meshRenderer.renderingLayerMask = enterDetectLayerMask;
            }
        }

        public override void OnExitDetect()
        {
            if (interactable)
            {
                meshRenderer.renderingLayerMask = enableInteractLayerMask;
            }
        }

        public override void Interact(PlayerObject playerObject)
        {
            interactable = false;
            
            storageUISystem.ShowUI(this);
        }

        private void InvokeOnTake(int slotIndex)
        {
            Debug.Log($"index: {slotIndex}");
            
            #region 清除被拿走的物品
                itemsData[slotIndex] = null;
            #endregion

            a();
        }

        private void InvokeOnClose()
        {
            a();
        }

        private void a()
        {
            #region 檢查是否還有物品可以拿取
                foreach (var itemData in itemsData)
                {
                    if (itemData is not null)
                    {
                        interactable = true;
                        return;
                    }
                }
            #endregion
            
            #region 設定箱子狀態
                interactable = false;
            #endregion

            #region 設定箱子外觀
                meshRenderer.renderingLayerMask = disableInteractLayerMask;
            #endregion
        }
    }
}