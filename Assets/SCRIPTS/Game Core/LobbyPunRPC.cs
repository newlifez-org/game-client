using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


    public class LobbyPunRPC : MonoBehaviour
    {
        public GameDataManager gameDataManager;
        [PunRPC]
        public void sendSpawnModelToOtherPlayer(string characterModelJson)
        {
            Debug.Log("sendSpawnModelToOtherPlayer:"+ characterModelJson);
            gameDataManager = JsonUtility.FromJson<GameDataManager>(characterModelJson);
        }
    }