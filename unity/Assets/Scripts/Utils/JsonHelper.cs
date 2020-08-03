using System;
using UnityEngine;

namespace Utils
{
    /// <summary>
    /// Class for json processing
    /// </summary>
    public static class JsonHelper
    {

        /// <summary>
        /// Deserializing json array method
        /// </summary>
        /// <param name="json">Serialized json</param>
        /// <returns>Deserialized json</returns>
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        /// <summary>
        /// Serializing json method
        /// </summary>
        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        /// <summary>
        /// Serializing json array method
        /// </summary>
        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        /// <summary>
        /// Optimize json array method.
        /// </summary>
        /// <param name="json">Serialized json</param>
        /// <returns>Optimize string(Serialized json)</returns>
        public static string fixJson(string json)
        {
            return "{\"Items\":" + json + "}";
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}
