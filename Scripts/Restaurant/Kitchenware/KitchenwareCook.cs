using System;
using System.Collections;
using Restaurant.Kitchenware.Bubble;
using UnityEngine;

namespace Restaurant.Kitchenware
{
    public class KitchenwareCook : MonoBehaviour
    {
        [field: Header("氣泡的生成位置")]
        [field: SerializeField] private Transform BubbleParent { get; set; }
        
        [field: Header("氣泡的預製件")]
        [field: SerializeField] private GameObject EmptyBubblePrefab { get; set; }
        
        private GameObject CurrentBubble { get; set; }
        private BubbleBase CurrentBubbleScript { get; set; }
        private bool IsBubbleClick { get; set; }
        
        private IEnumerator MainCoroutine { get; set; }
        private IEnumerator BubbleCoroutine { get; set; }
        
        private event Action OnBubbleClickEvent;

        private void Start()
        {
            MainCoroutine = MainProcess();
            StartCoroutine(MainCoroutine);
        }

        private void OnDisable()
        {
            if (MainCoroutine is not null)
            {
                StopCoroutine(MainCoroutine);
                MainCoroutine = null;
            }
            
            if (BubbleCoroutine is not null)
            {
                StopCoroutine(BubbleCoroutine);
                BubbleCoroutine = null;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            CurrentBubbleScript.PlayerEnter();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            CurrentBubbleScript.PlayerLeave();
        }

        private IEnumerator MainProcess()
        {
            BubbleCoroutine = InitEmptyBubble();
            yield return BubbleCoroutine;
        }

        private IEnumerator InitEmptyBubble()
        {
            CurrentBubble = Instantiate(EmptyBubblePrefab, BubbleParent);
            CurrentBubbleScript = CurrentBubble.GetComponent<BubbleBase>();
            
            yield return new WaitUntil(() => IsBubbleClick);
            IsBubbleClick = false;
        }
        
        private void OnBubbleClick() => IsBubbleClick = true;
    }
}