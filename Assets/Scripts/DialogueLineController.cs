using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLineController : DialogueBubbleController
{
    private TMPro.TextMeshProUGUI bubbleText;

    void Start()
    {
        bubbleText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    public void Init(EPlayer player, string text)
    {
        Init(player);
        bubbleText.text = text;
    }
}
