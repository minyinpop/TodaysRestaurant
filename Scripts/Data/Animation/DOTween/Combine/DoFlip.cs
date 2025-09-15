using Data.Animation.DOTween.Basic;

namespace Data.Animation.DOTween.Combine
{
    internal sealed class DoFlip
    {
        private readonly DoRotate RotateSettings;
        
        private readonly DoScale ScaleSettings01;
        private readonly DoScale ScaleSettings02;

        public DoFlip(DoRotate rotateSettings, DoScale scaleSettings01, DoScale scaleSettings02)
        {
            RotateSettings = rotateSettings;
            
            ScaleSettings01 = scaleSettings01;
            ScaleSettings02 = scaleSettings02;
        }
        
        public void GetValues(out DoRotate rotateSettings, out DoScale scaleSettings01, out DoScale scaleSettings02)
        {
            rotateSettings = RotateSettings;
            
            scaleSettings01 = ScaleSettings01;
            scaleSettings02 = ScaleSettings02;
        }
    }
}