namespace Data.DOTween_Value
{
    internal sealed class DoFlipValue
    {
        private readonly DoRotateValue DoRotateValue;
        
        private readonly DoScaleValue DoScaleValue1;
        private readonly DoScaleValue DoScaleValue2;

        public DoFlipValue(DoRotateValue DoRotateValue, DoScaleValue DoScaleValue1, DoScaleValue DoScaleValue2)
        {
            this.DoRotateValue = DoRotateValue;
            
            this.DoScaleValue1 = DoScaleValue1;
            this.DoScaleValue2 = DoScaleValue2;
        }
        
        public void GetValues(out DoRotateValue DoRotateValue, out DoScaleValue DoScaleValue1, out DoScaleValue DoScaleValue2)
        {
            DoRotateValue = this.DoRotateValue;
            
            DoScaleValue1 = this.DoScaleValue1;
            DoScaleValue2 = this.DoScaleValue2;
        }
    }
}