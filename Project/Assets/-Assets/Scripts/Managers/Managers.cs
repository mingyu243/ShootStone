using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : SingletonMono<Managers>
{
    GameManager _game = new GameManager();

    public static GameManager Game { get { return Instance._game; } }
}
