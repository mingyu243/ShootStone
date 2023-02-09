using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Result : MonoBehaviour
{
    public void OnClickMenuButton() // Bind Button Event.
    {
        Managers.Game.GameState = GameState.Ready;
    }

    public void OnClickPlayButton() // Bind Button Event.
    {
        Managers.Game.GameState = GameState.Ready;
        Managers.Game.GameState = GameState.Play;
    }
}
