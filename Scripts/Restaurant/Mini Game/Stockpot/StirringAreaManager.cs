using System;
using UnityEngine;

namespace Restaurant.Mini_Game.Stockpot
{
    internal class StirringAreaManager : MonoBehaviour
    {
        [field: Header("可以被拖曳的物件標籤")]
        [field: SerializeField] private LayerMask DraggableLayer { get; set; }
        
        public static event Action DraggableObjectEnter;
        public static event Action DraggableObjectExit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (1 << other.gameObject.layer != DraggableLayer.value)
                return;
            
            DraggableObjectEnter?.Invoke();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (1 << other.gameObject.layer != DraggableLayer.value)
                return;
            
            DraggableObjectEnter?.Invoke();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (1 << other.gameObject.layer != DraggableLayer.value)
                return;
            
            DraggableObjectExit?.Invoke();
        }
    }
}