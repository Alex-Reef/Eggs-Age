using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Money;
    public List<GameObject> Platforms = new List<GameObject>();
    public List<GameObject> Units = new List<GameObject>();
    public Material PlayerMaterial;

    private void Start()
    {
        Money = 0;
        Init();
        InvokeRepeating("PayDay", 2, 2);
    }

    public void AddMoney(int money)
    {
        Money += money;
    }

    private void PayDay()
    {
        int money = 5;
        foreach (var platform in Platforms)
        {
            if (platform.GetComponent<Platform>().Build
            && platform.GetComponent<Platform>().Build.GetComponent<Build>().BuildType == BuildsController.BuildType.Mine)
                money += 5;
        }
        money -= Units.Count * 2;
        Money += money;
    }

    private void OnDestroy()
    {
        Platforms.Clear();
        Units.Clear();
    }

    private void Init()
    {
        foreach (var platform in BuildsController.Instance.Platforms)
        {
            if (platform.GetComponent<Platform>().OwnerPlayer)
            {
                if (platform.GetComponent<Platform>().OwnerPlayer == this.gameObject)
                    Platforms.Add(platform);
            }
        }

        foreach (var unit in UnitsController.Instance.Units)
        {
            if (unit.GetComponent<Unit>().OwnerPlayer)
            {
                if (unit.GetComponent<Unit>().OwnerPlayer == this.gameObject)
                    Units.Add(unit);
            }
        }
    }
}
