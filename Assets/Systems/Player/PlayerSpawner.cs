using UnityEngine;
using Photon.Pun;
using Unity.Cinemachine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] playerPrefabs; // Lista de prefabs de jugadores
    
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

        int playerIndex = (PhotonNetwork.LocalPlayer.ActorNumber - 1) % spawnPositions.Length;
        Vector3 spawnPoint = spawnPositions[playerIndex];

        // Seleccionar un prefab aleatorio
        GameObject selectedPrefab = playerPrefabs[Random.Range(0, playerPrefabs.Length)];

        // Instanciar en red con Photon
        GameObject player = PhotonNetwork.Instantiate(selectedPrefab.name, spawnPoint, Quaternion.identity);

        if (player.GetComponent<PhotonView>().IsMine)
        {
            CinemachineCamera cinemachineCam = GameObject.FindWithTag("CinemachineCam").GetComponent<CinemachineCamera>();
            cinemachineCam.Follow = player.transform;
            cinemachineCam.LookAt = player.transform;
        }

        Debug.Log($"✅ Jugador {PhotonNetwork.LocalPlayer.ActorNumber} spawneado con {selectedPrefab.name} en {spawnPoint}");
    }
}