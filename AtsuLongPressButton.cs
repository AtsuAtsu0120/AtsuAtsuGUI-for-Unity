using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LocerlLongPressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private UnityEvent _longpressEvent;
    [SerializeField] private UnityEvent _pointerUpEvent;
    [SerializeField] private float _pressSecond;
    [SerializeField] private LocerlButton _button;

    private bool _isIvoked = false;
    private CancellationTokenSource _tokenSource;

    public async void OnPointerDown(PointerEventData eventData)
    {
        _tokenSource = new CancellationTokenSource();
        await LongPress(_tokenSource.Token);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _tokenSource.Cancel();
        if(_isIvoked)
        {
            _isIvoked = false;
            _pointerUpEvent.Invoke();
        }
        else
        {
            if (_button != null) _button._isAbleIvoke = true;
        }
    }

    public async UniTask LongPress(CancellationToken _token)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_pressSecond));

        //ÉLÉÉÉìÉZÉãÅI
        if (_token.IsCancellationRequested) return;

        _isIvoked = true;

        if (_button != null) _button._isAbleIvoke = false;

        _longpressEvent.Invoke();
    }
}
