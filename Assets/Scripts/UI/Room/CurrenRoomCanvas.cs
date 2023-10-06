using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrenRoomCanvas : MonoBehaviour
{
    [SerializeField] private PlayerListeningMenu _playerListeningMenu;
    [SerializeField] private LeaveRoomMenu _leaveRoomMenu;
    public LeaveRoomMenu LeaveRoomMenu
    {
        get { return _leaveRoomMenu; }
    }
    
    private RoomsCanvas _roomsCanvas;
    
    public void FirstInitialize(RoomsCanvas canvas)
    {
        _roomsCanvas = canvas;
        _playerListeningMenu.FirstInitialize(canvas);
        _leaveRoomMenu.FirstInitialize(canvas);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
