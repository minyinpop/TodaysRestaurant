namespace Common.Dialogue.Data
{
    public enum DialogueDataType
    {
        // 背景
        Show_Background = 101,
        Hide_Background = 102,
        
        // 角色
        Show_Character = 201,
        Hide_Character = 202,
        Play_Character_Animation = 203,
        
        // 對話
        Show_Full_Text = 301,
        Show_Lite_Text = 303,
        Hide_Text = 302,
        
        // 章節標題
        Show_Title = 401,
        Hide_Title = 402,
        
        // 聲音
        Fade_In_BGM = 501,
        Fade_Out_BGM = 502,
        Play_SFX = 503,
        
        // 工具
        Wait = 1001,
        ChangeScene = 1002
    }
}