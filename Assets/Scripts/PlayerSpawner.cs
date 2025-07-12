using Fusion;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;
    private void Awake()
    {
        GameEvents.OnGameStarted += LoadPlayerPrefab;
    }
    public void LoadPlayerPrefab()
    {
        //PlayerPrefab = playerPrefab;
    }
    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
           NetworkObject obj = Runner.Spawn(PlayerPrefab, new Vector3(0, 1, 0), Quaternion.identity);
           obj.GetComponent<NetworkAvatar>().RpcLoad();
        }
    }
}