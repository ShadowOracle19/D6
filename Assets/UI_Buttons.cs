using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Buttons : MonoBehaviour
{
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayGame()
    {
        anim.SetBool("PlayGame", true);
    }
}
