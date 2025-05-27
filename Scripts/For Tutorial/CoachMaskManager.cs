using System.Collections;
using Restaurant.Kitchenware;
using Restaurant.Mini_Game.Stockpot;
using UnityEngine;
using UtageExtensions;

namespace For_Tutorial
{
    public class CoachMaskManager : MonoBehaviour
    {
        [Header("圖片")]
        [SerializeField] private RectTransform coachMask;
        [SerializeField] private RectTransform finger;

        private void Awake()
        {
            if (coachMask is null)
            {
#if UNITY_EDITOR
                Debug.LogWarning($"{name} 遊戲物件裡的 coachMask 沒有被掛載，將自動關閉 {this} 組件");
#endif
                enabled = false;
                return;
            }

            if (finger is null)
            {
#if UNITY_EDITOR
                Debug.LogWarning($"{name} 遊戲物件裡的 finger 沒有被掛載，將自動關閉 {this} 組件");
#endif
                enabled = false;
                return;
            }
        }

        private void OnEnable()
        {
            KitchenwareManager.CloseCoachMaskEvent += Stop;
            SpoonManager.OnClickEvent += Stop;
        }

        private void OnDisable()
        {
            KitchenwareManager.CloseCoachMaskEvent -= Stop;
            SpoonManager.OnClickEvent -= Stop;
        }

        private void OnDestroy()
        {
            if (_zoomCoroutine is not null)
                StopCoroutine(_zoomCoroutine);
        }

        /// <summary>
        /// 用於 Dialogue System 的 UnityEvent 做呼叫
        /// </summary>
        /// <param name="data"> Coach Mask 要播放的 Data </param>
        public void Play(CoachManagerSO data)
        {
            if (_zoomCoroutine is not null)
                StopCoroutine(_zoomCoroutine);
            _zoomCoroutine = ZoomProcess(data);
            StartCoroutine(_zoomCoroutine);
        }

        /// <summary>
        /// 只用於接收廣播，不對外開放
        /// </summary>
        private void Stop()
        {
            coachMask.gameObject.SetActive(false);
            finger.gameObject.SetActive(false);
            if (_zoomCoroutine is null) return;
            StopCoroutine(_zoomCoroutine);
        }

        private IEnumerator _zoomCoroutine;
        private IEnumerator ZoomProcess(CoachManagerSO data)
        {
            coachMask.anchoredPosition = data.targetPos;
            coachMask.gameObject.SetActive(true);
            coachMask.SetWidth(data.initialSize.x);
            coachMask.SetHeight(data.initialSize.y);
            while (Mathf.Abs(data.targetSize.x - coachMask.sizeDelta.x) > .1f ||
                   Mathf.Abs(data.targetSize.y - coachMask.sizeDelta.y) > .1f)
            {
                var zoomSpeed = data.zoomSpeed * 100 * Time.unscaledDeltaTime;
                coachMask.SetWidth(Mathf.MoveTowards(coachMask.sizeDelta.x, data.targetSize.x, zoomSpeed));
                coachMask.SetHeight(Mathf.MoveTowards(coachMask.sizeDelta.y, data.targetSize.y, zoomSpeed));
                yield return null;
            }
        }
    }
}