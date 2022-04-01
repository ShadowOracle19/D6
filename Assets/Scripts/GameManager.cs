using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject dicePrefab;
    public GameObject selectedDice = null;
    public List<DiceBlock> diceBlocks = new List<DiceBlock>();

    //generating
    Vector2 generatingPosition;
    Vector2 positionToMoveTo;
    int num;//this value will be switched to -3 after the first layer of dice are generated

    //dice checking
    int location;//holds what location the corresponding dice is used within DiceListIndexCheck

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(c_GenerateBoard());
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void ClearSelectedBlock()
    {
       
    }

    void CompareBlocks(DiceBlock compareBlock)
    {
        

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
            generatingPosition.x += num;
            dice.GetComponent<DiceBlock>().MoveDice(positionToMoveTo);//activate the move function to move the dice block down once it is created
            diceBlocks.Add(dice.GetComponent<DiceBlock>());//add the dice block to a list to keep track of it 
            positionToMoveTo.x += num;
            yield return new WaitForSeconds(.1f);

        }
        yield return null;
    }

    public void CheckDice(DiceBlock dice)
    {
        Debug.Log(dice.generatedNumber);
        if (selectedDice == null)
        {
            DiceListIndexCheck(dice);
            dice.MoveDice(new Vector2(0,-3));
            selectedDice = dice.gameObject;

            Vector2 originalLocation = diceBlocks[location].transform.position;
            diceBlocks[location].MoveDice(new Vector2(diceBlocks[location].transform.position.x, 0.7f));//moves dice block from second row to first
            
            
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
}
