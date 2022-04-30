using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCardController : MonoBehaviour
{
    [SerializeField] private EPlayer player;

    [SerializeField] private Image card;
    [SerializeField] private Sprite[] cardSprites;

    private CharacterIcon[] icons;

    private int curSelection;

    private float horizontal;
    private bool confirmed;

    private void Start()
    {
        curSelection = 0;
        card.sprite = cardSprites[curSelection];

        icons = GetComponentsInChildren<CharacterIcon>();
        icons[0].Select();
    }

    private void Update()
    {
        horizontal = Input.GetAxis(player.Horizontal());

        if (Input.GetButtonDown(player.Confirm()))
        {
            ConfirmSelection();
        }

        if (horizontal > 0)
        {

        }
        else if (horizontal < 0)
        {

        }

    }

    public void MoveSelection(int increment)
    {
        curSelection = Mathf.Clamp(curSelection+increment, 0, cardSprites.Length);
        card.sprite = cardSprites[curSelection];

        // deselect + select icons
    }

    public void Select(int index)
    {
        if (index < cardSprites.Length) card.sprite = cardSprites[index];
    }

    public void ConfirmSelection()
    {
        confirmed = true;
        // confirm icon
    }

    public bool IsConfirmed() => confirmed;

}
