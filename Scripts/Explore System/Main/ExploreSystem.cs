using Explore_System.Child.Ingredient_Spawn_System;
using Explore_System.Main.State_Machine;
using Explore_System.Main.State_Machine.State;
using UnityEngine;

namespace Explore_System.Main
{
    public sealed class ExploreSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private IngredientSpawnSystem ingredientSpawnSystem;

        private readonly StateMachine _stateMachine = new();

        private void Awake()
        {
            if (ingredientSpawnSystem == null)
            {
                Debug.LogError($"{name} > {nameof(ExploreSystem)} > {nameof(ingredientSpawnSystem)} cannot be null.");
                gameObject.SetActive(false);
                return;
            }
        }

        public void StartSystem()
        {
            RoundStart();
        }

        public void EndSystem()
        {
        }
        
        #region State Machine
            #region RoundStart
                private void RoundStart()
                {
                    _stateMachine.ChangeState(new RoundStart(OnEnter, OnExit));
                    return;

                    void OnEnter()
                    {
                    }

                    void OnExit()
                    {
                    }
                }
            #endregion
        #endregion
    }
}