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
        public static API_CallingAPI Instance;

        [SerializeField] private GameObject MessagePanel;
        [SerializeField] private TMP_Text MessageText;
        [SerializeField] private Button ConfirmButton;
        [SerializeField] private UserData m_UserData = new UserData();
        [SerializeField] private WalletData m_WalletData = new WalletData();

        private void Awake()
        {
            _UIController = GetComponent<MM_UIController>();
            Instance = this;
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
            StartCoroutine(LoadWalletData(dataString));
        }

        IEnumerator LoadWalletData(string dataString)
        {
            m_WalletData = JsonUtility.FromJson<WalletData>(dataString);

            //Get MetaData
            for (int i = 0; i < m_WalletData.list_tokens.Count; i++)
            {
                string url = m_WalletData.list_tokens[i].token_uri;
                Debug.Log("ABAC:" + url);
                yield return StartCoroutine(GetMetaData(url));
            }
            Debug.Log("Loaded data");
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
                    var dataString = Encoding.UTF8.GetString(www.downloadHandler.data);

                    CharacterMetaData a = JsonUtility.FromJson<CharacterMetaData>(dataString);
                    API_Static.m_CharacterMetaData.Add(a);
                    Debug.Log(dataString);
                    _UIController.OpenMainMenu();
                }
            }
        }

        public void GetCharacterData(int index)
        {
            Debug.Log("data:" + API_Static.m_CharacterMetaData.Count);
            CharacterMetaData data = API_Static.m_CharacterMetaData[index];
            Debug.Log("attributes:" + data.attributes.Count);
            GameDataManager.Instance.Description = data.description;
            GameDataManager.Instance.ExternalURL = data.external_url;
            GameDataManager.Instance.AvatarURL = data.image;
            GameDataManager.Instance.CharacterName = data.name;

            for (int i = 0; i < data.attributes.Count; i++)
            {
                Debug.Log("part:" + data.attributes[i].type+":"+ data.attributes[i].name);
                if (data.attributes[i].type == "Eye")
                {
                    GameDataManager.Instance.Eye.Name = data.attributes[i].name;
                    GameDataManager.Instance.Eye.Value = data.attributes[i].value;
                    Debug.Log("Eye:" + data.attributes[i].name);
                }
                else if (data.attributes[i].type == "Body")
                {
                    GameDataManager.Instance.Body.Name = data.attributes[i].name;
                    GameDataManager.Instance.Body.Value = data.attributes[i].value;
                    Debug.Log("Eye:" + data.attributes[i].name);
                }
                else if (data.attributes[i].type == "EyeBrow")
                {
                    GameDataManager.Instance.EyeBrow.Name = data.attributes[i].name;
                    GameDataManager.Instance.EyeBrow.Value = data.attributes[i].value;
                    Debug.Log("Eye:" + data.attributes[i].name);
                }
                else if (data.attributes[i].type == "Eyelash")
                {
                    GameDataManager.Instance.EyeSlash.Name = data.attributes[i].name;
                    GameDataManager.Instance.EyeSlash.Value = data.attributes[i].value;
                    Debug.Log("Eye:" + data.attributes[i].name);
                }
                else if (data.attributes[i].type == "Hair")
                {
                    GameDataManager.Instance.Hair.Name = data.attributes[i].name;
                    GameDataManager.Instance.Hair.Value = data.attributes[i].value;
                    Debug.Log("Eye:" + data.attributes[i].name);
                }
                else if (data.attributes[i].type == "Pants")
                {
                    GameDataManager.Instance.Pants.Name = data.attributes[i].name;
                    GameDataManager.Instance.Pants.Value = data.attributes[i].value;
                    Debug.Log("Eye:" + data.attributes[i].name);
                }
                else if (data.attributes[i].type == "Shirt")
                {
                    GameDataManager.Instance.Shirt.Name = data.attributes[i].name;
                    GameDataManager.Instance.Shirt.Value = data.attributes[i].value;
                    Debug.Log("Eye:" + data.attributes[i].name);
                }
                else if (data.attributes[i].type == "Shoes")
                {
                    GameDataManager.Instance.Shoes.Name = data.attributes[i].name;
                    GameDataManager.Instance.Shoes.Value = data.attributes[i].value;
                    Debug.Log("Eye:" + data.attributes[i].name);
                }
                else if (data.attributes[i].type == "Glass")
                {
                    GameDataManager.Instance.Glasses.Name = data.attributes[i].name;
                    GameDataManager.Instance.Glasses.Value = data.attributes[i].value;
                    Debug.Log("Eye:" + data.attributes[i].name);
                }

            }
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
        public string type;
        public string name;
        public string value;
    }
}



