using System;

namespace Audio_System.Data
{
    [Serializable]
    public enum AudioSourceType
    {
        // BGM
        Common_BGM = 101,
        
        // SFX
        UI_SFX = 201,
        Footstep_SFX = 202,
        Interact_SFX = 203,
        Attack_SFX = 204,
        Other_SFX = 299,
        
        // AMB
        Restaurant_AMB = 301,
    }
}