using System;
using System.Linq;

public enum EScene
{
    Gallery,
    Street, 
    Pub
}

public static class ESceneExtensions
{
    public static EScene ToEScene(this string text)
    {
        if (text.Contains("hospody")) return EScene.Pub;
        if (text.Contains("ven")) return EScene.Street;
        else return EScene.Gallery;
    }
} 