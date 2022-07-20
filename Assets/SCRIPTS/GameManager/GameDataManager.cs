using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameDataManager
{
    public string Description;
    public string ExternalURL;
    public string AvatarURL;
    public string CharacterName;
    public EyePart Eye = new EyePart();
    public BodyPart Body = new BodyPart();
    public EyeBrowPart EyeBrow = new EyeBrowPart();
    public EyeSlashPart EyeSlash = new EyeSlashPart();
    public HairPart Hair = new HairPart();
    public PantsPart Pants = new PantsPart();
    public ShirtPart Shirt = new ShirtPart();
    public ShoesPart Shoes = new ShoesPart();
    public GlassPart Glasses = new GlassPart();

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
