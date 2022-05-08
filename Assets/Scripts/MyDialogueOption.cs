using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class MyDialogueOption : MonoBehaviour
{
    private Color defaultColor;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color confirmedColor;

    private TextMeshProUGUI optionText;

    void Start()
    {
        optionText = GetComponent<TextMeshProUGUI>();
        defaultColor = optionText.color;
    }

    public void SetText(string text)
    {
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
}
