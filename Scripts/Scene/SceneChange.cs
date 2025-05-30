using UnityEngine;
using UnityEngine.SceneManagement;
using Utage;

namespace Scene
{
    public class SceneChange : MonoBehaviour
    {
        [field: Header("")]
        [field: SerializeField] private AdvEngine AdvEngine { get; set; }

        private void OnDoCommand(AdvCommandSendMessage command)
        {
            switch (command.Name)
            {
                case "SceneChange":
                {
                    SceneManager.LoadScene(1);
                    break;
                }
            }
        }
    }
}