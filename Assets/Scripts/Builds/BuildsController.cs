using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildsController : MonoBehaviour
{
    public List<GameObject> Platforms = new List<GameObject>();
    public List<GameObject> Builds = new List<GameObject>();
    public static BuildsController Instance;

    public enum BuildType
    {
        Office,
        Scientific,
        Factory,
        Mine,
        Barracks
    }

    public enum PlatformType
    {
        City,
        Ocean,
        Mine
    }

    private void Awake()
    {
        Instance = this;
        Transform[] platforms = GetComponentsInChildren<Transform>();

        foreach (var platform in platforms)
        {
            if(platform.GetComponent<Platform>())
                Platforms.Add(platform.gameObject);
        }
    }

    private void OnDestroy()
    {
        Platforms.Clear();
        Instance = null;
    }
}
