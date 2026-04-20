using System;
using Common.Dialogue.SO.Main;
using Common.Scene_Starter;
using Restaurant_System.System.Main;
using UI_System.Dialogue_UI_System.Lite.Main;
using UnityEngine;

namespace Tutorial_System
{
    public sealed class RestaurantServesTutorialSystem : SceneStarter
    {
        [field: Header("系統")]
        [field: SerializeField] private DialogueLiteUISystem dialogueLiteUISystem;
        [field: SerializeField] private RestaurantSystem restaurantSystem;

        private void Awake()
        {
            if (dialogueLiteUISystem is null)
            {
                throw new InvalidOperationException($"{nameof(dialogueLiteUISystem)} 沒有被掛載。");
            }
            
            if (restaurantSystem is null)
            {
                throw new InvalidOperationException($"{nameof(restaurantSystem)} 沒有被掛載。");
            }
        }

        public override void InvokeOnSceneChangeComplete(SceneStarterData starterData)
        {
            if (starterData is null)
            {
                throw new ArgumentNullException(nameof(starterData), $"{nameof(starterData)} 不能傳入空值。");
            }

            if (starterData is not DialogueSO dialogueData)
            {
                throw new InvalidOperationException($"{nameof(starterData)} 不是 {nameof(DialogueSO)}。");
            }

            dialogueLiteUISystem.StartDialogue(
                dialogueData: dialogueData,
                onComplete: () =>
                {
                    restaurantSystem.StartSystem();
                });
        }
    }
}