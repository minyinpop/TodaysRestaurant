using System.Collections.Generic;
using Input_System;
using Player_System.Object;
using Restaurant_System.Object.Cookware.Object.Cook_Game.Object;
using UI_System.Player_UI_System.Main;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player_System.System

{
    public partial class PlayerSystem
    {
        [field: Header("Mouse System")]
        [field: SerializeField] private ItemDragSystem itemDragSystem;
        [field: SerializeField] private Camera mainCamera;
        [field: SerializeField] private EventSystem eventSystem;
        
        [field: Header("Tag")]
        [field: SerializeField] private string storageSlot;
        [field: SerializeField] private string utensilsTag;

        private void OnClickedLeftButton()
        {
            var position = InputSystem.MousePosition;
        
            if (!UI())
            {
                WorldSpace();
            }

            return;

            bool UI()
            {
                var pointer = new PointerEventData(eventSystem)
                {
                    position = position
                };
                
                var results = new List<RaycastResult>();
                eventSystem.RaycastAll(pointer, results);
                
                if (results.Count == 0) return false;
                if (results[0].gameObject.CompareTag(storageSlot))
                {
                    itemDragSystem.OnClick(results[0].gameObject);
                    return true;
                }

                return false;
            }
            
            void WorldSpace()
            {
                var ray = mainCamera.ScreenPointToRay(position);
                var hit2D = Physics2D.GetRayIntersection(ray);
                if (hit2D.collider is null) return;
                
                var obj = hit2D.collider.gameObject;
                if (obj.CompareTag(utensilsTag)) obj.GetComponent<Utensils>().OnClick(mainCamera);
            }
        }

        private void OnClickedRightButton()
        {
            PlayerUISystem.ClickRightButton();
        }
    }
}