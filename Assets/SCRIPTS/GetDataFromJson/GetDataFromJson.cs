using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewLifeZ.API;

namespace NewLifeZ
{
    public class GetDataFromJson : MonoBehaviour
    {
        [SerializeField] private BodyPart_Data bodyPart_Data;
        // Start is called before the first frame update
        void Start()
        {
            API_CallingAPI.Instance.GetCharacterData(1);
            ActiveBodyPart();

        }

        // Update is called once per frame
        void Update()
        {
        }

        void ActiveBodyPart()
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                GameObject go = this.transform.GetChild(i).gameObject;
                if (go.name == GameDataManager.Instance.Eye.Name)
                {
                    go.SetActive(true);
                    setMaterialForBodyPart(go, GameDataManager.Instance.Eye.Value, bodyPart_Data.EYELIST());
                }

                if (go.name == GameDataManager.Instance.EyeBrow.Name)
                {
                    go.SetActive(true);
                    setMaterialForBodyPart(go, GameDataManager.Instance.EyeBrow.Value, bodyPart_Data.EYEBROWLIST());
                }

                if (go.name == GameDataManager.Instance.EyeSlash.Name)
                {
                    go.SetActive(true);
                    setMaterialForBodyPart(go, GameDataManager.Instance.EyeSlash.Value, bodyPart_Data.EYELASHLIST());
                }

                if (go.name == GameDataManager.Instance.Hair.Name)
                {
                    go.SetActive(true);
                    setMaterialForBodyPart(go, GameDataManager.Instance.Hair.Value, bodyPart_Data.HAIRLIST());
                }

                if (go.name == GameDataManager.Instance.Body.Name)
                {
                    go.SetActive(true);
                    setMaterialForBodyPart(go, GameDataManager.Instance.Body.Value, bodyPart_Data.BODYLIST());
                }

                if (go.name == GameDataManager.Instance.Shirt.Name)
                {
                    go.SetActive(true);
                    setMaterialForBodyPart(go, GameDataManager.Instance.Shirt.Value, bodyPart_Data.SHIRTLIST());
                }
                if (go.name == GameDataManager.Instance.Shoes.Name)
                {
                    go.SetActive(true);
                    setMaterialForBodyPart(go, GameDataManager.Instance.Shoes.Value, bodyPart_Data.SHOESLIST());
                }
                if (go.name == GameDataManager.Instance.Pants.Name)
                {
                    go.SetActive(true);
                    setMaterialForBodyPart(go, GameDataManager.Instance.Pants.Value, bodyPart_Data.PANTLIST());
                }
                if (go.name == GameDataManager.Instance.Glasses.Name)
                {
                    go.SetActive(true);
                    setMaterialForBodyPart(go, GameDataManager.Instance.Glasses.Value, bodyPart_Data.GLASSLIST());
                }
            }
            
        }
        private void setMaterialForBodyPart(GameObject part, string materialName, Material[] materials)
        {
            foreach(var item in materials)
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

