using UI_System.Lobby_UI_System.Main;
using UnityEngine;

namespace Lobby_System.Child
{
    public sealed class MapSystem : MonoBehaviour
    {
        public void RequireMapUI()
        {
            LobbyUISystem.RequireMapUI();
        }
    }
}