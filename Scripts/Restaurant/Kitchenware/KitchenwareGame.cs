using Restaurant.Mini_Game;
using UnityEngine;

namespace Restaurant.Kitchenware
{
    internal class KitchenwareGame : MonoBehaviour
    {
        [field: Header("小遊戲的生成位置")]
        [field: SerializeField] private Transform GameParent { get; set; }
        
        [field: Header("小遊戲的預製件")]
        [field: SerializeField] private GameObject GamePrefab { get; set; }
        private GameObject GameObj { get; set; }
        
        private KitchenwareManager KitchenwareManager { get; set; }

        private void Awake()
        {
            KitchenwareManager = GetComponent<KitchenwareManager>();
        }

        public void OnGameStart()
        {
            GameObj = Instantiate(GamePrefab, GameParent);
            GameObj.GetComponent<MiniGameBase>().Init(this);
        }

        public void OnGameCancel()
        {
            if (GameObj is null)
                return;

            Destroy(GameObj);
            GameObj = null;
        }

        public void OnGameFinish()
        {
            KitchenwareManager.OnGameFinish();
            
            Destroy(GameObj);
            GameObj = null;
        }
    }
}