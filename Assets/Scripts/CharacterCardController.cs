using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterCardController : MonoBehaviour
{
    [SerializeField] private EPlayer player;

    [SerializeField] private Image card;
    [SerializeField] private Sprite[] cardSprites;

    private CharacterIcon[] icons;

    private float lastMoved;

    private int curSelection;

    [HideInInspector] public UnityEvent<EPlayer, bool, int> confirmationChange;


    private void Start()
    {
        curSelection = 0;
        card.sprite = cardSprites[curSelection];

        icons = GetComponentsInChildren<CharacterIcon>();
        icons[curSelection].Select();

        confirmationChange = new UnityEvent<EPlayer, bool, int>();
    }

    private void Update()
    {
        if (Input.GetButtonDown(player.Confirm()))
        {
            ConfirmSelection();
        }

        if (Input.GetAxis(player.Horizontal()) != 0)
        {
            MoveSelection();
        }

    }

    public void MoveSelection()
    {
        // check time
        if (Time.time - lastMoved < Utility.axisDelta) return;
        lastMoved = Time.time;

        // deselect and deconfirm current
        icons[curSelection].Deselect();
        Confirm(false);

        // increment current selection
        curSelection = (++curSelection) % Utility.numCharacterChoices;

        // change character card sprite
        card.sprite = cardSprites[curSelection];

        // select the next icon
        icons[curSelection].Select();
    }


    public void ConfirmSelection()
    {
        Confirm(true);
        // confirm icon
        icons[curSelection].Confirm();
    }

    private void Confirm(bool confirm)
    {
        confirmationChange.Invoke(player, confirm, curSelection);
    }

}
