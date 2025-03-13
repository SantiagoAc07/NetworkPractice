using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class Rooms : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField joinInputfield, createInputfield;
    [SerializeField] private string roomName;

    private void Start()
    {
        joinInputfield.onValueChanged.AddListener(text => { roomName = text; });
        createInputfield.onValueChanged.AddListener(text => { roomName = text; });
    }

    [ContextMenu("Create")]
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2; // Máximo 2 jugadores en la sala

        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    [ContextMenu("Join")]
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    [ContextMenu("Leave Room")]
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log($"Has creado la sala: {roomName}");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log($"Te has unido a la sala {PhotonNetwork.CurrentRoom.Name}");
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.LogError($"Error al crear la sala: {message}");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.LogError($"Error al unirse a la sala: {message}");
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log("Has salido de la sala.");
    }
}