using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewLifeZ
{
    public static class GameConstant
    {
        public static class SceneName
        {
            public const string MAIN_GAME = "MainGame";
            public const string MAIN_MENU = "MainMenu";
            public const string GOLDMINE = "GoldMine";
        }

        public static class PhotonRoom
        {
            public const string MAIN_ROOM = "NewLifeZ_Room";
            public const string GOLD_MINE = "GoldMine_Room";
        }

        public static class PlayerPref
        {
            public const string CHARACTER_LAST_SAVE = "CharacterSaved";
            public const string CHARACTER_LAST_NAME = "Username";
        }
    }
}

