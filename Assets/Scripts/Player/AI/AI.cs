using UnityEngine;

public class AI : MonoBehaviour
{
    public GameObject player;    

    private void Start()
    {
        InvokeRepeating("CheckState", 3, 3);
        //CheckState();
    }

    private void OnDestroy()
    {
        player = null;
    }

    private void CheckState()
    {
        Debug.Log("Selecting");
        // For testing only one state
        //RemoveAllStates();
        //gameObject.AddComponent<AIState_BuildingsFactories>();
        //gameObject.GetComponent<AIState_BuildingsFactories>().Player = player;
        
        AIStatesTree tree = new AIStatesTree();
        tree.BuildTree();
        AIStatesTree.AIState selectState = tree.GetState(tree.states);
        switch (selectState)
        {
            case AIStatesTree.AIState.Capturing_The_Mines:
                {
                    RemoveAllStates();
                    gameObject.AddComponent<AIState_CaptMines>();
                    gameObject.GetComponent<AIState_CaptMines>().Player = player;
                    break;
                }
            case AIStatesTree.AIState.Building_Factories:
                {
                    RemoveAllStates();
                    gameObject.AddComponent<AIState_BuildingsFactories>();
                    gameObject.GetComponent<AIState_BuildingsFactories>().Player = player;
                    break;
                }
            case AIStatesTree.AIState.Hiring_Units:
                {
                    RemoveAllStates();
                    gameObject.AddComponent<AIState_HiringUnits>();
                    gameObject.GetComponent<AIState_HiringUnits>().Player = player;
                    break;
                }
        }
    }

    private bool FreePlatform()
    {
        foreach(var platform in player.GetComponent<Player>().Platforms)
        {
            if(platform.GetComponent<Platform>().OwnerPlayer)
                return true;
        }
        return false;
    }

    private float GetMinDistanceToFreePlatform()
    {
        float dist = 20;

        foreach (var AIplatform in player.GetComponent<Player>().Platforms)
        {
            foreach (var platform in BuildsController.Instance.Platforms)
            {
                if (!platform.GetComponent<Platform>().OwnerPlayer)
                {
                    float tmpDist = Vector3.Distance(AIplatform.transform.position, platform.transform.position);
                    if (tmpDist < dist)
                    {
                        dist = tmpDist;
                    }
                }
            }
        }

        return dist;
    } 

    private float GetMinDistanceToPlayers()
    {
        float dist = 20;

        foreach (var AIplatform in player.GetComponent<Player>().Platforms)
        {
            foreach (var platform in BuildsController.Instance.Platforms)
            {
                if (platform.GetComponent<Platform>().OwnerPlayer)
                {
                    if(platform.GetComponent<Platform>().OwnerPlayer != player)
                    {
                        float tmpDist = Vector3.Distance(AIplatform.transform.position, platform.transform.position);
                        if(tmpDist < dist)
                        {
                            dist = tmpDist;
                        }
                    }
                }
            }
        }

        return dist;
    }

    private void RemoveAllStates()
    {
        foreach(var comp in player.GetComponents<Component>())
        {
            if(!(comp is Player) && !(comp is Transform) && !(comp is AI))
                DestroyImmediate(comp);
        }
    }
}
