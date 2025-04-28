namespace Restaurant.Customer.Bubble.Category
{
    public class CustomerThinkBubbleBase : BubbleBase
    {
        public override void Init(float time) => Destroy(gameObject, time);
    }
}