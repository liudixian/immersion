using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngineInternal;
using System;
using DG.Tweening;

public class LeftandRight : MonoBehaviour, IBeginDragHandler, IEndDragHandler

{
    public Button pageLastButton;
    public Button pageNextButton;
    //  public Text pageNumText;


    [Header("用来控制界面是否可以拖动的组建")]
    private CanvasGroup canvasGroup;


    [SerializeField]
    private bool buttonPageEnable;
    [Header("默认显示第几页从0开始")]
    public int m_nowPage;//从0开始
    [Header("设置总页数")]
    public int m_pageCount;//页码总数量

    [Header("是否启动动画")]
    public bool isEnableAnimation = true;
    [Header("动画播放速度")]
    public float SCROLL_SMOOTH_TIME = 20F;

    [Header("拖拽百分比超过屏幕的百分之多少切换下一张")]
    public float m_dragNum;

    private float m_pageAreaSize;
    private float scrollMoveSpeed = 0f;
    private bool scrollNeedMove = false;
    private float scrollTargetValue;
    public ScrollRect scrollRect;

    private bool isRegistEvent = false;

    public GameObject[] PanelList;

    void start()
    {
        initUP();
    }
    private void Awake()
    {
        // toggle.onValueChanged.AddListener(OnValueChange);
        m_nowPage = m_pageCount;
        InitManager(m_pageCount, 0, isEnableAnimation);

    }

    public void OnValueChange(bool isDrap)
    {
        //    canvasGroup.interactable = isDrap;
        //    canvasGroup.blocksRaycasts = isDrap;
        //  InitManager(m_pageCount, m_nowPage, isEnableAnimation);
    }


    public bool SetButtonStatus
    {
        set
        {
            buttonPageEnable = value;
            pageLastButton.interactable = buttonPageEnable && pageLastButton.interactable;
            pageNextButton.interactable = buttonPageEnable && pageNextButton.interactable;
        }
    }

    public void InitManager(int pageNum, int targetPage = 0, bool isShowAnim = false)
    {
        RegistEvent();
        m_pageCount = pageNum;
        m_pageAreaSize = 1f / (m_pageCount - 1);
        ChangePage(targetPage, isShowAnim);
    }

    private void RegistEvent()
    {
        if (isRegistEvent)
            return;
        isRegistEvent = true;
        if (pageLastButton != null)
            pageLastButton.onClick.AddListener(delegate { Paging(-1); });
        if (pageNextButton != null)
            pageNextButton.onClick.AddListener(delegate { Paging(1); });
    }

    public void Paging(int num)
    {
        if (istouch==false)
        {
            //maxNum-1,从0开始
            num = (num < 0) ? -1 : 1;
            int temp = Mathf.Clamp(m_nowPage + num, 0, m_pageCount - 1);
            if (m_nowPage == temp)
                return;
            ChangePage(temp, isEnableAnimation);
            istouch = true;
        }
     
    }
    void Update()
    {
        ScrollControl();
        if (Input.GetKey(KeyCode.A))
        {
            initUP();
        }
    }

    public void AnimationPlay(int ID)
    {
        PanelList[ID].GetComponent<DOTweenAnimation>().DORestartAllById((Task * 10 + ID).ToString());
    }


    public void AnimationInit(int ID)
    {
        PanelList[ID].GetComponent<DOTweenAnimation>().DORewindAllById((Task * 10 + ID).ToString());
    }
    public void initUP()
    {
        InitManager(m_pageCount, 0, false);
        this.GetComponent<ScrollRect>().horizontalScrollbar.value = 0;
        //  Yeshu.sprite = YeshuSpr[0];
    }
    public int GetPageNum { get { return m_nowPage; } }
    //按页翻动
    private void ScrollControl()
    {
        if (!scrollNeedMove)
            return;
        if (Mathf.Abs(scrollRect.horizontalNormalizedPosition - scrollTargetValue) < 0.0001f)
        {
            scrollRect.horizontalNormalizedPosition = scrollTargetValue;
            scrollNeedMove = false;
            return;
        }
        scrollRect.horizontalNormalizedPosition = Mathf.Lerp(scrollRect.horizontalNormalizedPosition, scrollTargetValue, Time.deltaTime * SCROLL_SMOOTH_TIME);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        scrollNeedMove = false;
        scrollTargetValue = 0;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        int tempPage = m_nowPage;

        int num = (((scrollRect.horizontalNormalizedPosition - (m_nowPage * m_pageAreaSize)) >= 0) ? 1 : -1);

        if (Mathf.Abs(scrollRect.horizontalNormalizedPosition - (m_nowPage * m_pageAreaSize)) >= (m_pageAreaSize / 5f) * m_dragNum)
        {
            tempPage += num;
            ChangePage1(tempPage);
        }

        ChangePage(tempPage);

    }
    public int Task;
    public bool istouch = false;
    public void FixedUpdate()
    {
        //if (Input.touchCount != 0)
        //{
        //    if (Math.Abs(Input.GetTouch(0).deltaPosition.x) < 3)
        //    {
        //        return;
        //    }
        //}
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        //{

        // //   Debug.Log(Input.GetTouch(0).deltaPosition.x + "Input.GetTouch(0).deltaPosition.x");
        //    if (Input.GetTouch(0).deltaPosition.x < 18)
        //    {
        //        Debug.Log("向左移动");
        //        Paging(1);
        //    }
        //    else
        //    {
        //        Debug.Log("向右移动");
        //        Paging(-1);

        //    }
        //}
        //// 当输入的触点数量大于0，且手指不动时

        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
        //{
        //    return;
        //}
        //if (Input.touchCount == 0)
        //{
        //    istouch = false;
        //    return;
        //}
    }
    //翻页 
    public void ChangePage(int pageNum, bool isShowAnim = true)
    {

        if (pageNum >= m_pageCount)
            pageNum = m_pageCount - 1;
        if (pageNum < 0)
            pageNum = 0;
        //  AnimationInit(pageNum);
        m_nowPage = pageNum;
        //   AnimationPlay(m_nowPage);
        ChangePageText(pageNum);
        if (isShowAnim)
        {
            scrollTargetValue = m_nowPage * m_pageAreaSize;
            scrollNeedMove = true;
            scrollMoveSpeed = 0;
        }
        else
        {
            scrollRect.verticalNormalizedPosition = m_nowPage * m_pageAreaSize;
        }
        ChangePageText(m_nowPage);
    }

    public void ChangePage1(int pageNum)
    {


        if (pageNum >= m_pageCount)
            pageNum = m_pageCount - 1;
        if (pageNum < 0)
            pageNum = 0;
        AnimationInit(pageNum);
        m_nowPage = pageNum;
        AnimationPlay(m_nowPage);
        //   Yeshu.sprite = YeshuSpr[m_nowPage];
    }

    //public Image Yeshu;
    //public Sprite[] YeshuSpr;
    //控制按钮的和文字
    public void ChangePageText(int num)
    {
        int maxPageTo0Start = m_pageCount - 1;
        m_nowPage = Mathf.Clamp(num, 0, maxPageTo0Start);

        //if (pageNumText != null)
        //    pageNumText.text = (m_nowPage + 1).ToString() + "/" + m_pageCount.ToString();

        if (maxPageTo0Start == 0)
        {
            scrollRect.enabled = false;
            pageLastButton.interactable = false;
            pageNextButton.interactable = false;
            return;
        }
        else
        {
            pageLastButton.interactable = true;
            pageNextButton.interactable = true;
            scrollRect.enabled = true;
        }
        SetButtonStatus = buttonPageEnable;
        if (!buttonPageEnable)
            return;

        if (m_nowPage == 0 && pageLastButton.interactable)
            pageLastButton.interactable = false;
        if (m_nowPage >= maxPageTo0Start && pageNextButton.interactable)
            pageNextButton.interactable = false;
        if (m_nowPage > 0 && (!pageLastButton.interactable))
            pageLastButton.interactable = true;
        if (m_nowPage < maxPageTo0Start && (!pageNextButton.interactable))
            pageNextButton.interactable = true;

    }
}
