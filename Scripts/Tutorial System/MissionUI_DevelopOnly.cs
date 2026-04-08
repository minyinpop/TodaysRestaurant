using System;
using Common.Item.Object;
using Common.Scene_Name;
using Common.Scene_Starter;
using TMPro;
using UnityEngine;

namespace Tutorial_System
{
    public sealed class MissionUI_DevelopOnly : MonoBehaviour
    {
        [field: SerializeField] private TextMeshProUGUI targetText;
        [field: SerializeField] private SceneNameSO sceneNameData;
        [field: SerializeField] private SceneStarterData sceneStarterData;

        private int _currentCount;
        private const int _targetCount = 7;

        public static Action<SceneNameSO, SceneStarterData> ChangeScene_DevelopOnly;

        private void Awake()
        {
            ItemObject.OnTake += Add;
        }

        private void Start()
        {
            targetText.text = $"目前獲取 <color=red>{_currentCount}</color>/{_targetCount} 個食材";
        }

        private void OnDestroy()
        {
            ItemObject.OnTake -= Add;
        }

        private void Add()
        {
            _currentCount += 1;
            targetText.text = $"目前獲取 <color=red>{_currentCount}</color>/{_targetCount} 個食材";
            
            if (_currentCount >= _targetCount)
            {
                if (ChangeScene_DevelopOnly is null)
                {
                    throw new InvalidOperationException(nameof(ChangeScene_DevelopOnly) + "沒人訂閱");
                }

                ChangeScene_DevelopOnly.Invoke(sceneNameData, sceneStarterData);
            }
        }
    }
}