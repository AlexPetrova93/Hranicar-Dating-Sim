public enum EPlayer
{
    Left, Right, NDEF
}

public static class EPlayerExtensions
{
    public static string Horizontal(this EPlayer player)
    {
        return player == EPlayer.Left ? "HorizontalLeft" : "HorizontalRight";
    }
    public static string Vertical(this EPlayer player)
    {
        return player == EPlayer.Left ? "VerticalLeft" : "VerticalRight";
    }
    public static string Confirm(this EPlayer player)
    {
        return player == EPlayer.Left ? "ConfirmLeft" : "ConfirmRight";
    }

    public static EPlayer ToEPlayer(this string str)
    {
        if (str.Contains(EPlayer.Left.ToString())) return EPlayer.Left;
        else if (str.Contains(EPlayer.Right.ToString())) return EPlayer.Right;
        else return EPlayer.NDEF;
    }
}