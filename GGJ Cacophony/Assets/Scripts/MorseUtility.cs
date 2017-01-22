using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MorseUtility
{
    public const int spaceBetweenLetters    = 5;
    public const int spaceBetweenWords      = 11;
    public const int spaceBetweenMessages   = 30;

    public static string GenerateMorseSentence(string input)
    {
        string output = "";
        for (int i = 0; i < input.Length; i++) {
            output += GetMorseLetter(input[i]);
            if (i < input.Length - 1 && input[i+1] != ' ') {
                output += GetMorseLetterSeperator();
            }
        }
        return output;
    }

    public static string GetMorseLetter(char input)
    {
        //Guarantee lowercase:
        input = ("" + input).ToLower()[0];


        if(input == ' ') {
            return GetMorseSpace();
        }

        switch (input) {
            case 'a': return ".-";
            case 'b': return "-...";
            case 'c': return "-.-.";
            case 'd': return "-..";
            case 'e': return ".";
            case 'f': return "..-.";
            case 'g': return "--.";
            case 'h': return "....";
            case 'i': return "..";
            case 'j': return ".---";
            case 'k': return "-.-";
            case 'l': return ".-..";
            case 'm': return "--";
            case 'n': return "-.";
            case 'o': return "---";
            case 'p': return ".--.";
            case 'q': return "--.-";
            case 'r': return ".-.";
            case 's': return "...";
            case 't': return "-";
            case 'u': return "..-";
            case 'v': return "...-";
            case 'w': return ".--";
            case 'x': return "-..-";
            case 'y': return "-.--";
            case 'z': return "--..";
            case '1': return ".----";
            case '2': return "..---";
            case '3': return "...--";
            case '4': return "....-";
            case '5': return ".....";
            case '6': return "-....";
            case '7': return "--...";
            case '8': return "---..";
            case '9': return "----.";
            case '0': return "-----";
            default: return "";
        }
    }

    public static string GetMorseLetterSeperator()
    {
        string output = "";
        for (int i = 0; i < spaceBetweenLetters; i++) {
            output += ' ';
        }
        return output;
    }

    public static string GetMorseSpace()
    {
        string output = "";
        for (int i = 0; i < spaceBetweenWords; i++) {
            output += ' ';
        }
        return output;
    }

    public static string[][] GetMorseWordLetters(string morseStringIn)
    {
        string[][] output;
        string[] words = morseStringIn.Split(new string[] { MorseUtility.GetMorseSpace() }, System.StringSplitOptions.RemoveEmptyEntries);
        output = new string[words.Length][];

        for (int i = 0; i < output.Length; i++) {
            output[i] = words[i].Split(new string[] { MorseUtility.GetMorseLetterSeperator() }, System.StringSplitOptions.RemoveEmptyEntries);
        }

        return output;
    }
}
