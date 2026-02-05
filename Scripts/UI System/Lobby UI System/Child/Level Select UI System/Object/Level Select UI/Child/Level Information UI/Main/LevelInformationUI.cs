using Common.Level.Main;
using Common.Object.Storage_Slot.Child;
using UI_System.Lobby_UI_System.Child.Level_Select_UI_System.Object.Level_Select_UI.Child.Level_Information_UI.Child;
using UnityEngine;

namespace UI_System.Lobby_UI_System.Child.Level_Select_UI_System.Object.Level_Select_UI.Child.Level_Information_UI.Main
{
    public sealed class LevelInformationUI : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private RectTransform ingredientSlotContainerParent;
        [field: SerializeField] private RectTransform enemySlotContainerParent;
        [field: SerializeField] private LevelInformationSlotContainer slotContainerPrefab;
        [field: SerializeField] private LevelInformationSlot slotPrefab;

        private bool _initialized;

        private void Awake()
        {
            if (ingredientSlotContainerParent == null)
            {
                Debug.Log($"{nameof(LevelInformationUI)} > {nameof(ingredientSlotContainerParent)} cannot be null.");
                return;
            }
            
            if (slotContainerPrefab == null)
            {
                Debug.Log($"{nameof(LevelInformationUI)} > {nameof(slotContainerPrefab)} cannot be null.");
                return;
            }
            
            if (slotPrefab == null)
            {
                Debug.Log($"{nameof(LevelInformationUI)} > {nameof(slotPrefab)} cannot be null.");
            }
        }

        public void Initialize(LevelSO levelData)
        {
            if (_initialized)
            {
                Debug.Log($"{nameof(LevelInformationUI)} > {nameof(Initialize)} is already initialized.)");
            }
            else
            {
                _initialized = true;

                const int maxSlotsPerRow = 4;
                var currentSlotsAmount = 0;
                
                var currentContainer = Instantiate(slotContainerPrefab, ingredientSlotContainerParent);
                
                // Ingredient TODO 更改成每行有 4 個
                foreach (var ingredientData in levelData.LevelIngredient.ingredientsData)
                {
                    if (currentSlotsAmount < maxSlotsPerRow)
                    {
                        var newSlot = Instantiate(slotPrefab, currentContainer.transform);
                        
                        currentSlotsAmount++;
                    }
                    else
                    {
                        currentContainer = Instantiate(slotContainerPrefab, ingredientSlotContainerParent);
                        
                        var newSlot = Instantiate(slotPrefab, currentContainer.transform);
                        
                        currentSlotsAmount = 1;
                    }
                }
            }
        }
    }
}