using System.Collections;
using Restaurant.Kitchenware.Bubble.Category;
using UnityEngine;

namespace Restaurant.Kitchenware
{
    public class KitchenwareGameManager : MonoBehaviour
    {
        [field: Header("小遊戲的生成位置")]
        [field: SerializeField] private Transform GameParent { get; set; }
        
        [field: Header("小遊戲的預製件")]
        [field: SerializeField] private GameObject GamePrefab { get; set; }
        private GameObject Game { get; set; }

        private bool GameFinish { get; set; }
        
        private IEnumerator MainCoroutine { get; set; }
        
        private KitchenwareCookBubble KitchenwareCookBubble { get; set; }

        private void OnDisable()
        {
            if (MainCoroutine is not null)
            {
                StopCoroutine(MainCoroutine);
                MainCoroutine = null;
            }
        }

        public void StartGame()
        {
            MainCoroutine = MainProcess();
            StartCoroutine(MainCoroutine);
        }

        private IEnumerator MainProcess()
        {
            Game = Instantiate(GamePrefab, GameParent);
            
            yield return new WaitUntil(() => GameFinish);
        }
    }
}