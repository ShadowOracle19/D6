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
    public TextMeshProUGUI dotsOrNumber;

    //how to play text
    public TextMeshProUGUI title;//the d6 text on screen
    public TextMeshProUGUI text1;//the score text on screen
    public TextMeshProUGUI text2;//the chain text on screen
    public TextMeshProUGUI text3;
    public TextMeshProUGUI text4;

    //Stats
    public TextMeshProUGUI TimesPlayedNum;
    public TextMeshProUGUI GamesWonNum;
    public TextMeshProUGUI GamesLostNum;
    public TextMeshProUGUI winStreakNum;
    public TextMeshProUGUI bestChainNum;
    public TextMeshProUGUI highScoreNum;
    public TextMeshProUGUI TimesPlayed;
    public TextMeshProUGUI GamesWon;
    public TextMeshProUGUI GamesLost;
    public TextMeshProUGUI winStreak;
    public TextMeshProUGUI bestChain;
    public TextMeshProUGUI highScore;

    public Image bar;
    public Image menuBar;
    public Image Panel;
    public List<Image> circles = new List<Image>();//amount of selections left used to change the color of them

    //Dots and numbers
    public bool isDots = true;
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

                    //changes the text color of the how to play
                    title.color = colors1[0];
                    text1.color = colors1[0];
                    text2.color = colors1[0];
                    text3.color = colors1[0];
                    text4.color = colors1[0];

                    //changes the text color of the stats
                    TimesPlayedNum.color = colors1[0];
                    GamesWonNum   .color = colors1[0];
                    GamesLostNum  .color = colors1[0];
                    winStreakNum  .color = colors1[0];
                    bestChainNum  .color = colors1[0];
                    highScoreNum  .color = colors1[0];
                    TimesPlayed   .color = colors1[4];
                    GamesWon      .color = colors1[4];
                    GamesLost     .color = colors1[4];
                    winStreak     .color = colors1[4];
                    bestChain     .color = colors1[4];
                    highScore     .color = colors1[4];

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

                    //changes the text color of the how to play
                    title.color = colors2[0];
                    text1.color = colors2[0];
                    text2.color = colors2[0];
                    text3.color = colors2[0];
                    text4.color = colors2[0];

                    TimesPlayedNum.color = colors2[0];
                    GamesWonNum.color = colors2[0];
                    GamesLostNum.color = colors2[0];
                    winStreakNum.color = colors2[0];
                    bestChainNum.color = colors2[0];
                    highScoreNum.color = colors2[0];
                    TimesPlayed.color = colors2[4];
                    GamesWon.color = colors2[4];
                    GamesLost.color = colors2[4];
                    winStreak.color = colors2[4];
                    bestChain.color = colors2[4];
                    highScore.color = colors2[4];
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

                    //changes the text color of the how to play
                    title.color = colors3[0];
                    text1.color = colors3[0];
                    text2.color = colors3[0];
                    text3.color = colors3[0];
                    text4.color = colors3[0];

                    TimesPlayedNum.color = colors3[0];
                    GamesWonNum.color = colors3[0];
                    GamesLostNum.color = colors3[0];
                    winStreakNum.color = colors3[0];
                    bestChainNum.color = colors3[0];
                    highScoreNum.color = colors3[0];
                    TimesPlayed.color = colors3[4];
                    GamesWon.color = colors3[4];
                    GamesLost.color = colors3[4];
                    winStreak.color = colors3[4];
                    bestChain.color = colors3[4];
                    highScore.color = colors3[4];

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

                    //changes the text color of the how to play
                    title.color = colors4[0];
                    text1.color = colors4[0];
                    text2.color = colors4[0];
                    text3.color = colors4[0];
                    text4.color = colors4[0];

                    TimesPlayedNum.color = colors4[0];
                    GamesWonNum.color = colors4[0];
                    GamesLostNum.color = colors4[0];
                    winStreakNum.color = colors4[0];
                    bestChainNum.color = colors4[0];
                    highScoreNum.color = colors4[0];
                    TimesPlayed.color = colors4[4];
                    GamesWon.color = colors4[4];
                    GamesLost.color = colors4[4];
                    winStreak.color = colors4[4];
                    bestChain.color = colors4[4];
                    highScore.color = colors4[4];

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

    public void DotsAndNumbers()
    {
        if(isDots)
        {
            isDots = false;
            SendMessage("GM_DotsAndColors", isDots);
            dotsOrNumber.text = "DISPLAY: \nNUMBER";
        }
        else if(!isDots)
        {
            isDots = true;
            SendMessage("GM_DotsAndColors", isDots);
            dotsOrNumber.text = "DISPLAY: \nDOTS";
        }
    }

    public void HowToPlay()
    {
        anim.SetBool("HowToPlay", !anim.GetBool("HowToPlay"));
    }

    public void Stats()
    {
        anim.SetBool("Stats", !anim.GetBool("Stats"));
    }
}
