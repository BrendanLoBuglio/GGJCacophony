using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MorseDisplayConsole : MonoBehaviour
{
    private Text mText;

    private void Start ()
    {
        mText = GetComponent<Text>();
        drawBorders();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            drawBorders();
        }
    }

    private void drawBorders ()
    {
        int textAreaWidth = getTextAreaWidth();
        //mText.text = "";
        for (int i = 0; i < textAreaWidth; i++) {
            mText.text += '@';
        }
	}

    private int getTextAreaWidth()
    {
        CharacterInfo charInfo;
        mText.text = "AAAAAAAAA";
        Canvas.ForceUpdateCanvases();
        Debug.Log(mText.font.fontNames[0]);
        bool response = mText.font.GetCharacterInfo('A', out charInfo);

        Debug.Log("Response is " + response + ", Advance is " + charInfo.advance);
        return charInfo.advance;
    }
}
