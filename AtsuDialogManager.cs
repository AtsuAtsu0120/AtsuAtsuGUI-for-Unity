using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class DialogManager : MonoBehaviour
{
    public delegate void OnDialogButton();

    protected Camera _mainCamera;

    [HideInInspector] public string _title;
    [HideInInspector] public string _message;
    [HideInInspector] public string _button;

    public OnDialogButton _dialogButton;

    [SerializeField] protected LocalizedStringTable _table;
    [SerializeField] private Canvas _dialogCanvas;

    [SerializeField] private LocalizeStringEvent _titleLocalizeEvent;
    [SerializeField] private TextMeshProUGUI _titleText;

    [SerializeField] private LocalizeStringEvent _messageLocalizeEvent;
    [SerializeField] private TextMeshProUGUI _messageText;

    [SerializeField] private LocalizeStringEvent _buttonLocalizeEvent;
    [SerializeField] private TextMeshProUGUI _buttonText;

    public virtual void Awake()
    {
        //メインカメラを取得
        _mainCamera = Camera.main;
        //キャンバスにカメラを指定
        _dialogCanvas.worldCamera = _mainCamera;

        var _keyList = _table.GetTable().Values.ToList();

        //設定
        //ローカライズに含まれていればそれを適用、含まれてなければそのまま代入する。
        if (_keyList.Exists(x => x.Key == _title))
        {
            _titleLocalizeEvent.SetEntry(_title);
        }
        else
        {
            _titleText.text = _title;
        }
        if (_keyList.Exists(x => x.Key == _message))
        {
            _messageLocalizeEvent.SetEntry(_message);
        }
        else
        {
            _messageText.text = _message;
        }
        if(_keyList.Exists(x => x.Key == _button))
        {
            _buttonLocalizeEvent.SetEntry(_button);
        }
        else
        {
            _buttonText.text = _button;
        }
    }
    public virtual void OnDesitionButton()
    {
        _dialogButton?.Invoke();
        GameObject.Find("AudioSorce-SE").GetComponent<AudioManager>().PlayDecisionAudio();
        Destroy(gameObject);
    }
}
