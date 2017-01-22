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
        if (!Input.GetKey(KeyCode.Return))
        {
            return;
        }
        if (!gameStarted)
        {
            StartGame();
            return;
        }
        if (TextLog.GameOver)
        {
            Application.Quit();
            return;
        }
        if (message == "")
        {
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
                Digest();
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
            case "n":
            case "e":
            case "w":
            case "s":
                WalkInDirection(stringToDirection(splitMessage[0]));
                break;
            case "inspect":
            case "check":
                if (splitMessage.Length > 1)
                {
                    Inspect(splitMessage[1]);
                }
                else
                {
                    Look();
                }
                break;
            case "look":
            case "repeat":
                Look();
                break;
            case "do":
                Do();
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

    public void Look()
    {
        PlayerState.instance.EnterRoom(PlayerState.instance.currentRoom);
    }

    public void Do()
    {
        Room currentRoom = PlayerState.instance.currentRoom;
        List<string> verbs = new List<string>();
        foreach(WorldObject o in currentRoom.objects)
        {
            foreach(VerbNounAction verb in o.validActions)
            {
                string firstAlias = verb.actionName.Split(';')[0];
                if (!verbs.Contains(firstAlias))
                {
                    verbs.Add(firstAlias);
                }
            }
        }
        if (verbs.Count > 0)
        {
            TextLog.AddTextLineToTextLog("You can", false);
            for (int k = 0; k < verbs.Count; k++)
            {
                TextLog.AddTextLineToTextLog(verbs[k], false);
            }
            TextLog.AddWhiteSpace();
        }

        else
        {
            TextLog.AddTextLineToTextLog("You can't do much here");
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
            state.stateVariables.Remove("AteLightbulb");
            state.stateVariables.Remove("AteBatter");
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
        TextLog.AddTextLineToTextLog(helpText);
    }

    public void Pulse(string[] splitMessage)
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        bool first = true;
        for(int k=1; k < splitMessage.Length; k++){
            if (first)
            {
                first = false;
            }
            else
            {
                builder.Append(" ");
            }
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
        if (!currentRoom.CanGoInDirection(direction.Value))
        {
            TextLog.instance.AddTextLine(currentRoom.GetConnectionInDirection(direction.Value).inactiveExplanation);
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
            case "n":
                return Direction.north;
            case "east":
            case "e":
                return Direction.east;
            case "west":
            case "w":
                return Direction.west;
            case "south":
            case "s":
                return Direction.south;
        }
        return null;
    }

}
