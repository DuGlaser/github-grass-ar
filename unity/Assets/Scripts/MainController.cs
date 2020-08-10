using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MainController : MonoBehaviour
{

    public GameObject sampleToObject;

    private ARSessionOrigin arOrigin;
    private ARRaycastManager raycastManager;
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    private Api.Github githubApi;
    private bool isApiSucceeded = false;

    void Start()
    {
        githubApi = new Api.Github();
        sendGithubApi();
    }

    void Update()
    {
        // for debug
        if (Input.GetKeyDown("space") && isApiSucceeded)
        {
            var res = githubApi.ReturnResponse<Api.Github.Response>();
            foreach (var item in res[0].ContributionDays)
            {
                Debug.Log(item.ContributionCount);
                Debug.Log(item.Date);
                Debug.Log(item.Color);

                /* prefabController.CreateGithubObject(item.Color, item.Date, item.ContributionCount, objectToPlace); */
            }
        }

        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
        }
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
                isApiSucceeded = true;
                var res = githubApi.ReturnResponse<Api.Github.Response>();
            }
            else
            {
                Debug.Log(result.error);
            }
        });
    }

    private void PlaceObject()
    {
        Instantiate(sampleToObject, placementPose.position, placementPose.rotation);

/*         var res = githubApi.ReturnResponse<Api.Github.Response>(); */
/*         foreach (var item in res[0].ContributionDays) */
/*         { */
/*             Debug.Log(item.ContributionCount); */
/*             Debug.Log(item.Date); */
/*             Debug.Log(item.Color); */

/*             prefabController.CreateGithubObject(item.Color, item.Date, item.ContributionCount, objectToPlace); */
/*         } */
    }
}
