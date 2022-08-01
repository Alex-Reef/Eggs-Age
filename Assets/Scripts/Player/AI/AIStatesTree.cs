using System.Collections.Generic;
using UnityEngine;

public class AIStatesTree
{
    public AIStatesNode states;

    public AIStatesTree()
    {
        states = new AIStatesNode();
    }

    public void Add(AIStatesNode item)
    {
        states.Add(item);
    }

    public enum AIState
    {
        Internal_Policy,
        Amplification,
        Development,
        Economic,
        Army,
        Capturing_The_Mines,
        Building_Factories,
        Hiring_Units,
    }

    public AIState GetState(AIStatesNode States)
    {
        if(States.states.Count == 0)
            return States.state;
        else{
            int select = Random.Range(0, States.states.Count);
            return GetState(States.states[select]);
        }
    }

    public void BuildTree()
    {
        AIStatesNode internalPol = new AIStatesNode();
        internalPol.state = AIState.Internal_Policy;

        internalPol.Add(BuildANodeOfDevelopment());
        internalPol.Add(BuildANodeOfAmplification());

        states.Add(internalPol);  
    }

    private AIStatesNode BuildANodeOfAmplification()
    {
        AIStatesNode newState = new AIStatesNode();
        newState.state = AIState.Amplification;

        AIStatesNode node = new AIStatesNode();
        node.state = AIState.Army;
        newState.Add(node);

        node.state = AIState.Hiring_Units;
        newState.Add(node);

        return newState;
    }

    private AIStatesNode BuildANodeOfDevelopment()
    {
        AIStatesNode newState = new AIStatesNode();
        newState.state = AIState.Development;

        AIStatesNode node = new AIStatesNode();
        node.state = AIState.Economic;

        AIStatesNode subNode1 = new AIStatesNode();
        subNode1.state = AIState.Capturing_The_Mines;
        node.Add(subNode1);

        AIStatesNode subNode2 = new AIStatesNode();
        subNode2.state = AIState.Building_Factories;
        node.Add(subNode2);

        newState.Add(node);

        return newState;
    }
}
