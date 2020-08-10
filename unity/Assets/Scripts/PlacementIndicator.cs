using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour
{

    private ARRaycastManager raycastManager;
    private GameObject visual;

    private Camera cam;

    public bool isActive = false;

    public static PlacementIndicator instance;

    void Awake()
    {
        instance = this;
        raycastManager = FindObjectOfType<ARRaycastManager>();
        visual = transform.GetChild(0).gameObject;
        cam = Camera.main;
    }

    void Start()
    {
        visual.SetActive(false);
    }

    void Update()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;

            var cameraForward = cam.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            transform.rotation = Quaternion.LookRotation(cameraBearing);

            if (!visual.activeInHierarchy)
            {
                visual.SetActive(true);
                isActive = true;
            }
        }
    }
}
