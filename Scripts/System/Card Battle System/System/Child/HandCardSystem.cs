using System.Card_Battle_System.Object.Card_Slot;
using System.Card_Battle_System.Object.Card.Base;
using System.Collections;
using System.Collections.Generic;
using Data.DOTween.Basic;
using DG.Tweening;
using UnityEngine;

namespace System.Card_Battle_System.System.Child
{
    internal sealed class HandCardSystem : MonoBehaviour
    {
        [field: Header("Card Slot")]
        [field: SerializeField] private Transform SpawnParent;
        [field: SerializeField] private GameObject SlotPrefab;
        
        private readonly List<CardSlot> CardSlots = new();
        
        private readonly DoAnchorPos AnchorPosSettings = new(Vector3.zero, .5f, true, Ease.OutExpo);
        
        private const float AddDuration = .25f;
        
        private IEnumerator AddCor;

        private void OnDisable()
        {
            if (AddCor is not null)
            {
                StopCoroutine(AddCor);
                AddCor = null;
            }
        }
        
        /// <summary>
        /// 添加卡片到玩家的牌堆
        /// </summary>
        /// <param name="cards">被添加的卡片</param>
        /// <param name="onComplete">完成後的回傳</param>
        public void Add(List<ICard> cards, Action onComplete = null)
        {
            AddCor = AddCoroutine(cards, onComplete);
            StartCoroutine(AddCor);
        }

        private IEnumerator AddCoroutine(List<ICard> cards, Action onComplete = null)
        {
            for (var i = 0; i < cards.Count; i++)
            {
                var index = i;
                var slot = Instantiate(SlotPrefab, SpawnParent);
                var slotScript = slot.GetComponent<CardSlot>();
                var card = cards[index];
                
                CardSlots.Add(slotScript);
                slotScript.Set(card);
                card.MoveToParent(slot.transform, AnchorPosSettings, () =>
                {
                    if (index != cards.Count - 1) return;
                    onComplete?.Invoke();
                    AddCor = null;
                });
                
                yield return new WaitForSeconds(AddDuration);
            }
        }
    }
}