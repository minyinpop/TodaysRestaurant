namespace Restaurant.Customer.Bubble.Category
{
    public class CustomerHappyBubble : BubbleBase
    {
        public override void Init(float time) => Destroy(gameObject, time);
    }
}