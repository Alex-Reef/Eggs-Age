using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersController : MonoBehaviour
{
    public List<GameObject> Players = new List<GameObject>();
    public static PlayersController Instance;

    private void Awake()
    {
        Instance = this;
        Transform[] players = GetComponentsInChildren<Transform>();

        foreach(var player in players)
        {
            Players.Add(player.gameObject);
        }
    }

    private void OnDestroy()
    {
        Players.Clear();
        Instance = null;
    }
}
