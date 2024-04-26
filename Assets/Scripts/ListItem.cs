using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class ListItem : MonoBehaviour
{
    [SerializeField] TMP_Text _name;
    [SerializeField] TMP_Text _playersCount;

    public void SetParameters(RoomInfo roomInfo)
    {
        _name.text = roomInfo.Name;
        _playersCount.text = $"{roomInfo.PlayerCount}/{roomInfo.MaxPlayers}";
    }
}
