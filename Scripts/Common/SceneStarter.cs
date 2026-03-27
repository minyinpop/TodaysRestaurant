using System;
using Common.Level.Child.Level_Enemy;
using Common.Level.Main;
using UnityEngine;

namespace Common
{
    public abstract class SceneStarter : MonoBehaviour
    {
        public virtual void StartSystem(BattleEnemyEntry entry) { }

        public virtual void StartSystem(LevelSO levelData, Action onComplete) { }
    }
}