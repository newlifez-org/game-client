using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using StarterAssets;
using UnityEngine.UI;

namespace NewLifeZ.MainGame
{
    public class MG_EnterForest : MonoBehaviourPun
    {
        [Header("Panel")]
        [SerializeField] private GameObject ConfirmToMovePanel;
        [SerializeField] private GameObject PlayerIsMine;

        [SerializeField] private Button ConfirmButton;
        [SerializeField] private Button ExitButton;

        private void Start()
        {
            AddListener();
        }

        void AddListener()
        {
            ConfirmButton.onClick.AddListener(OnConfirmClicked);
            ExitButton.onClick.AddListener(OnExitClicked);
        }

        public void OnTriggerEnter(Collider other)
        {
            PhotonView pv = other.GetComponent<PhotonView>();
            if (pv != null && pv.IsMine)
            {
                ConfirmToMovePanel.SetActive(true);
                PlayerIsMine = other.gameObject;
                PlayerIsMine.GetComponent<ThirdPersonController>().LockCameraPosition = true;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            PhotonView pv = other.GetComponent<PhotonView>();
            if (pv != null && pv.IsMine)
            {
                ConfirmToMovePanel.SetActive(false);
                PlayerIsMine = other.gameObject;
                PlayerIsMine.GetComponent<ThirdPersonController>().LockCameraPosition = false;
            }
        }

        private void OnConfirmClicked()
        {
            MG_PhotonConnector.Instance.MoveTo_Forest();
        }

        private void OnExitClicked()
        {
            ConfirmToMovePanel.SetActive(false);
        }
    }
}

