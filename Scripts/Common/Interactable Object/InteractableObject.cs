using Player_System.Object;

namespace Common.Interactable_Object
{
    public interface InteractableObject
    {
        public void OnEnterDetect();
        public void OnExitDetect();

        public bool OnInteract(PlayerObject playerObject);
    }
}