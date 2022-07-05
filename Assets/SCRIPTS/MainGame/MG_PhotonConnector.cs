using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using Cysharp.Threading.Tasks;
using System;

namespace NewLifeZ.MainGame
{
    public class MG_PhotonConnector : MonoBehaviourPunCallbacks
    {
        public static MG_PhotonConnector Instance;
        public GameObject LocalInstancePLayer;

        [SerializeField] GameObject PlayerPrefab;
        [SerializeField] private GameObject LoadingPanel;
        [SerializeField] private GameObject SecondCamera;

        private bool IsGoingTo_GoldMine = false;

        #region Unity Methods
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            string randomName = $"Tester{Guid.NewGuid().ToString()}";
            ConnectToPhoton(randomName);
            PhotonNetwork.NickName = PlayerPrefs.GetString(GameConstant.PlayerPref.CHARACTER_LAST_NAME);
            Debug.Log("Nickname:" + PhotonNetwork.NickName);
        }

        //Instance Player
        private void InitPlayer()
        {
            if (LocalInstancePLayer == null)
            {
                Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                LocalInstancePLayer = PhotonNetwork.Instantiate(Path.Combine("RS", PlayerPrefab.name), MG_SpawnPlayerPosition.Instance.transform.position, Quaternion.identity);
                LoadingPanel.SetActive(false);
                SecondCamera.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                //FS_NamePickUp.instance.StartChat();
            }
            else
            {
                Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
            }
        }
        #endregion

        #region Private Methods
        private void ConnectToPhoton(string nickName)
        {
            Debug.Log($"Connect to Photon as {nickName}");
            //PhotonNetwork.AuthValues = new AuthenticationValues(PhotonNetwork.NickName);
            PhotonNetwork.AutomaticallySyncScene = false;
            PhotonNetwork.ConnectUsingSettings();
        }
        private void CreatePhotonRoom(string roomName)
        {
            RoomOptions ro = new RoomOptions();
            ro.IsOpen = true;
            ro.IsVisible = true;
            ro.MaxPlayers = 0;
            PhotonNetwork.JoinOrCreateRoom(roomName, ro, TypedLobby.Default);
        }
        #endregion

        #region Phothon Callbacks
        public override void OnConnectedToMaster()
        {
            Debug.Log("You have connected to the Photon Master Sever");
            if (!PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinLobby();
            }
        }
        public override void OnJoinedLobby()
        {
            CreatePhotonRoom("Room " + GameConstant.PhotonRoom.MAIN_ROOM);
        }
        public override void OnCreatedRoom()
        {
            Debug.Log($"You have created a room named {PhotonNetwork.CurrentRoom.Name}");

        }

        public override void OnJoinedRoom()
        {
            Debug.Log($"You have joined the Photon room named {PhotonNetwork.CurrentRoom.Name}");
            InitPlayer();
        }
        public override void OnLeftRoom()
        {
            if (IsGoingTo_GoldMine)
            {
                IsGoingTo_GoldMine = false;
                PhotonNetwork.LoadLevel(GameConstant.SceneName.GOLDMINE);
            }
        }
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log($"You failed to join a Photon room; {message}");
        }
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log($"Another player has joined the room {newPlayer.UserId}");
        }
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log($"Player {otherPlayer.UserId} has left the room");
        }
        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            Debug.Log($"New Master Client is {newMasterClient.UserId}");
        }
        #endregion

        #region PUBLIC METHOD
        public async void MoveTo_GoldMine()
        {
            PhotonNetwork.LeaveRoom();
            await UniTask.Yield();
            IsGoingTo_GoldMine = true;
        }
        #endregion
    }
}

