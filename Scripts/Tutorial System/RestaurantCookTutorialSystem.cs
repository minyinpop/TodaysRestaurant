using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Audio_System.Data;
using Audio_System.Main;
using Common.Button;
using Common.Database;
using Common.Scene_Name;
using Common.Scene_Starter;
using DG.Tweening;
using Restaurant_System.Object.Cookware.Object.Cook_Game.System.Main;
using Restaurant_System.Object.Cookware.Object.Cook_Selection.System.Main;
using Restaurant_System.Object.Cookware.System;
using TMPro;
using UnityEngine;

namespace Tutorial_System
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class RestaurantCookTutorialSystem : SceneStarter
    {
        [field: Header("自身組件")]
        [field: SerializeField] private new DoAnimation animation;
        
        [field: Header("氣泡點擊指示")]
        [field: SerializeField] private GameObject clickIndicator;
        
        [field: Header("提示 1")]
        [field: SerializeField] private CanvasGroup tip1CanvasGroup;
        [field: SerializeField] private TextMeshProUGUI tip1Text;
        
        [field: Header("提示 2")]
        [field: SerializeField] private CanvasGroup tip2CanvasGroup;
        [field: SerializeField] private Button tip2ConfirmButton;
        
        [field: Header("下個劇情的資料")]
        [field: SerializeField] private SceneStarterData dialogueStarterData;
        
        [field: Header("背景音樂")]
        [field: SerializeField] private FadeInBGMData fadeInBGMData;

        private DoFade_CanvasGroup _fadeIn;
        private DoFade_CanvasGroup _fadeOut;

        public static event Action<SceneNameSO, SceneStarterData> OnTutorialComplete;

        private void Awake()
        {
            if (animation is null)
            {
                throw new InvalidOperationException($"{nameof(animation)} 沒有被掛載。");
            }
            
            if (clickIndicator is null)
            {
                throw new InvalidOperationException($"{nameof(clickIndicator)} 沒有被掛載。");
            }
            
            if (tip1CanvasGroup is null)
            {
                throw new InvalidOperationException($"{nameof(tip1CanvasGroup)} 沒有被掛載。");
            }

            if (tip1Text is null)
            {
                throw new InvalidOperationException($"{nameof(tip1Text)} 沒有被掛載。");
            }

            if (tip2CanvasGroup is null)
            {
                throw new InvalidOperationException($"{nameof(tip2CanvasGroup)} 沒有被掛載。");
            }
            
            if (tip2ConfirmButton is null)
            {
                throw new InvalidOperationException($"{nameof(tip2ConfirmButton)} 沒有被掛載。");
            }
            
            if (dialogueStarterData is null)
            {
                throw new InvalidOperationException($"{nameof(dialogueStarterData)} 沒有被掛載。");
            }
            
            CookSelectionSystem.OnSelectionUIOpen += OnSelectionUIOpen;
            
            CookSelectionSystem.OnPutIngredientUIOpen += OnPutIngredientUIOpen;
            CookSelectionSystem.OnPutIngredientUIClose += OnPutIngredientUIClose;

            tip2ConfirmButton.OnClick += OnClickTip2ConfirmButton;
            
            CookwareSystem.OnGameTime += OnGameTimeReady;
            CookwareSystem.OnCookComplete += OnCookComplete;
            CookwareSystem.OnAddDish += OnAddDish;

            CookGameSystem.OnGameStart += OnGameStart;
            CookGameSystem.OnGameFinish += OnGameFinish;

            _fadeIn = new DoFade_CanvasGroup(
                endValue: 1,
                duration: .05f,
                ease: Ease.Linear);
            
            _fadeOut = new DoFade_CanvasGroup(
                endValue: 0,
                duration: .05f,
                ease: Ease.Linear);
        }

        private void OnDestroy()
        {
            CookSelectionSystem.OnSelectionUIOpen -= OnSelectionUIOpen;
            
            CookSelectionSystem.OnPutIngredientUIOpen -= OnPutIngredientUIOpen;
            CookSelectionSystem.OnPutIngredientUIClose -= OnPutIngredientUIClose;
            
            tip2ConfirmButton.OnClick -= OnClickTip2ConfirmButton;
            
            CookwareSystem.OnGameTime -= OnGameTimeReady;
            CookwareSystem.OnCookComplete -= OnCookComplete;
            CookwareSystem.OnAddDish -= OnAddDish;
            
            CookGameSystem.OnGameStart -= OnGameStart;
            CookGameSystem.OnGameFinish -= OnGameFinish;
        }
        
        #region 步驟 1
            public override void InvokeOnSceneChangeComplete()
            {
                AudioSystem.Instance.CommonBGM.FadeInBGM(fadeInBGMData);
                
                RefreshTip1("請靠近<b><color=yellow>深煮鍋</color></b>並<b><color=yellow>點擊氣泡</color></b>");
            }
            
            private void OnSelectionUIOpen()
            {
                clickIndicator.SetActive(false);
                
                RefreshTip1("請選擇<b><color=yellow>料理</color></b>");
            }
        #endregion

        #region 步驟 2
            private void OnPutIngredientUIOpen()
            {
                tip2CanvasGroup.gameObject.SetActive(true);
                
                animation.DoFade_CanvasGroup(
                    canvasGroup: tip2CanvasGroup,
                    settings: _fadeIn,
                    onComplete: () =>
                    {
                        animation.DoFade_CanvasGroup(
                            canvasGroup: tip1CanvasGroup,
                            settings: _fadeOut);
                    });
            }
            
            private void OnClickTip2ConfirmButton()
            {
                animation.DoFade_CanvasGroup(
                    canvasGroup: tip2CanvasGroup,
                    settings: _fadeOut,
                    onComplete: () =>
                    {
                        tip2CanvasGroup.gameObject.SetActive(false);
                        
                        RefreshTip1("請放置<b><color=yellow>食材</color></b>並點擊<b><color=#F3BF96>烹飪按鈕</color></b>");
                    });
            }
        #endregion

        #region 步驟 3
            private void OnPutIngredientUIClose()
            {
                animation.DoFade_CanvasGroup(
                    canvasGroup: tip2CanvasGroup,
                    settings: _fadeOut,
                    onComplete: () =>
                    {
                        RefreshTip1("等待<b><color=yellow>深煮鍋</color></b>的烹飪");
                    });
            }
        #endregion
        
        #region 步驟 4
            private void OnGameTimeReady()
            {
                clickIndicator.SetActive(true);
                RefreshTip1("點擊<b><color=yellow>氣泡</color></b>開始<b><color=yellow>遊玩小遊戲</color></b>");
            }
        #endregion
        
        #region 步驟 5
            private void OnGameStart()
            {
                clickIndicator.SetActive(false);
                
                RefreshTip1("<b><color=yellow>拖曳湯勺</color></b>來攪拌食材");
            }
            
            private void OnGameFinish()
            {
                RefreshTip1("繼續等待<b><color=yellow>深煮鍋</color></b>完成最後的烹飪");
            }
        #endregion
        
        #region 步驟 6
            private void OnCookComplete()
            {
                clickIndicator.SetActive(true);
                RefreshTip1("請靠近<b><color=yellow>深煮鍋</color></b>並<b><color=yellow>點擊氣泡</color></b>或是<b><color=yellow>按下互動鍵</color></b>");
            }

            private void OnAddDish()
            {
                if (OnTutorialComplete is null)
                {
                    Debug.Log($"沒有 class 訂閱 {nameof(OnTutorialComplete)}。");
                    return;
                }
                
                SceneNameDatabase.GetSceneName(SceneNameType.Dialogue_Scene, out var sceneNameData);
                OnTutorialComplete.Invoke(sceneNameData, dialogueStarterData);
            }
        #endregion

        private void RefreshTip1(string content)
        {
            animation.DoFade_CanvasGroup(
                canvasGroup: tip1CanvasGroup,
                settings: _fadeOut,
                onComplete: () =>
                {
                    tip1Text.text = content;

                    animation.DoFade_CanvasGroup(
                        canvasGroup: tip1CanvasGroup,
                        settings: _fadeIn);
                });
        }
    }
}