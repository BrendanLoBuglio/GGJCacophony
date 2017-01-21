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
    public HashSet<string> stateVariables = new HashSet<string>();
    public HashSet<string> allVerbs = new HashSet<string>();

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
                        if (!allVerbs.Contains(validVerbs[k]))
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
        TextLog.AddTextLineToTextLog(currentRoom.description, false);

    }

}
