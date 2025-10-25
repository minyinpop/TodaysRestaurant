using UnityEngine;

namespace System.Restaurant.Child.Cookware.System.Child.Cook_Bubble.Main
{
    [RequireComponent(typeof(Button))]
    internal abstract class Bubble : MonoBehaviour
    {
        public virtual void SetInteractable(bool interactable) { }

        public event Action OnClick;
        protected void OnClicked() { OnClick?.Invoke(); }
        
        public virtual void CoutDown(float time, Action onComplete) { }
    }
}