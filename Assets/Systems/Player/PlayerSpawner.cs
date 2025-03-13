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
        int playerIndex = PhotonNetwork.CurrentRoom.PlayerCount - 1; // 0 para el primero, 1 para el segundo

        if (playerIndex >= spawnPositions.Length)
        {
            Debug.LogError("¡Error! Hay más jugadores de los permitidos en la carrera.");
            return;
        }

        Vector3 spawnPoint = spawnPositions[playerIndex];

        PhotonNetwork.Instantiate("Player", spawnPoint, Quaternion.identity, 0);
    }
}