using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using NewLifeZ.MainMenu;
using UnityEngine.Networking;
using System.Text;

namespace NewLifeZ.API
{
    public class API_CallingAPI : MonoBehaviour
    {
        private MM_UIController _UIController;

        [SerializeField] private GameObject MessagePanel;
        [SerializeField] private TMP_Text MessageText;
        [SerializeField] private Button ConfirmButton;
        [SerializeField] private UserData m_UserData = new UserData();
        [SerializeField] private WalletData m_WalletData = new WalletData();
        [SerializeField] private CharacterMetaData m_CharacterMetaData = new CharacterMetaData();

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

            m_UserData = JsonUtility.FromJson<UserData>(dataString);

            StartCoroutine(API_Request.GetRequest(API_Endpoints.GET_JSON_METADATA, m_UserData.token.idToken, HandleGetMetaDataSuccess, HandleGetMetaDataFail));
        }

        public void handleLoginFailed(string dataString)
        {
            Debug.Log(dataString);
            API_Response<System.Object> response = JsonUtility.FromJson<API_Response<System.Object>>(dataString);
            MessagePanel.SetActive(true);
            MessageText.text = response.message;
            _UIController.Loading(false);
        }

        private void HandleGetMetaDataSuccess(string dataString)
        {
            m_WalletData = JsonUtility.FromJson<WalletData>(dataString);

            //Get MetaData
            StartCoroutine(GetMetaData(m_WalletData.list_tokens[0].token_uri));
        }

        private IEnumerator GetMetaData(string uri)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(uri))
            {
                yield return www.SendWebRequest();

                // Request and wait for the desired page.
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
                else
                {
                    var dataString =  Encoding.UTF8.GetString(www.downloadHandler.data);

                    m_CharacterMetaData = JsonUtility.FromJson<CharacterMetaData>(dataString);

                    GetCharacterData(m_CharacterMetaData);
                }
            }
        }

        private void GetCharacterData(CharacterMetaData data)
        {
            GameDataManager.Instance.Description = data.description;
            GameDataManager.Instance.ExternalURL = data.external_url;
            GameDataManager.Instance.AvatarURL = data.image;
            GameDataManager.Instance.CharacterName = data.name;

            GameDataManager.Instance.Eye.Name = data.attributes[0].name;
            GameDataManager.Instance.Eye.Value = data.attributes[0].value;


            GameDataManager.Instance.Body.Name = data.attributes[1].name;
            GameDataManager.Instance.Body.Value = data.attributes[1].value;


            GameDataManager.Instance.EyeBrow.Name = data.attributes[2].name;
            GameDataManager.Instance.EyeBrow.Value = data.attributes[2].value;


            GameDataManager.Instance.EyeSlash.Name = data.attributes[3].name;
            GameDataManager.Instance.EyeSlash.Value = data.attributes[3].value;


            GameDataManager.Instance.Hair.Name = data.attributes[4].name;
            GameDataManager.Instance.Hair.Value = data.attributes[4].value;


            GameDataManager.Instance.Pants.Name = data.attributes[5].name;
            GameDataManager.Instance.Pants.Value = data.attributes[5].value;


            GameDataManager.Instance.Shirt.Name = data.attributes[6].name;
            GameDataManager.Instance.Shirt.Value = data.attributes[6].value;


            GameDataManager.Instance.Shorts.Name = data.attributes[7].name;
            GameDataManager.Instance.Shorts.Value = data.attributes[7].value;
        }

        private void HandleGetMetaDataFail(string msg)
        {
            Debug.Log("dataString = " + msg);
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

[Serializable]
public class UserData
{
    public Token token;
    public string email;
    public int exp;
    public string uid;
    public int auth_time;
    public string token_use;
}

[Serializable]
public class Token
{
    public string accessToken;
    public string idToken;
    public string refreshToken;
}

[Serializable]
public class WalletData
{
    public string wallet_address;
    public List<WalletToken> list_tokens = new List<WalletToken>();
}

[Serializable]
public class WalletToken
{
    public string token_id;
    public string token_uri;
}

[Serializable]
public class CharacterMetaData
{
    public string description;
    public string external_url;
    public string image;
    public string name;
    public List<CharacterMetaDataAttribute> attributes = new List<CharacterMetaDataAttribute>();
}

[Serializable]
public class CharacterMetaDataAttribute
{
    public string name;
    public string value;
}
