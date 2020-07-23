using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LogoBtnClick : MonoBehaviour
{
    public GameObject LOGO;
    public GameObject ZhaoShangSheKou;
    public GameObject ZhaoShangSheKouZaiShangHai;
    public GameObject ZhaoShangWuYe;
    public GameObject TempObject;

    void Start()
    {
        ZhaoShangSheKou.SetActive(false);
        ZhaoShangSheKouZaiShangHai.SetActive(false);
        ZhaoShangWuYe.SetActive(false);
    }
    void Update()
    {
        
    }

    //LOGO 页面 按钮点击事件
    public void clickLogoBtn(string btnName)
    {
        LOGO.SetActive(false);
        switch (btnName)
        {
            case "招商蛇口":
                ZhaoShangSheKou.SetActive(true);
                ZhaoShangSheKou.GetComponent<LeftandRight>().PanelList[0].GetComponent<DOTweenAnimation>().DORestartAllById("10");
                TempObject = ZhaoShangSheKou;
                break;
            case "招商蛇口在上海":
                ZhaoShangSheKouZaiShangHai.SetActive(true);
                ZhaoShangSheKouZaiShangHai.GetComponent<LeftandRight>().PanelList[0].GetComponent<DOTweenAnimation>().DORestartAllById("20");
                TempObject = ZhaoShangSheKouZaiShangHai;
                break;
            case "招商物业":
                ZhaoShangWuYe.SetActive(true);
                ZhaoShangWuYe.GetComponent<LeftandRight>().PanelList[0].GetComponent<DOTweenAnimation>().DORestartAllById("30");
                TempObject = ZhaoShangWuYe;
                break;
            default:
                break;
        }
    }
    //返回到LOGO页面 BACK按钮点击事件
    public void clickBack()
    {
        LOGO.SetActive(true);
        LOGO.GetComponent<DOTweenAnimation>().DORestartAllById("1");
        TempObject.GetComponent<LeftandRight>().initUP();
        ZhaoShangSheKou.SetActive(false);
        ZhaoShangSheKouZaiShangHai.SetActive(false);
        ZhaoShangWuYe.SetActive(false);
    }
}
