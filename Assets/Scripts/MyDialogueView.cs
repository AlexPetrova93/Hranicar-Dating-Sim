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

    private Action onDialogueLineFinished;

    private GameManager gameManager;

    private bool currentlyWriting;
    private bool wishesToInterrupt;

    public override void DialogueStarted()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (gameManager.PlayersInControl && Input.GetButtonDown(playerInControl.Confirm()))
        {
            gameManager.audioManager.PlayPress();

            if (currentlyWriting)
            {
                wishesToInterrupt = true;
            }
            else
            {
                onDialogueLineFinished?.Invoke();
            }
        }
    }

    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        Debug.Log("Running line");
        playerInControl = dialogueLine.CharacterName.ToEPlayer();

        this.WaitThenContinue(
            gameManager.PlayersInControl ? 0 : Utility.fadeTime,
            () =>
            {
                DialogueLineController dialogueBubble = Instantiate(dialogueLinePrefab);
                dialogueBubble.transform.SetParent(transform, false);
                dialogueBubble.Init(playerInControl, "");

                currentBubble = dialogueBubble;

                this.onDialogueLineFinished = onDialogueLineFinished;

                StartCoroutine(WriteText(dialogueBubble, dialogueLine.TextWithoutCharacterName.Text));
            }
        );
    }

    private IEnumerator WriteText(DialogueLineController dialogueBubble, string text)
    {
        currentlyWriting = true;
        gameManager.audioManager.PlayTyping();
        foreach (char letter in text)
        {
            if (wishesToInterrupt) break;

            dialogueBubble.AddLetter(letter);

            yield return new WaitForSeconds(Utility.typingTime);
        }

        dialogueBubble.SetText(text);

        currentlyWriting = false;
        wishesToInterrupt = false;
        gameManager.audioManager.StopTyping();
    }

    public override void InterruptLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        Debug.Log("Interupting line");
        StopAllCoroutines();
        gameManager.audioManager.StopTyping();

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
        if (currentBubble) currentBubble.Destroy();
    }
    
    public override void DialogueComplete()
    {
        if (currentBubble) currentBubble.Destroy();
    }

}
