using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NewLifeZ.API;
using TMPro;
using UnityEngine.SceneManagement;

namespace NewLifeZ.MainMenu
{
    public class MM_UIController : MonoBehaviour
    {
        public static MM_UIController Instance;

        [Header("Button")]
        [SerializeField] private Button LoginButton;

        [Header("Login Panel")]
        [SerializeField] private GameObject LoginPanel;
        [SerializeField] private GameObject LoadingPanel;

        [Header("Menu Panel")]
        [SerializeField] private Button EnterWorld_Button;

        [SerializeField] private GameObject MainMenuPanel;
        [SerializeField] private TMP_Text PlayerName_txt;
        [SerializeField] private TMP_Text Version_txt;

        [Header("InputField")]
        [SerializeField] private TMP_InputField usernameInputField;
        [SerializeField] private TMP_InputField passwordInputField;

        private API_CallingAPI _CallingAPI;

        private void Awake()
        {
            Instance = this;

            _CallingAPI = GetComponent<API_CallingAPI>();
        }

        void Start()
        {
            LoginButton.onClick.AddListener(() => _CallingAPI.Login(usernameInputField.text, passwordInputField.text));

            EnterWorld_Button.onClick.AddListener(EnterWorld);

            Version_txt.text = Application.version;
        }

        public void SetNameDisplay()
        {
            PlayerName_txt.text = PlayerPrefs.GetString(GameConstant.PlayerPref.CHARACTER_LAST_NAME);
        }

        private void EnterWorld()
        {
            SceneManager.LoadScene(GameConstant.SceneName.MAIN_GAME);
        }

        public void Loading(bool status)
        {
            LoadingPanel.gameObject.SetActive(status);
            LoginPanel.gameObject.SetActive(!status);
        }

        public void OpenMainMenu()
        {
            LoadingPanel.SetActive(false);
            LoginPanel.SetActive(false);
            MainMenuPanel.SetActive(true);
        }
        public string GetUserName()
        {
            return usernameInputField.text;
        }
    }
}

