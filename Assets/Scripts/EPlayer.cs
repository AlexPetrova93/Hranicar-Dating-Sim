public enum EPlayer
{
    Left, Right, Flavor, NDEF
}

public static class EPlayerExtensions
{
    public static string Horizontal(this EPlayer player)
    {
        switch (player)
        {
            case EPlayer.Left:
                return "HorizontalLeft";
            case EPlayer.Right:
                return "HorizontalRight";
            default:
                return "Horizontal";
        }
    }
    public static string Vertical(this EPlayer player)
    {
        switch (player)
        {
            case EPlayer.Left:
                return "VerticalLeft";
            case EPlayer.Right:
                return "VerticalRight";
            default:
                return "Vertical";
        }
    }
    public static string Confirm(this EPlayer player)
    {
        switch (player)
        {
            case EPlayer.Left:
                return "ConfirmLeft";
            case EPlayer.Right:
                return "ConfirmRight";
            default:
                return "Confirm";
        }
    }

    public static EPlayer ToEPlayer(this string str)
    {
        if (str.Contains(EPlayer.Left.ToString())) return EPlayer.Left;
        else if (str.Contains(EPlayer.Right.ToString())) return EPlayer.Right;
        else if (str.Contains(EPlayer.Flavor.ToString())) return EPlayer.Flavor;
        else return EPlayer.NDEF;
    }
}