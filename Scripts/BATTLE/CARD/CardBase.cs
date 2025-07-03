using BATTLE.CARD.STATE_MACHINE;
using BATTLE.CARD.STATE_MACHINE.STATE;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BATTLE.CARD
{
    internal abstract class CardBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [field: SerializeField] private RectTransform SelectionOrderParent;
        private GameObject SelectionOrderObject;
        
        private CardStateMachine StateMachine = new();

        private void Start()
        {
            ChangeState(new DeselectedState());
        }
        
        
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            StateMachine.OnPointerEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            StateMachine.OnPointerExit();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            StateMachine.OnPointerClick();
        }
        
        
        
        /// <summary>
        /// 用於更換卡片狀態的方法
        /// 可用於外部直接呼叫並做更換
        /// </summary>
        /// <param name="newState"> 下個狀態 </param>
        public void ChangeState(ICardState newState)
        {
            StateMachine.ChangeState(newState, this);
        }
        
        
        
        public void SetSelectionOrder(GameObject prefab)
        {
            SelectionOrderObject = Instantiate(prefab, SelectionOrderParent);
        }

        public void RemoveSelectionOrder()
        {
            
        }
    }
}