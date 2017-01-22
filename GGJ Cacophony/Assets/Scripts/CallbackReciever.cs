using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallbackReciever : MonoBehaviour {

    public static void EnableObjectStatic(Room room, string objectName)
    {
        WorldObject toActivate = room.FindWorldObject(objectName, /*mustbeactive*/false);
        toActivate.active = true;
    }

	public void EnableObject(string objectName)
    {
        EnableObjectStatic(PlayerState.instance.currentRoom, objectName);
    }

    public void AddToState(string stateName)
    {
        PlayerState state = PlayerState.instance;
        state.stateVariables.Add(stateName);
    }

    public void TeleportToCatArea()
    {
        PlayerState.instance.EnterRoom(RoomInstancer.instance.teleportRoom);
    }


}
