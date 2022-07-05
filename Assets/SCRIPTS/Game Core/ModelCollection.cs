using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewLifeZ
{
    [CreateAssetMenu]
    public class ModelCollection : ScriptableObject
    {
        public List<ModelData> ModelPrefabs;

        private const string ResourcesContainer = "RS";

        public string GetPrefabNameWithIndex(int index)
        {
            if (index < ModelPrefabs.Count)
                return System.IO.Path.Combine(ResourcesContainer, ModelPrefabs[index].name);
            else
                return "";
        }

        public ModelData GetPrefabWithIndex(int index)
        {
            if (ModelPrefabs.Count <= index)
                return null;

            return ModelPrefabs[index];
        }
    }
}
