using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MainController : MonoBehaviour
{

    public GameObject objectToPlace;
    public GameObject placementIndicator;

    private ARSessionOrigin arOrigin;
    private ARRaycastManager raycastManager;
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    private Api.Github githubApi;
    private bool isApiSucceeded = false;

    private PrefabController prefabController;

    void Start()
    {
        githubApi = new Api.Github();
        sendGithubApi();

        arOrigin = FindObjectOfType<ARSessionOrigin>();
        raycastManager = FindObjectOfType<ARRaycastManager>();
        prefabController = FindObjectOfType<PrefabController>();
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        // for debug
        if (Input.GetKeyDown("space") && isApiSucceeded)
        {
            var res = githubApi.ReturnResponse<Api.Github.Response>();
            foreach (var item in res[0].ContributionDays)
            {
                Debug.Log(item.ContributionCount);
                Debug.Log(item.Date);
                Debug.Log(item.Color);

                prefabController.CreateGithubObject(item.Color, item.Date, item.ContributionCount);
            }
        }

        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && isApiSucceeded)
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
        Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            // カメラの向きに合わせて回転させる
            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }
}
