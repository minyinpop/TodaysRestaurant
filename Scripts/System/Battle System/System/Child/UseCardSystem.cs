using System.Battle_System.Object.Card_Slot;
using System.Battle_System.Object.Card.Base;
using System.Battle_System.System.Child.Selected_Card_System.Main;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data.Animation.DOTween.Basic;
using Data.General;
using DG.Tweening;
using UnityEngine;

namespace System.Battle_System.System.Child
{
    internal sealed class UseCardSystem : MonoBehaviour
    {
        [field: Header("Card Slot")]
        [field: SerializeField] private Transform SpawnParent;
        [field: SerializeField] private GameObject SlotPrefab;
        
        private readonly List<CardSlot> CardSlots = new();

        private IEnumerator CurrentCor;
        private IEnumerator RecycleCor;

        private void OnEnable()
        {
            SelectedCardSystem.BeforeCloseUI += Add;
        }

        private void OnDisable()
        {
            SelectedCardSystem.BeforeCloseUI -= Add;
            if (CurrentCor is not null)
            {
                StopCoroutine(CurrentCor);
                CurrentCor = null;
            }
            
            if (RecycleCor is not null)
            {
                StopCoroutine(RecycleCor);
                RecycleCor = null;
            }
        }

        private void Add(List<ICard> cards, Action onComplete = null)
        {
            for (var i = 0; i < cards.Count; i++)
            {
                var index = i;
                var slot = Instantiate(SlotPrefab, SpawnParent);
                var slotScript = slot.GetComponent<CardSlot>();
                var card = cards[index];
                CardSlots.Add(slotScript);
                slotScript.Set(card);
                card.Move(slot.transform, new DoAnchorPos(Vector2.zero, .5f, true, Ease.OutExpo), () =>
                {
                    if (index != cards.Count - 1) return;
                    onComplete?.Invoke();
                });
            }
        }
        
        public void Use(Action<bool> haveEnemyAlive, Action enemyAllDeath)
        {
            CurrentCor = UseCoroutine(haveEnemyAlive, enemyAllDeath);
            StartCoroutine(CurrentCor);
        }

        private IEnumerator UseCoroutine(Action<bool> haveEnemyAlive, Action enemyAllDeath)
        {
            var onUseComplete = false;
            var haveAnyEnemyAlive = false;
            var slot = CardSlots[0];
            slot.Get(out var card);
            card.Use(
                haveEnemyAlive: () =>
                {
                    onUseComplete = true;
                    haveAnyEnemyAlive = true;
                },
                enemyAllDeath: () =>
                {
                    onUseComplete = true;
                });
            card.DestroyCard(() =>
            {
                CardSlots.Remove(slot);
                Destroy(slot.gameObject);
            });
            yield return new WaitUntil(() => onUseComplete);
            yield return new WaitForSeconds(1);
            if (haveAnyEnemyAlive)
                haveEnemyAlive?.Invoke(CardSlots.Any());
            else
                enemyAllDeath?.Invoke();
            CurrentCor = null;
        }
        
        public void RecycleCard(CardType targetType, Action onComplete)
        {
            RecycleCor = RecycleCardCoroutine();
            StartCoroutine(RecycleCor);
            return;
            
            IEnumerator RecycleCardCoroutine()
            {
                var completes = new List<bool>();
                foreach (var slot in CardSlots)
                {
                    slot.Get(out var card);
                    card.GetCardType(out var type);
                    if (type == targetType)
                    {
                        completes.Add(false);
                        var index = completes.Count - 1;
                        card.DestroyCard(
                            onComplete: () =>
                            {
                                completes[index] = true;
                            });
                    }
                    else
                        slot.Set(card);
                }
                
                yield return new WaitUntil(() => completes.All(c => c));
                onComplete?.Invoke();
            }
        }
    }
}