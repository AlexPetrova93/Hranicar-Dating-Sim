using System;
using System.Linq;

public enum ESetting
{
    Gallery,
    Street, 
    Pub,
    Noise
}

public static class ESettingExtensions
{
    public static ESetting ToESetting(this string text)
    {
        if (text.Contains("hospody")) return ESetting.Pub;
        if (text.Contains("ven")) return ESetting.Street;
        else return ESetting.Gallery;
    }
} 