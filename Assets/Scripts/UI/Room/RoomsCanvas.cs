using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsCanvas : MonoBehaviour
{
   [SerializeField] private CreateAndJoinRoomCanvas _createAndJoinRoomCanvas;
   public CreateAndJoinRoomCanvas CreateAndJoinRoomCanvas
   {
      get { return _createAndJoinRoomCanvas; }
   }

   [SerializeField] private CurrenRoomCanvas _currenRoomCanvas;
   public CurrenRoomCanvas CurrenRoomCanvas
   {
      get { return _currenRoomCanvas; }
   }

   private void Awake()
   {
      FirstInitialize();
   }

   private void FirstInitialize()
   {
      CreateAndJoinRoomCanvas.FirstInitialize(this);
      CurrenRoomCanvas.FirstInitialize(this);
   }
}
