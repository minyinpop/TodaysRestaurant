using System.Collections.Generic;
using Common.Data.Level.Main;
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
        
        private readonly List<GameObject> _containers = new();

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
                Debug.Log($"{nameof(LevelInformationUI)} > {nameof(Initialize)} > is already initialized.)");
            }
            else
            {
                _initialized = true;
                Spawn(levelData);
            }
        }

        public void Refresh(LevelSO levelData)
        {
            if (_initialized)
            {
                foreach (var container in _containers)
                {
                    Destroy(container);
                }

                _containers.Clear();
                
                Spawn(levelData);
            }
            else
            {
                Debug.Log($"{nameof(LevelInformationUI)} > need to initialize first.)");
            }
        }

        private void Spawn(LevelSO levelData)
        {
            #region Ingredient
                InstantiateContainer(ingredientSlotContainerParent, out var container);

                foreach (var ingredient in levelData.LevelIngredient.IngredientsData)
                {
                    if (!container.CanAddSlot())
                    {
                        InstantiateContainer(ingredientSlotContainerParent, out container);
                    }
                    
                    var newSlot = Instantiate(slotPrefab);
                    newSlot.Initialize(ingredient);
                    
                    container.TryAddSlot(newSlot);
                }

                container.FullSlot(slotPrefab);
            #endregion
            
            #region Enemy
                InstantiateContainer(enemySlotContainerParent, out container);

                foreach (var enemy in levelData.LevelEnemy.EnemiesData)
                {
                    if (!container.CanAddSlot())
                    {
                        InstantiateContainer(enemySlotContainerParent, out container);
                    }
                        
                    var newSlot = Instantiate(slotPrefab);
                    newSlot.Initialize(enemy);
                        
                    container.TryAddSlot(newSlot);
                }

                container.FullSlot(slotPrefab);
            #endregion

            return;

            void InstantiateContainer(RectTransform parent, out LevelInformationSlotContainer container)
            {
                container = Instantiate(slotContainerPrefab, parent);
                _containers.Add(container.gameObject);
            }
        }
    }
}