using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    static UiManager inst = null;

    Dictionary<string, bool> noteMode = new Dictionary<string, bool>();

    ToggleGroup numberBox = null;


    // singleton
    public static UiManager Inst
    {
        get
        {
            if (inst == null)
            {
                inst = FindObjectOfType<UiManager>();

                if (inst == null)
                {
                    inst = new GameObject("UiManager").GetComponent<UiManager>();
                }
            }
            return inst;
        }
    }

    private void Awake()
    {
        // if ���� �ƴϸ� ��.��.
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else
        {
            Destroy(this.gameObject);
        }
    }


    private void Start()
    {
        // Number Box�� toggle group ã��
        numberBox = GameObject.Find("Number Box").GetComponent<ToggleGroup>();



        noteMode.Add("InputMode", true);
        noteMode.Add("NoteMode", false);


    }



    // Note mode�� ���� number box�� toggle group�� Ȱ��ȭ / ��Ȱ��ȭ �Ѵ�
    public void NoteMode(string isInputMode)
    {
        numberBox.enabled = noteMode[isInputMode];
    }



}
