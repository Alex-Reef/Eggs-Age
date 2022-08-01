using UnityEngine;

public class AIState_HiringUnits : MonoBehaviour
{
    public GameObject Player;
    void Start()
    {
        Debug.Log("Hiring Units");
        Transform pos = SelectingBuild();
        GameObject unit = SelectingUnit();
        if (pos && unit)
            HiringUnit(pos, unit);
    }

    // Selecting build where AI can spawn units
    private Transform SelectingBuild()
    {
        var builds = Player.GetComponent<Player>().Platforms.FindAll(x => x.GetComponent<Platform>().Build);
        if (builds.Count > 0)
        {
            builds = builds.FindAll(x => x.GetComponent<Platform>().Build.GetComponent<Build>().BuildType == BuildsController.BuildType.Barracks);
            if (builds.Count > 0)
            {
                int select = Random.Range(0, builds.Count);
                Transform pos = builds[select].transform;
                return pos;
            }
        }
        return null;
    }

    // Selecting unit for spawn
    private GameObject SelectingUnit()
    {
        int select = Random.Range(0, UnitsController.Instance.UnitPrefabs.Count);
        GameObject unit = UnitsController.Instance.UnitPrefabs[select];
        return unit;
    }

    // Hiring selected unit around selected build
    private void HiringUnit(Transform pos, GameObject unit)
    {
        Vector3 spawnPos = new Vector3(pos.position.x + Random.Range(0, 3), pos.position.y, pos.position.z + Random.Range(0, 3));
        GameObject newUnit = Instantiate(unit, spawnPos, Quaternion.identity);
        newUnit.GetComponent<Unit>().OwnerPlayer = Player;
        Player.GetComponent<Player>().Units.Add(newUnit);
        if(Player.GetComponent<Player>().Money >= 10)
            Player.GetComponent<Player>().AddMoney(-10);
    }
}
