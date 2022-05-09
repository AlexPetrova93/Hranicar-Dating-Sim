using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class MyDialogueOption : MonoBehaviour
{
    private Color defaultColor;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color confirmedColor;

    private TextMeshProUGUI optionText;

    private int dialogueOptionID;

    public void Init(int dialogueOptionID, string text)
    {
        optionText = GetComponent<TextMeshProUGUI>();
        defaultColor = optionText.color;

        this.dialogueOptionID = dialogueOptionID;
        optionText.text = text;
    }

    public void Select()
    {
        optionText.color = selectedColor;
    }

    public void Deselect()
    {
        optionText.color = defaultColor;
    }

    public void Confirm()
    {
        optionText.color = confirmedColor;
    }

    public int GetOptionID() => dialogueOptionID;

}
