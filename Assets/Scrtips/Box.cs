using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Box : MonoBehaviour
{
    TextMeshProUGUI myNumBox = null;
    Queue<int> myBoxNums = null;


    public int MyNum
    {
        get
        { return int.Parse(myNumBox.text); }
        set
        { myNumBox.text = value.ToString(); }
    }

    // Start is called before the first frame update
    void Awake()
    {
        myNumBox = GetComponentInChildren<TextMeshProUGUI>();
    }



      
    public void OnClick_Num()
    {
        Debug.Log("³ª¸¦ ´­·¶´Ù!!");
    
    
    
    }



    void availNums(Queue<int> nums)
    {
        myBoxNums = new Queue<int>(nums);
    }
}
