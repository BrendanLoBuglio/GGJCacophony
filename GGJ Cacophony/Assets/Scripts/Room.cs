using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World {
    Room[] rooms;
}




[CreateAssetMenu(fileName = "Room", menuName = "GGJ/Create Room", order = 1)]
public class Room : ScriptableObject{
    [TextArea(1,30)]
    public string description;
    public WorldObject[] objects;
    public RoomConnection[] connections;

    public Room GetRoomInDirection(Direction dir)
    {
        for(int k=0; k < connections.Length; k++)
        {
            if(connections[k].direction == dir)
            {
                return connections[k].destinationRoom;
            }
        }
        return null;
    }

    public WorldObject FindWorldObject (string name, bool mustBeActive = true)
    {
        for(int k=0; k < objects.Length; k++)
        {
            if (mustBeActive && objects[k].active == false) continue;
            if(objects[k].name.ToLower() == name.ToLower())
            {
                return objects[k];
            }
        }
        return null;
    }
}

[System.Serializable]
public class RoomConnection
{
    public Direction direction;
    public Room destinationRoom;
    public bool active = true;
    public string inactiveExplanation;
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

    public VerbNounAction FindValidAction(string verb)
    {
        for (int k = 0; k < validActions.Length; k++)
        {
            string[] possibleVerbs = validActions[k].actionName.Split(';');
            for (int c = 0; c < possibleVerbs.Length; c++)
            {
                if(verb.ToLower() == possibleVerbs[k].ToLower())
                {
                    return validActions[k];
                }
            }
        }
        return null;
    }

}

[System.Serializable]
public class VerbNounAction
{
    public string actionName;
    public ActionEvent[] events = new ActionEvent[1];
    private int actionEventIndex = 0;

    public ActionEvent GetNextActionEvent()
    {
        actionEventIndex = Mathf.Min(actionEventIndex + 1, events.Length);
        return events[actionEventIndex];
    }
}

[System.Serializable]
public class ActionEvent
{
    [TextArea(1, 5)]
    public string actionDescription;
    public UnityEngine.Events.UnityEvent actionCallback = new UnityEngine.Events.UnityEvent();
}