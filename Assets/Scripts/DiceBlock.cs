using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiceBlock : MonoBehaviour
{
    GameManager gameManager;
    [Header("Dice numbers")]
    public List<Sprite> diceNumbers = new List<Sprite>();//List of sprite faces which the generated number will be pulled from
    public SpriteRenderer diceFace;
    public int generatedNumber;//generate a number from 1-6

    [Header("Dice Color")]
    public List<Color> colors = new List<Color>();//List of colors that will be pulled from when a number for the color is generated
    public int generatedColor;//generate randomly a number from 1-4
    public SpriteRenderer diceFaceColor;

    //Move Dice Variables
    private Vector2 velocity = Vector2.zero;
    float smoothTime = 0.3f;
    bool moveDice = false;
    Vector2 desiredPosition;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        generateRandom();
    }

    private void Update()
    {
        diceFace.sprite = diceNumbers[generatedNumber - 1];
        diceFaceColor.color = colors[generatedColor - 1];

        if(moveDice)
        {
            DiceMoving();
        }
    }

    private void OnMouseDown()
    {
        gameManager.SendMessage("CheckDice", this);
    }

    public void generateRandom()
    {
        generatedNumber = Random.Range(1, 7);
        generatedColor = Random.Range(1, 5);
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
