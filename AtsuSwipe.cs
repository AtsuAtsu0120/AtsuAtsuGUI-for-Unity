using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class AtsuSwipe : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //��Ƀh���b�N�������̃C�x���g
    public UnityEvent _dragUpEvent;
    //���Ƀh���b�N�������̃C�x���g
    public UnityEvent _dragDownEvent;

    private float _dragStartPotisionY;
    private float _dragStartPotisionX;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragStartPotisionY = eventData.position.y;
        _dragStartPotisionX = eventData.position.x;
        //�X���C�v�ƃ{�^���̏����������ɋN����Ȃ��悤��
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
            Debug.Log("��Ƀh���b�O");
        }
        else if (_dragEndPotisionY - _dragStartPotisionY < 50 && _dragStartPotisionX - _dragEndPotisionX < 100 && _dragStartPotisionX - _dragEndPotisionX > -100)
        {
            _dragDownEvent?.Invoke();
            Debug.Log("���Ƀh���b�O");
        }
        //�X���C�v�ƃ{�^���̏����������ɋN����Ȃ��悤��
        if (TryGetComponent<LocerlButton>(out var _button)) _button._isAbleIvoke = true;
    }
}
