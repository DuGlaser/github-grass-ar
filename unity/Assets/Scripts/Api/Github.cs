using System;
using System.Collections.Generic;
using UnityEngine;

namespace Api
{
    public class Github : Web.ApiBase
    {

        [Serializable]
        public struct Request
        {
            public string name;
            public string from;
            public string to;
        }

        public Request reequest;

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
        }

        public Response response;
    }
}
