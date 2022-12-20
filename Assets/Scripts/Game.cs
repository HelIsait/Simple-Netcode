#nullable enable
using Players;
using System.Collections.Generic;
using UI;
using Unity.Netcode;
using UnityEngine;
using Utils;


public class Game : NetworkBehaviour
{
    [SerializeField] private WinScreen winScreen = null!;
    [SerializeField] private NetworkManager network = null!;
    [Header("Debug")] [SerializeField] private List<Player> registred = new();


    private void Awake()
    {
        winScreen.EnsureNotNull();
        network.OnServerStarted += NetworkOnOnServerStarted;
    }


    private void OnDestroy()
    {
        network.OnServerStarted -= NetworkOnOnServerStarted;
        if (network.IsServer)
        {
            network.EnsureNotNull().OnClientConnectedCallback -= NetworkOnOnClientConnectedCallback;
        }
    }


    private void NetworkOnOnServerStarted()
    {
        if (network.IsServer)
        {
            foreach (var (key, value) in network.ConnectedClients!)
            {
                NetworkOnOnClientConnectedCallback(key);
            }

            network.EnsureNotNull().OnClientConnectedCallback += NetworkOnOnClientConnectedCallback;
        }
    }


    [ClientRpc]
    private void ShowScreenClientRpc(string message)
    {
        winScreen.Show(message);
    }


    private void NetworkOnOnClientConnectedCallback(ulong obj)
    {
        var player = network.ConnectedClients![obj]!.PlayerObject.GetComponent<Player>();
        registred.Add(player);
        player!.GetComponent<Health>()!.death.AddListener(() =>
        {
            foreach (var p in registred)
            {
                if (p.GetComponent<Health>()!.Alive)
                {
                    ShowScreenClientRpc(p.name);
                    return;
                }
            }

            ShowScreenClientRpc("No winners");
        });
    }
}