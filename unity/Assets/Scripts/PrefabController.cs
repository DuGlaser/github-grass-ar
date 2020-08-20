using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabController : MonoBehaviour
{
    [SerializeField]
    private GameObject graph;

    private List<GameObject> graphs = new List<GameObject>();
    private List<float> graphsTopScale = new List<float>();

    [SerializeField]
    private float speed = 2.0f;

    public float startPrefabPositionX = -0.175f;
    public static PrefabController instance;
    private float curPrefabPositionX;

    void Awake()
    {
        instance = this;
        curPrefabPositionX = startPrefabPositionX;
    }

    void Update()
    {
        for (var i = 0; i < graphs.Count; i++)
        {
            GameObject obj = graphs[i];
            Vector3 curScale = graphs[i].transform.localScale;
            if (curScale.y < graphsTopScale[i])
            {
                curScale.y = Mathf.MoveTowards(curScale.y, graphsTopScale[i], Time.deltaTime * speed);
                obj.transform.localScale = curScale;
            }
        }
    }


    public void CreateGithubObjects(Api.Github.Response[] res, GameObject parent)
    {
        foreach (var item in res[0].ContributionDays)
        {
            Debug.Log(item.ContributionCount);
            Debug.Log(item.Date);
            Debug.Log(item.Color);

            GameObject obj = Instantiate(graph, parent.transform);
            obj.transform.localPosition = new Vector3(curPrefabPositionX, 0, 0);
            curPrefabPositionX += 0.068f;

            Color color = default(Color);
            if (ColorUtility.TryParseHtmlString(item.Color, out color))
            {
                obj.transform.GetChild(0).GetComponent<Renderer>().material.color = color;
            }

            graphs.Add(obj);
            graphsTopScale.Add(obj.transform.localScale.y * item.ContributionCount);

        }

        curPrefabPositionX = startPrefabPositionX;
    }
}
