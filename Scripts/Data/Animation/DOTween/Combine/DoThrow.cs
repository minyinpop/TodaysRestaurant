using Data.Animation.DOTween.Basic;

namespace Data.Animation.DOTween.Combine
{
    internal sealed class DoThrow
    {
        private readonly DoAnchorPos AnchorPosSettings;
        private readonly DoRotate RotateSettings;
        
        private readonly DoScale _scaleSettings01;
        private readonly DoScale _scaleSettings02;

        public DoThrow(DoAnchorPos anchorPosSettings, DoRotate rotateSettings, DoScale scaleSettings01, DoScale scaleSettings02)
        {
            AnchorPosSettings = anchorPosSettings;
            RotateSettings = rotateSettings;
            
            _scaleSettings01 = scaleSettings01;
            _scaleSettings02 = scaleSettings02;
        }

        public void GetValues(out DoAnchorPos anchorPosSettings, out DoRotate rotateSettings, out DoScale scaleSettings01, out DoScale scaleSettings02)
        {
            anchorPosSettings = AnchorPosSettings;
            rotateSettings = RotateSettings;
            
            scaleSettings01 = _scaleSettings01;
            scaleSettings02 = _scaleSettings02;
        }
    }
}