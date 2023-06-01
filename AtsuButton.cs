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
/// �G��̋֎~�B
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
        //�G�������ȂǂɃJ���[��ύX���郂�[�h�̎�true
        if (_colorChange)
        {
            //�G�����Ƃ��ɔZ�����āA���ꂽ�Ƃ��ɔ������郂�[�h�̎�true
            if (_changingColorInversion)
            {
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0.7f);
            }
            else
            {
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1f);
            }

        }
        //�X���C�v�ƃ{�^���������ɔ������Ȃ��悤�ɁB
        _buttonUpEvent?.Invoke();
        _isAbleIvoke = false;
        await UniTask.Delay(TimeSpan.FromSeconds(_delaySecond));
        _isAbleIvoke = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isAbleIvoke) return;
        //�G�������ȂǂɃJ���[��ύX���郂�[�h�̎�true
        if (_colorChange)
        {
            //�G�����Ƃ��ɔZ�����āA���ꂽ�Ƃ��ɔ������郂�[�h�̎�true
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
        //�G�������ȂǂɃJ���[��ύX���郂�[�h�̎�true
        if (_colorChange)
        {
            //�G�����Ƃ��ɔZ�����āA���ꂽ�Ƃ��ɔ������郂�[�h�̎�true
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
