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

    [SerializeField]
    GameObject placementIndicator;

    private Vector3 prefabTransfrom;
    public static PrefabController instance;

    void Awake()
    {
        instance = this;
        prefabTransfrom = new Vector3(-placementIndicator.GetComponent<Renderer>().bounds.size.x / 2, 0, 0);
    }

    public void CreateGithubObject(string color, string date, int count, GameObject parent)
    {
        prefabTransfrom.x += placementIndicator.GetComponent<Renderer>().bounds.size.x / 7;

        GameObject instantiatePrefab;

        switch (color)
        {
            case "#216e39":
                instantiatePrefab = GITHUB_LEVEL_5;
                break;

            case "#30c14e":
                instantiatePrefab = GITHUB_LEVEL_4;
                break;

            case "#40c463":
                instantiatePrefab = GITHUB_LEVEL_3;
                break;

            case "#9be9a8":
                instantiatePrefab = GITHUB_LEVEL_2;
                break;

            default:
                instantiatePrefab = GITHUB_LEVEL_1;
                break;
        }

        Instantiate(instantiatePrefab, prefabTransfrom, Quaternion.identity, parent.transform);
    }
}
