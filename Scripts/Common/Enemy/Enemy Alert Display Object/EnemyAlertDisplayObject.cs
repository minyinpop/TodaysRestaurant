using System;
using System.Collections;
using UnityEngine;

namespace Common.Enemy.Enemy_Alert_Display_Object
{
    public sealed class EnemyAlertDisplayObject : MonoBehaviour
    {
        private bool _initialized;
        
        private IEnumerator _countDownCoroutine;

        private void OnDisable()
        {
            StopCoroutine(_countDownCoroutine);
        }

        private void OnDestroy()
        {
            _countDownCoroutine = null;
        }

        public void Initialize(int countDownTime, Action onCountDownEnd)
        {
            if (_initialized)
            {
                Debug.Log($"{name} is already initialized.");
                return;
            }
            
            _initialized = true;
            
            _countDownCoroutine = CountDownCoroutine();
            StartCoroutine(_countDownCoroutine);
            return;

            IEnumerator CountDownCoroutine()
            {
                yield return new WaitForSeconds(countDownTime);
                onCountDownEnd.Invoke();
            }
        }
    }
}