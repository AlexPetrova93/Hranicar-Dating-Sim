using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Image bgRenderer;
    [SerializeField] private Image characterLeftRenderer;
    [SerializeField] private Image characterRightRenderer;

    [SerializeField] private Sprite[] bgSprites;

    public void SetCharacterSprites(Sprite left, Sprite right)
    {
        characterLeftRenderer.sprite = left;
        characterRightRenderer.sprite = right;
    }

    public void SetRandomBackground()
    {
        bgRenderer.sprite = bgSprites[Random.Range(0, bgSprites.Length)];
    }
}
