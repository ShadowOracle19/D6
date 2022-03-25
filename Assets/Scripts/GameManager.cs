using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject dicePrefab;
    public List<GameObject> diceBlocks = new List<GameObject>();
    public DiceBlock selectedBlock, blockToCompare;
    float x1 = -6;
    float x2 = -6;

    public bool blockSelected;

    // Start is called before the first frame update
    void Start()
    {
        GenerateBoard();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))//compare block
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider.gameObject.tag == "Dice")
            {
                //check if there is a selected dice if not select one 
                //compare the two dice selected
                Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
                CompareBlocks(hit.collider.gameObject.GetComponent<DiceBlock>());
            }
            else
            {
                return;
            }
        }
        if(Input.GetMouseButtonDown(1))//select block
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);


            if (hit.collider.gameObject.tag == "Dice")
            {
                //move the dice clicked to the desired location which is (0,-3)
                if(!blockSelected && !hit.collider.gameObject.GetComponent<DiceBlock>().onTierTwo)
                {
                    hit.collider.gameObject.GetComponent<DiceBlock>().SelectBlock();
                    blockSelected = true;
                    selectedBlock = hit.collider.gameObject.GetComponent<DiceBlock>();
                    //diceBlocks.Remove(selectedBlock.gameObject);
                    MoveBlockDownARow(diceBlocks.IndexOf(selectedBlock.gameObject));
                }
                
            }
            else
            {
                return;
            }
        }

        
    }

    void GenerateBoard()
    {
        blockSelected = false;
        //generate first row
        for (int i = 0; i < 5; i++)
        {
            var dice = Instantiate(dicePrefab);
            dice.transform.position = new Vector2(x1, 0.6f);
            dice.GetComponent<DiceBlock>().onTierTwo = false;
            diceBlocks.Add(dice);
            x1 += 3;
        }
        //generate second row
        for (int i = 0; i < 5; i++)
        {
            var dice = Instantiate(dicePrefab);
            dice.transform.position = new Vector2(x2, 3.45f);
            dice.GetComponent<DiceBlock>().onTierTwo = true;
            diceBlocks.Add(dice);
            x2 += 3;
        }
    }

    public void ClearSelectedBlock()
    {
        Destroy(selectedBlock.gameObject);
        selectedBlock = null;
        blockSelected = false;
    }

    void CompareBlocks(DiceBlock compareBlock)
    {
        blockToCompare = compareBlock;
        if((blockToCompare.generatedNumber == selectedBlock.generatedNumber)&&(blockToCompare.generatedColor != selectedBlock.generatedColor))//checks if the dice has the same number but different color
        {
           
            Destroy(selectedBlock.gameObject);
            selectedBlock = null;
            selectedBlock = blockToCompare;
            selectedBlock.SelectBlock();
            MoveBlockDownARow(diceBlocks.IndexOf(selectedBlock.gameObject));

        }

    }

    //Fix: Instead of doing it this hardcoded set it to plus 5 whatever is selected

    void MoveBlockDownARow(int blockIndex)
    {
        
        var newBlock1 = Instantiate(dicePrefab, diceBlocks[blockIndex+5].transform.position, Quaternion.identity);
        diceBlocks[blockIndex + 5].gameObject.GetComponent<DiceBlock>().MoveDown();
        diceBlocks.Insert(blockIndex, diceBlocks[blockIndex + 5]);
        diceBlocks.Insert(blockIndex + 5, newBlock1);

        //switch (blockIndex)
        //{
        //    case 0://if its 0 select object 5
        //        diceBlocks[5].gameObject.GetComponent<DiceBlock>().MoveDown();
        //        diceBlocks.Insert(0, diceBlocks[5]);
        //        var newBlock0 = Instantiate(dicePrefab);
        //        newBlock0.transform.position = new Vector2(-6, 3.45f);
        //        diceBlocks.Insert(5, newBlock0);
        //        break;

        //    case 1://if its 1 select object 6
        //        diceBlocks[6].gameObject.GetComponent<DiceBlock>().MoveDown();
        //        diceBlocks.Insert(1, diceBlocks[6]);
        //        var newBlock1 = Instantiate(dicePrefab);
        //        newBlock1.transform.position = new Vector2(-3, 3.45f);
        //        diceBlocks.Insert(6, newBlock1);
        //        break;

        //    case 2://if its 2 select object 7
        //        diceBlocks[7].gameObject.GetComponent<DiceBlock>().MoveDown();
        //        diceBlocks.Insert(2, diceBlocks[7]);
        //        var newBlock2 = Instantiate(dicePrefab);
        //        newBlock2.transform.position = new Vector2(0, 3.45f);
        //        diceBlocks.Insert(7, newBlock2);
        //        break;

        //    case 3://if its 3 select object 8
        //        diceBlocks[8].gameObject.GetComponent<DiceBlock>().MoveDown();
        //        diceBlocks.Insert(3, diceBlocks[8]);
        //        var newBlock3 = Instantiate(dicePrefab);
        //        newBlock3.transform.position = new Vector2(3, 3.45f);
        //        diceBlocks.Insert(8, newBlock3);
        //        break;

        //    case 4://if its 4 select object 9
        //        diceBlocks[9].gameObject.GetComponent<DiceBlock>().MoveDown();
        //        diceBlocks.Insert(4, diceBlocks[9]);
        //        var newBlock4 = Instantiate(dicePrefab);
        //        newBlock4.transform.position = new Vector2(6, 3.45f);
        //        diceBlocks.Insert(9, newBlock4);
        //        break;

        //    default:
        //        break;
        //}
    }
}
