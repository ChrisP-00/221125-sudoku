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
        // �� �Ʒ� ���� ���پ� ä�� �ִ´�. 
        for (int x = 0; x < 9; ++x)
        {
            int areaX = x / 3;  // ������ X �� �� 

            int ranNum = ranDomBox[x];

            map[areaY, areaX][0, x % 3].MyNum = ranNum;
        }

        // ��ĭ�� �̵�! 
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










    // ���� ���� �� ���� �ߺ����� Ȯ�� �ϱ� ���� ����Ʈ! 
    List<int>[] areaDuplicationList = new List<int>[9];

    // Ȯ���� ���� ����Ʈ -> Queue�� ���ڸ� ��� Ȯ�� �Ͽ����� ������ �ӽ� ����Ʈ
    List<int> duplicatedList = new List<int>();

    // box pos, area pos, randomBox
    int CheckDuplication(int xPos, int yPox, int areaX, int areaY, Queue<int> ranBox, int idx)
    {
        // Ȯ���� ���� ����Ʈ �ʱ�ȭ 
        duplicatedList.Clear();

        int tempNum = 0;

        // duplicatedList == ���ԵǾ��ִٸ� ��� ���ڸ� Ȯ���� ��
        do
        {
            // ���� ���ڸ� �ϳ� ����
            tempNum = ranBox.Dequeue();

            int count = 0;

            // ���� ���ڰ� �̹� Ȯ���� ���� ����Ʈ�� �ִٸ� �ٽ� �ְ� ���ο� ���ڸ� ������. 
            while (duplicatedList.Contains(tempNum))
            {
                // ���� �ڽ��� �ְ� 
                ranBox.Enqueue(tempNum);

                // ���ο� ���� ���ڸ� �����Ѵ� 
                tempNum = ranBox.Dequeue();

                count++;

                // ���� �ڽ��� ��� ���ڸ� Ȯ���߾��ٸ� ������. 
                if (count >= ranBox.Count)
                {
                    return 99;
                }
            }


            // ���� Ȯ�� 
            // x ������ ������ �Ű��� Ȯ�� �Ѵ�. 
            // �ش� ������ 9ĭ �ȿ� ���ڰ� �ߺ� �Ǵ� �� Ȯ�� �Ѵ�. 
            for (int y = 0; y < 3; ++y)
            {
                for (int x = 0; x < 3; ++x)
                {
                    // �ߺ��� ���ڰ� �ִٸ� ������ ���ڸ� Queue�� �ְ� �ٸ� ���ڸ� ������. 
                    if (map[areaY, areaX][y, x].MyNum == tempNum)
                    {
                        // �̹� Ȯ���� ���ڶ��
                        if (duplicatedList.Contains(tempNum))
                        {
                            return 11;
                        }

                        // Ȯ�� ����Ʈ�� �ְ� 
                        duplicatedList.Add(tempNum);

                        // Queue�� �ٽ� �ְ� 
                        ranBox.Enqueue(tempNum);

                        // ���ο� ���ڸ� ������. 
                        tempNum = ranBox.Dequeue();

                        // ���ο� ���ڸ� ������ ����üũ�� �ٽ� �Ѵ�. 
                        y = -1;
                        break;
                    }
                }
            }


            // ���� üũ�� ������ column üũ�� �Ѵ�. 
            // check column duplication 
            for (int y = 0; y < 9; ++y)
            {
                // ���ڰ� �ߺ��� �ִٸ�! 
                if (map[y / 3, areaX][y % 3, xPos].MyNum == tempNum)
                {
                    // Ȯ�� ����Ʈ�� �ְ� 
                    duplicatedList.Add(tempNum);

                    // ���� �ڽ��� �ٽ� �־��ְ� 
                    ranBox.Enqueue(tempNum);

                    // column Ȯ���� �����ϰ� ������ �ٽ� �ϵ��� �Ѵ�. 
                    break;
                }
            }

        } while (duplicatedList.Contains(tempNum));

        // ������ column�� ��� Ȯ�� �ؼ� �ߺ��� ���ٸ� �ش� ���ڸ� ��ȯ~
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














