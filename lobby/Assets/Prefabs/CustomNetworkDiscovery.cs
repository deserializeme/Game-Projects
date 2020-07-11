using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkDiscovery : NetworkDiscovery
{

    private static CustomNetworkDiscovery instance;

    public static CustomNetworkDiscovery Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<CustomNetworkDiscovery>();
            }
            return instance;
        }
    }

    private CustomNetworkManager customNetworkManager;

    private void Start()
    {
        customNetworkManager = CustomNetworkManager.Instance;
        broadcastData = CustomNetworkManager.Instance.GenerateNetworkBroadcastData();
        Initialize();
    }

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        Debug.LogError("Received Broadcast! From address: " + fromAddress + ", data: " + data);
        string[] items = data.Split(CustomNetworkManager.BroadcastMessageDelimeter);
        if (items.Length == 2 && items[0] == CustomNetworkManager.BroadcastMessagePrefix)
        {
            if(customNetworkManager != null && customNetworkManager.client == null)
            {
                Debug.LogError("Attempting to connect to: " + fromAddress);
                customNetworkManager.networkAddress = fromAddress;
                customNetworkManager.networkPort = int.Parse(items[1]);
                customNetworkManager.StartClient();
            }
        }
    }

    

}
