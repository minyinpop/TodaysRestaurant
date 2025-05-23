using Restaurant.Kitchenware;
using UnityEngine;

namespace Restaurant.Mini_Game
{
    internal abstract class MiniGameBase : MonoBehaviour
    {
        public abstract void Init(KitchenwareGame game);
    }
}