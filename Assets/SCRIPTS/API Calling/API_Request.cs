using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace NewLifeZ.API
{
    public class API_Request : MonoBehaviour
    {

        public static IEnumerator GetRequest(string uri, string bearerToken = null, Action<string> OnSuccess = null, Action<string> OnFailed = null)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(uri))
            {
                // www.SetRequestHeader("Api-Token", API_Constants.API_TOKEN);
                if (bearerToken != null)
                {
                    www.SetRequestHeader("Authorization", "Bearer " + bearerToken);
                }
                // Request and wait for the desired page.
                yield return www.SendWebRequest();

                string[] pages = uri.Split('/');
                int page = pages.Length - 1;

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                    OnFailed?.Invoke(Encoding.UTF8.GetString(www.downloadHandler.data));
                }
                else
                {
                    OnSuccess?.Invoke(Encoding.UTF8.GetString(www.downloadHandler.data));
                }
            }
        }

        public static IEnumerator PostRequest(string endpoint, string body, string bearerToken = null, Action<string> OnSuccess = null, Action<string> OnFailed = null)
        {
            Debug.Log($"token: {bearerToken}");
            UnityWebRequest www;
            using (www = UnityWebRequest.Post(endpoint, body))
            {
                www.SetRequestHeader("Accept", "application/json");
                www.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
                www.uploadHandler = new UploadHandlerRaw(string.IsNullOrEmpty(body) ? null : Encoding.UTF8.GetBytes(body));
                if (bearerToken != null)
                {

                    www.SetRequestHeader("Authorization", "Bearer " + bearerToken);
                }
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                    OnFailed?.Invoke(Encoding.UTF8.GetString(www.downloadHandler.data));
                }
                else
                {
                    OnSuccess?.Invoke(Encoding.UTF8.GetString(www.downloadHandler.data));
                }
            }
        }
    }
}
