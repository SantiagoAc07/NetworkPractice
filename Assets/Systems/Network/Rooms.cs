using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class Rooms : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField joinInputfield, createInputfield;
    private string joinRoomName = "";
    private string createRoomName = "";

    private void Start()
    {
        joinInputfield.onValueChanged.AddListener(text => joinRoomName = text);
        createInputfield.onValueChanged.AddListener(text => createRoomName = text);
    }

    [ContextMenu("Create")]
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(createRoomName))
        {
            Debug.LogError("⚠ No puedes crear una sala sin nombre.");
            return;
        }

        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = 4, // Incrementado para pruebas
            EmptyRoomTtl = 5000
        };

        PhotonNetwork.CreateRoom(createRoomName, roomOptions);
    }

    [ContextMenu("Join")]
    public void JoinRoom()
    {
        if (string.IsNullOrEmpty(joinRoomName))
        {
            Debug.LogWarning("⚠ Nombre de la sala vacío. Intentando unirse a una aleatoria...");
            PhotonNetwork.JoinRandomRoom();
            return;
        }

        PhotonNetwork.JoinOrCreateRoom(joinRoomName, new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default);
    }

    [ContextMenu("Leave Room")]
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($" Sala creada: {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($" Te has unido a la sala {PhotonNetwork.CurrentRoom.Name}");
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError($" Error al crear la sala: {message}");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError($" No se pudo unir a la sala: {message}");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogError($" No hay salas disponibles. {message}");
    }

    public override void OnLeftRoom()
    {
        Debug.Log(" Has salido de la sala.");
    }
}
