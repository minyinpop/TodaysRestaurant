using UnityEngine;

namespace Interactable_Object.Base
{
    internal abstract class InteractableBase : MonoBehaviour
    {
        internal abstract void Select();
        internal abstract void Deselect();
        internal abstract bool Interact();
    }
}