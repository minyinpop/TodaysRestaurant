using Restaurant.Mini_Game;
using UnityEngine;

namespace Restaurant.Kitchenware
{
    internal class KitchenwareGame : MonoBehaviour
    {
        private Transform GameParent { get; set; }
        
        private GameObject GamePrefab { get; set; }
        
        private GameObject GameObj { get; set; }
        
        private KitchenwareManager KitchenwareManager { get; set; }

        private void Awake()
        {
            KitchenwareManager = GetComponent<KitchenwareManager>();
        }

        public void Init(Transform gameParent, GameObject gamePrefab)
        {
            GameParent = gameParent;
            GamePrefab = gamePrefab;
        }

        public void OnGameStart()
        {
            KitchenwareManager.CinemachineCamera.Priority = 20;
            GameObj = Instantiate(GamePrefab, GameParent);
            GameObj.GetComponent<MiniGameBase>().Init(this);
        }

        public void OnGameCancel()
        {
            KitchenwareManager.CinemachineCamera.Priority = 0;
            
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