using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInstancer : MonoBehaviour {

    public static RoomInstancer _instance;
    public static RoomInstancer instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<RoomInstancer>();
            }
            return _instance;
        }
    }
    public Room startRoom;
    public Room teleportRoom;

	// Use this for initialization
	void Start () {
        List<string> spawnedRooms = new List<string>();
        startRoom = RecursivelySpawnRoom(startRoom, ref spawnedRooms);
        teleportRoom = RecursivelySpawnRoom(teleportRoom, ref spawnedRooms);
	}
	
    public Room RecursivelySpawnRoom(Room firstRoom, ref List<string> spawnedRooms)
    {
        if (!firstRoom.realRoom)
            return null;
        Room toReturn = ScriptableObject.Instantiate(firstRoom);
        spawnedRooms.Add(firstRoom.name);
        for(int k=0; k < toReturn.connections.Length; k++)
        {
            if (!spawnedRooms.Contains(toReturn.connections[k].destinationRoom.name.Replace("(Clone)","")))
            {
                Room connection = RecursivelySpawnRoom(toReturn.connections[k].destinationRoom, ref spawnedRooms);
                if (connection != null)
                {
                    toReturn.connections[k].destinationRoom = connection;
                    RoomConnection con = connection.GetConnectionInDirection(ToInverseDirection(toReturn.connections[k].direction));
                    if (con != null)
                    {
                        con.destinationRoom = toReturn;
                    }
                }
            }
        }
        toReturn.name = toReturn.name.Replace("(Clone)", "");
        return toReturn;
    }

    public Direction ToInverseDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.east:
                return Direction.west;
            case Direction.north:
                return Direction.south;
            case Direction.south:
                return Direction.north;
            case Direction.west:
                return Direction.east;
        }
        return Direction.east;
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Equals))
        {
            Time.timeScale = 9;
        }
        else
        {
            Time.timeScale = 1;
        }
	}
}
