using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject dicePrefab;
    public DiceBlock selectedDice = null;
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

            topBlock.isOnTop = false;//sets the dice on the second row to no longer on top as its gonna move
            topBlock.MoveDice(new Vector2(originalLocation.x, 0.7f));//moves dice block from second row to first

            diceBlocks.Remove(topBlock);//removes the top dice block so duplicates arent put in the list
            diceBlocks.Insert(diceBlocks.IndexOf(dice), topBlock);//inserts the dice above the dice selected in that slot of the list
            

            diceBlocks.RemoveAt(diceBlocks.IndexOf(dice));//removes the selected dice from the list

            diceBlocks.Insert(location, newBlock.gameObject.GetComponent<DiceBlock>());//inserts the new dice within the list at the empty location
            diceBlocks[location].MoveDice(new Vector2(originalLocation.x, 3.5f));//moves the dice to the empty slot

            selectedDice.isSelected = true;
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
            topBlock.MoveDice(new Vector2(originalLocation.x, 0.7f));//moves dice block from second row to first

            diceBlocks.Remove(topBlock);//removes the top dice block so duplicates arent put in the list
            diceBlocks.Insert(diceBlocks.IndexOf(dice), topBlock);//inserts the dice above the dice selected in that slot of the list


            diceBlocks.RemoveAt(diceBlocks.IndexOf(dice));//removes the selected dice from the list

            diceBlocks.Insert(location, newBlock.gameObject.GetComponent<DiceBlock>());//inserts the new dice within the list at the empty location
            diceBlocks[location].MoveDice(new Vector2(originalLocation.x, 3.5f));//moves the dice to the empty slot

            selectedDice.isSelected = true;
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
}
