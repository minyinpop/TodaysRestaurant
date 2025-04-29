namespace Restaurant.Customer.Bubble.Category
{
    public class CustomerAngryBubbleBase : BubbleBase
    {
        public override void Init(float time) => Destroy(gameObject, time);
    }
}