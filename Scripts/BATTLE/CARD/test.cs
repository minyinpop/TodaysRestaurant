using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private AnimationCurve verticalCurve; // 用來管理卡片的垂直分佈曲線
    [SerializeField] private RectTransform[] cards; // 卡片（UI 的 RectTransform）

    void ArrangeCardsByCurve()
    {
        int cardCount = cards.Length;

        for (int i = 0; i < cardCount; i++)
        {
            // 計算每張卡片的歸一化進度（從 0 到 1）
            float t = (float)i / (cardCount - 1);

            // 通過曲線獲取 y 軸的偏移位置
            float yOffset = verticalCurve.Evaluate(t);

            // 獲取卡片當前的位置（由 Layout Group 控制 x 軸）
            Vector3 position = cards[i].anchoredPosition;

            // 僅修改 y 軸的值
            position.y = yOffset;

            // 更新該卡片的位置
            cards[i].anchoredPosition = position;
        }
    }

    void Update()
    {
        ArrangeCardsByCurve(); // 將卡片根據曲線分佈
    }
}