using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Button;
using Common.Dialogue.SO.Main;
using Common.Item.Data;
using Common.Scene_Starter;
using Input_System;
using Restaurant_System.System.Main;
using UI_System.Dialogue_UI_System.Lite.Main;
using UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.Object.Item_Slot.Base;
using UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.System.Child.Open_Page.Main;
using UnityEngine;
using TutorialTip = Common.Tutorial_Tip.Main.TutorialTip;

namespace Tutorial_System
{
    public sealed class RestaurantServesTutorialSystem : SceneStarter
    {
        [field: Header("系統")]
        [field: SerializeField] private DialogueLiteUISystem dialogueLiteUISystem;
        [field: SerializeField] private RestaurantSystem restaurantSystem;
        
        [field: Header("教學 1")]
        [field: SerializeField] private TutorialTip tip1;
        [field: SerializeField] private Button openButton;
        
        [field: Header("教學 2")]
        [field: SerializeField] private TutorialTip tip2;
        [field: SerializeField] private TutorialTip tip3;
        [field: SerializeField] private OpenState foodMenuOpenState;
        
        [field: Header("教學 3")]
        [field: SerializeField] private TutorialTip tip4;
        [field: SerializeField] private TutorialTip tip5_1;
        [field: SerializeField] private TutorialTip tip5_2;
        
        private IEnumerator _tutorialCoroutine;

        private void OnDestroy()
        {
            if (_tutorialCoroutine is not null)
            {
                StopCoroutine(_tutorialCoroutine);
                _tutorialCoroutine = null;
            }
        }

        public override void InvokeOnSceneChangeComplete(SceneStarterData starterData)
        {
            if (starterData is null)
            {
                throw new ArgumentNullException(nameof(starterData), $"{nameof(starterData)} 不能傳入空值。");
            }

            if (starterData is not DialogueSO dialogueData)
            {
                throw new InvalidOperationException($"{nameof(starterData)} 不是 {nameof(DialogueSO)}。");
            }
            
            InputSystem.DisablePlayerWalk();

            dialogueLiteUISystem.StartDialogue(
                dialogueData: dialogueData,
                onComplete: () =>
                {
                    restaurantSystem.StartSystem();
                    StartTutorial();
                });
        }

        private void StartTutorial()
        {
            foodMenuOpenState.SetInteractable(false);
            foodMenuOpenState.UnlockFoodPage.SetInteractable(false);
            foodMenuOpenState.SelectFoodPage.SetInteractable(false);
            
            Tutorial1(
                onComplete: () =>
                {
                    Tutorial2(
                        onComplete: () =>
                        {
                            Tutorial3(
                                onComplete: () =>
                                {
                                    Debug.Log("教學內容結束。");
                                });
                        });
                });
        }

        private void Tutorial1(Action onComplete)
        {
            tip1.ShowTip(
                onComplete: () =>
                {
                    openButton.OnClick += OnClickButton;
                    openButton.SetInteractable(true);
                });
            return;

            void OnClickButton()
            {
                openButton.OnClick -= OnClickButton;
                tip1.HideTip(onComplete);
            }
        }

        private void Tutorial2(Action onComplete)
        {
            tip2.ShowTip(
                onComplete: () =>
                {
                    tip3.ShowTip(
                        onComplete: () =>
                        {
                            foodMenuOpenState.UnlockFoodPage.UnlockFoodSlots[0].OnClick += OnClickSlot;
                            Debug.Log("A"); foodMenuOpenState.UnlockFoodPage.SetInteractable(true); Debug.Log("B");
                        });
                });
            
            return;
            
            void OnClickSlot(ItemSlot slot, ItemSO itemData)
            {
                foodMenuOpenState.UnlockFoodPage.UnlockFoodSlots[0].OnClick -= OnClickSlot;
                tip3.HideTip(onComplete);
            }
        }

        private void Tutorial3(Action onComplete)
        {
            _tutorialCoroutine = TutorialCoroutine();
            StartCoroutine(_tutorialCoroutine);
            return;
            
            IEnumerator TutorialCoroutine()
            {
                var complete = false;
                var completes = new Dictionary<TutorialTip, bool>
                {
                    { tip5_1, false },
                    { tip5_2, false }
                };

                tip4.ShowTip(
                    onComplete: () =>
                    {
                        tip5_1.ShowTip(
                            onComplete: () =>
                            {
                                foodMenuOpenState.UnlockFoodPage.SetInteractable(true);
                            });
                        tip5_2.ShowTip(
                            onComplete: () =>
                            {
                                foodMenuOpenState.SelectFoodPage.SetInteractable(true);
                            });
                        
                        foodMenuOpenState.UnlockFoodPage.UnlockFoodSlots[0].OnClick += OnClickSlot;
                        foodMenuOpenState.SelectFoodPage.SelectFoodSlots[0].OnClick += OnClickSlot;
                    });

                yield return new WaitUntil(() => complete);
                
                tip5_1.HideTip(
                    onComplete: () =>
                    {
                        completes[tip5_1] = true;
                    });
                tip5_2.HideTip(
                    onComplete: () =>
                    {
                        completes[tip5_2] = true;
                    });

                yield return new WaitUntil(() => completes.Values.All(c => c));

                onComplete.Invoke();

                yield break;
                
                void OnClickSlot(ItemSlot slot, ItemSO itemData)
                {
                    foodMenuOpenState.UnlockFoodPage.UnlockFoodSlots[0].OnClick -= OnClickSlot;
                    foodMenuOpenState.SelectFoodPage.SelectFoodSlots[0].OnClick -= OnClickSlot;

                    complete = true;
                }
            }
        }
    }
}