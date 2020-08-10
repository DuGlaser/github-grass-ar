using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MainController : MonoBehaviour
{

    public GameObject placeToObjectGroup;

    private ARSessionOrigin arOrigin;
    private PlacementIndicator placementIndicator;

    private Api.Github githubApi;
    private bool isApiSucceeded = false;

    void Start()
    {
        githubApi = new Api.Github();
        sendGithubApi();

        placementIndicator = GetComponent<PlacementIndicator>();
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

    public void OnPlaceGithubObject()
    {
        Vector3 placeToObjectPosition = PlacementIndicator.instance.transform.position;
        Quaternion placeToObjectRotation = PlacementIndicator.instance.transform.rotation;

        GameObject parent = Instantiate(
            placeToObjectGroup,
            placeToObjectPosition,
            placeToObjectRotation
            );

        var res = githubApi.ReturnResponse<Api.Github.Response>();
        PrefabController.instance.CreateGithubObjects(res, parent);
    }
}
