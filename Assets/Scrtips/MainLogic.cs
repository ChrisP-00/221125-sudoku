using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLogic : MonoBehaviour
{
    //int[,] board = new int[9, 9];


    //private void Awake()
    //{

    //    List<int> myRanBox = genRanBox();
    //    int idx = 0;
    //    int curY = 0;
    //    int moveY = 0;
    //    int moveX = 0;

    //    // generate numbers of first 3x3 box
    //    for (int y = 0; y < 3; ++y)
    //    {
    //        for (int x = 0; x < 3; ++x)
    //        {
    //            board[y, x] = myRanBox[++idx];
    //        }
    //    }

    //    // for 2,3 box 

    //    for (int line = 1; line <= 2; ++line)
    //    {
    //        idx = 0;

    //        moveY = 1;
    //        moveX = 3;

    //        for (int y1 = 0; y1 < 3; ++y1)
    //        {
    //            for (int x1 = 0; x1 < 3; ++x1)
    //            {

    //                // (0 + 1) % 3 => 1, (0 + 2) % 3 => 2 (0 + 3) % 3 => 0
    //                // move Y line 1 up 
    //                curY = (y1 + moveY) % 3;

    //                board[curY, moveX * line + x1] = myRanBox[++idx];
    //            }
    //        }
    //    }

    //    // for next line 



    //}


    //List<int> genRanBox()
    //{
    //    // generate random number 
    //    List<int> randomBox = new List<int>();

    //    while (randomBox.Count < 9)
    //    {
    //        int randNum = Random.Range(1, 10);

    //        // if the number already in randombox, continue
    //        if (randomBox.Contains(randNum))
    //            continue;

    //        randomBox.Add(randNum);
    //    }

    //    return randomBox;
    //}








}
