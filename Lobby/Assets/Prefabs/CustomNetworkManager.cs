using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager
{ 

    public const string BroadcastMessagePrefix = "ConnectionBroadcastMessage";

    public const char BroadcastMessageDelimeter = ':';

    private static CustomNetworkManager instance;

    public static CustomNetworkManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<CustomNetworkManager>();
            }
            return instance;
        }
    }

    public string GenerateNetworkBroadcastData()
    {
        return BroadcastMessagePrefix + BroadcastMessageDelimeter + networkPort;
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Debug.Log("Set up new player");
        SetupNewPlayer(conn, playerControllerId);
    }

    public void SetupNewPlayer(NetworkConnection conn, short playerControllerId )
    {
        GameObject player = Instantiate(playerPrefab);
        player.transform.position = new Vector3(player.transform.position.x + conn.connectionId * 2, player.transform.position.y, player.transform.position.z);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

        if(playerControllerId == 0)
        {
            Renderer rend = player.GetComponent<Renderer>();
            rend.material.color = Color.red;
            Debug.Log("Spawned PLayer1");
        }

        if (playerControllerId == 1)
        {
            Renderer rend = player.GetComponent<Renderer>();
            rend.material.color = Color.blue;
            Debug.Log("Spawned Player2");
        }
    }


}
