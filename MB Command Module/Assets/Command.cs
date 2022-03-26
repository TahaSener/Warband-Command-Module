using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Command : MonoBehaviour
{
    
    [SerializeField]
    private GameObject army;
    Vector3 offset;
    private bool isCommand= false;
    private Transform[] armyMembers;
    private List<Transform> firstRowUnits;
    private List<Transform> seconRowUnits;
    private float totalUnit;
    private int firstRowUnitSize;
    private int secondRowUnitSize;
    private float firstRowTotalSize;
    private float secondRowTotalSize;
    private float firstRowStartPoint;
    private float secondRowStartPoint;
    private float targetPosition;
    private int isTotalArmyReached = 0;
    private List<Transform> reachedArmyMemberName;

   
    private void Awake()
    {
        reachedArmyMemberName = new List<Transform>();
        firstRowUnits = new List<Transform>();
        seconRowUnits = new List<Transform>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if(isCommand== true)
            {
                Debug.Log("girid");
                firstRowUnits.Clear();
                seconRowUnits.Clear();
                reachedArmyMemberName.Clear();
                isCommand = false;
                totalUnit = 0;
            }
            commandGetPosition();
        }
        if (isCommand == true)
        {
            moveAI();
        }
        
    }
    private void commandGetPosition()
    {
        offset = Camera.main.transform.GetComponent<rayCast>().commandpoint;
        offset.y = 0;
        
        armyMembers = army.GetComponentsInChildren<Transform>();
        foreach(Transform ai in armyMembers)
        {
            if (ai.name.StartsWith("ai"))
            {
                totalUnit += 1;
            }
        }
        if (totalUnit % 2 == 0)
        {
            firstRowUnitSize = (int)totalUnit / 2;
            secondRowUnitSize = (int)totalUnit / 2;
        }
        else
        {
            firstRowUnitSize = (int)Mathf.Ceil(totalUnit/2);
            secondRowUnitSize = (int)(totalUnit-firstRowUnitSize);
        }
        int count = 1;
        foreach(Transform ai in armyMembers)
        {
            if (ai.name.StartsWith("ai"))
            {
                Transform temp=ai;
                if (count <= firstRowUnitSize)
                {
                    ai.GetComponent<aiAnim>().walk();
                    firstRowUnits.Add(ai.transform);
                    count++;
                }
                else
                {
                    ai.GetComponent<aiAnim>().walk();
                    seconRowUnits.Add(ai.transform);
                }
                
            }
        }
        firstRowTotalSize = (firstRowUnitSize * 3)-3;
        secondRowTotalSize = (secondRowUnitSize * 3)-3;
        firstRowStartPoint = firstRowTotalSize / 2;
        secondRowStartPoint = secondRowTotalSize / 2;
        //Debug.Log("asker ilk bölük:"+firstRowUnitSize +" ilk bölük uzunluk"+ firstRowTotalSize + " ilk bölük başlangıç node"+ -firstRowStartPoint);
        isCommand = true;



    }
    private void moveAI()
    {

        int i =  0;
        int i2 = 0;
        foreach (Transform ai in firstRowUnits)
        {
            
            targetPosition = -firstRowStartPoint + (i * 3);
            Vector3 targetPositionLocation = new Vector3(offset.x - targetPosition, offset.y, offset.z);
            ai.position = Vector3.MoveTowards(ai.position, targetPositionLocation, 2f * Time.deltaTime);
            if(Vector3.Distance(ai.position, targetPositionLocation) < 0.001f && !reachedArmyMemberName.Contains(ai))
            {
                ai.GetComponent<aiAnim>().stop();
                reachedArmyMemberName.Add(ai);
            }
            i++;

        }
        foreach (Transform ai in seconRowUnits)
        {

            targetPosition = -secondRowStartPoint + (i2 * 3);
            Vector3 targetPositionLocation = new Vector3(offset.x - targetPosition, offset.y, offset.z-2);
            ai.position = Vector3.MoveTowards(ai.position, targetPositionLocation, 2f * Time.deltaTime);
            
            if (Vector3.Distance(ai.position, targetPositionLocation) < 0.001f && !reachedArmyMemberName.Contains(ai))
            {
                ai.GetComponent<aiAnim>().stop();
                reachedArmyMemberName.Add(ai);
            }
            i2++;
            

        }
        //Debug.Log(reachedArmyMemberName.Count + "   $$$$$$$$$  " + totalUnit);
        if (totalUnit == reachedArmyMemberName.Count)
        {
            firstRowUnits.Clear();
            seconRowUnits.Clear();
            reachedArmyMemberName.Clear();
            isCommand = false;
            totalUnit = 0;
            
        }
        //for(int i = 0; i < firstRowUnitSize; i++)
        //{
        //    if (armyMembers[i].name.StartsWith("ai"))
        //    {
        //        targetPosition = -firstRowStartPoint + (i * 3);
        //        armyMembers[i].position = Vector3.MoveTowards(armyMembers[i].position, new Vector3(offset.x - targetPosition, offset.y, offset.z), 2f * Time.deltaTime);
        //        if (i == firstRowUnitSize - 1)
        //        {
        //            isCommand = false;
        //        }
        //    }
        //    else
        //    {
        //        i -= 1;
        //    }

        //}
        //foreach (Transform ai in armyMembers)
        //{
        //    if (ai.name.StartsWith("ai"))
        //    {

        //        ai.position = Vector3.MoveTowards(ai.position, offset, 2f * Time.deltaTime);
        //        if (Vector3.Distance(ai.position, offset) < 0.001f)
        //        {
        //            isCommand = false;

        //        }
        //    }

        //}



    }
}
