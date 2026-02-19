using Player_System.System;

namespace Common.Interactable_Object
{
    public interface InteractableObject
    {
        public void OnEnterDetect();
        public void OnExitDetect();

        public bool OnInteract(PlayerSystem playerSystem);
    }
}