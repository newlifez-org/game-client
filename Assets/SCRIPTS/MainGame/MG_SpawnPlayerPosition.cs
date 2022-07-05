using UnityEngine;

namespace NewLifeZ.MainGame
{
    public class MG_SpawnPlayerPosition : MonoBehaviour
    {
        public static MG_SpawnPlayerPosition Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}

