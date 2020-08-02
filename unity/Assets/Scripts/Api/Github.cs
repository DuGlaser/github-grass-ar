using System;

namespace Api
{
  public class Github : Web.ApiBase
  {

    [Serializable]
    public struct Reequest
    {
        public string name;
        public string from;
        public string to;
    }

    public Reequest reequest;

    [Serializable]
    public struct Response
    {
      public Weeks[] weeks;
    }

    [Serializable]
    public struct  Weeks
    {
      public ContributionDays[] contributionDays;
    }

    [Serializable]
    public struct ContributionDays 
    {
        public int contributionCount;
        public string color;
    }

    public Response response;
  }
}
