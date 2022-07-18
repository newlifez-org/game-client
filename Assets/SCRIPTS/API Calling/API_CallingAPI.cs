using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using NewLifeZ.MainMenu;

namespace NewLifeZ.API
{
    public class API_CallingAPI : MonoBehaviour
    {
        private MM_UIController _UIController;

        [SerializeField] private GameObject MessagePanel;
        [SerializeField] private TMP_Text MessageText;
        [SerializeField] private Button ConfirmButton;

        private void Awake()
        {
            _UIController = GetComponent<MM_UIController>();
        }

        private void Start()
        {
            ConfirmButton.onClick.AddListener(OnConfirmButtonClicked);
        }

        public void handleLoginSuccess(string dataString)
        {
            API_Response<LoginResponse> loginResponse = JsonUtility.FromJson<API_Response<LoginResponse>>(dataString);
            API_Constants.API_TOKEN = loginResponse.data.token;
            API_Constants.UserData = loginResponse.data;
            _UIController.OpenMainMenu();
            PlayerPrefs.SetString(GameConstant.PlayerPref.CHARACTER_LAST_NAME, _UIController.GetUserName());
            MM_UIController.Instance.SetNameDisplay();
        }

        public void handleLoginFailed(string dataString)
        {
            Debug.Log(dataString);
            API_Response<System.Object> response = JsonUtility.FromJson<API_Response<System.Object>>(dataString);
            MessagePanel.SetActive(true);
            MessageText.text = response.message;
            _UIController.Loading(false);
        }

        public void OnConfirmButtonClicked()
        {
            MessagePanel.SetActive(false);
        }

        public void Login(string username, string password)
        {
            LoginRequest loginRequest = new LoginRequest();
            loginRequest.username = username;
            loginRequest.password = password;
            string loginRequestString = JsonUtility.ToJson(loginRequest);
            Debug.Log(loginRequestString);
            _UIController.Loading(true);
            StartCoroutine(API_Request.PostRequest(API_Endpoints.LOGIN, loginRequestString, null, handleLoginSuccess, handleLoginFailed));
        }
    }
}

[Serializable]
public class LoginResponse
{
    public string name;
    public string token;
}

[Serializable]
public class LoginRequest
{
    public string username;
    public string password;
}

