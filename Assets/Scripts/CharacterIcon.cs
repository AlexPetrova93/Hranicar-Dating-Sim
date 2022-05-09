using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CharacterIcon : MonoBehaviour
{
    [SerializeField] private Image targetGraphic;

    private Sprite defaultIcon;
    [SerializeField] private Sprite selectedIcon;
    [SerializeField] private Sprite confirmedIcon;

    private void Start()
    {
        defaultIcon = targetGraphic.sprite;
    }

    public void Select()
    {
        targetGraphic.sprite = selectedIcon;
    }

    public void Deselect()
    {
        targetGraphic.sprite = defaultIcon;
    }

    public void Confirm()
    {
        targetGraphic.sprite = confirmedIcon;
    }

}
