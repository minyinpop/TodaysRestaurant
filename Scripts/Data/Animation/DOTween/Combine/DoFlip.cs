using Data.Animation.DOTween.Basic;

namespace Data.Animation.DOTween.Combine
{
    internal sealed class DoFlip
    {
        private readonly DoRotate RotateSettings;
        
        private readonly DoScale _scaleSettings01;
        private readonly DoScale _scaleSettings02;

        public DoFlip(DoRotate rotateSettings, DoScale scaleSettings01, DoScale scaleSettings02)
        {
            RotateSettings = rotateSettings;
            
            _scaleSettings01 = scaleSettings01;
            _scaleSettings02 = scaleSettings02;
        }
        
        public void GetValues(out DoRotate rotateSettings, out DoScale scaleSettings01, out DoScale scaleSettings02)
        {
            rotateSettings = RotateSettings;
            
            scaleSettings01 = _scaleSettings01;
            scaleSettings02 = _scaleSettings02;
        }
    }
}