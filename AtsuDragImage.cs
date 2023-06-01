using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;

public class AtsuDragImage : MonoBehaviour, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Canvas _canvas;

    [SerializeField] private Sprite _sprite;
    [SerializeField] private LocalizedString _localizedString;
    [SerializeField] private IvokeType _ivokeType;

    private Vector2 _prevPos;
    private Transform _parentTransform;

    public void Awake()
    {
        _parentTransform = transform.parent;
        _prevPos = _parentTransform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, Input.mousePosition, _canvas.worldCamera, out pos);
        _parentTransform.position = _canvas.transform.TransformPoint(pos);
    }

    public void OnDrop(PointerEventData eventData)
    {
        List<RaycastResult> _raycastList = new();
        EventSystem.current.RaycastAll(eventData, _raycastList);

        foreach (var _obj in _raycastList)
        {
            var _gameObj = _obj.gameObject;

            if (_gameObj.TryGetComponent<LocerlDropImage>(out var _locerlDropImage))
            {
                //�G���[�΍�
                if (_locerlDropImage._ivokeType != _ivokeType) return;

                _locerlDropImage._sprite = _sprite;
                _locerlDropImage._localizedString = _localizedString;
                _locerlDropImage.OnDrop();
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _parentTransform.localPosition = _prevPos;
    }
}
