using System;
using System.Collections.Generic;

namespace Api
{
    /// <summary>
    /// Github api class
    /// </summary>
    public class Github : Web.ApiBase
    {

        /// <summary>
        /// Request params
        /// </summary>
        [Serializable]
        public struct Request
        {
            public string name;
            public string from;
            public string to;
        }

        public Request reequest;

        /// <summary>
        /// Response params
        /// </summary>
        [Serializable]
        public struct Response
        {
            public List<ContributionDaysDetail> ContributionDays;
        }

        [Serializable]
        public struct ContributionDaysDetail
        {
            public int ContributionCount;
            public string Color;
            public string Date;
        }

        public Response response;

        /// <summary>
        /// Deserialized json array and return response obj
        /// </summary>
        /// <typeparam name="T"> Response type</typeparam>
        /// <returns>Response obj</returns>
        public new T[] ReturnResponse<T>()
        {
            string newJson = Utils.JsonHelper.fixJson(resJson);
            return Utils.JsonHelper.FromJson<T>(newJson);
        }
    }
}
