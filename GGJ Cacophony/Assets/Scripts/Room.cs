using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World {
    Room[] rooms;
}




[CreateAssetMenu(fileName = "Room", menuName = "GGJ/Create Room", order = 1)]
public class Room : ScriptableObject{
    public string description;
    public WorldObject[] objects;
    public RoomConnection[] connections;
}

[System.Serializable]
public class RoomConnection
{
    public Direction direction;
    public Room destinationRoom;
    public bool active = true;
}

public enum Direction
{
    north, south, east, west
}

[System.Serializable]
public class WorldObject
{
    public string name;
    public string inspectionDescription;
    public VerbNounAction[] validActions;
    public bool active = true;
}

[System.Serializable]
public class VerbNounAction
{
    public string actionName;
    public ActionEvent[] events = new ActionEvent[1];
}

[System.Serializable]
public class ActionEvent
{
    [TextArea(1, 5)]
    public string actionDescription;
    public string actionFunctionCall;
}