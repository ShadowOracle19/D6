using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject dicePrefab;
    public DiceBlock selectedDice = null;
    public List<DiceBlock> diceBlocks = new List<DiceBlock>();
    Animator anim;

    //generating
    Vector2 generatingPosition;
    Vector2 positionToMoveTo;
    int num;//this value will be switched to -3 after the first layer of dice are generated

    //dice checking
    int location;//holds what location the corresponding dice is used within DiceListIndexCheck

    //Gameplay
    [SerializeField]
    private int score;
    public int chain;
    public int diceSwapLeft;
    public TextMeshProUGUI t_score;
    public TextMeshProUGUI t_chain;
    public List<Image> swapLeftImages = new List<Image>();
    public Sprite emptyCircle, filledCircle;
    public AudioSource blip;
    public float pitch = 1;
    public Slider total;

    //Endless mode
    bool playingEndless = false;
    int endlessScore;
    int targetScore = 50;

    //Player Stats
    public float TimesPlayed;
    public float GamesWon;
    public float GamesLost;
    public float winStreak;
    public float bestChain;
    public float highScore;
    public TextMeshProUGUI _TimesPlayed;
    public TextMeshProUGUI _GamesWon;
    public TextMeshProUGUI _GamesLost;
    public TextMeshProUGUI _winStreak;
    public TextMeshProUGUI _bestChain;
    public TextMeshProUGUI _highScore;

    //Color change
    int colorNumber = 1;

    bool isDots = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        t_score.text = score.ToString() + "/ 50";
        t_chain.text = chain.ToString() + " Chain";
        total.value = score;

        AddPlayerStats();
    }
    public void PlayGame()
    {
        TimesPlayed += 1;
        for (int i = 0; i < swapLeftImages.Count; i++)
        {
            swapLeftImages[i].sprite = filledCircle;
        }
        diceSwapLeft = 6;
        score = 0;
        chain = 0;
        StartCoroutine(c_GenerateBoard());
    }

    IEnumerator c_GenerateBoard()
    {
        generatingPosition = new Vector2(-6, 10f);//sets generating position to first position
        positionToMoveTo = new Vector2(-6, 0.7f);//sets move to the first position
        num = 3;
        for (int i = 0; i< 10; i++)
        {
            if (i == 5)
            {
                num = -3;
                generatingPosition.x = 6;
                positionToMoveTo.x = 6;
                positionToMoveTo.y = 3.5f;
            }
            var dice = Instantiate(dicePrefab, generatingPosition, Quaternion.identity);//create a dice block at a location at (x, 10f)
            dice.GetComponent<DiceBlock>().DotsActive = isDots;
            dice.GetComponent<DiceBlock>().colorToChange = colorNumber;
            generatingPosition.x += num;
            dice.GetComponent<DiceBlock>().MoveDice(positionToMoveTo);//activate the move function to move the dice block down once it is created
            diceBlocks.Add(dice.GetComponent<DiceBlock>());//add the dice block to a list to keep track of it 
            positionToMoveTo.x += num;

            if(i >= 5)
            {
                dice.GetComponent<DiceBlock>().isOnTop = true;
            }

            yield return new WaitForSeconds(.1f);

        }
        yield return null;
    }

    public void CheckDice(DiceBlock dice)
    {
        Debug.Log(dice.generatedNumber);
        if (selectedDice == null)//if there is no block selected this will play
        {
            DiceListIndexCheck(dice);//checks the location of the dice above the one selected
            dice.MoveDice(new Vector2(0,-3));//moves the selected dice
            
            selectedDice = dice;//adds dice to the selected block

            DiceBlock topBlock = diceBlocks[location];

            Vector2 originalLocation = topBlock.transform.position;//gets the location of the dice above it 

            var newBlock = Instantiate(dicePrefab, new Vector2(originalLocation.x, 10f), Quaternion.identity);//creates the new dice above the one about to move down
            newBlock.GetComponent<DiceBlock>().isOnTop = true;//sets the new block to is on top 
            newBlock.GetComponent<DiceBlock>().colorToChange = colorNumber;
            newBlock.GetComponent<DiceBlock>().DotsActive = isDots;
            topBlock.isOnTop = false;//sets the dice on the second row to no longer on top as its gonna move
            topBlock.DotsActive = isDots;
            topBlock.MoveDice(new Vector2(originalLocation.x, 0.7f));//moves dice block from second row to first

            diceBlocks.Remove(topBlock);//removes the top dice block so duplicates arent put in the list
            diceBlocks.Insert(diceBlocks.IndexOf(dice), topBlock);//inserts the dice above the dice selected in that slot of the list
            

            diceBlocks.RemoveAt(diceBlocks.IndexOf(dice));//removes the selected dice from the list

            diceBlocks.Insert(location, newBlock.gameObject.GetComponent<DiceBlock>());//inserts the new dice within the list at the empty location
            diceBlocks[location].MoveDice(new Vector2(originalLocation.x, 3.5f));//moves the dice to the empty slot

            selectedDice.isSelected = true;

            AddPoints();
            return;
        }
        if((selectedDice != null) && //to make sure there is a selected dice
            ((selectedDice.generatedColor == dice.generatedColor)||//if the colors are the same
            (selectedDice.generatedNumber+1 == dice.generatedNumber)||//if the number is one higher
            (selectedDice.generatedNumber == 6 && dice.generatedNumber == 1)))//just the check to go from 6 to 1
        {
            
            DiceListIndexCheck(dice);//checks the location of the dice above the one selected
            dice.MoveDice(new Vector2(0, -3));//moves the selected dice

            selectedDice.MoveDice(new Vector2(0, -10f));//moves the already selected block down to be deleted
            selectedDice = dice;//adds dice to the selected block

            DiceBlock topBlock = diceBlocks[location];

            Vector2 originalLocation = topBlock.transform.position;//gets the location of the dice above it 

            var newBlock = Instantiate(dicePrefab, new Vector2(originalLocation.x, 10f), Quaternion.identity);//creates the new dice above the one about to move down
            newBlock.GetComponent<DiceBlock>().isOnTop = true;//sets the new block to is on top 

            topBlock.isOnTop = false;//sets the dice on the second row to no longer on top as its gonna move
            topBlock.DotsActive = isDots;
            newBlock.GetComponent<DiceBlock>().colorToChange = colorNumber;
            newBlock.GetComponent<DiceBlock>().DotsActive = isDots;
            topBlock.MoveDice(new Vector2(originalLocation.x, 0.7f));//moves dice block from second row to first

            diceBlocks.Remove(topBlock);//removes the top dice block so duplicates arent put in the list
            diceBlocks.Insert(diceBlocks.IndexOf(dice), topBlock);//inserts the dice above the dice selected in that slot of the list


            diceBlocks.RemoveAt(diceBlocks.IndexOf(dice));//removes the selected dice from the list

            diceBlocks.Insert(location, newBlock.gameObject.GetComponent<DiceBlock>());//inserts the new dice within the list at the empty location
            diceBlocks[location].MoveDice(new Vector2(originalLocation.x, 3.5f));//moves the dice to the empty slot

            selectedDice.isSelected = true;

            AddPoints();
            return;
        }

    }


    //
    // dice 0 is below dice 9
    // dice 1 is below dice 8
    // dice 2 is below dice 7
    // dice 3 is below dice 6
    // dice 4 is below dice 5
    //
    void DiceListIndexCheck(DiceBlock dice)//checks the corresponding dice
    {
        switch (diceBlocks.IndexOf(dice))
        {
            case 0:
                location = 9;
                break;
            case 1:
                location = 8;
                break;
            case 2:
                location = 7;
                break;
            case 3:
                location = 6;
                break;
            case 4:
                location = 5;
                break;
            default:
                break;
        }
    }

    void AddPoints()
    {
        if(playingEndless)
        {
            endlessScore += 1;
        }
        blip.Play();
        chain += 1;
        score += 1;
        pitch += 0.2f;
        CheckIfWon();
        if (pitch >= 2.1f) return;
        blip.pitch = pitch;
    }

    public void GameOver()
    {
        StartCoroutine(c_GameOver());
    }

    IEnumerator c_GameOver()
    {

        for (int i = 0; i < 5; i++)
        {
            diceBlocks[i].MoveDice(new Vector2(-14, 0.7f));
            diceBlocks[i].isOnTop = true;
        }
        for (int i = 5; i < 10; i++)
        {
            diceBlocks[i].MoveDice(new Vector2(14, 3.5f));
            diceBlocks[i].isOnTop = true;
        }
        if(selectedDice != null) Destroy(selectedDice.gameObject);

        if(score > highScore)
        {
            highScore = score;
        }
        score = 0;
        chain = 0;
        selectedDice = null;
        yield return new WaitForSeconds(0.1f);

        anim.SetBool("PlayGame", false);
        anim.SetBool("Stats", true);
        playingEndless = false;
        yield return null;
    }

    void CheckIfWon()//after points are added it checks if the score is correct for win state
    {
        if (score == 50 && playingEndless != true)
        {
            GamesWon += 1;
            winStreak += 1;
            GameOver();
        }
        else if (score == targetScore && playingEndless)//in endless mode when the player gets to 50 they refresh their swaps left
        {
            for (int i = 0; i < swapLeftImages.Count; i++)
            {
                swapLeftImages[i].sprite = filledCircle;
            }
            diceSwapLeft = 6;
            targetScore += 50;
        }
    }

    public void EndlessMode()//activates endless mode
    {
        playingEndless = true;
    }

    public void AddPlayerStats()//plays once game is fully over to add the stats from that round 
    {
        _TimesPlayed.text = TimesPlayed.ToString();
        _GamesWon.text = GamesWon.ToString();
        _GamesLost.text = GamesLost.ToString();
        _winStreak.text = winStreak.ToString();
        _bestChain.text = bestChain.ToString();
        _highScore.text = highScore.ToString();
    }
    public void GM_ChangeColor(int color)
    {
        colorNumber = color;
    }

    public void GM_DotsAndColors(bool _isDots)
    {
        isDots = _isDots;
    }
}
