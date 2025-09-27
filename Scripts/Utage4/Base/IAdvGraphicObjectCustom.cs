using Utage;

namespace Utage4.Base
{
    internal interface IAdvGraphicObjectCustom
    {
        public void ChangeResourceOnDrawSub(AdvGraphicInfo graphic);
        public void OnEffectColorsChange(AdvEffectColor color);
    }
    
    internal interface IAdvGraphicObjectCustomCommand
    {
        public void SetCommandArg(AdvCommand command);
    }
}