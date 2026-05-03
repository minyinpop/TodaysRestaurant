using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Restaurant_Statistical_Report;
using UI_System.Restaurant_UI_System.Child.Statistical_Report_UI_System.Child;
using UnityEngine;

namespace UI_System.Restaurant_UI_System.Child.Statistical_Report_UI_System.Main
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class StatisticalReportUISystem : MonoBehaviour
    {
        [field: Header("動畫組件")]
        [field: SerializeField] private new DoAnimation animation;
        [field: SerializeField] private DoFade_CanvasGroup fadeInSettings;
        [field: SerializeField] private DoFade_CanvasGroup fadeOutSettings;
        
        [field: Header("遮罩")]
        [field: SerializeField] private CanvasGroup maskCanvasGroup;

        [field: Header("統計表")]
        [field: SerializeField] private CanvasGroup statisticalTableUICanvasGroup;
        [field: SerializeField] private StatisticalReportUI statisticalReportUI;

        public void OpenStatisticalTableUI(RestaurantStatisticalReport reportData)
        {
            maskCanvasGroup.gameObject.SetActive(true);
            
            animation.DoFade_CanvasGroup(
                canvasGroup: maskCanvasGroup,
                settings: fadeInSettings,
                onComplete: () =>
                {
                    statisticalTableUICanvasGroup.gameObject.SetActive(true);
                    
                    animation.DoFade_CanvasGroup(
                        canvasGroup: statisticalTableUICanvasGroup,
                        settings: fadeInSettings,
                        onComplete: () =>
                        {
                            statisticalReportUI.ShowInformation(
                                reportData: reportData,
                                onComplete: () =>
                                {
                                    
                                });
                        });
                });
        }
    }
}