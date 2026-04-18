<<<<<<< HEAD
using Animation_System.DOTween;
=======
using System;
using Animation_System.DOTween;
using Explore_System.System.Child.Battle_System.Object.Card;
>>>>>>> 3ad1eae70e157ec4132887cd1d3419d305c0ca19
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.System.Child
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class CardInformationSystem : MonoBehaviour
    {
<<<<<<< HEAD
=======
        [field: Header("卡片資訊生成點")]
        [field: SerializeField] private RectTransform cardParent;
        
        private GameObject _cardInformation;

        private void Awake()
        {
            if (cardParent is null)
            {
                throw new InvalidOperationException($"{cardParent} 沒有被掛載。");
            }
        }

        public void ShowCardInformation(Card card)
        {
            _cardInformation = Instantiate(card.CardData.CardInformationPrefab, cardParent);
        }

        public void HideCardInformation(Card card)
        {
            Destroy(_cardInformation);
        }
>>>>>>> 3ad1eae70e157ec4132887cd1d3419d305c0ca19
    }
}