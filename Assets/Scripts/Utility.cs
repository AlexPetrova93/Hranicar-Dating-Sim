using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static readonly int numCharacterChoices = 4;
    public static int mainLevelBuildIndex = 2;
    public static float axisDelta = .3f;

    public static int numDialogueOptions = 3;

    // dialogue gfx layout
    public static int bubbleTextPaddingDefault = 20;
    public static int bubbleTextPaddingSide = 50;

}

public static class Extensions
{
    public static int MoveIndex(this int current, int increment, int maxSize)
    {
        current = (current + increment) < 0 ? maxSize - 1 : (current + increment) % maxSize;
        return current;
    }
}