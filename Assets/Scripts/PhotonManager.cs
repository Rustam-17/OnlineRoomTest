using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private string _regionCloud;
    [SerializeField] private TMP_InputField _playersCountMax;
    [SerializeField] private TMP_InputField _roomName;
    [SerializeField] private ListItem _roomPrefab;
    [SerializeField] private Transform _content;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion(_regionCloud);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PhotonNetwork.InLobby)
            {
                Debug.Log("�� ������ � �����");
            }
            else if (PhotonNetwork.InRoom)
            {
                Debug.Log($"�� ������ � ������� {PhotonNetwork.CurrentRoom.Name}");
            }
            else
            {
                Debug.Log("��, ��� �� ������");
            }
        }
    }

    public void CreateRoom()
    {
        if (PhotonNetwork.IsConnected == false)
        {
            Debug.Log("���������� ������� �������, ��� ����������� � �������");
            return;
        }

        RoomOptions roomOptions = new RoomOptions();
        int.TryParse(_playersCountMax.text, out int playersCountMax);
        roomOptions.MaxPlayers = playersCountMax;

        PhotonNetwork.CreateRoom(_roomName.text, roomOptions, TypedLobby.Default);
        Debug.Log("Room was created");
    }    

    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log($"�� ������������ � �������: {PhotonNetwork.CloudRegion}");

        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"�� ����������� �� �������");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"������� �������: {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log($"�� ������� ������� �������: {PhotonNetwork.CurrentRoom.Name}\n" +
                  $"���: {returnCode}. {message}");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"�� �������������� � �������: {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomsInfo)
    {
        Debug.Log("1");
        
        foreach (RoomInfo roomInfo in roomsInfo)
        {
            ListItem newRoom = Instantiate(_roomPrefab, _content);
            newRoom.SetParameters(roomInfo);
        }
    }
}
