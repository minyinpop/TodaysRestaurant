using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Button;
using Common.Dialogue.SO.Main;
using Common.Item.Data;
using Common.Scene_Starter;
using Common.Value.Type;
using Input_System;
using Restaurant_System.Object.Cookware.System;
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
        
        [field: Header("教學 4")]
        [field: SerializeField] private TutorialTip tip6;
        [field: SerializeField] private TutorialTip tip7;
        
        [field: Header("教學 5")]
        [field: SerializeField] private TutorialTip tip8;
        [field: SerializeField] private TutorialTip tip9;
        [field: SerializeField] private CookwareSystem  stockpotSystem;
        
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
            Tutorial1(
                onComplete: () =>
                {
                    Tutorial2(
                        onComplete: () =>
                        {
                            Tutorial3(
                                onComplete: () =>
                                {
                                    Tutorial4(
                                        onComplete: () =>
                                        {
                                            Tutorial5(
                                                onComplete: () =>
                                                {
                                                    Debug.Log("教學結束。");
                                                });
                                        });
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
                            foodMenuOpenState.UnlockFoodPage.SetInteractable(true);
                        });
                });
            
            return;
            
            void OnClickSlot(ItemSlot slot, ItemSO itemData)
            {
                foodMenuOpenState.UnlockFoodPage.UnlockFoodSlots[0].OnClick -= OnClickSlot;
                foodMenuOpenState.UnlockFoodPage.SetInteractable(false);
                
                tip3.HideTip(
                    onComplete: () =>
                    {
                        onComplete.Invoke();
                    });
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
                    foodMenuOpenState.UnlockFoodPage.SetInteractable(false);
                    foodMenuOpenState.SelectFoodPage.SetInteractable(false);

                    complete = true;
                }
            }
        }

        private void Tutorial4(Action onComplete)
        {
            tip6.ShowTip(
                onComplete: () =>
                {
                    tip7.ShowTip(
                        onComplete: () =>
                        {
                            foodMenuOpenState.FoodTypeButtons[1].OnClick += OnClickFoodTypeButton;
                            foodMenuOpenState.FoodTypeButtons[1].SetInteractable(true);
                        });
                });

            return;

            void OnClickFoodTypeButton(FoodType  foodType)
            {
                foodMenuOpenState.FoodTypeButtons[1].OnClick -= OnClickFoodTypeButton;
                foodMenuOpenState.FoodTypeButtons[1].SetInteractable(false);
                
                tip7.HideTip(
                    onComplete: () =>
                    {
                        onComplete.Invoke();
                    });
            }
        }

        private void Tutorial5(Action onComplete)
        {
            tip8.ShowTip(
                onComplete: () =>
                {
                    tip9.ShowTip(
                        onComplete: () =>
                        {
                            foodMenuOpenState.UnlockFoodPage.SetInteractable(true);
                            foodMenuOpenState.SelectFoodPage.SetInteractable(true);

                            foreach (var foodTypeButton in foodMenuOpenState.FoodTypeButtons)
                            {
                                foodTypeButton.SetInteractable(true);
                            }

                            foodMenuOpenState.ConfirmButton.OnClick += OnClickContinue;
                            foodMenuOpenState.ConfirmButton.SetInteractable(true);
                        });
                });

            return;

            void OnClickContinue()
            {
                foodMenuOpenState.SelectFoodPage.SelectFoodSlots[0].GetSlotState(out var slotState);
                
                if (slotState is not ItemSlotState.Select)
                {
                    return;
                }

                foodMenuOpenState.UnlockFoodPage.SetInteractable(false);
                foodMenuOpenState.SelectFoodPage.SetInteractable(false);
                
                foreach (var foodTypeButton in foodMenuOpenState.FoodTypeButtons)
                {
                    foodTypeButton.SetInteractable(false);
                }

                foodMenuOpenState.ConfirmButton.OnClick -= OnClickContinue;
                foodMenuOpenState.ConfirmButton.SetInteractable(false);
                
                tip9.HideTip(
                    onComplete: () =>
                    {
                        stockpotSystem.StartSystem();
                        
                        onComplete.Invoke();
                    });
            }
        }
    }
}