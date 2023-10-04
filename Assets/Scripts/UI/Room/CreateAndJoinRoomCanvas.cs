using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAndJoinRoomCanvas : MonoBehaviour
{
    [SerializeField]
    private CreateRoom _createRoom;
    private RoomsCanvas _roomsCanvas;
    
    public void FirstInitialize(RoomsCanvas canvas)
    {
        _roomsCanvas = canvas;
        _createRoom.FirstInitialize(canvas);
    }
}
