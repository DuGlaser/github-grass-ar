using System;
using UnityEngine;

namespace Utils
{
    public enum EnvironmentType
    {
        Dev,
        Stg,
        Prod
    }

    /// <summary>
    /// Class to switch variables depending on the enviroment.
    /// </summary>
    public class Enviroment
    {
    /* Using Unity Cloud Build */
    /* #if ENV_STG */
    /*     public static readonly EnvironmentType Type = EnvironmentType.Stg; */
    /*     public static readonly string BASE_URL = "http://localhost:3000/api/v1"; */

    /* #elif ENV_PROD */
    /*     public static readonly EnvironmentType Type = EnvironmentType.Prod; */
    /*     public static readonly string BASE_URL = DefineValue.BASE_URL; */

    /* #else */
    /*     public static readonly EnvironmentType Type = EnvironmentType.Dev; */
    /*     public static readonly string BASE_URL = DefineValue.BASE_URL; */

    /* #endif */

      #if UNITY_EDITOR
        public static readonly EnvironmentType Type = EnvironmentType.Dev;
        public static readonly string BaseUrl = "http://localhost:3000/api/v1";
      #else
        public static readonly EnvironmentType Type = EnvironmentType.Prod;
        public static readonly string BaseUrl = DefineValue.BASE_URL;
      #endif
    }
}
