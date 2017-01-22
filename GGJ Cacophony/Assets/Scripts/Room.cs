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
    public bool realRoom = true;

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

    public bool CanGoInDirection(Direction dir)
    {
        RoomConnection connection = GetConnectionInDirection(dir);
        PlayerState state = PlayerState.instance;
        if (connection.requirements != null)
        {
            for (int k = 0; k < connection.requirements.Length; k++)
            {
                if (!state.stateVariables.Contains(connection.requirements[k]))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public RoomConnection GetConnectionInDirection(Direction dir)
    {
        for (int k = 0; k < connections.Length; k++)
        {
            if (connections[k].direction == dir)
            {
                return connections[k];
            }
        }
        return null;
    }

    public WorldObject FindWorldObject (string name, bool mustBeActive = true)
    {
        for(int k=0; k < objects.Length; k++)
        {
            if (mustBeActive != objects[k].active) continue;
            string[] aliases = objects[k].name.Split(';');
            foreach (string alias in aliases)
            {
                if (alias.ToLower().Replace(" ", "") == name.ToLower().Replace(" ", ""))
                {
                    return objects[k];
                }
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
    public string[] requirements;
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
    public bool hidden = false;
    public bool active = true;

    public VerbNounAction FindValidAction(string verb)
    {
        for (int k = 0; k < validActions.Length; k++)
        {
            string[] possibleVerbs = validActions[k].actionName.Split(';');
            for (int c = 0; c < possibleVerbs.Length; c++)
            {
                if(verb.ToLower() == possibleVerbs[c].ToLower())
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
    private int actionEventIndex = -1;

    public ActionEvent GetNextActionEvent()
    {
        actionEventIndex = Mathf.Min(actionEventIndex + 1, events.Length-1);
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