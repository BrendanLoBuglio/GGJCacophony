using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLog : MonoBehaviour {

    public GameObject textLinePrefab;
    private RectTransform mRectTransform;
    private Text lastGennedTextLine;
    public float waitTimeBetweenLines = .5f;
    private Color col;
    Coroutine printCoroutine;

    Queue<string> printQueue = new Queue<string>();

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

    private void Start()
    {
        mRectTransform = this.GetComponent<RectTransform>();
    }

    public static void AddTextLineToTextLog(string line, bool addSpace = true)
    {
        instance.AddTextLine(line, addSpace);
    }

    public static void AddWhiteSpace()
    {
        instance.AddTextLine("", false);
    }

	public void AddTextLine(string line, bool addSpace = true)
    {
        // print a line for space
        if (addSpace)
        {
            printQueue.Enqueue("");    
        }
        printQueue.Enqueue(line);
        if (printCoroutine == null)
        {
            printCoroutine = StartCoroutine(RecursivelyPrintLines());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && lastGennedTextLine != null) {
            getLineWidthOfTextField(lastGennedTextLine);
        }
    }


    private string PrintLine(string line)
    {
        lastGennedTextLine = ((GameObject)Instantiate(textLinePrefab, Vector2.zero, Quaternion.identity)).GetComponent<Text>();
        lastGennedTextLine.text = line;
        Color originalCol = lastGennedTextLine.color;
        lastGennedTextLine.transform.SetParent(transform);
        lastGennedTextLine.transform.SetSiblingIndex(lastGennedTextLine.transform.GetSiblingIndex() - 1);
        lastGennedTextLine.rectTransform.sizeDelta = mRectTransform.sizeDelta;
        lastGennedTextLine.transform.localScale = textLinePrefab.transform.localScale;

        lastGennedTextLine.color = new Color(originalCol.r, originalCol.g, originalCol.b, 0);
        Canvas.ForceUpdateCanvases();
        lastGennedTextLine.color = new Color(originalCol.r, originalCol.g, originalCol.b, 1);
        if (lastGennedTextLine.cachedTextGenerator.lineCount > 1)
        {
            var uiLines = lastGennedTextLine.cachedTextGenerator.lines;
            string currentLine = line.Substring(0, uiLines[1].startCharIdx);
            lastGennedTextLine.text = currentLine;
            StartCoroutine(addWhiteSpace(lastGennedTextLine));
            return line.Substring(uiLines[1].startCharIdx);
        }
        else {
            StartCoroutine(addWhiteSpace(lastGennedTextLine));
            return "";
        }
    }

    private int getLineWidthOfTextField(Text textField)
    {
        string testString = "";
        for (int i = 0; i < 1000; i++) {
            testString += "@";
        }
        return getLineWidthOfTextField(textField, testString);
    }

    private int getLineWidthOfTextField(Text textField, string testString)
    {
        Text testTextObj = ((GameObject)Instantiate(textLinePrefab, Vector2.zero, Quaternion.identity)).GetComponent<Text>();
        testTextObj.text = testString;
        testTextObj.rectTransform.sizeDelta = mRectTransform.sizeDelta;
        testTextObj.color = Color.clear;
        testTextObj.transform.SetParent(this.transform);
        testTextObj.transform.SetSiblingIndex(0);
        Canvas.ForceUpdateCanvases();

        if(testTextObj.cachedTextGenerator.lines.Count <= 1) {
            //Only one liner:
            return testString.Length;
        }
        int width = testTextObj.cachedTextGenerator.lines[1].startCharIdx;
        Debug.Log("Returning width " + width);

        Destroy(testTextObj.gameObject);
        return width;
    }

    IEnumerator addWhiteSpace(Text text) {
        yield return null;
        text.text += " ";
    }

    IEnumerator RecursivelyPrintLines()
    {
        if (EntryField.inputField.interactable) {
            EntryField.inputField.interactable = false;
        }
        string message = printQueue.Dequeue();
        message = PrintLine(message);
        while(message.Length > 0 || printQueue.Count != 0)
        {
            yield return new WaitForSeconds(waitTimeBetweenLines);
            if(message.Length == 0)
            {
                message = printQueue.Dequeue();
            }
            message = PrintLine(message);
        }
        printCoroutine = null;
        EntryField.inputField.interactable = true;
    }

}
