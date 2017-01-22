using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLog : MonoBehaviour {

    public static bool GameOver;

    public AudioClip lineClip;

    public GameObject textLinePrefab;
    private RectTransform mRectTransform;
    private Text lastGennedTextLine;
    public float waitTimeBetweenLines = .5f;
    private Color col;
    Coroutine printCoroutine;

    Queue<string> printQueue = new Queue<string>();

    #region Setup:
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

    private void Awake()
    {
        mRectTransform = this.GetComponent<RectTransform>();
    }
    #endregion

    public static void AddTextLineToTextLog(string line, bool addSpace = true)
    {
        instance.AddTextLine(line, addSpace);
    }

    public static void AddWhiteSpace()
    {
        instance.AddTextLine("", false);
    }

    public void AddTextLine(string line)
    {
        string[] lines = line.Split('\n');
        foreach (string l in lines)
        {
            AddTextLine(l, false);
        }
    }

	public void AddTextLine(string line, bool addSpace = true)
    {
        string[] lines = line.Split('\n');
        foreach (string l in lines)
        {
            printQueue.Enqueue(l);
        }
        // print a line for space
        if (addSpace)
        {
            printQueue.Enqueue("");
        }
        if (printCoroutine == null)
        {
            printCoroutine = StartCoroutine(RecursivelyPrintLines());
        }
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
            if(message.Length == 0) {
                message = printQueue.Dequeue();
            }
            message = PrintLine(message);
        }
        printCoroutine = null;
        EntryField.inputField.interactable = true;
    }

    private string PrintLine(string textIn)
    {
        AudioSource.PlayClipAtPoint(lineClip, Camera.main.transform.position);

        int lineWidth = getLineWidthOfTextField(textIn);

        lastGennedTextLine = ((GameObject)Instantiate(textLinePrefab, Vector2.zero, Quaternion.identity)).GetComponent<Text>();
        lastGennedTextLine.text = textIn.Substring(0, lineWidth);
        lastGennedTextLine.transform.SetParent(transform);
        lastGennedTextLine.transform.SetSiblingIndex(lastGennedTextLine.transform.GetSiblingIndex() - 1);
        lastGennedTextLine.transform.localPosition = new Vector3(lastGennedTextLine.transform.localPosition.x, lastGennedTextLine.transform.localPosition.y, 0);
        lastGennedTextLine.rectTransform.sizeDelta = mRectTransform.sizeDelta;
        lastGennedTextLine.transform.localScale = textLinePrefab.transform.localScale;

        if(textIn.Length > lineWidth) {
            return textIn.Substring(lineWidth);
        }
        else {
            return "";
        }
    }

    public int GetLineWidthOfTextField()
    {
        string testString = "";
        for (int i = 0; i < 1000; i++) {
            testString += "@";
        }
        return getLineWidthOfTextField(testString);
    }

    private int getLineWidthOfTextField(string testString)
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

        Destroy(testTextObj.gameObject);
        return width;
    }
}
