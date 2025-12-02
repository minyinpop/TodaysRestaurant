using System.Economy.Child.Cookware.System.Child.Cook_Bubble.Main;
using Interface;
using UnityEngine;

namespace System.Explore.Ingredient
{
    internal class Ingredient : MonoBehaviour, InteractableObject
    {
        public Bubble Bubble;

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