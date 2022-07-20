using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewLifeZ.API;
using Photon.Pun;

namespace NewLifeZ
{
    public class GetDataFromJson : MonoBehaviour
    {
        [SerializeField] private BodyPart_Data bodyPart_Data;
        PhotonView PV;
        [SerializeField] private GameObject model;
        // Start is called before the first frame update

        public void Start()
        {
            Debug.Log("Get data from json");
            PV = GetComponent<PhotonView>();
            Debug.Log("Mine:" + PV.IsMine);
            if(PV == null)
            {
                Debug.Log("BI NULL");
            }
            if (PV.IsMine)
            {
                ActiveBodyPart(API_CallingAPI.Instance.gameDataManagerLocalPlayer);
                PV.RPC("sendSpawnModelToOtherPlayer", RpcTarget.OthersBuffered, JsonUtility.ToJson(API_CallingAPI.Instance.gameDataManagerLocalPlayer));
            }
            else
            {
                Debug.Log("Client:" + GetComponent<LobbyPunRPC>().gameDataManager == null ?"null":"yess");
                ActiveBodyPart(GetComponent<LobbyPunRPC>().gameDataManager);
            }
        }

        void ActiveBodyPart(GameDataManager modelData)
        {
            for (int i = 0; i < model.transform.childCount; i++)
            {

                GameObject go = model.transform.GetChild(i).gameObject;
                if (go.name == modelData.Eye.Name)
                {
                    go.SetActive(true);
                    setMaterialForBodyPart(go, modelData.Eye.Value, bodyPart_Data.EYELIST());
                }

                if (go.name == modelData.EyeBrow.Name)
                {
                    go.SetActive(true);
                    setMaterialForBodyPart(go, modelData.EyeBrow.Value, bodyPart_Data.EYEBROWLIST());
                }

                if (go.name == modelData.EyeSlash.Name)
                {
                    go.SetActive(true);
                    setMaterialForBodyPart(go, modelData.EyeSlash.Value, bodyPart_Data.EYELASHLIST());
                }

                if (go.name == modelData.Hair.Name)
                {
                    go.SetActive(true);
                    setMaterialForBodyPart(go, modelData.Hair.Value, bodyPart_Data.HAIRLIST());
                }

                if (go.name == modelData.Body.Name)
                {
                    go.SetActive(true);
                    setMaterialForBodyPart(go, modelData.Body.Value, bodyPart_Data.BODYLIST());
                }

                if (go.name == modelData.Shirt.Name)
                {
                    go.SetActive(true);
                    setMaterialForBodyPart(go, modelData.Shirt.Value, bodyPart_Data.SHIRTLIST());
                }
                if (go.name == modelData.Shoes.Name)
                {
                    go.SetActive(true);
                    setMaterialForBodyPart(go, modelData.Shoes.Value, bodyPart_Data.SHOESLIST());
                }
                if (go.name == modelData.Pants.Name)
                {
                    go.SetActive(true);
                    setMaterialForBodyPart(go, modelData.Pants.Value, bodyPart_Data.PANTLIST());
                }
                if (go.name == modelData.Glasses.Name)
                {
                    go.SetActive(true);
                    setMaterialForBodyPart(go, modelData.Glasses.Value, bodyPart_Data.GLASSLIST());
                }
            }

        }
        private void setMaterialForBodyPart(GameObject part, string materialName, Material[] materials)
        {
            foreach (var item in materials)
            {
                if (item.name == materialName)
                {
                    Debug.Log(item.name);
                    part.GetComponent<SkinnedMeshRenderer>().material = item;
                    break;
                }
            }
        }
    }
}

