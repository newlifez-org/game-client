using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewLifeZ.API;

namespace NewLifeZ.MainMenu
{
    public class MM_InstanceCharacter : MonoBehaviour
    {
        public GameObject ModelPrefab;

        private void Start()
        {
            InstanceCharacter();
        }

        void InstanceCharacter()
        {
            for(int i = 0; i < API_Static.m_CharacterMetaData.Count; i++)
            {
                Instantiate(ModelPrefab, transform);
            }
        }
    }
}

