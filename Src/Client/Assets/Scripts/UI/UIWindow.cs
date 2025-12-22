using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UIWindow : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public delegate void CloseHandler(UIWindow sender, WindowResult result);

    public event CloseHandler OnClose;

    public virtual System.Type Type
    { get { return this.GetType(); } }

    // 拖拽功能
    public GameObject Window;
    private Vector2 offsetPos;

    public enum WindowResult
    {
        None = 0,
        Yes,
        No,
    }
    public void Close(WindowResult result = WindowResult.None)
    {
        UIManager.Instance.Close(this.Type);
        if (this.OnClose != null)
            this.OnClose(this, result);
        this.OnClose = null;
    }

    public virtual void OnCloseClick()
    {
        this.Close();
    }

    public virtual void OnYesClick()
    {
        this.Close(WindowResult.Yes);
    }

    private void OnMouseDown()
    {
        Debug.LogFormat(this.name + "Clicked");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Window == null) return;

        Window.transform.position = eventData.position - offsetPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Window == null) return;

        offsetPos = eventData.position - (Vector2)Window.transform.position;
    }
}