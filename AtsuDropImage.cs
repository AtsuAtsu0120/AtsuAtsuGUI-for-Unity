using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class AtsuDropImage : MonoBehaviour
{
    [SerializeField] private UnityEvent _event;
    public IvokeType _ivokeType;

    [HideInInspector] public Sprite _sprite;
    [HideInInspector] public LocalizedString _localizedString;

    public void OnDrop()
    {
        switch(_ivokeType)
        {
            case IvokeType.Image: GetComponent<Image>().sprite = _sprite; break;
            case IvokeType.Text: GetComponent<LocalizeStringEvent>().SetEntry(_localizedString.TableReference.ToString()); break;
        }
        _event.Invoke();
    }
}
public enum IvokeType
{
    Image,
    Text,
    None
}
