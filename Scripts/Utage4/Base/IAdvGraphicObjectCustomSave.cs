using System.IO;

namespace Utage4.Base
{
    internal interface IAdvGraphicObjectCustomSave
    {
        public void WriteSaveDataCustom(BinaryWriter writer);
        public void ReadSaveDataCustom(BinaryReader reader);
    }
}