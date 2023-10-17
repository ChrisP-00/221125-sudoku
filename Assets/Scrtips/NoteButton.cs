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
        // note mode�϶� 
        if (isNoteOn)
        {
            // �ξ��� ��������Ʈ�� ���� �ٲ۴� 
            myImage.sprite = notePose;
            myImage.color = Color.black;

            myText.text = "Note Mode";
            myText.color = Color.black;

            // UImanager���� notemode�ΰ� �˸�
            UiManager.Inst.NoteMode("NoteMode");
        }

        // input mode�϶� 
        else
        {
            // �ξ����� ��������Ʈ�� ���� �ٲ۴�
            myImage.sprite = inputPose;
            myImage.color = Color.white;

            myText.text = "Input Mode";
            myText.color = Color.white;

            // UiManager���� inputmode�ΰ� �˸�
            UiManager.Inst.NoteMode("InputMode");
        }

    }





}
