using System;
using UnityEngine;

/// <summary>
/// BaseUI를 사용할 때 필요한 Data Class
/// </summary>
public class BaseUIData
{
    public Action OnShow;
    public Action OnClose;
}

/// <summary>
/// UIManager에서 사용하기 위한 UI Class
/// </summary>
public class BaseUI : MonoBehaviour
{
    [SerializeField]
    private Animation _uiOpenAnim;

    private Action _onShow;
    private Action _onClose;

    /// <summary>
    /// BaseUI를 초기화 하는 메소드
    /// </summary>
    public virtual void Init(Transform anchor)
    {
        Logger.Log($"{GetType()}::Init()");

        _onShow = null;
        _onClose = null;

        transform.SetParent(anchor);

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = Vector3.zero;
        rectTransform.localScale = Vector3.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
    }

    /// <summary>
    /// BaseUIData를 BaseUI에 전달하는 메소드
    /// </summary>
    public virtual void SetInfo(BaseUIData uiData)
    {
        Logger.Log($"{GetType()}::SetInfo()");

        _onShow = uiData.OnShow;
        _onClose = uiData.OnClose;
    }

    /// <summary>
    /// UI를 화면에 보이는 메소드
    /// </summary>
    public virtual void ShowUI()
    {
        if (_uiOpenAnim)
        {
            _uiOpenAnim.Play();
        }

        _onShow?.Invoke();
        _onShow = null;
    }

    /// <summary>
    /// UI를 화면에서 지우는 메소드
    /// </summary>
    public virtual void CloseUI(bool isCloseAll = false)
    {
        if (!isCloseAll)
        {
            _onClose?.Invoke();
        }
        _onClose = null;

        UIManager.Instance.CloseUI(this);
    }
}