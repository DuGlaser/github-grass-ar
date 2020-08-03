using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sample : MonoBehaviour
{
    private Api.Github githubApi;
    // Start is called before the first frame update
    void Start()
    {
        githubApi = new Api.Github();

        sendGithubApi();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void sendGithubApi()
    {
        githubApi.EndPoint = "/get_contributions_info";
        githubApi.reequest.name = "DuGlaser";
        githubApi.reequest.from = "2020-07-19";
        githubApi.reequest.to = "2020-07-25";

        githubApi.Send<Api.Github.Request>(ref githubApi.reequest, result =>
        {
            if (result.isSucceeded)
            {
                var res = githubApi.ReturnResponse<Api.Github.Response>();
                Debug.Log(res[0].ContributionDays[0].Color);
            }
            else
            {
                Debug.Log(result.error);
            }
        });
    }
}
