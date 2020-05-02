using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

[System.Serializable]
public class ObjectMethodPair
{
    public GameObject parentObject;
    public string voiceMethod;
}


[System.Serializable]
public class VoiceCommandObject
{
    public ObjectMethodPair[] objectsToActivate;
    public string phraseToActivate;
}

public class VoiceControl : MonoBehaviour
{
    public VoiceCommandObject[] voiceCommands;

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, VoiceCommandObject> commands = new Dictionary<string, VoiceCommandObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < voiceCommands.Length; i++)
        {
            commands.Add(voiceCommands[i].phraseToActivate, voiceCommands[i]);
        }

        keywordRecognizer = new KeywordRecognizer(commands.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecogSpeech;
        keywordRecognizer.Start();
    }

    private void RecogSpeech(PhraseRecognizedEventArgs speech)
    {
        VoiceCommandObject commandObject = commands[speech.text];
       
        foreach (ObjectMethodPair obj in commandObject.objectsToActivate)
        {
            obj.parentObject.SendMessage(obj.voiceMethod);
        }
    }
}
