using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewLifeZ.API
{
    public class API_Endpoints : MonoBehaviour
    {
        private const string BASE_URL = "https://dev.api.newlifez.io/v1.0/api";

        public const string LOGIN = BASE_URL + "/auth/sign-in";
        public const string SIGNUP = BASE_URL + "/auth/signup";
    }

}
