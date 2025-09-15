using Data.Animation.DOTween.Basic;

namespace Data.Animation.DOTween.Combine
{
    internal sealed class DoThrow
    {
        private readonly DoAnchorPos AnchorPosSettings;
        private readonly DoRotate RotateSettings;
        
        private readonly DoScale ScaleSettings01;
        private readonly DoScale ScaleSettings02;

        public DoThrow(DoAnchorPos anchorPosSettings, DoRotate rotateSettings, DoScale scaleSettings01, DoScale scaleSettings02)
        {
            AnchorPosSettings = anchorPosSettings;
            RotateSettings = rotateSettings;
            
            ScaleSettings01 = scaleSettings01;
            ScaleSettings02 = scaleSettings02;
        }

        public void GetValues(out DoAnchorPos anchorPosSettings, out DoRotate rotateSettings, out DoScale scaleSettings01, out DoScale scaleSettings02)
        {
            anchorPosSettings = AnchorPosSettings;
            rotateSettings = RotateSettings;
            
            scaleSettings01 = ScaleSettings01;
            scaleSettings02 = ScaleSettings02;
        }
    }
}