using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public GameObject OwnerPlayer;
    public int Health;
    public TextMesh HealthBar;
    private NavMeshAgent agent;
    public UnitsController.UnitTasks Task;
    public UnitsController.UnitType Type;

    private Transform targetPos = null;

    private void Start()
    {
        HealthBar.text = Health.ToString();
        agent = GetComponent<NavMeshAgent>();
        UnitsController.Instance.Units.Add(this.gameObject);
        Task = UnitsController.UnitTasks.None;
    }

    private void Update()
    {
        HealthBar.transform.rotation = Camera.main.transform.rotation; 

        if (targetPos)
        {
            if (Vector3.Distance(targetPos.position, transform.position) <= 5.2)
            {
                targetPos = null;
                agent.ResetPath();
                Task = UnitsController.UnitTasks.None;
            }
        }
    }

    public void GetDamage(int damage)
    {
        if (Health <= 0)
            Destroy(this.gameObject);
        else
        {
            Health -= damage;
            HealthBar.text = Health.ToString();
        }
    }

    public void GoTo(Transform pos)
    {
        agent.SetDestination(pos.position);
        Task = UnitsController.UnitTasks.Walk;
        targetPos = pos;
    }

    public void Cancel()
    {
        targetPos = null;
        agent.ResetPath();
    }

    public void OnDestroy()
    {
        OwnerPlayer.GetComponent<Player>().Units.Remove(this.gameObject);
        UnitsController.Instance.Units.Remove(this.gameObject);
    }
}
