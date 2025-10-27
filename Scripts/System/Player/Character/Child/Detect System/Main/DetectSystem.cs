using System.Collections.Generic;
using System.Economy.Child.Cookware.System.Main;
using System.Economy.Child.Customer.Main;
using System.Player.Character.Child.Detect_System.Child;
using UnityEngine;

namespace System.Player.Character.Child.Detect_System.Main
{
    internal sealed class DetectSystem : MonoBehaviour
    {
        [field: Header("Detect Area")]
        [field: SerializeField] private DetectArea CookwareDetectArea;
        [field: SerializeField] private DetectArea CustomerDetectArea;
        
        private readonly Queue<Action> ActiveActions = new();
        
        private void Start()
        {
            StartDetect();
        }

        private void OnDisable()
        {
            StopDetect();
        }

        private void StartDetect()
        {
            CookwareDetectArea.OnDetect += CookwareOnDetect;
            ActiveActions.Enqueue(() => CookwareDetectArea.OnDetect -= CookwareOnDetect);
            
            CookwareDetectArea.OnUnDetect += CookwareOnUnDetect;
            ActiveActions.Enqueue(() => CookwareDetectArea.OnUnDetect -= CookwareOnUnDetect);

            CustomerDetectArea.OnDetect += CustomerOnDetect;
            ActiveActions.Enqueue(() => CustomerDetectArea.OnDetect -= CustomerOnDetect);
            
            CustomerDetectArea.OnUnDetect += CustomerOnUnDetect;
            ActiveActions.Enqueue(() => CustomerDetectArea.OnUnDetect -= CustomerOnUnDetect);
            return;

            void CookwareOnDetect(GameObject cookware)
            {
                var system = cookware.GetComponent<CookwareSystem>();
                system.SetInteractable(true);
            }
            
            void CookwareOnUnDetect(GameObject cookware)
            {
                var system = cookware.GetComponent<CookwareSystem>();
                system.SetInteractable(false);
            }

            void CustomerOnDetect(GameObject customer)
            {
                var system = customer.GetComponent<CustomerSystem>();
                system.SetInteractable(true);
            }

            void CustomerOnUnDetect(GameObject customer)
            {
                var system = customer.GetComponent<CustomerSystem>();
                system.SetInteractable(false);
            }
        }

        private void StopDetect()
        {
            while (ActiveActions.Count > 0)
            {
                var action = ActiveActions.Dequeue();
                action?.Invoke();
            }
        }
    }
}