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
        // if 내가 아니면 파.괘.
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
        // Number Box의 toggle group 찾기
        numberBox = GameObject.Find("Number Box").GetComponent<ToggleGroup>();



        noteMode.Add("InputMode", true);
        noteMode.Add("NoteMode", false);


    }



    // Note mode에 따라 number box의 toggle group을 활성화 / 비활성화 한다
    public void NoteMode(string isInputMode)
    {
        numberBox.enabled = noteMode[isInputMode];
    }



}
