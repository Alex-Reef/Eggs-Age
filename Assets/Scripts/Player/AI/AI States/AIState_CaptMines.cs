using System.Collections.Generic;
using UnityEngine;
using System;

public class AIState_CaptMines : MonoBehaviour
{
    public GameObject Player;
    private List<GameObject> selectedUnits = new List<GameObject>();

    void Start()
    {
        StartOperations();
    }

    private void OnDestroy()
    {
        Player = null;
    }

    private void StartOperations()
    {
        var info = GetMinDistanceToFreePlatform();
        if (info.Item1 < 30)
        {
            if(Player.GetComponent<Player>().Units.Count >= 3)
            {
                Capture(info.Item2);
            }
        }
    }

    private void Capture(Transform platform)
    {
        var units = Player.GetComponent<Player>().Units;
        foreach(var unit in units)
        {
            if (unit.GetComponent<Unit>().Task == UnitsController.UnitTasks.None)
                selectedUnits.Add(unit);
        }

        Debug.Log(selectedUnits.Count);
        if(selectedUnits.Count > 0)
        {
            int Select = 0;
            if (selectedUnits.Count <= 2)
                Select = UnityEngine.Random.Range(0, selectedUnits.Count);
            if (selectedUnits.Count > 2 && selectedUnits.Count <= 6)
                Select = UnityEngine.Random.Range(0, selectedUnits.Count / 2);
            else
                Select = UnityEngine.Random.Range(0, 4);  

            selectedUnits[Select].GetComponent<Unit>().GoTo(platform);
            selectedUnits[Select].GetComponent<Unit>().Task = UnitsController.UnitTasks.Capture;
        }
        selectedUnits.Clear();
    }

    private Tuple<float, Transform> GetMinDistanceToFreePlatform()
    {
        float dist = 30;
        Transform platformPos = null;

        foreach (var AIplatform in Player.GetComponent<Player>().Platforms)
        {
            foreach (var platform in BuildsController.Instance.Platforms)
            {
                if (!platform.GetComponent<Platform>().OwnerPlayer)
                {
                    float tmpDist = Vector3.Distance(AIplatform.transform.position, platform.transform.position);
                    if (tmpDist < dist)
                    {
                        dist = tmpDist;
                        platformPos = platform.transform;
                    }
                }
            }
        }

        return new Tuple<float, Transform>(dist, platformPos);
    }
}
