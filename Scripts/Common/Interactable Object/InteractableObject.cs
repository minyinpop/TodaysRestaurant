using Player_System.Object;

namespace Common.Interactable_Object
{
    public interface InteractableObject
    {
        public bool Interactable { get; }
        
        public void OnEnterDetect(PlayerObject playerObject);
        public void OnExitDetect();

        public void Interact(PlayerObject playerObject);
    }
}