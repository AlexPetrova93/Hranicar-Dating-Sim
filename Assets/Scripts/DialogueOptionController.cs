using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueOptionController : DialogueBubbleController
{
    [SerializeField] private MyDialogueOption dialogueOptionPrefab;

    private List<MyDialogueOption> myDialogueOptions;

    private float lastMoved;
    private int curSelection;

    private bool optionsActive;

    private void Start()
    {
        lastMoved = 0;
        curSelection = 0;
        optionsActive = false;
    }

    public void Init(DialogueOption[] dialogueOptions, EPlayer player)
    {
        Init(player);
        foreach (var option in dialogueOptions)
        {
            MyDialogueOption myOption = Instantiate(dialogueOptionPrefab);
            myOption.SetText(option.Line.Text.Text);
            myDialogueOptions.Add(myOption);
        }
        optionsActive = true;
    }

    void Update()
    {
        if (!optionsActive) return;

        if (Input.GetButtonDown(player.Confirm()))
        {
            //ConfirmSelection();
        }

        if (Input.GetAxis(player.Horizontal()) < 0)
        {
            MoveSelection(-1);
        }
        else if (Input.GetAxis(player.Horizontal()) > 0)
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
}
