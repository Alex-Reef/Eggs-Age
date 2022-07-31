using System.Collections.Generic;
using UnityEngine;

public class AIStatesTree
{
    public List<AIStatesNode> states;

    public AIStatesTree()
    {
        states = new List<AIStatesNode>();
    }

    public void Add(AIStatesNode item)
    {
        states.Add(item);
    }

    public enum AIState
    {
        Foreign_Policy,
        Internal_Policy,
        Amplification,
        Development,
        Agressive,
        Peaseful,
        Technology,
        Economic,
        City_Defense,
        Army,
        Millitary_Operations,
        Deterioration_Of_Relations,
        Strengthening_Relationships,
        Union,
        Expeditions,
        Construction_Of_Scientific_Buildings,
        Capturing_The_Mines,
        Building_Factories,
        Trade,
        Millitary,
        Opening_The_State_Border,
        Send_A_Gift,
        Strengthening_Buildings,
        Turret_Building,
        Hiring_Units,
        Unit_Upgrade,
        Mine_Blocking,
        War,
        Insult,
        Closing_The_State_Border
    }

    public AIState GetState(List<AIStatesNode> States)
    {
        int select = Random.Range(0, States.Count);
        if (States[select].GetStatesNode().Count == 0)
            return States[select].GetCurentState();
        else
            return GetState(States[select].GetStatesNode());
    }

    public void BuildTree()
    {
        AIStatesNode foreignPol = new AIStatesNode();
        foreignPol.state = AIState.Foreign_Policy;

        AIStatesNode internalPol = new AIStatesNode();
        internalPol.state = AIState.Internal_Policy;

        internalPol.Add(BuildANodeOfDevelopment());

        states.Add(internalPol);
        states.Add(foreignPol);
    }

    private void BuildANodeOfAggression()
    {

    }

    private void BuildANodeOfPeaseful()
    {

    }

    private void BuildANodeOfAmplification()
    {

    }

    private AIStatesNode BuildANodeOfDevelopment()
    {
        AIStatesNode newState = new AIStatesNode();
        newState.state = AIState.Development;

        AIStatesNode node = new AIStatesNode();
        node.state = AIState.Economic;
        newState.Add(node);

        node.state = AIState.Capturing_The_Mines;
        newState.Add(node);

        return newState;
    }
}