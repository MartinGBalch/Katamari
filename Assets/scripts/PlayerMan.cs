using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class PlayerMan
{
    public static Player localPlayer;
    public static List<Player> players = new List<Player>();

    //static int ID;

    //public static Player currentPlayer
    //{
    //    get
    //    {
    //        return players.Where(x => x.maxHealth == ID).ToArray()[0];
    //    }
    //}


    public static List<Player> bluePlayers
    {
        get
        {
            return players.Where(x => x.team == Player.Team.Blue).ToList();
        }
    }

    public static List<Player> redPlayers
    {
        get
        {
            return players.Where(x => x.team == Player.Team.Red).ToList();
        }
    }
}
