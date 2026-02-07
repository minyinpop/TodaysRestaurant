using System.Collections;
using Common.Data.Level.Main;
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

        private IEnumerator _initializeCoroutine;

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

        private void OnDestroy()
        {
            if (_initializeCoroutine != null)
            {
                StopCoroutine(_initializeCoroutine);
                _initializeCoroutine = null;
            }
        }

        public void Initialize(LevelSO levelData)
        {
            if (_initialized)
            {
                Debug.Log($"{nameof(LevelInformationUI)} > {nameof(Initialize)} > {nameof(InitializeCoroutine)} is already initialized.)");
            }
            else
            {
                _initialized = true;
                
                _initializeCoroutine = InitializeCoroutine();
                StartCoroutine(_initializeCoroutine);
            }
            
            return;

            IEnumerator InitializeCoroutine()
            {
                #region Ingredient
                    var newContainer = Instantiate(slotContainerPrefab, ingredientSlotContainerParent);

                    foreach (var ingredient in levelData.LevelIngredient.IngredientsData)
                    {
                        if (!newContainer.CanAddSlot())
                        {
                            newContainer = Instantiate(slotContainerPrefab, ingredientSlotContainerParent);
                        }
                        
                        var newSlot = Instantiate(slotPrefab);
                        newSlot.Initialize(ingredient);
                        
                        newContainer.TryAddSlot(newSlot);
                    }

                    while (true)
                    {
                        if (newContainer.CanAddSlot())
                        {
                            var newSlot = Instantiate(slotPrefab);
                            newContainer.TryAddSlot(newSlot);
                        }

                        yield return null;
                    }
                #endregion
                
                #region Enemy
                    newContainer = Instantiate(slotContainerPrefab, enemySlotContainerParent);

                    foreach (var enemy in levelData.LevelEnemy.EnemiesData)
                    {
                        if (!newContainer.CanAddSlot())
                        {
                            newContainer = Instantiate(slotContainerPrefab, enemySlotContainerParent);
                        }

                        var newSlot = Instantiate(slotPrefab);
                        newSlot.Initialize(enemy);
                    }
                #endregion
            }
        }
    }
}