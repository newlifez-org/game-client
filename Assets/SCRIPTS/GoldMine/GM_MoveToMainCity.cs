using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using StarterAssets;

namespace NewLifeZ.GoldMine
{
    public class GM_MoveToMainCity : MonoBehaviourPun
    {
        [SerializeField] private GameObject PlayerIsMine;

        public void OnTriggerEnter(Collider other)
        {
            PhotonView pv = other.GetComponent<PhotonView>();
            if (pv != null && pv.IsMine)
            {
                Debug.Log("Moving to Zoo");
                PlayerIsMine = other.gameObject;
                PlayerIsMine.GetComponent<ThirdPersonController>().LockCameraPosition = true;
                GM_PhotonConnect.Instance.MoveTo_MainGame();
            }
        }
    }
}

