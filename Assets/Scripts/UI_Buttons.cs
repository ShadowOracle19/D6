using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Buttons : MonoBehaviour
{
    //animator
    Animator anim;

    //color change
    int number = 4;//handles what number we are on to change the color for now we are only going from 1-4
    public List<Button> buttons = new List<Button>();
    public List<Color> colors1 = new List<Color>();
    public List<Color> colors2 = new List<Color>();
    public List<Color> colors3 = new List<Color>();
    public List<Color> colors4 = new List<Color>();
    public TextMeshProUGUI d6Text;//the d6 text on screen
    public TextMeshProUGUI Score;//the score text on screen
    public TextMeshProUGUI Chain;//the chain text on screen
    public Image bar;
    public Image menuBar;
    public Image Panel;
    public List<Image> circles = new List<Image>();
    private void Start()
    {
        anim = GetComponent<Animator>();
        ChangeColor();
    }
    private void Update()
    {
        
    }
    public void PlayGame()
    {
        anim.SetBool("PlayGame", true);
    }

    public void PlayEndless()
    {
        anim.SetBool("PlayGame", true);
        SendMessage("EndlessMode");
    }

    public void ChangeColor()
    {
        number += 1;
        if(number > 4)
        {
            number = 1;
        }
        SendMessage("GM_ChangeColor", number);//sends a message to the game manager to change the dice block color as they generate

        for (int i = 0; i < buttons.Count; i++)
        {
            switch (number)
            {
                case 1:
                    d6Text.color = colors1[0];
                    Score.color = colors1[0];
                    Chain.color = colors1[0];
                    bar.color = colors1[4];
                    menuBar.color = colors1[0];
                    Panel.color = colors1[4];
                    buttons[i].GetComponent<Image>().color = colors1[i];
                    for (int j = 0; j < circles.Count; j++)
                    {
                        circles[j].color = colors1[3];
                    }
                    break;
                case 2:
                    d6Text.color = colors2[1];
                    Score.color = colors2[1];
                    Chain.color = colors2[1];
                    bar.color = colors2[4];
                    menuBar.color = colors2[1];
                    Panel.color = colors2[4];
                    buttons[i].GetComponent<Image>().color = colors2[i];
                    for (int j = 0; j < circles.Count; j++)
                    {
                        circles[j].color = colors2[3];
                    }
                    break;
                case 3:
                    d6Text.color = colors3[0];
                    Score.color = colors3[0];
                    Chain.color = colors3[0];
                    bar.color = colors3[4];
                    menuBar.color = colors3[0];
                    Panel.color = colors3[4];
                    buttons[i].GetComponent<Image>().color = colors3[i];
                    for (int j = 0; j < circles.Count; j++)
                    {
                        circles[j].color = colors3[3];
                    }
                    break;
                case 4:
                    d6Text.color = colors4[0];
                    Score.color = colors4[0];
                    Chain.color = colors4[0];
                    bar.color = colors4[4];
                    menuBar.color = colors4[0];
                    Panel.color = colors4[4];
                    buttons[i].GetComponent<Image>().color = colors4[i];
                    for (int j = 0; j < circles.Count; j++)
                    {
                        circles[j].color = colors4[3];
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
