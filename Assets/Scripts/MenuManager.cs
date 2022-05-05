using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private Dictionary<EPlayer, bool> confirmedSelections;

    private CharacterCardController[] cardControllers;

    private GameManager gameManager;

    private void Start()
    {
        confirmedSelections = new Dictionary<EPlayer, bool>
        {
            { EPlayer.Left, false },
            { EPlayer.Right, false }
        };

        gameManager = FindObjectOfType<GameManager>();

        cardControllers = GetComponentsInChildren<CharacterCardController>();
        foreach (var cont in cardControllers)
        {
            cont.confirmationChange.AddListener(ConfirmSelection);
            cont.confirmationChange.AddListener(gameManager.ConfirmChoices);
        }
    }

    public void ConfirmSelection(EPlayer player, bool confirm, int selection)
    {
        confirmedSelections[player] = confirm;
        if (CheckConfirmed()) gameManager.NextScene();
    }

    private bool CheckConfirmed()
    {
        bool allConfirmed = true;
        foreach (var player in confirmedSelections)
        {
            allConfirmed &= player.Value;
        }
        return allConfirmed;
    }

}
