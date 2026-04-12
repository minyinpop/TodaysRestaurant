using System;
using UnityEngine;

namespace Common.Scene_Starter
{
    public class SceneStarter : MonoBehaviour
    {
        public virtual void StartSystem(Action onComplete)
        {
            Debug.Log($"該場景沒有 {nameof(SceneStarter)}，或是沒有 {nameof(SceneStarter)} 繼承 {nameof(StartSystem)}");
            onComplete.Invoke();
        }

        public virtual void StartSystem(SceneStarterData starterData, Action onComplete)
        {
            Debug.Log($"該場景沒有 {nameof(SceneStarter)}，或是沒有 {nameof(SceneStarter)} 繼承 {nameof(StartSystem)}");
            onComplete.Invoke();
        }

        public virtual void StartSystemWhenFinish()
        {
            Debug.Log($"該場景沒有 {nameof(SceneStarter)}，或是沒有 {nameof(SceneStarter)} 繼承 {nameof(StartSystem)}");
        }

        public virtual void StartSystemWhenFinish(SceneStarterData starterData)
        {
            Debug.Log($"該場景沒有 {nameof(SceneStarter)}，或是沒有 {nameof(SceneStarter)} 繼承 {nameof(StartSystem)}");
        }

        public virtual void OnTransitionComplete()
        {
            Debug.Log($"該場景沒有 {nameof(SceneStarter)}，或是沒有 {nameof(SceneStarter)} 繼承 {nameof(OnTransitionComplete)}");
        }
    }
}