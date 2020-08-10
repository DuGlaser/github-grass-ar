using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabController : MonoBehaviour
{
    [SerializeField]
    GameObject GITHUB_LEVEL_1;

    [SerializeField]
    GameObject GITHUB_LEVEL_2;

    [SerializeField]
    GameObject GITHUB_LEVEL_3;

    [SerializeField]
    GameObject GITHUB_LEVEL_4;

    [SerializeField]
    GameObject GITHUB_LEVEL_5;

    public float startPrefabPositionX = -0.175f;
    public static PrefabController instance;

    private GameObject curGitHubObject;
    private float curPrefabPositionX;

    void Awake()
    {
        instance = this;
        curPrefabPositionX = startPrefabPositionX;
    }

    public void CreateGithubObjects(Api.Github.Response[] res, GameObject parent)
    {
        foreach (var item in res[0].ContributionDays)
        {
            Debug.Log(item.ContributionCount);
            Debug.Log(item.Date);
            Debug.Log(item.Color);

            SelectObject(item.Color);

            GameObject obj = Instantiate(curGitHubObject, parent.transform);
            obj.transform.localPosition = new Vector3(curPrefabPositionX, 0, 0);
            curPrefabPositionX += 0.068f;
        }

        curPrefabPositionX = startPrefabPositionX;
    }

    private void SelectObject(string color)
    {
        switch (color)
        {
            case "#216e39":
                curGitHubObject = GITHUB_LEVEL_5;
                break;

            case "#30c14e":
                curGitHubObject = GITHUB_LEVEL_4;
                break;

            case "#40c463":
                curGitHubObject = GITHUB_LEVEL_3;
                break;

            case "#9be9a8":
                curGitHubObject = GITHUB_LEVEL_2;
                break;

            default:
                curGitHubObject = GITHUB_LEVEL_1;
                break;
        }
    }
}
