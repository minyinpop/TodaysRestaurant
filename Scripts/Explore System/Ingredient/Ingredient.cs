using Common;
using Common.Object;
using UnityEngine;

namespace Explore_System.Ingredient
{
    internal class Ingredient : MonoBehaviour, InteractableObject
    {
        public ClickableBubble Bubble;

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