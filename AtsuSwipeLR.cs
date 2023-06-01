using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AtsuSwipeLR : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //�E�Ƀh���b�N�������̃C�x���g
    public UnityEvent _dragRightEvent;
    //���Ƀh���b�N�������̃C�x���g
    public UnityEvent _dragLeftEvent;

    private float _dragStartPotisionX;
    private float _dragStartPotisionY;

    [SerializeField] bool _isParentImage;

    private Image _image;
    public void Awake()
    {
        var _parent = this.transform.parent;
        if(_isParentImage)
        {
            _image = _parent.GetComponent<Image>();
        }
        else
        {
            _image = GetComponent<Image>();
        }

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragStartPotisionX = eventData.position.x;
        _dragStartPotisionY = eventData.position.y; 
        //�X���C�v�ƃ{�^���̏����������ɋN����Ȃ��悤��
        if (TryGetComponent<LocerlButton>(out var _button)) _button._isAbleIvoke = false;
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var _dragEndPotisionX = eventData.position.x;
        var _dragEndPotisionY = eventData.position.y;

        if (_dragEndPotisionX - _dragStartPotisionX > 100 && (_dragStartPotisionY - _dragEndPotisionY < 150 && _dragStartPotisionY - _dragEndPotisionY > -150))
        {
            _dragRightEvent?.Invoke();
        }
        else if (_dragEndPotisionX - _dragStartPotisionX < -100 && (_dragStartPotisionY - _dragEndPotisionY < 150 && _dragStartPotisionY - _dragEndPotisionY > -150))
        {
            _dragLeftEvent?.Invoke();
        }
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1f);
        //�X���C�v�ƃ{�^���̏����������ɋN����Ȃ��悤��
        if (TryGetComponent<LocerlButton>(out var _button)) _button._isAbleIvoke = true;
    }
}
