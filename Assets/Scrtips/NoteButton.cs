using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class NoteButton : MonoBehaviour
{

    Toggle myToggle = null;
    Image myImage = null;
    TextMeshProUGUI myText = null;

    Sprite notePose;
    Sprite inputPose;


    void Awake()
    {
        myToggle = GetComponent<Toggle>();
        myToggle.onValueChanged.AddListener(delegate
        {
            OnClickMe(myToggle.isOn);
        });

        myImage = GetComponentInChildren<Image>();

        myText = GetComponentInChildren<TextMeshProUGUI>();

        notePose = Resources.Load<Sprite>("Note Pose");
        inputPose = Resources.Load<Sprite>("Input Pose");
    }


    public void OnClickMe(bool isNoteOn)
    {
        // note mode일때 
        if (isNoteOn)
        {
            // 부엉이 스프라이트와 색을 바꾼다 
            myImage.sprite = notePose;
            myImage.color = Color.black;

            myText.text = "Note Mode";
            myText.color = Color.black;

            // UImanager에게 notemode인걸 알림
            UiManager.Inst.NoteMode("NoteMode");
        }

        // input mode일때 
        else
        {
            // 부엉이의 스프라이트와 색을 바꾼다
            myImage.sprite = inputPose;
            myImage.color = Color.white;

            myText.text = "Input Mode";
            myText.color = Color.white;

            // UiManager에게 inputmode인걸 알림
            UiManager.Inst.NoteMode("InputMode");
        }

    }





}
