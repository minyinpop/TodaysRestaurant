namespace Restaurant.Customer.Bubble.Category
{
    public class CustomerAngryBubble : BubbleBase
    {
        public override void Init(float time) => Destroy(gameObject, time);
    }
}