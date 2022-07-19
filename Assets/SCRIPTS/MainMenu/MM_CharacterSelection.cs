using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace NewLifeZ.MainMenu
{
    public class MM_CharacterSelection : MonoBehaviour
    {
        [Header("Main Setting")]
        public Transform viewPos;
        public GameObject character;
        public int startID;
        public int endID;
        public int index = 0;

        [Header("UI Setting")]
        [SerializeField] private Button NextCharacterButton;
        [SerializeField] private Button PreviousCharacterButton;
        [SerializeField] private Button StartGameButton;
        [SerializeField] private TMP_Text playerName;

        GameObject tempCharacter;
        void OnEnable()
        {
            tempCharacter = Instantiate(character, viewPos);
            index = startID;
            foreach (Transform child in tempCharacter.transform)
            {
                child.gameObject.SetActive(false);
            }
            tempCharacter.transform.GetChild(index).gameObject.SetActive(true);
        }
        void Start()
        {
            Debug.Log(PlayerPrefs.GetString(GameConstant.PlayerPref.CHARACTER_LAST_NAME));
            playerName.text = PlayerPrefs.GetString(GameConstant.PlayerPref.CHARACTER_LAST_NAME);
            //playerName.text = PhotonNetwork.NickName;
            AddListener();
        }

        void AddListener()
        {
            // add listener to button
            NextCharacterButton.onClick.AddListener(NextCharacter);
            PreviousCharacterButton.onClick.AddListener(PreviousCharacter);
            StartGameButton.onClick.AddListener(StartGame);
        }

        void NextCharacter()
        {
            tempCharacter.transform.GetChild(index).gameObject.SetActive(false);
            if (index < endID)
            {
                index++;
            }
            else
            {
                index = startID;
            }
            tempCharacter.transform.GetChild(index).gameObject.SetActive(true);
            // update property
            // update price
        }
        void PreviousCharacter()
        {
            tempCharacter.transform.GetChild(index).gameObject.SetActive(false);
            if (index > startID)
            {
                index--;
            }
            else
            {
                index = endID;
            }
            tempCharacter.transform.GetChild(index).gameObject.SetActive(true);
            // update property
            // update price
        }

        public void StartGame()
        {
            SceneManager.LoadScene(GameConstant.SceneName.MAIN_GAME);
        }
    }
}

