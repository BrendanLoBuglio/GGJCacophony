using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAdventureParser : MonoBehaviour {

    [TextArea(1,30)]
    public string helpText;
    public Room gameStartRoom;
    bool gameStarted;

    private void Start()
    {
        TextLog.AddTextLineToTextLog("Press Enter to begin");
    }

	public void ParseMessage(string message)
    {
        if (!gameStarted)
        {
            StartGame();
            return;
        }
        TextLog.instance.AddTextLine(message);
        message = message.ToLower();
        string[] splitMessage = message.Split(' ');
        switch (splitMessage[0])
        {
            case "help":
                Help();
                break;
            case "digest":
                break;
            case "pulse":
                Pulse(splitMessage);
                break;
            // walking verbs
            case "go":
            case "walk":
            case "proceed":
            case "head":
            case "move":
                WalkInDirection(stringToDirection(splitMessage[1]));
                break;
            // shorthand for walking
            case "north":
            case "east":
            case "west":
            case "south":
                WalkInDirection(stringToDirection(splitMessage[0]));
                break;
            case "inspect":
            case "check":
                Inspect(splitMessage[1]);
                break;
            case "fuck":
                TextLog.instance.AddTextLine("No");
                break;
            case "":
                TextLog.AddTextLineToTextLog("");
                break;
            default:
                if (ValidateActionInput(splitMessage))
                {
                    ProcessActionOnNoun(splitMessage);
                }
                break;
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        PlayerState.instance.EnterRoom(gameStartRoom);
    }

    public bool CheckIfVerbExists(string[] splitMessage)
    {
        for(int k=0; k < splitMessage.Length; k++)
        {
            if (PlayerState.instance.allVerbs.Contains(splitMessage[k].ToLower()))
            {
                return true;
            }
        }
        return false;
    }

    bool ValidateActionInput(string[] splitMessage)
    {
        if (!CheckIfVerbExists(splitMessage))
        {
            TextLog.AddTextLineToTextLog("huh?");
            return false;
        }
        if(splitMessage.Length == 1)
        {
            TextLog.AddTextLineToTextLog("Can't just do that on nothing");
            return false;
        }
        return true;
    }

    public void ProcessActionOnNoun(string[] splitMessage)
    {
        string action = splitMessage[0];
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        for(int k=1; k < splitMessage.Length; k++)
        {
            builder.Append(splitMessage[k]);
        }
        string nounString = builder.ToString();
        Room currentRoom = PlayerState.instance.currentRoom;
        WorldObject  noun=currentRoom.FindWorldObject(nounString);
        if(noun == null)
        {
            TextLog.instance.AddTextLine("Can't do that to something that doesn't exist");
            return;
        }
        VerbNounAction objectAction = noun.FindValidAction(action);
        if(objectAction == null)
        {
            TextLog.instance.AddTextLine("Can't do that to the " + nounString);
            return;
        }
        ActionEvent currentActionEvent = objectAction.GetNextActionEvent();
        TextLog.AddTextLineToTextLog(currentActionEvent.actionDescription);
        currentActionEvent.actionCallback.Invoke();
    }

    public void Help()
    {
        TextLog.instance.AddTextLine(helpText);
    }

    public void Pulse(string[] splitMessage)
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        for(int k=1; k < splitMessage.Length; k++){
            builder.Append(splitMessage[k]);
        }
        string messageToSend = builder.ToString();
        MessageSender.instance.SendTextMessage(messageToSend);
    }

    public void Inspect(string objectName)
    {
        Room currentRoom = PlayerState.instance.currentRoom;
        WorldObject inspectedObject = currentRoom.FindWorldObject(objectName);
        if(inspectedObject == null)
        {
            TextLog.instance.AddTextLine("That object doesn't exist dummy");
            return;
        }
        TextLog.instance.AddTextLine(inspectedObject.inspectionDescription);
        
    }

    public void WalkInDirection(Direction? direction)
    {
        if(direction == null)
        {
            TextLog.instance.AddTextLine("You cannot walk in a direction that is not a direction");
            return;
        }
        PlayerState state = PlayerState.instance;
        Room currentRoom = state.currentRoom;
        Room destinationRoom = currentRoom.GetRoomInDirection(direction.Value);
        if(destinationRoom == null)
        {
            TextLog.instance.AddTextLine("You cannot walk " + direction.Value.ToString() + " there's shit in the way");
            return;
        }
        TextLog.instance.AddTextLine("You walk " + direction.ToString());
        state.EnterRoom(destinationRoom);
    }

    public Direction? stringToDirection(string inString)
    {
        switch (inString)
        {
            case "north":
                return Direction.north;
            case "east":
                return Direction.east;
            case "west":
                return Direction.west;
            case "south":
                return Direction.south;
        }
        return null;
    }

}
