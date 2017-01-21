using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLog : MonoBehaviour {

    public GameObject textLinePrefab;
    public float waitTimeBetweenLines = .5f;
    private Color col;

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

    public static void AddTextLineToTextLog(string line)
    {
        instance.AddTextLine(line);
    }

	public void AddTextLine(string line)
    {
        // print a line for space
        PrintLine("");
        StartCoroutine(RecursivelyPrintLines(line));
        
    }

    private string PrintLine(string line)
    {
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
            StartCoroutine(addWhiteSpace(text));
            return line.Substring(uiLines[1].startCharIdx);
        }
        else {
            StartCoroutine(addWhiteSpace(text));
            return "";
        }
    }

    IEnumerator addWhiteSpace(Text text) {
        yield return null;
        text.text += " ";
    }

    IEnumerator RecursivelyPrintLines(string message)
    {
        message = PrintLine(message);
        while(message.Length > 0)
        {
            yield return new WaitForSeconds(waitTimeBetweenLines);
            message = PrintLine(message);
        }
    }

}
