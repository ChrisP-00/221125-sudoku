using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    // [areaY,areaX] [y,x]
    Box[,][,] map = new Box[3, 3][,];
    // List that contains random numbers
    List<int> ranDomBox;
    // prefab of box 
    GameObject prefab;

    private void Awake()
    {
        // find and load prefab in resources folder 
        prefab = Resources.Load<GameObject>("Box");

        for (int areaY = 0; areaY < 3; ++areaY)
        {
            for (int areaX = 0; areaX < 3; ++areaX)
            {
                map[areaY, areaX] = new Box[3, 3];

                for (int y = 0; y < 3; ++y)
                {
                    for (int x = 0; x < 3; ++x)
                    {
                        float myXPos = x - 4 + areaX * 3 + areaX * 0.1f;
                        float myYPos = y - 4 + areaY * 3 + areaY * 0.1f;

                        // initialize 
                        map[areaY, areaX][y, x] = Instantiate(prefab, new Vector3(myXPos, myYPos, 0), Quaternion.identity).gameObject.GetComponent<Box>();
                    }
                }
            }
        }

        ranDomBox = genRanBox();

        // move Y pos of area 
        for (int aY = 0; aY < 3; ++aY)
        {
            // generate first line 
            generateFirstLine(aY);

            // move numbers to set second and third line of each area 
            for (int y = 0; y < 2; ++y)
            {
                for (int aX = 0; aX < 3; ++aX)
                {
                    for (int x = 0; x < 3; ++x)
                    {
                        int nAreaX = (aX + 1) % 3; // move to next area!! 

                        // first line of each area
                        map[aY, aX][y + 1, x].MyNum = map[aY, nAreaX][y, x].MyNum;
                    }
                }
            }
        }

        // shuffle a random number of times 
        for (int x = Random.Range(2, 10); x < 10; ++x)
        {
            shuffle();
            Debug.Log("Shuffle!!" + x);
        }

    }





    //=======================================================
    #region Functions 2 Set Board

    // set first line of each area 
    void generateFirstLine(int areaY)
    {
        // 맨 아래 부터 한줄씩 채워 넣는다. 
        for (int x = 0; x < 9; ++x)
        {
            int areaX = x / 3;  // 구역의 X 축 값 

            int ranNum = ranDomBox[x];

            map[areaY, areaX][0, x % 3].MyNum = ranNum;
        }

        // 한칸씩 이동! 
        int temp = ranDomBox[0];
        ranDomBox.RemoveAt(0);
        ranDomBox.Add(temp);
    }


    // generate random numbers 
    List<int> genRanBox()
    {
        // generate random number 
        List<int> randomBox = new List<int>();

        while (randomBox.Count < 9)
        {
            int randNum = Random.Range(1, 10);

            // if the number already in randombox, continue
            if (!randomBox.Contains(randNum))
                randomBox.Add(randNum);
        }

        return randomBox;
    }

    public void shuffle()
    {
        // shuffle in line 
        for (int aY = 0; aY < 3; ++aY)
        {
            int howMany = Random.Range(2, 6);

            for (int mix = howMany; mix < 4; ++mix)
            {
                for (int y = 0; y < 2; ++y)
                {
                    for (int aX = 0; aX < 3; ++aX)
                    {
                        for (int x = 0; x < 3; ++x)
                        {
                            int temp = map[aY, aX][y, x].MyNum;

                            map[aY, aX][y, x].MyNum = map[aY, aX][y + 1, x].MyNum;

                            map[aY, aX][y + 1, x].MyNum = temp;
                        }
                    }
                }
            }
        }


        // shuffle in column
        for (int aX = 0; aX < 3; ++aX)
        {
            int howMany = Random.Range(2, 6);

            for (int mix = howMany; mix < 4; ++mix)
            {
                for (int x = 0; x < 2; ++x)
                {
                    for (int aY = 0; aY < 3; ++aY)
                    {
                        for (int y = 0; y < 3; ++y)
                        {
                            int temp = map[aY, aX][y, x].MyNum;

                            map[aY, aX][y, x].MyNum = map[aY, aX][y, x + 1].MyNum;

                            map[aY, aX][y, x + 1].MyNum = temp;
                        }
                    }
                }
            }
        }
    }





    #endregion
    //=======================================================










    // 숫자 생성 시 구역 중복됨을 확인 하기 위한 리스트! 
    List<int>[] areaDuplicationList = new List<int>[9];

    // 확인한 숫자 리스트 -> Queue의 숫자를 모두 확인 하였는지 보려는 임시 리스트
    List<int> duplicatedList = new List<int>();

    // box pos, area pos, randomBox
    int CheckDuplication(int xPos, int yPox, int areaX, int areaY, Queue<int> ranBox, int idx)
    {
        // 확인한 숫자 리스트 초기화 
        duplicatedList.Clear();

        int tempNum = 0;

        // duplicatedList == 포함되어있다면 모든 숫자를 확인한 것
        do
        {
            // 랜덤 숫자를 하나 선택
            tempNum = ranBox.Dequeue();

            int count = 0;

            // 꺼낸 숫자가 이미 확인한 숫자 리스트에 있다면 다시 넣고 새로운 숫자를 꺼낸다. 
            while (duplicatedList.Contains(tempNum))
            {
                // 랜덤 박스에 넣고 
                ranBox.Enqueue(tempNum);

                // 새로운 랜덤 숫자를 선택한다 
                tempNum = ranBox.Dequeue();

                count++;

                // 랜덤 박스의 모든 숫자를 확인했었다면 나간다. 
                if (count >= ranBox.Count)
                {
                    return 99;
                }
            }


            // 구역 확인 
            // x 축으로 구역을 옮가며 확인 한다. 
            // 해당 구역의 9칸 안에 숫자가 중복 되는 지 확인 한다. 
            for (int y = 0; y < 3; ++y)
            {
                for (int x = 0; x < 3; ++x)
                {
                    // 중복된 숫자가 있다면 기존의 숫자를 Queue에 넣고 다른 숫자를 꺼낸다. 
                    if (map[areaY, areaX][y, x].MyNum == tempNum)
                    {
                        // 이미 확인한 숫자라면
                        if (duplicatedList.Contains(tempNum))
                        {
                            return 11;
                        }

                        // 확인 리스트에 넣고 
                        duplicatedList.Add(tempNum);

                        // Queue에 다시 넣고 
                        ranBox.Enqueue(tempNum);

                        // 새로운 숫자를 꺼낸다. 
                        tempNum = ranBox.Dequeue();

                        // 새로운 숫자를 가지고 구역체크를 다시 한다. 
                        y = -1;
                        break;
                    }
                }
            }


            // 구역 체크가 끝나면 column 체크를 한다. 
            // check column duplication 
            for (int y = 0; y < 9; ++y)
            {
                // 숫자가 중복이 있다면! 
                if (map[y / 3, areaX][y % 3, xPos].MyNum == tempNum)
                {
                    // 확인 리스트에 넣고 
                    duplicatedList.Add(tempNum);

                    // 랜덤 박스에 다시 넣어주고 
                    ranBox.Enqueue(tempNum);

                    // column 확인을 종료하고 구역을 다시 하도록 한다. 
                    break;
                }
            }

        } while (duplicatedList.Contains(tempNum));

        // 구역과 column을 모두 확인 해서 중복이 없다면 해당 숫자를 반환~
        return tempNum;
    }


    //==================================================================================
    // functions for test

    public void Click_Me()
    {

        SceneManager.LoadScene("SampleScene");

    }



    public void shuffle_Line()
    {
        // shuffle in line 
        for (int aY = 0; aY < 3; ++aY)
        {
            int howMany = Random.Range(2, 6);

            for (int mix = howMany; mix < 4; ++mix)
            {
                for (int y = 0; y < 2; ++y)
                {
                    for (int aX = 0; aX < 3; ++aX)
                    {
                        for (int x = 0; x < 3; ++x)
                        {
                            int temp = map[aY, aX][y, x].MyNum;

                            map[aY, aX][y, x].MyNum = map[aY, aX][y + 1, x].MyNum;

                            map[aY, aX][y + 1, x].MyNum = temp;

                            Debug.Log($"shuffle in line! [{aX},{aY}][{x},{y}] = {map[aY, aX][y, x].MyNum} and [{aX},{aY}][{x},{y + 1}] = {map[aY, aX][y + 1, x].MyNum}");
                        }
                    }
                }
            }

        }

    }

    public void shuffle_Column()
    {
        // shuffle in column
        for (int aX = 0; aX < 3; ++aX)
        {
            int howMany = Random.Range(2, 6);

            for (int mix = howMany; mix < 4; ++mix)
            {
                for (int x = 0; x < 2; ++x)
                {
                    for (int aY = 0; aY < 3; ++aY)
                    {
                        for (int y = 0; y < 3; ++y)
                        {
                            int temp = map[aY, aX][y, x].MyNum;

                            map[aY, aX][y, x].MyNum = map[aY, aX][y, x + 1].MyNum;

                            map[aY, aX][y, x + 1].MyNum = temp;

                            Debug.Log($"shuffle in column! [{aX},{aY}][{x},{y}] = {map[aY, aX][y, x].MyNum} and [{aX},{aY}][{x + 1},{y}] = {map[aY, aX][y, x + 1].MyNum}");
                        }
                    }
                }

            }

        }

    }


}














