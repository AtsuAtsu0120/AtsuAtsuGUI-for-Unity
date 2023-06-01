using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// 触るの禁止。
/// </summary>
public class LocerlSwipe : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //上にドラックした時のイベント
    public UnityEvent _dragUpEvent;
    //下にドラックした時のイベント
    public UnityEvent _dragDownEvent;

    private float _dragStartPotisionY;
    private float _dragStartPotisionX;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragStartPotisionY = eventData.position.y;
        _dragStartPotisionX = eventData.position.x;
        //スワイプとボタンの処理が同時に起こらないように
        if (TryGetComponent<LocerlButton>(out var _button)) _button._isAbleIvoke = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var _dragEndPotisionY = eventData.position.y;
        var _dragEndPotisionX = eventData.position.x;
        if (_dragEndPotisionY - _dragStartPotisionY > 50 && _dragStartPotisionX - _dragEndPotisionX < 100 && _dragStartPotisionX - _dragEndPotisionX > -100)
        {
            _dragUpEvent?.Invoke();
            Debug.Log("上にドラッグ");
        }
        else if (_dragEndPotisionY - _dragStartPotisionY < 50 && _dragStartPotisionX - _dragEndPotisionX < 100 && _dragStartPotisionX - _dragEndPotisionX > -100)
        {
            _dragDownEvent?.Invoke();
            Debug.Log("下にドラッグ");
        }
        //スワイプとボタンの処理が同時に起こらないように
        if (TryGetComponent<LocerlButton>(out var _button)) _button._isAbleIvoke = true;
    }
}
