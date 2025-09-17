using System.Battle_System.Object.Card.Base;
using System.Battle_System.Object.Card.Base.Attack_Type;
using System.Battle_System.Object.Card.Type.Battle.System.Main;
using System.Battle_System.Object.Character.Type.Enemy.Base;
using System.Battle_System.Object.Character.Type.Friendly.Base;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace System.Battle_System.System.Child
{
    internal sealed class EnemyTeamSystem : MonoBehaviour
    {
        [field: Header("Object")]
        [field: SerializeField] private List<EnemyBase> Enemies;

        private IEnumerator CurrentCor;
        
        private void OnEnable()
        {
            FriendlyBase.OnAttack += Hurt;
        }

        private void OnDisable()
        {
            FriendlyBase.OnAttack -= Hurt;
            if (CurrentCor is not null)
            {
                StopCoroutine(CurrentCor);
                CurrentCor = null;
            }
        }

        public void Attack(Action onComplete = null)
        {
            if (CurrentCor is not null)
            {
                StopCoroutine(CurrentCor);
                CurrentCor = null;
            }

            CurrentCor = AttackCoroutine(onComplete);
            StartCoroutine(CurrentCor);
        }

        private IEnumerator AttackCoroutine(Action onComplete = null)
        {
            var enemies = Enemies.ToList();
            for (var i = enemies.Count; i > 0; i--)
            {
                var randomIndex = UnityEngine.Random.Range(0, enemies.Count);
                var enemy = enemies[randomIndex];
                var attackComplete = false;
                enemies.Remove(enemy);
                enemy.Attack(() =>
                {
                    attackComplete = true;
                    if (enemies.Count != 0) return;
                    onComplete?.Invoke();
                });
                yield return new WaitUntil(() => attackComplete);
            }

            CurrentCor = null;
        }

        #region Hurt
            private void Hurt(BattleCard card, Action onComplete = null)
            {
                switch (card)
                {
                    case ISingleAttack:
                    {
                        var randomIndex = UnityEngine.Random.Range(0, Enemies.Count);
                        Enemies[randomIndex].Hurt(card, onComplete);
                        break;
                    }
                    case IAoEAttack:
                    {
                        for (var i = 0; i < Enemies.Count; i++)
                        {
                            var index = i;
                            Enemies[index].Hurt(card, () =>
                            {
                                if (index != Enemies.Count - 1) return;
                                onComplete?.Invoke();
                            });
                        }

                        break;
                    }
                }
            }
        #endregion
    }
}