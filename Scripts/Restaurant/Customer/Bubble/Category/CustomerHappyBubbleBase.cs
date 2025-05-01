namespace Restaurant.Customer.Bubble.Category
{
    public class CustomerHappyBubbleBase : BubbleBase
    {
        public override void Init(float time) => Destroy(gameObject, time);
    }
}