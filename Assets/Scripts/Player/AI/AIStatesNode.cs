using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStatesNode
{
    public List<AIStatesNode> states;
    public AIStatesTree.AIState state;

    public AIStatesNode()
    {
        states = new List<AIStatesNode>();
    }
    
    public void Add(AIStatesNode item)
    {
        states.Add(item);
    }

    public List<AIStatesNode> GetStatesNode()
    {
        return states;
    }

    public AIStatesTree.AIState GetCurentState()
    {
        return state;
    }
}
