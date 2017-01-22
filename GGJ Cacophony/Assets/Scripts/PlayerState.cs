using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    public static PlayerState _instance;
    public static PlayerState instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PlayerState>();
            }
            return _instance;
        }
    }

    public Room[] allRooms;
    public Room currentRoom;
    public List<string> stateVariables = new List<string>();
    public List<string> allVerbs = new List<string>();

    void Start()
    {
        foreach(Room r in allRooms)
        {
            foreach(WorldObject o in r.objects)
            {
                foreach(VerbNounAction verb in o.validActions)
                {
                    string[] validVerbs = verb.actionName.Split(';');
                    for(int k=0; k < validVerbs.Length; k++)
                    {
                        if (!allVerbs.Contains(validVerbs[k].ToLower()))
                        {
                            allVerbs.Add(validVerbs[k].ToLower());
                        }
                    }
                }
            }
        }
    }

    public void EnterRoom(Room room)
    {
        currentRoom = room;
        TextLog.AddTextLineToTextLog(currentRoom.name + " - ");
        TextLog.AddTextLineToTextLog(currentRoom.description);
        for(int k=0; k < currentRoom.connections.Length; k++)
        {
            RoomConnection conn = currentRoom.connections[k];
            TextLog.AddTextLineToTextLog("To the " + conn.direction.ToString() + " is the " + conn.destinationRoom.name,false);

        }
        TextLog.AddWhiteSpace();
    }

}
