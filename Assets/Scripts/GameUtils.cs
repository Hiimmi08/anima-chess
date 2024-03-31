using Pieces;
using System;

public static class GameUtils
{
    public static string GetPieceName(this Rank rank)
    {
        return Enum.GetName(typeof(Rank), rank);
    }
}