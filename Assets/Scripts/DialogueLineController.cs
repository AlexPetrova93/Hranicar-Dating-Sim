using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLineController : DialogueBubbleController
{
    private TMPro.TextMeshProUGUI bubbleText;

    public void Init(EPlayer player, string text)
    {
        Init(player);

        bubbleText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        bubbleText.text = text;
    }

    public void SetText(string text)
    {
        bubbleText.text = text;
    }

    public void AddLetter(char letter)
    {
        bubbleText.text += letter;
    }
}
