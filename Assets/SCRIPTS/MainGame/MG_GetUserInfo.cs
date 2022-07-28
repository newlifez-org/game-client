using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace NewLifeZ.MainGame
{
    public class MG_GetUserInfo : MonoBehaviour
    {
        [SerializeField] private TMP_Text Username_text;
        [SerializeField] private TMP_Text Level_text;
        // Start is called before the first frame update
        void Start()
        {
            Username_text.text = PlayerPrefs.GetString(GameConstant.PlayerPref.CHARACTER_LAST_NAME);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

