using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    Camera cam;
    NavMeshAgent agent;
    public LayerMask ground;
    private Vector3 targetPostion;

    void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("Stop", 0.5f, 0.5f);
    }

    private void Stop()
    {
        if (Vector3.Distance(this.gameObject.transform.position, targetPostion) < 0.7)
        {
            agent.ResetPath();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                targetPostion = hit.point;
                agent.SetDestination(hit.point);
            }
        }
    }
}
