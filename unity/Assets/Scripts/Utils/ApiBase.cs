using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Web
{
    /// <summary>
    /// Base class of HTTP
    /// </summary>
    public class ApiBase
    {

        /// <summary>
        /// A struct to set http results
        /// <summary>
        public struct Result
        {
            public bool isSucceeded { get; private set; }
            public string error { get; private set; }

            public void Suceeded()
            {
                this.isSucceeded = true;
                this.error = "";
            }

            public void Failed(string error)
            {
                this.isSucceeded = false;
                this.error = error;
            }
        }

        private readonly string BaseUrl = Utils.Enviroment.BaseUrl;

        protected string resJson;

        public string EndPoint { get; set; }

        /// <summary>
        /// Convert the request to json and perform http(POST)
        /// </summary>
        /// <typeparam name="T">Request type</typeparam>
        /// <param name="request">Request obj</param>
        /// <param name="cb">Callback func</param>
        public void Send<T>(ref T request, Action<Result> cb)
        {
            string reqJson = JsonUtility.ToJson(request);
            byte[] postData = System.Text.Encoding.UTF8.GetBytes(reqJson);

            var url = BaseUrl + EndPoint;

            Utils.CoroutineHandler.StartStaticCoroutine(onSend(url, postData, cb));
        }

        /// <summary>
        /// Perform http
        /// </summary>
        /// <param name="url">Target url</param>
        /// <param name="postData">Post data</param>
        /// <param name="cb">Callback func</param>
        /// <returns>Coroutine</returns>
        private IEnumerator onSend(string url, byte[] postData, Action<Result> cb)
        {
            var req = new UnityWebRequest(url, "POST");
            req.uploadHandler = (UploadHandler)new UploadHandlerRaw(postData);
            req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");

            yield return req.SendWebRequest();

            Result result = new Result();

            if (req.isNetworkError || req.isHttpError)
            {
                result.Failed(req.error);
            }
            else
            {
                resJson = req.downloadHandler.text;
                result.Suceeded();
            }
            cb(result);
        }

        /// <summary>
        /// Return http response
        /// </summary>
        /// <typeparam name="T"> Response type</typeparam>
        /// <returns>Response obj</returns>
        public virtual T ReturnResponse<T>()
        {
            return JsonUtility.FromJson<T>(resJson);
        }
    }
}
