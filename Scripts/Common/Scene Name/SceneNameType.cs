namespace Common.Scene_Name
{
    [System.Serializable]
    public enum SceneNameType
    {
        // 標題
        Title_Scene = 1,
        
        // 大廳
        Lobby_Scene = 101,
        
        // 對話
        Dialogue_Scene = 201,
        
        // 餐廳
        Restaurant_Scene = 301,
        
        // 探索
        Explore_Scene = 1001,
        
        // 探索 - 森林
        Explore_Forest_Terrain_Scene = 1002,
        Explore_Forest_Explore_Scene = 1003,
        Explore_Forest_Battle_Scene = 1004,
        
        // 教學 - 探索
        Explore_Tutorial_Scene = 9001,
        Explore_Tutorial_Terrain_Scene = 9002,
        Explore_Tutorial_Explore_Scene = 9003,
        
        // 教學 - 餐廳
        Restaurant_Cook_Tutorial_Scene = 9101,
        Restaurant_Serves_Tutorial_Scene = 9102,
        
        // 教學 - 戰鬥
        Battle_Tutorial_Scene = 9201,
    }
}