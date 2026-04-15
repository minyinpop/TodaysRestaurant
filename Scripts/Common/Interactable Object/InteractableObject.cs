using Player_System.Object;

namespace Common.Interactable_Object
{
    public interface InteractableObject
    {
        public void OnEnterDetect(PlayerObject playerObject);
        public void OnExitDetect(PlayerObject playerObject);

        public void OnInteractStart();
        public bool OnInteract(PlayerObject playerObject);
        public void OnInteractEnd();
    }
}