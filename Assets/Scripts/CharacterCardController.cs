using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterCardController : MonoBehaviour
{
    [SerializeField] private EPlayer player;

    [SerializeField] private Image card;
    private Sprite[] cardSprites;

    private CharacterIcon[] icons;

    private float lastMoved;

    private int curSelection;

    [HideInInspector] public UnityEvent<EPlayer, bool, int> confirmationChange;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        cardSprites = gameManager.characterSprites;

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

        // play sound
        gameManager.audioManager.PlayNext();

        // deselect and deconfirm current
        icons[curSelection].Deselect();
        Confirm(false);

        // increment current selection
        curSelection = curSelection.MoveIndex(increment, Utility.numCharacterChoices);

        // change character card sprite
        card.sprite = cardSprites[curSelection];

        // select the next icon
        icons[curSelection].Select();
    }


    private void ConfirmSelection()
    {
        Confirm(true);
        // confirm icon
        icons[curSelection].Confirm();

        // play sound
        gameManager.audioManager.PlayPress();
    }

    private void Confirm(bool confirm)
    {
        confirmationChange.Invoke(player, confirm, curSelection);
    }

}
