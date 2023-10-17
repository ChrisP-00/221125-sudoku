using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteNums : MonoBehaviour
{
    Toggle myToggle = null;
    TextMeshProUGUI myText;

    int num;

    Color isOn;
    Color isOff;



    private void Awake()
    {
        myToggle = GetComponent<Toggle>();
        myToggle.onValueChanged.AddListener(delegate
        {
            OnClickMe(myToggle.isOn);
        });


        myText = GetComponentInChildren<TextMeshProUGUI>();
        num = int.Parse(myText.text);

        isOn = new Color(1f, 0.8f, 0f);
        isOff = new Color(0.4f, 0.4f, 0.4f);

        myText.color = isOff;
    }


    public void OnClickMe(bool toggleisOn)
    {

        if (toggleisOn)
        {
            myText.color = isOn;
            Debug.Log(num);
        }

        else
        {
            myText.color = isOff;
        }

    }

}
