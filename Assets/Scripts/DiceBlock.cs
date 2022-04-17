//////////////////////////////////////////////////////////////////////
//FileName: DiceBlock.cs
//FileType: Visual C# Source File
//Author: Lucy Coates
//Created On: 4/01/2022 2:18:18PM
//Last Modified On: 4/17/2022 7:26:11PM
//Description: This file handles the randomizing of the dice's 
// number and color as well as holds the list of possible colors the
// dice can use. it handles the movement of the dice and what happens
// when they reach certain points as well as allowing the player to click
// them. 
//////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DiceBlock : MonoBehaviour
{
    GameManager gameManager;//just to get a reference to the game manager when needed in code 
    [Header("Dice numbers")]
    public List<Sprite> diceNumbers = new List<Sprite>();//List of sprite faces which the generated number will be pulled from
    public SpriteRenderer diceFace;
    public int generatedNumber;//generate a number from 1-6
    public TextMeshPro number;
    public bool DotsActive = true;

    [Header("Dice Color")]
    public List<Color> colors1 = new List<Color>();//List of colors that will be pulled from when a number for the color is generated
    public List<Color> colors2 = new List<Color>();//List of colors that will be pulled from when a number for the color is generated
    public List<Color> colors3 = new List<Color>();//List of colors that will be pulled from when a number for the color is generated
    public List<Color> colors4 = new List<Color>();//List of colors that will be pulled from when a number for the color is generated
    public int generatedColor;//generate randomly a number from 1-4
    public SpriteRenderer diceFaceColor;

    //Move Dice Variables
    private Vector2 velocity = Vector2.zero;
    float smoothTime = 0.3f;
    bool moveDice = false;
    Vector2 desiredPosition;

    //dice press
    public bool isOnTop = false;

    //remove selected dice
    public bool isSelected = false;

    //change color
    public int colorToChange;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        generateRandom();
    }

    private void Update()
    {
        if(DotsActive)
        {
            diceFace.gameObject.SetActive(true);
            number.gameObject.SetActive(false);
            diceFace.sprite = diceNumbers[generatedNumber - 1];
        }
        else if(!DotsActive)
        {
            diceFace.gameObject.SetActive(false);
            number.gameObject.SetActive(true);
            number.text = generatedNumber.ToString();
        }

        switch (colorToChange)
        {
            case 1:
                diceFaceColor.color = colors1[generatedColor - 1];
                break;
            case 2:
                diceFaceColor.color = colors2[generatedColor - 1];
                break;
            case 3:
                diceFaceColor.color = colors3[generatedColor - 1];
                break;
            case 4:
                diceFaceColor.color = colors4[generatedColor - 1];
                break;
            default:
                break;
        }


        if (moveDice)
        {
            DiceMoving();
        }

        if (transform.position.y <= -6.5f)
        {
            if(gameManager.selectedDice == this)
            {
                gameManager.selectedDice = null;
                if(gameManager.chain > gameManager.bestChain)
                {
                    gameManager.bestChain = gameManager.chain;
                }
                gameManager.chain = 0;
                gameManager.pitch = 1;
                gameManager.diceSwapLeft -= 1;

                if(gameManager.diceSwapLeft <= -1)
                {
                    gameManager.GamesLost += 1;
                    if(gameManager.winStreak > 0)gameManager.winStreak = 0;
                    gameManager.winStreak -= 1;
                    gameManager.GameOver();
                    return;
                }

                gameManager.swapLeftImages[gameManager.diceSwapLeft].sprite = gameManager.emptyCircle;
            }
            Destroy(gameObject);
        }
        if(transform.position.x <= -12f)
        {
            gameManager.selectedDice = null;
            gameManager.diceBlocks.Remove(this);
            Destroy(gameObject);
        }
        if (transform.position.x >= 12f)
        {
            gameManager.selectedDice = null;
            gameManager.diceBlocks.Remove(this);
            Destroy(gameObject);
        }

    }

    private void OnMouseDown()
    {
        if (isOnTop) return;
        if (isSelected)
        {
            MoveDice(new Vector2(this.transform.position.x, -10f));
            return;
        }
        gameManager.SendMessage("CheckDice", this);
    }

    public void generateRandom()
    {
        generatedNumber = Random.Range(1, 7);
        generatedColor = Random.Range(1, 6);

        
    }

    public void MoveDice( Vector2 _desiredPosition)
    {
        desiredPosition = _desiredPosition;
        moveDice = true;
        
    }

    void DiceMoving()
    {
        Vector2 targetToMoveOriginalPos = transform.position;
        transform.position = Vector2.SmoothDamp(targetToMoveOriginalPos, desiredPosition, ref velocity, smoothTime);
        if((Vector2)transform.position == desiredPosition)
        {
            moveDice = false;
        }
    }

}
