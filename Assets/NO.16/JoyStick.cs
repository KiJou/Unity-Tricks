using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [Header("摇杆半径")]
    public float JoyStickRadius = 72.0f;
    [Header("摇杆速度")]
    public float JoyStickResetSpeed = 5.0f;

    // 摇杆组件
    private RectTransform selfTransform;
    // 是否触摸摇杆
    private bool isTouched = false;
    // 摇杆默认位置
    private Vector2 originPosition;
    // 摇杆移动方向
    private Vector2 touchedAxis;
    public Vector2 TouchedAxis
    {
        get
        {
            if (touchedAxis.magnitude < JoyStickRadius)
                return touchedAxis.normalized / JoyStickRadius;
            return touchedAxis.normalized;
        }
    }
    /// <summary>
    /// 定义触摸开始事件委托
    /// </summary>
    public delegate void JoyStickTouchBegin(Vector2 vec);
    /// <summary>
    /// 定义触摸过程事件委托
    /// </summary>
    public delegate void JoyStickTouchMove(Vector2 vec);
    /// <summary>
    /// 定义触摸结束事件委托
    /// </summary>
    public delegate void JoyStickTouchEnd();
    /// <summary>
    /// 注册触摸开始事件
    /// </summary>
    public event JoyStickTouchBegin OnJoyStickTouchBegin;
    /// <summary>
    /// 注册触摸过程事件
    /// </summary>
    public event JoyStickTouchMove OnJoyStickTouchMove;
    /// <summary>
    /// 注册触摸结束事件
    /// </summary>
    public event JoyStickTouchEnd OnJoyStickTouchEnd;

    void Start()
    {
        // 初始化虚拟摇杆的默认方向
        selfTransform = GetComponent<RectTransform>();
        originPosition = selfTransform.anchoredPosition;
    }

    void Update()
    {
        // 手动触发OnJoyStickTouchMove事件
        if (isTouched && touchedAxis.magnitude >= JoyStickRadius)
        {
            if (OnJoyStickTouchMove != null)
                OnJoyStickTouchMove(TouchedAxis);
        }
        // 松开摇杆后让摇杆回到默认位置
        if (selfTransform.anchoredPosition.magnitude > originPosition.magnitude)
            selfTransform.anchoredPosition -= TouchedAxis * JoyStickResetSpeed * Time.deltaTime;
    }

    // 触摸开始
    public void OnPointerDown(PointerEventData eventData)
    {
        isTouched = true;
        touchedAxis = GetJoyStickAxis(eventData);
        if (OnJoyStickTouchBegin != null)
            OnJoyStickTouchBegin(TouchedAxis);
    }

    // 触摸结束
    public void OnPointerUp(PointerEventData eventData)
    {
        isTouched = false;
        selfTransform.anchoredPosition = originPosition;
        touchedAxis = Vector2.zero;
        if (OnJoyStickTouchEnd != null)
            OnJoyStickTouchEnd();
    }

    // 触摸过程
    public void OnDrag(PointerEventData eventData)
    {
        touchedAxis = GetJoyStickAxis(eventData);
        if (OnJoyStickTouchMove != null)
            OnJoyStickTouchMove(TouchedAxis);
    }

    // 返回摇杆的偏移量
    private Vector2 GetJoyStickAxis(PointerEventData eventData)
    {
        // 获取手指位置的世界坐标
        Vector3 worldPosition;
        // 设置摇杆位置
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(selfTransform,
                 eventData.position, eventData.pressEventCamera, out worldPosition))
            selfTransform.position = worldPosition;
        // 获取摇杆偏移
        Vector2 touchAxis = selfTransform.anchoredPosition - originPosition;
        // 摇杆偏移限制
        if (touchAxis.magnitude >= JoyStickRadius)
        {
            touchAxis = touchAxis.normalized * JoyStickRadius;
            selfTransform.anchoredPosition = touchAxis;
        }
        return touchAxis;
    }
}