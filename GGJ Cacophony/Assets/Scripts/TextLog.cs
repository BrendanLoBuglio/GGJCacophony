using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLog : MonoBehaviour {

    public GameObject textLinePrefab;
    public float waitTimeBetweenLines = .5f;
    private Color col;
    Coroutine printingLinesCoroutine;
    Queue<string> textQueue = new Queue<string>();

    public static TextLog _instance;
    public static TextLog instance {
        get{
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<TextLog>();
            }
            return _instance;
        }
    }

    public static void AddTextLineToTextLog(string line, bool addSpace = true)
    {
        instance.AddTextLine(line, addSpace);
    }

	public void AddTextLine(string line, bool addSpace = true)
    {
        // print a line for space
        if (addSpace)
        {
            textQueue.Enqueue("Empty");
        }
        textQueue.Enqueue(line);
        if(printingLinesCoroutine == null)
        {
            printingLinesCoroutine = StartCoroutine(RecursivelyPrintLines());
        }
        
    }

    private string PrintLine(string line)
    {
        string toReturn = "";
        if (line == "Empty") { line = ""; toReturn = "Empty"; }
        GameObject  spawnedLine = (GameObject)Instantiate(textLinePrefab, Vector2.zero, Quaternion.identity);
        Text text = spawnedLine.GetComponent<Text>();
        text.text = line;
        Color originalCol = text.color;
        spawnedLine.transform.SetParent(transform);
        spawnedLine.transform.SetSiblingIndex(spawnedLine.transform.GetSiblingIndex() - 1);

        text.color = new Color(originalCol.r, originalCol.g, originalCol.b, 0);
        Canvas.ForceUpdateCanvases();
        text.color = new Color(originalCol.r, originalCol.g, originalCol.b, 1);
        if (text.cachedTextGenerator.lineCount > 1)
        {
            var uiLines = text.cachedTextGenerator.lines;
            string currentLine = line.Substring(0, uiLines[1].startCharIdx);
            text.text = currentLine;
            Canvas.ForceUpdateCanvases();
            StartCoroutine(addWhiteSpace(text));
            return line.Substring(uiLines[1].startCharIdx);
        }
        else {
            StartCoroutine(addWhiteSpace(text));
            return toReturn;
        }
    }

    IEnumerator addWhiteSpace(Text text) {
        yield return null;
        text.text += " ";
        Canvas.ForceUpdateCanvases();
    }

    IEnumerator RecursivelyPrintLines()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        EntryField.inputField.interactable = false;
        EntryField.inputField.DeactivateInputField();
        string message = textQueue.Dequeue();
        message = PrintLine(message);
        while(message.Length > 0 || textQueue.Count != 0)
        {
            if (message != "Empty")
            {
                yield return new WaitForSeconds(waitTimeBetweenLines);
            }
            if(message == "Empty" || message.Length == 0)
            {
                message = textQueue.Dequeue();
            }
            message = PrintLine(message);
        }
        printingLinesCoroutine = null;
        EntryField.inputField.ActivateInputField();
        EntryField.inputField.interactable = true;
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(EntryField.inputField.gameObject);
    }

}
