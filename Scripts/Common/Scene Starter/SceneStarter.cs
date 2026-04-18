using System;
using UnityEngine;

namespace Common.Scene_Starter
{
    public class SceneStarter : MonoBehaviour
    {
        public virtual void InvokeOnSceneLoad(Action onComplete)
        {
            Debug.Log($"該場景沒有 {nameof(SceneStarter)}，或是沒有 {nameof(SceneStarter)} 繼承 {nameof(InvokeOnSceneLoad)}");
            onComplete.Invoke();
        }

        public virtual void InvokeOnSceneLoad(SceneStarterData starterData, Action onComplete)
        {
            Debug.Log($"該場景沒有 {nameof(SceneStarter)}，或是沒有 {nameof(SceneStarter)} 繼承 {nameof(InvokeOnSceneLoad)}");
            onComplete.Invoke();
        }

        public virtual void InvokeOnSceneChangeComplete()
        {
            Debug.Log($"該場景沒有 {nameof(SceneStarter)}，或是沒有 {nameof(SceneStarter)} 繼承 {nameof(InvokeOnSceneLoad)}");
        }
    }
}