using System.Collections.Generic;
using UnityEngine;

public class AIState_BuildingsFactories : MonoBehaviour
{
    public GameObject Player;
    private List<GameObject> freePlatforms = new List<GameObject>();

    void Start()
    {
        Debug.Log("Building Factories");
        if (FreePlatform())
        {
            SelectBuild(freePlatforms[SelectPlatform()]);
        }
    }

    private bool FreePlatform()
    {
        foreach (var platform in Player.GetComponent<Player>().Platforms)
        {
            if (!platform.GetComponent<Platform>().Build)
            {
                return true;
            }
        }
        return false;
    }

    private int SelectPlatform()
    {
        foreach (var platform in Player.GetComponent<Player>().Platforms)
        {
            if (!platform.GetComponent<Platform>().Build)
            {
                freePlatforms.Add(platform);
            }
        }

        return Random.Range(0, freePlatforms.Count);
    }

    private void SelectBuild(GameObject platform)
    {
        switch (platform.GetComponent<Platform>().Type)
        {
            case BuildsController.PlatformType.Mine:
                {
                    Debug.Log("Mine");
                    GameObject build = BuildsController.Instance.Builds.Find(x => x.GetComponent<Build>().BuildType == BuildsController.BuildType.Mine);
                    platform.GetComponent<Platform>().Building(build);
                    if(Player.GetComponent<Player>().Money >= 8)
                        Player.GetComponent<Player>().AddMoney(-8);
                    break;
                }
            case BuildsController.PlatformType.City:
                {
                    Debug.Log("City");
                    var builds = BuildsController.Instance.Builds.FindAll(x => x.GetComponent<Build>().BuildType != BuildsController.BuildType.Mine);
                    int select = Random.Range(0, builds.Count);
                    platform.GetComponent<Platform>().Building(builds[select]);
                    if(Player.GetComponent<Player>().Money >= 8)
                        Player.GetComponent<Player>().AddMoney(-8);
                    break;
                }
            case BuildsController.PlatformType.Ocean:
                {
                    Debug.Log("Ocean");
                    GameObject build = BuildsController.Instance.Builds.Find(x => x.GetComponent<Build>().BuildType == BuildsController.BuildType.Mine);
                    platform.GetComponent<Platform>().Building(build);
                    if(Player.GetComponent<Player>().Money >= 8)
                        Player.GetComponent<Player>().AddMoney(-8);
                    break;
                }
        }
    }
}
