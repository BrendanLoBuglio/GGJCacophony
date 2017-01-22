using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WikiPlayerManager : MonoBehaviour
{
    private InputField mInputField;
    private EntryField mEntryField;
    [SerializeField] private MorseDisplayConsole mMorseDisplayConsole;
    [SerializeField] private TextLog mTextLog;

    public void Start()
    {
        mInputField = GetComponent<InputField>();
        mEntryField = GetComponent<EntryField>();
    }

    public void OnEndTextEdit(string message)
    {
        if (Input.GetKey(KeyCode.Return) && !string.IsNullOrEmpty(mInputField.text)) {
            mEntryField.SendTextMessage();
            mEntryField.Reset();
            mTextLog.AddTextLine(message);
            mMorseDisplayConsole.Show();
        }
    }
}
