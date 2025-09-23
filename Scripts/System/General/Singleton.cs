using UnityEngine;

namespace System.General
{
    internal sealed class Singleton : MonoBehaviour
    {
        private static Singleton Instance;

        private void Awake()
        {
            if (Instance is not null && !Equals(Instance, this))
            {
                Destroy(this);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}