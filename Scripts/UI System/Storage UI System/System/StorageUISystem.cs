using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Input_System;
using Storage_System.Main;
using UI_System.Storage_UI_System.Object;
using UnityEngine;

namespace UI_System.Storage_UI_System.System
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class StorageUISystem : MonoBehaviour
    {
        [field: Header("動畫")]
        [field: SerializeField] private new DoAnimation animation;
        [field: SerializeField] private DoFade_CanvasGroup fadeInSettings;
        [field: SerializeField] private DoFade_CanvasGroup fadeOutSettings;
        
        [field: Header("遮罩")]
        [field: SerializeField] private CanvasGroup maskCanvasGroup;
        
        [field: Header("介面生成位置")]
        [field: SerializeField] private RectTransform storageUIParent;
        
        private StorageUI _currentStorageUI;
        
        public event Action<int> OnTake;
        public event Action OnClose;
        
        public void ShowUI(StorageSystem storageSystem)
        {
            #region 關閉玩家的移動
                InputSystem.DisablePlayerWalk();
            #endregion
            
            #region 生成介面並初始化
                _currentStorageUI = Instantiate(storageSystem.StorageUIPrefab, storageUIParent).GetComponent<StorageUI>();
                _currentStorageUI.Initialize(storageSystem.ItemsData);
                _currentStorageUI.OnTake += InvokeOnTake;
                _currentStorageUI.OnClickCloseButton += InvokeOnClickCloseButton;
            #endregion
            
            #region 介面動畫
                maskCanvasGroup.gameObject.SetActive(true);
                
                animation.DoFade_CanvasGroup(
                    canvasGroup: maskCanvasGroup,
                    settings: fadeInSettings,
                    onComplete: () =>
                    {
                        _currentStorageUI.gameObject.SetActive(true);
                        
                        animation.DoFade_CanvasGroup(
                            canvasGroup: _currentStorageUI.GetComponent<CanvasGroup>(),
                            settings: fadeInSettings,
                            onComplete: () =>
                            {
                                _currentStorageUI.SetInteractable(true);
                            });
                    });
            #endregion
        }

        private void HideUI()
        {
            #region 條件式檢查
                if (OnClose is null)
                {
                    throw new InvalidOperationException($"{nameof(OnClose)} 沒有被訂閱。");
                }
            #endregion
            
            #region 介面動畫
                _currentStorageUI.SetInteractable(false);
            
                animation.DoFade_CanvasGroup(
                    canvasGroup: _currentStorageUI.GetComponent<CanvasGroup>(),
                    settings: fadeOutSettings,
                    onComplete: () =>
                    {
                        #region 移除儲物介面
                            _currentStorageUI.OnTake -= InvokeOnTake;
                            _currentStorageUI.OnClickCloseButton -= InvokeOnClickCloseButton;
                            
                            Destroy(_currentStorageUI.gameObject);
                        #endregion
                        
                        animation.DoFade_CanvasGroup(
                            canvasGroup: maskCanvasGroup,
                            settings: fadeOutSettings,
                            onComplete: () =>
                            {
                                maskCanvasGroup.gameObject.SetActive(false);
                            });
                    });
            #endregion
            
            #region 開啟玩家移動
                InputSystem.EnablePlayerWalk();
            #endregion
            
            #region 發送廣播
                OnClose.Invoke();
            #endregion
        }

        private void InvokeOnTake(int slotIndex)
        {
            if (OnTake is null)
            {
                throw new InvalidOperationException($"{nameof(OnTake)} 沒有被訂閱。");
            }

            OnTake.Invoke(slotIndex);
        }

        private void InvokeOnClickCloseButton()
        {
            HideUI();
        }
    }
}