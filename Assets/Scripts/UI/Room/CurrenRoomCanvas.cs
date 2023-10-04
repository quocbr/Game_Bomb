using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrenRoomCanvas : MonoBehaviour
{
    private RoomsCanvas _roomsCanvas;
    
    public void FirstInitialize(RoomsCanvas canvas)
    {
        _roomsCanvas = canvas;
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
