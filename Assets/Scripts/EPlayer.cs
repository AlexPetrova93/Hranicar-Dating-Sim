public enum EPlayer
{
    Left, Right
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
}