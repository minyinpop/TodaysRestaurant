namespace Restaurant.Customer.Bubble.Category
{
    public class CustomerThinkBubble : BubbleBase
    {
        public override void Init(float time) => Destroy(gameObject, time);
    }
}