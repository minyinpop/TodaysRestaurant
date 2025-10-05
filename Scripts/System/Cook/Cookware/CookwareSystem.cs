using System.Collections;
using System.Cook.Cookware.State_Machine;
using System.Cook.Cookware.State_Machine.State;
using Data.Item.Type.Dish;
using General.Object;
using UnityEngine;

namespace System.Cook.Cookware
{
    internal sealed class CookwareSystem : MonoBehaviour
    {
        [field: Header("Bubble")]
        [field: SerializeField] private GameObject EmptyBubblePrefab;
        [field: SerializeField] private GameObject CookBubblePrefab;
        [field: SerializeField] private GameObject GameTimeBubblePrefab;
        [field: SerializeField] private Transform SpawnParent;

        private GameObject Bubble;
        private Button BubbleScript;
        
        private DishSO DishData;

        private readonly StateMachine StateMachine = new();

        private IEnumerator CookCor;

        public static event Action<Action<DishSO>> OnClickEmptyBubble;

        private void Start()
        {
            OnEmptyState();
        }
        
        #region State Machine
            #region OnEmptyState
                private void OnEmptyState()
                {
                    StateMachine.ChangeState(new OnEmpty(
                        onEnter: () =>
                        {
                            Bubble = Instantiate(EmptyBubblePrefab, SpawnParent);
                            BubbleScript = Bubble.GetComponent<Button>();
                            BubbleScript.SetInteractable(true);
                            BubbleScript.OnClick += OnClick;
                        },
                        onExit: () =>
                        {
                            Destroy(Bubble);
                            Bubble = null;
                            BubbleScript = null;
                        }));
                    return;
                    
                    void OnClick()
                    {
                        BubbleScript.SetInteractable(false);
                        OnClickEmptyBubble?.Invoke(
                            dishData =>
                            {
                                DishData = dishData;
                                if (dishData is null) BubbleScript.SetInteractable(true);
                                else OnCookState();
                            });
                    }
                }
            #endregion

        #region OnCookState
                private void OnCookState()
                {
                    StateMachine.ChangeState(new OnCook(
                        onEnter: () =>
                        {
                            Bubble = Instantiate(CookBubblePrefab, SpawnParent);
                            BubbleScript = Bubble.GetComponent<Button>();
                            CookCor = CookCoroutine();
                            StartCoroutine(CookCor);
                            return;

                            IEnumerator CookCoroutine()
                            {
                                yield break;
                            }
                        },
                        onExit: () =>
                        {
                        }));
                }
            #endregion
        #endregion
    }
}