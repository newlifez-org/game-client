using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameDataManager : Singleton<GameDataManager>
{
    public string Description;
    public string ExternalURL;
    public string AvatarURL;
    public string CharacterName;
    public EyePart Eye;
    public BodyPart Body;
    public EyeBrowPart EyeBrow;
    public EyeSlashPart EyeSlash;
    public HairPart Hair;
    public PantsPart Pants;
    public ShirtPart Shirt;
    public ShoesPart Shoes;
    public GlassPart Glasses;

    [Serializable]
    public class EyePart
    {
        public string Name;
        public string Value;
    }

    [Serializable]
    public class BodyPart
    {
        public string Name;
        public string Value;
    }

    [Serializable]
    public class EyeBrowPart
    {
        public string Name;
        public string Value;
    }

    [Serializable]
    public class EyeSlashPart
    {
        public string Name;
        public string Value;
    }

    [Serializable]
    public class HairPart
    {
        public string Name;
        public string Value;
    }

    [Serializable]
    public class PantsPart
    {
        public string Name;
        public string Value;
    }

    [Serializable]
    public class ShirtPart
    {
        public string Name;
        public string Value;
    }

    [Serializable]
    public class ShoesPart
    {
        public string Name;
        public string Value;
    }

    [Serializable]
    public class GlassPart
    {
        public string Name;
        public string Value;
    }
}
