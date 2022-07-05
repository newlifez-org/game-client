using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace NewLifeZ
{
    public class ModelSpawner : MonoBehaviourPun
    {
        private int modelOtherIndex = 0;

        private void Start()
        {
            ModelData modelData = null;
            if (photonView.IsMine)
            {
                int modelIndex = PlayerPrefs.GetInt(GameConstant.PlayerPref.CHARACTER_LAST_SAVE);
                //modelData = API_Constants.loadedCharList[modelIndex].modelData;
                Instantiate(modelData, transform);
                Debug.Log(modelIndex);

                this.photonView.RPC(nameof(PlayerSelectModelIndex), RpcTarget.AllBuffered, modelIndex);
            }
            else
            {
                //modelData = API_Constants.loadedCharList[modelOtherIndex].modelData;
                Instantiate(modelData, transform);
            }

            var animator = GetComponent<Animator>();
            animator.avatar = modelData.ModelID;
            animator?.Rebind();
        }

        [PunRPC]
        void PlayerSelectModelIndex(int index)
        {
            modelOtherIndex = index;
        }
    }
}

