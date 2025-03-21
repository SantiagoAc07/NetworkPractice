using UnityEngine;
using Photon.Pun;
using Unity.Cinemachine;

public class PlayerSpawner : MonoBehaviour
{
    private Vector3[] spawnPositions = new Vector3[]
    {
        new Vector3(2.57f, 0.5f, 49.64f),
        new Vector3(-2.57f, 0.5f, 49.64f)
    };

    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if (!PhotonNetwork.InRoom) return;

        // Cargar los personajes disponibles en Resources/Characters/
        GameObject[] characterPrefabs = Resources.LoadAll<GameObject>("Characters");

        if (characterPrefabs == null || characterPrefabs.Length == 0)
        {
            Debug.LogError("⚠ No se encontraron personajes en Resources/Characters/");
            return;
        }

        // Elegir un personaje aleatorio
        GameObject selectedCharacter = characterPrefabs[Random.Range(0, characterPrefabs.Length)];

        // Obtener posición de spawn basada en el ActorNumber del jugador
        int playerIndex = (PhotonNetwork.LocalPlayer.ActorNumber - 1) % spawnPositions.Length;
        Vector3 spawnPoint = spawnPositions[playerIndex];

        // Instanciar el jugador en la red
        GameObject player = PhotonNetwork.Instantiate(selectedCharacter.name, spawnPoint, Quaternion.identity);

        // Configurar Cinemachine solo para el jugador local
        if (player.GetComponent<PhotonView>().IsMine)
        {
            AssignCinemachineCamera(player);
        }

        Debug.Log($"✅ {PhotonNetwork.LocalPlayer.NickName} spawneado como {selectedCharacter.name} en {spawnPoint}");
    }

    private void AssignCinemachineCamera(GameObject player)
    {
        CinemachineVirtualCamera cinemachineCam = GameObject.FindWithTag("CinemachineCam")?.GetComponent<CinemachineVirtualCamera>();

        if (cinemachineCam != null)
        {
            cinemachineCam.Follow = player.transform;
            cinemachineCam.LookAt = player.transform;
            Debug.Log("✅ Cámara de Cinemachine asignada correctamente.");
        }
        else
        {
            Debug.LogWarning("⚠ No se encontró la Cinemachine Camera en la escena. Asegúrate de que tenga el tag 'CinemachineCam'.");
        }
    }
}
