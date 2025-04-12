namespace Restaurant.Bubble.Category
{
    // ==================================================
    // 思考餐點氣泡的程式碼。
    // 顧客在思考要點甚麼餐點的氣泡。
    // ==================================================
    
    public class ThinkingBubble : BubbleManager
    {
        /// <summary>
        /// 當氣泡生成時，會倒數時間，然後刪除氣泡。
        /// </summary>
        /// <param name="time"> 刪除間隔時間。 </param>
        public override void OnInit(float time)
        {
            Destroy(gameObject, time);
        }
    }
}