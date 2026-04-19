using System;
using System.Collections;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Animation_System.DOTween.Combine;
using Animation_System.Spine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Explore_System.System.Child.Battle_System.Object.Card.Battle
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class BattleCard : Card
    {
        [field: Header("子資料 - 組件")]
        [field: SerializeField, FormerlySerializedAs("CardRect")] private RectTransform cardRect;
        [field: SerializeField, FormerlySerializedAs("CardSurfaceRect")] private RectTransform cardSurfaceRect;
        [field: SerializeField, FormerlySerializedAs("CardFront")] private GameObject front;
        [field: SerializeField, FormerlySerializedAs("CardBack")] private GameObject back;
        
        [field: Header("子資料 - 動畫")]
        [field: SerializeField, FormerlySerializedAs("DoAnimation")] private new DoAnimation animation;
        
        [field: Header("子資料 - 資料")]
        [field: SerializeField, FormerlySerializedAs("BattleCardData")] private BattleCardSO battleCardData;
                                                                                public BattleCardSO BattleCardData => battleCardData;
        
        [field: Header("子資料 - 選擇順序")]
        [field: SerializeField, FormerlySerializedAs("CardOrderParent")] private Transform orderParent;
        
        public static event Action<Card, SpineAnimation, Action, Action> OnUse;
        
        private GameObject _cardOrder;
        
        private IEnumerator _moveAndFlipCoroutine;

        private void OnDisable()
        {
            if (_moveAndFlipCoroutine is not null)
            {
                StopCoroutine(_moveAndFlipCoroutine);
                _moveAndFlipCoroutine = null;
            }
        }

        #region PointerEvent
            protected override void OnPointerEnter()
            {
                if (Interactable)
                {
                    animation.DoScale_UI(cardSurfaceRect, new DoScale(Vector2.one * 1.25f, .25f, Ease.OutCubic));
                    InvokeOnHover();
                }
            }
            
            protected override void OnPointerExit()
            {
                if (Interactable)
                {
                    animation.DoScale_UI(cardSurfaceRect, new DoScale(Vector2.one, .25f, Ease.OutCubic));
                    InvokeOnHoverExit();
                }
            }
            
            protected override void OnPointerClick()
            {
                if (Interactable)
                {
                    InvokeOnClick();
                }
            }
        #endregion

        #region Main Function
            public override void Use(Action haveEnemyAlive, Action enemyAllDeath)
            {
                OnUse?.Invoke(this, battleCardData.AttackSpine[Random.Range(0, battleCardData.AttackSpine.Count)],
                    () =>
                    {
                        // haveEnemyAlive
                        haveEnemyAlive?.Invoke();
                    },
                    () =>
                    {
                        // enemyAllDeath
                        enemyAllDeath?.Invoke();
                    });
            }

            public override void DestroyCard(Action onComplete)
            {
                animation.DoScale_UI(
                    rect: cardRect,
                    settings: new DoScale(Vector2.zero, .5f, Ease.InOutBack),
                    onComplete: () =>
                    {
                        onComplete.Invoke();
                        Destroy(gameObject);
                    });
            }
        #endregion

        #region Order
            public void SetCardOrder(GameObject cardOrderPrefab)
            {
                if (_cardOrder is not null)
                    Destroy(_cardOrder);
                _cardOrder = Instantiate(cardOrderPrefab, orderParent);
            }
            
            public void RemoveCardOrder()
            {
                if (_cardOrder is null) return;
                Destroy(_cardOrder);
                _cardOrder = null;
            }
        #endregion
            
        #region Animation
            public void Move(Transform parent, DoAnchorPos settings, Action onComplete = null)
            {
                cardRect.SetParent(parent);
                animation.DoAnchorPos(
                    rect: cardRect,
                    settings: settings,
                    onComplete: () =>
                    {
                        onComplete?.Invoke();
                    });
            }

            public void MoveAndFlip(Transform parent, DoAnchorPos anchorPosSettings, DoFlip flipSettings, Action onComplete = null)
            {
                _moveAndFlipCoroutine = MoveAndFlipCoroutine();
                StartCoroutine(_moveAndFlipCoroutine);
                return;
                
                IEnumerator MoveAndFlipCoroutine()
                {
                    var rotateComplete = false;
                    var scaleComplete = false;
                    cardRect.SetParent(parent);
                    flipSettings.GetValues(out var rotateSettings, out var scaleSettings01, out var scaleSettings02);
                    animation.DoAnchorPos(
                        rect: cardRect,
                        settings: anchorPosSettings,
                        onComplete: () =>
                        {
                            animation.DoRotate(
                                rect: cardSurfaceRect,
                                settings: rotateSettings,
                                onUpdate: () =>
                                {
                                    var y = cardSurfaceRect.eulerAngles.y;
                                    if (back.activeSelf && y is < 270 and > 90)
                                    {
                                        front.SetActive(true);
                                        back.SetActive(false);
                                    }
                                    else if (front.activeSelf && y is < 360 and > 270 or < 90 and > 0)
                                    {
                                        front.SetActive(false);
                                        back.SetActive(true);
                                    }
                                },
                                onComplete: () =>
                                {
                                    rotateComplete = true;
                                });
                            animation.DoScale_UI(
                                rect: cardSurfaceRect,
                                settings: scaleSettings01,
                                onComplete: () =>
                                {
                                    animation.DoScale_UI(
                                        rect: cardSurfaceRect,
                                        settings: scaleSettings02,
                                        onComplete: () =>
                                        {
                                            scaleComplete = true;
                                        });
                                });
                        });
                    yield return new WaitUntil(() => rotateComplete && scaleComplete);
                    onComplete?.Invoke();
                    _moveAndFlipCoroutine = null;
                }
            }
        #endregion
    }
}