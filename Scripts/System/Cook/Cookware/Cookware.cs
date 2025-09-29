using System.Cook.Cookware.State_Machine;
using System.Cook.Cookware.State_Machine.State;
using General.Object;
using UnityEngine;

namespace System.Cook.Cookware
{
    internal sealed class Cookware : MonoBehaviour
    {
        [field: Header("Bubble")]
        [field: SerializeField] private GameObject EmptyBubblePrefab;
        [field: SerializeField] private Transform SpawnParent;

        private GameObject Bubble;
        private Button BubbleScript;
        
        private readonly StateMachine StateMachine = new();

        public static event Action OnClickEmptyBubble;

        private void Start()
        {
            OnEmptyState();
        }
        
        #region State Machine
            private void OnEmptyState()
            {
                StateMachine.ChangeState(new Empty(
                    onEnter: () =>
                    {
                        Bubble = Instantiate(EmptyBubblePrefab, SpawnParent);
                        BubbleScript = Bubble.GetComponent<Button>();
                        BubbleScript.SetInteractable(true);
                        BubbleScript.OnClick += OnClick;
                        return;

                        void OnClick()
                        {
                            BubbleScript.SetInteractable(false);
                            OnClickEmptyBubble?.Invoke();
                        }
                    },
                    onExit: () =>
                    {
                    }));
            }
        #endregion
    }
}