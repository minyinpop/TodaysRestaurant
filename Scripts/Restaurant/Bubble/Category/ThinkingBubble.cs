namespace Restaurant.Bubble.Category
{
    // ==================================================
    // 
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