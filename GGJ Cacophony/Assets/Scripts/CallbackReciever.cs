﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallbackReciever : MonoBehaviour {

    public static void EnableObjectStatic(Room room, string objectName)
    {
        WorldObject toActivate = room.FindWorldObject(objectName, /*mustbeactive*/false);
        toActivate.active = true;
    }

	public void EnableObject(Room room, string objectName)
    {
        EnableObjectStatic(room, objectName);
    }

    public void AddToState(string stateName)
    {
        PlayerState state = PlayerState.instance;
        state.stateVariables.Add(stateName);
    }


}
