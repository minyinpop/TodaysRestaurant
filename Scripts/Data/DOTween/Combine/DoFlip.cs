using Data.DOTween.Basic;

namespace Data.DOTween.Combine
{
    internal sealed class DoFlip
    {
        private readonly DoRotate RotateSettings;
        
        private readonly DoScale ScaleSettings01;
        private readonly DoScale ScaleSettings02;

        /// <summary>
        /// 設置動畫參數
        /// </summary>
        /// <param name="rotateSettings">旋轉的動畫參數</param>
        /// <param name="scaleSettings01">前半部分的動畫參數</param>
        /// <param name="scaleSettings02">後半部分的動畫參數</param>
        public DoFlip(DoRotate rotateSettings, DoScale scaleSettings01, DoScale scaleSettings02)
        {
            RotateSettings = rotateSettings;
            
            ScaleSettings01 = scaleSettings01;
            ScaleSettings02 = scaleSettings02;
        }
        
        /// <summary>
        /// 獲取動畫的參數
        /// </summary>
        /// <param name="rotateSettings">旋轉的動畫參數</param>
        /// <param name="scaleSettings01">前半部分的動畫參數</param>
        /// <param name="scaleSettings02">後半部分的動畫參數</param>
        public void GetValues(out DoRotate rotateSettings, out DoScale scaleSettings01, out DoScale scaleSettings02)
        {
            rotateSettings = RotateSettings;
            
            scaleSettings01 = ScaleSettings01;
            scaleSettings02 = ScaleSettings02;
        }
    }
}