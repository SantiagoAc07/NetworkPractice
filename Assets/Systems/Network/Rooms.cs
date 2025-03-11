using System;
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
        joinInputfield.onValueChanged.AddListener(text => {roomName = text; });
        createInputfield.onValueChanged.AddListener(text => {roomName = text; });
    }

    [ContextMenu("Create")]
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(roomName);
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

        print($"Has Created the room! Name: {roomName}");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        print($"Has Connected To Room {PhotonNetwork.CurrentRoom.Name}!");
        
        PhotonNetwork.LoadLevel("Game");
        
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);

        print($"Room Creation Failed!\nCode: {returnCode} Error: {message}");
    }

    

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);

        print($"Room Connection Failed!\nCode: {returnCode} Error: {message}");
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();

        print("Has Left The Room!");
    }
}