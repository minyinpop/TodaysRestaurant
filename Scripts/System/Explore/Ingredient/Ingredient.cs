using Common.Clickable_Bubble.Interface;
using Interface;
using UnityEngine;

namespace System.Explore.Ingredient
{
    internal class Ingredient : MonoBehaviour, InteractableObject
    {
        public IClickableBubble Bubble;

        private void Start()
        {
            Bubble.SetInteractable(true);
        }

        #region InteractableObject
            public void OnEnterDetect()
            {
                Debug.Log(gameObject.name);
            }

            public void OnExitDetect()
            {
            }
        #endregion
    }
}