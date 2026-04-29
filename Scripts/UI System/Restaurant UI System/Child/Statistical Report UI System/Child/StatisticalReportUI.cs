using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Restaurant_Statistical_Report;
using UnityEngine;

namespace UI_System.Restaurant_UI_System.Child.Statistical_Report_UI_System.Child
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class StatisticalReportUI : MonoBehaviour
    {
        [field: Header("動畫")]
        [field: SerializeField] private new DoAnimation animation;
        [field: SerializeField] private DoFade_CanvasGroup showTextSettings;
        
        [field: Header("資訊容器")]
        [field: SerializeField] private InformationContainer totalCustomerCount;
        [field: SerializeField] private InformationContainer happyCustomerCount;
        [field: SerializeField] private InformationContainer angryCustomerCount;
        [field: SerializeField] private InformationContainer earnedCount;

        public void ShowInformation(RestaurantStatisticalReport reportData)
        {
            ShowInformationProcess(
                text: reportData.TotalCustomerCount.ToString(),
                container: totalCustomerCount,
                onComplete: () =>
                {
                    ShowInformationProcess(
                        text: reportData.HappyCustomerCount.ToString(),
                        container: happyCustomerCount,
                        onComplete: () =>
                        {
                            ShowInformationProcess(
                                text: reportData.AngryCustomerCount.ToString(),
                                container: angryCustomerCount,
                                onComplete: () =>
                                {
                                    ShowInformationProcess(
                                        text: reportData.EarnedCount.ToString(),
                                        container: earnedCount,
                                        onComplete: () =>
                                        {
                                        });
                                });
                        });
                });
            return;
            
            void ShowInformationProcess(string text, InformationContainer container, Action onComplete)
            {
                container.NumberText.text = text;
                
                container.gameObject.SetActive(true);
                
                animation.DoFade_CanvasGroup(
                    canvasGroup: container.GetComponent<CanvasGroup>(),
                    settings: showTextSettings,
                    onComplete: () =>
                    {
                        onComplete.Invoke();
                    });
            }
        }
    }
}