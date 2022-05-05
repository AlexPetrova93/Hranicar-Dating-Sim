using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterIcon : MonoBehaviour
{
    private Image targetGraphic;

    private Sprite defaultIcon;
    [SerializeField] private Sprite selected;
    [SerializeField] private Sprite confirmed;

    private void Start()
    {
        targetGraphic = GetComponent<Image>();
        defaultIcon = targetGraphic.sprite;
    }

    public void Select()
    {
        targetGraphic.sprite = selected;
    }

    public void Deselect()
    {
        targetGraphic.sprite = defaultIcon;
    }

    public void Confirm()
    {
        targetGraphic.sprite = confirmed;
    }

}
