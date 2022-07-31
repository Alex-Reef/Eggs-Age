using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsController : MonoBehaviour
{
    public List<GameObject> Units = new List<GameObject>();
    public List<GameObject> UnitPrefabs = new List<GameObject>();
    public static UnitsController Instance;

    public enum UnitType
    {
        None,
        Pistol,
        Sword
    }

    public enum UnitTasks
    {
        None,
        Walk,
        Patruling,
        Attack,
        Capture,
        Defense
    }

    private void Awake()
    {
        Instance = this;
        Transform[] units = GetComponentsInChildren<Transform>();

        foreach (var unit in units)
        {
            if (unit.GetComponent<Unit>())
                Units.Add(unit.gameObject);
        }
    }

    private void OnDestroy()
    {
        Units.Clear();
        Instance = null;
    }
}
