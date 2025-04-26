using UnityEngine;

namespace Restaurant.Player
{
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(PlayerAnimator))]
    public class Player : MonoBehaviour
    {
        private PlayerController Controller { get; set; }
        private PlayerAnimator Animator { get; set; }

        private void Awake()
        {
            Controller = GetComponent<PlayerController>();
            Animator = GetComponent<PlayerAnimator>();
        }
    }
}