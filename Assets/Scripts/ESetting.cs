using System;
using System.Linq;

public enum ESetting
{
    Gallery,
    Street, 
    Pub,
    Noise,
    Menu
}

public static class ESettingExtensions
{
    public static ESetting ToESetting(this string text)
    {
        if (text.Contains("hospody") || text.Contains("pub")) return ESetting.Pub;
        if (text.Contains("ven") || text.Contains("zahájení") || text.Equals("") || text.Contains("street")) return ESetting.Street;
        if (text.Contains("noise")) return ESetting.Noise;
        else return ESetting.Gallery;
    }
} 