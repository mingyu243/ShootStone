using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Ready : MonoBehaviour
{
    Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnClickPlayButton() // Bind Button Event.
    {
        StartCoroutine(Do());

        IEnumerator Do()
        {
            _animator.SetTrigger("HIDE");

            yield return new WaitForSeconds(0.5f);

            Managers.Game.GameState = GameState.Play;
        }
    }
}
