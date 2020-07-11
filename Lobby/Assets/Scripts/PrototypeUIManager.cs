using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeUIManager : MonoBehaviour
{

    public GameObject UIParent;

    private CustomNetworkManager networkManager;

    private CustomNetworkDiscovery networkDiscovery;

    void Start()
    {
        networkManager = CustomNetworkManager.Instance;
        networkDiscovery = CustomNetworkDiscovery.Instance;

    }

    public void StartHost()
    {
        networkManager.StartHost();
        networkDiscovery.StartAsServer();
        DisableButtons();
    }

    public void StartClient()
    {
        networkDiscovery.StartAsClient();
        DisableButtons();
    }

    public void DisableButtons()
    {
        UIParent.SetActive(false);
    }
}