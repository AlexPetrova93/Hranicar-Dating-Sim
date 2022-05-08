using System;
using UnityEngine;
using Yarn.Unity;

public class MyDialogueView : DialogueViewBase
{
    [SerializeField] private DialogueLineController dialogueLinePrefab;
    [SerializeField] private DialogueOptionController dialogueOptionsPrefab;

    private EPlayer playerInControl = EPlayer.Left;

    private void Update()
    {
        // call this on confirm ! - keep track of which player is currently in control !
        if (Input.GetButtonDown(playerInControl.Confirm()))
        {
            UserRequestedViewAdvancement();
        }
    }

    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        playerInControl = dialogueLine.CharacterName.ToEPlayer();

        DialogueLineController dialogueBubble = Instantiate(dialogueLinePrefab);
        dialogueBubble.Init(playerInControl, dialogueLine.Text.Text);
    }

    public override void InterruptLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        playerInControl = dialogueLine.CharacterName.ToEPlayer();

    }

    public override void DismissLine(Action onDismissalComplete)
    {
        // lower the opacity (+ let it get pushed up - this should happen automatically if you set up scroll rect correctly)
    }


    public override void RunOptions(DialogueOption[] dialogueOptions, Action<int> onOptionSelected)
    {
        playerInControl = dialogueOptions[0].Line.CharacterName.ToEPlayer();

    }


}
