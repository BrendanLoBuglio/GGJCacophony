using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAdventureParser : MonoBehaviour {

    [TextArea(1,30)]
    public string helpText;
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
        if (message.Replace(" ", "") != "")
        {
            TextLog.instance.AddTextLine(message, false);
        }
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
                break;
            default:
                if (ValidateActionInput(splitMessage))
                {
                    ProcessActionOnNoun(splitMessage);
                }
                break;
        }
    }

    public void Digest()
    {
        PlayerState state = PlayerState.instance;
        bool ateLightbulb = false;
        bool ateBatter = false;
        if (state.stateVariables.Contains("AteLightbulb"))
        {
            ateLightbulb = true;
        }
        if (state.stateVariables.Contains("AteBatter"))
        {
            ateBatter = true;
        }
        if (ateLightbulb && !ateBatter)
        {
            TextLog.AddTextLineToTextLog("The lightbulb rests comfortably in your gut");
        }
        else if (!ateLightbulb && ateBatter)
        {
            TextLog.AddTextLineToTextLog("The batter weighs your belly down");
        }
        else if (ateLightbulb && ateBatter)
        {
            TextLog.AddTextLineToTextLog("Soon your stomach has done its work, and you pass a steaming pile of broken glass, from which the batter juts like a flag. The one-eyed Krobi sees it, just as you slip beyond sight; it caws with glee when it discovers your leavings, for it knows that where there is a cake-mixer, cake must follow. You need only wait before it chokes upon the glass. Everything has gone according to plan.");
            CallbackReciever.EnableObjectStatic(PlayerState.instance.currentRoom, "crows");
        }
        else if (!ateLightbulb && !ateBatter)
        {
            TextLog.AddTextLineToTextLog("Your empty belly rumbles");
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        PlayerState.instance.EnterRoom(RoomInstancer.instance.startRoom);
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
        bool beginning = true;
        for (int k=1; k < splitMessage.Length; k++)
        {
            if (splitMessage[k] != "the")
            {
                if (beginning)
                    beginning = false;
                builder.Append(" ");
                builder.Append(splitMessage[k]);
            }
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
