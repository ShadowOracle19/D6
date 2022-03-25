using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiceBlock : MonoBehaviour, IPointerClickHandler
{
    [Header("Dice numbers")]
    public List<Sprite> diceNumbers = new List<Sprite>();//List of sprite faces which the generated number will be pulled from
    public SpriteRenderer diceFace;
    public int generatedNumber;//generate a number from 1-6

    [Header("Dice Color")]
    public List<Color> colors = new List<Color>();//List of colors that will be pulled from when a number for the color is generated
    public int generatedColor;//generate randomly a number from 1-4
    public SpriteRenderer diceFaceColor;

    public bool onTierTwo;//if the dice block is on the second tier of dice/just generated

    private void Start()
    {
        generateRandom();
    }

    private void Update()
    {
        diceFace.sprite = diceNumbers[generatedNumber - 1];
        diceFaceColor.color = colors[generatedColor - 1];

        
    }

    public void generateRandom()
    {
        generatedNumber = Random.Range(1, 7);
        generatedColor = Random.Range(1, 5);
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Dice clicked:" + generatedNumber);
    }
}
