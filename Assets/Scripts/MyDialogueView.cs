using System;
using System.Collections;
using UnityEngine;
using Yarn.Unity;

public class MyDialogueView : DialogueViewBase
{
    [SerializeField] private DialogueLineController dialogueLinePrefab;
    [SerializeField] private DialogueOptionController dialogueOptionsPrefab;

    private EPlayer playerInControl = EPlayer.Left;

    private DialogueBubbleController currentBubble;
    private DialogueBubbleController previousBubble;

    private Action onDialogueLineFinished;

    private void Update()
    {
        // call this on confirm ! - keep track of which player is currently in control !
        if (Input.GetButtonDown(playerInControl.Confirm()))
        {
            Debug.Log("Player " + playerInControl + " pressing confirm");
            onDialogueLineFinished?.Invoke();
        }
    }

    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        Debug.Log("Running line");
        playerInControl = dialogueLine.CharacterName.ToEPlayer();

        DialogueLineController dialogueBubble = Instantiate(dialogueLinePrefab);
        dialogueBubble.transform.SetParent(transform, false);
        dialogueBubble.Init(playerInControl, dialogueLine.TextWithoutCharacterName.Text);

        currentBubble = dialogueBubble;

        this.onDialogueLineFinished = onDialogueLineFinished;

        //StartCoroutine(RunLineInternal(dialogueLine));
    }

    private IEnumerator RunLineInternal(LocalizedLine dialogueLine)
    {
        yield return new WaitForSeconds(2f);

    }

    public override void InterruptLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        Debug.Log("Interupting line");
        StopAllCoroutines();

        (currentBubble as DialogueLineController).SetText(dialogueLine.TextWithoutCharacterName.Text);

        this.onDialogueLineFinished = onDialogueLineFinished;
    }

    public override void DismissLine(Action onDismissalComplete)
    {
        // lower the opacity (+ let it get pushed up - this should happen automatically if you set up scroll rect correctly)
        Debug.Log("Dismissing line");
        PushBubble();

        onDismissalComplete.Invoke();
    }


    public override void RunOptions(DialogueOption[] dialogueOptions, Action<int> onOptionSelected)
    {
        Debug.Log("Running options");
        playerInControl = dialogueOptions[0].Line.CharacterName.ToEPlayer();

        onDialogueLineFinished = null;

        DialogueOptionController dialogueBubble = Instantiate(dialogueOptionsPrefab);
        dialogueBubble.transform.SetParent(transform, false);
        dialogueBubble.Init(playerInControl, dialogueOptions, onOptionSelected, PushBubble);

        currentBubble = dialogueBubble;
    }

    private void PushBubble()
    {
        currentBubble.Destroy();
        if (previousBubble) previousBubble.Destroy();
        previousBubble = currentBubble;
    }
    
    public override void DialogueComplete()
    {
        if (currentBubble) currentBubble.Destroy();
        if (previousBubble) previousBubble.Destroy();
    }

}
