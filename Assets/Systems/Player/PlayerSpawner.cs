using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    private Vector3[] spawnPositions = new Vector3[]
    {
        new Vector3(2.57f, 0.5f, 49.64f),
        new Vector3(-2.57f, 0.5f, 49.64f)
    };

    void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if (!PhotonNetwork.InRoom) return;

        // Usamos ActorNumber para asignar posiciones únicas a los jugadores
        int playerIndex = (PhotonNetwork.LocalPlayer.ActorNumber - 1) % spawnPositions.Length;

        Vector3 spawnPoint = spawnPositions[playerIndex];

        // Instanciamos al jugador en la red
        GameObject player = PhotonNetwork.Instantiate("Player", spawnPoint, Quaternion.identity);
        
        Debug.Log($"✅ Jugador {PhotonNetwork.LocalPlayer.ActorNumber} spawneado en {spawnPoint}");
    }
}
