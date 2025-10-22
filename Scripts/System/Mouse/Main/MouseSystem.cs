using System.Collections.Generic;
using System.Cook.Child.Cookware.System.Child.Cook_Game.Object;
using System.Input.Main;
using System.Mouse.Child.Item;
using UnityEngine;
using UnityEngine.EventSystems;

namespace System.Mouse.Main
{
    internal sealed class MouseSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private ItemDragSystem ItemDragSystem;
        
        [field: Header("Camera")]
        [field: SerializeField] private Camera MainCamera;
        
        [field: Header("Event System")]
        [field: SerializeField] private EventSystem EventSystem;
        
        [field: Header("Tag")]
        [field: SerializeField] private string ItemSlotTag;
        [field: SerializeField] private string UtensilsTag;
        
        private void OnEnable()
        {
            InputSystem.OnClickMouseLeftButton += OnPointerClick;
        }
        
        private void OnDisable()
        {
            InputSystem.OnClickMouseLeftButton -= OnPointerClick;
        }

        private void OnPointerClick()
        {
            InputSystem.GetMousePosition(out var position);
            if (!UI())
            {
                WorldSpace();
            }
            
            return;

            bool UI()
            {
                var pointer = new PointerEventData(EventSystem)
                {
                    position = position
                };
                var results = new List<RaycastResult>();
                EventSystem.RaycastAll(pointer, results);
                foreach (var result in results)
                {
                    var obj = result.gameObject;
                    if (obj.CompareTag(ItemSlotTag))
                    {
                        ItemDragSystem.OnClick(obj);
                        return true;
                    }
                }

                return false;
            }

            void WorldSpace()
            {
                var ray = MainCamera.ScreenPointToRay(position);
                var hit2D = Physics2D.GetRayIntersection(ray);
                if (hit2D.collider is not null)
                {
                    var obj = hit2D.collider.gameObject;
                    if (obj.CompareTag(UtensilsTag))
                    {
                        obj.GetComponent<Utensils>().OnClick(MainCamera);
                        return;
                    }
                }
            }
        }
    }
}