using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class DialogueOptionController : DialogueBubbleController
{
    [SerializeField] private MyDialogueOption dialogueOptionPrefab;

    [SerializeField] private RectTransform optionsParent;

    private List<MyDialogueOption> myDialogueOptions;

    private float lastMoved;
    private int curSelection;

    private Action<int> onOptionSelected;
    private Action onSelectionComplete;

    private bool optionsActive;

    private GameManager gameManager;

    public void Init(EPlayer player, DialogueOption[] dialogueOptions, Action<int> onOptionSelected, Action onSelectionComplete)
    {
        Init(player);

        myDialogueOptions = new List<MyDialogueOption>();
        foreach (var option in dialogueOptions)
        {
            MyDialogueOption myOption = Instantiate(dialogueOptionPrefab);
            myOption.transform.SetParent(optionsParent, false);
            myOption.Init(option.DialogueOptionID, option.Line.TextWithoutCharacterName.Text);
            myDialogueOptions.Add(myOption);
        }

        this.onOptionSelected = onOptionSelected;
        this.onSelectionComplete = onSelectionComplete;

        lastMoved = 0;
        curSelection = 0;

        myDialogueOptions[curSelection].Select();

        optionsActive = true;

        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (!optionsActive || !gameManager.PlayersInControl) return;

        if (Input.GetButtonDown(player.Confirm()))
        {
            ConfirmSelection();
        }

        if (Input.GetAxis(player.Vertical()) < 0)
        {
            MoveSelection(-1);
        }
        else if (Input.GetAxis(player.Vertical()) > 0)
        {
            MoveSelection(1);
        }
    }

    private void MoveSelection(int increment)
    {
        // check time
        if (Time.time - lastMoved < Utility.axisDelta) return;
        lastMoved = Time.time;

        // deselect current
        myDialogueOptions[curSelection].Deselect();

        // increment current selection
        curSelection = curSelection.MoveIndex(increment, myDialogueOptions.Count);

        // select the next option
        myDialogueOptions[curSelection].Select();
    }

    private void ConfirmSelection()
    {
        myDialogueOptions[curSelection].Confirm();

        // remove the other options
        List<MyDialogueOption> optionsToRemove = new List<MyDialogueOption>();
        for (int i = 0; i < myDialogueOptions.Count; i++)
        {
            if (i != curSelection) optionsToRemove.Add(myDialogueOptions[i]);
        }
        foreach (MyDialogueOption option in optionsToRemove)
        {
            Destroy(option.gameObject);
        }

        // order matters !
        onSelectionComplete?.Invoke();
        onOptionSelected?.Invoke(myDialogueOptions[curSelection].GetOptionID());

        optionsActive = false;

        onOptionSelected = null;
    }
}
