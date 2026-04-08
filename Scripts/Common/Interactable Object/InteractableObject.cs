using Player_System.Object;

namespace Common.Interactable_Object
{
    public interface InteractableObject
    {
        public void OnEnterDetect(PlayerObject playerObject);
        public void OnExitDetect(PlayerObject playerObject);

        public bool OnInteract(PlayerObject playerObject);
    }
}