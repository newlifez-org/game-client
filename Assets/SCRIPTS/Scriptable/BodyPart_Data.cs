using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewLifeZ
{
    [CreateAssetMenu]
    public class BodyPart_Data : ScriptableObject
    {
        [SerializeField] private Material[] EYE;
        [SerializeField] private Material[] EYELASH;
        [SerializeField] private Material[] EYEBROW;
        [SerializeField] private Material[] HAIR;
        [SerializeField] private Material[] SHIRT;
        [SerializeField] private Material[] PANT;
        [SerializeField] private Material[] SHOES;
        [SerializeField] private Material[] GLASS;
        [SerializeField] private Material[] BODY;

        public  Material[] EYELIST()
        {
            return EYE;
        }

        public Material[] SHOESLIST()
        {
            return SHOES;
        }

        public Material[] PANTLIST()
        {
            return PANT;
        }

        public Material[] SHIRTLIST()
        {
            return SHIRT;
        }

        public Material[] BODYLIST()
        {
            return BODY;
        }

        public Material[] HAIRLIST()
        {
            return HAIR;
        }

        public Material[] EYELASHLIST()
        {
            return EYELASH;
        }

        public Material[] EYEBROWLIST()
        {
            return EYEBROW;
        }

        public Material[] GLASSLIST()
        {
            return GLASS;
        }

    }
}

