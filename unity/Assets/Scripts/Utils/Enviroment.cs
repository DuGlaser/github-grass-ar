using System;

namespace Utils
{
    public enum EnvironmentType
    {
        Dev,
        Stg,
        Prod
    }

    public class Settings {
        public string BASE_URL;

        public Settings(string BaseUrl) 
        {
          this.BASE_URL = BaseUrl;
        }
    }

    public class Enviroment
    {
    /* #if ENV_STG */
    /*     public static readonly EnvironmentType Type = EnvironmentType.Stg; */
    /*     public static readonly Settings settings = new Settings(BaseUrl: Environment.GetEnvironmentVariable("BASE_URL")); */

    /* #elif ENV_PROD */
    /*     public static readonly EnvironmentType Type = EnvironmentType.Prod; */
    /*     public static readonly Settings settings = new Settings(BaseUrl: Environment.GetEnvironmentVariable("BASE_URL")); */

    /* #else */
    /*     public static readonly EnvironmentType Type = EnvironmentType.Dev; */
    /*     public static readonly Settings settings = new Settings(BaseUrl: "http://localhost:3000/api/v1"); */

    /* #endif */

      #if UNITY_EDITOR
        public static readonly EnvironmentType Type = EnvironmentType.Dev;
        public static readonly Settings settings = new Settings(BaseUrl: "http://localhost:3000/api/v1");
      #else
        public static readonly EnvironmentType Type = EnvironmentType.Prod;
        public static readonly Settings settings = new Settings(BaseUrl: Environment.GetEnvironmentVariable("BASE_URL"));
      #endif
    }
}
