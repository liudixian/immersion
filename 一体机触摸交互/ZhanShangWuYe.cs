using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ZhanShangWuYe : MonoBehaviour
{
    public GameObject FatherObject;
    public List<GameObject> ZhaoShangeList = new List<GameObject>();
    
    public void Init()
    {
        for(int i = 0; i < ZhaoShangeList.Count; i++)
        {
            ZhaoShangeList[i].SetActive(false);
        }
    }

    void Start()
    {
        Init();
    }
    public void clickBtn(int num)
    {
        FatherObject.SetActive(true);
        Init();
        ZhaoShangeList[num].SetActive(true);
        //ZhaoShangSheKou.GetComponent<LeftandRight>().PanelList[0].GetComponent<DOTweenAnimation>().DORestartAllById("10");
        ZhaoShangeList[num].GetComponent<DOTweenAnimation>().DORestartAllById((40 + num).ToString());
    }

    public void clickBack()
    {
        Init();
        FatherObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

