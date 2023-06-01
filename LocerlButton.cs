using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

/// <summary>
/// 触るの禁止。
/// </summary>
public class LocerlButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool _isAbleIvoke = true;
    public bool _colorChange = true;
    public bool _changingColorInversion = false;
    public int _delaySecond;


    [SerializeField] private UnityEvent _buttonUpEvent;

    private Image _image;
    public void Awake()
    {
        var _parent = this.transform.parent;
        _image = _parent.GetComponent<Image>();
    }
    public async void OnPointerClick(PointerEventData eventData)
    {
        if (!_isAbleIvoke) return;
        //触った時などにカラーを変更するモードの時true
        if (_colorChange)
        {
            //触ったときに濃くして、離れたときに薄くするモードの時true
            if (_changingColorInversion)
            {
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0.7f);
            }
            else
            {
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1f);
            }

        }
        //スワイプとボタンが同時に発動しないように。
        _buttonUpEvent?.Invoke();
        _isAbleIvoke = false;
        await UniTask.Delay(TimeSpan.FromSeconds(_delaySecond));
        _isAbleIvoke = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isAbleIvoke) return;
        //触った時などにカラーを変更するモードの時true
        if (_colorChange)
        {
            //触ったときに濃くして、離れたときに薄くするモードの時true
            if (_changingColorInversion)
            {
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1f);
            }
            else
            {
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0.7f);
            }
        } 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //触った時などにカラーを変更するモードの時true
        if (_colorChange)
        {
            //触ったときに濃くして、離れたときに薄くするモードの時true
            if (_changingColorInversion && _isAbleIvoke)
            {
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0.7f);
            }
            else
            {
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1f);
            }
        } 
    }
}
