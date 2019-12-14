using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface INotifyViewModel
{
    void OnValueChanged(UnityAction onValueChanged);
}

public class TBindableProperty<T> : INotifyViewModel
{
    public class ValueChangedEvent : UnityEvent<T> { }

    T _value;
    public T Value
    {
        get => _value;
        set 
        {
            _value = value;
            _event.Invoke(value);
        }
    }
    ValueChangedEvent _event;

    public TBindableProperty(T value = default(T))
    {
        _value = value;
        _event = new ValueChangedEvent();
    }

    public void OnValueChanged(UnityAction<T> onValueChanged)
    {
        _event.AddListener(onValueChanged);
    }

    public void OnValueChanged(UnityAction onValueChanged)
    {
        OnValueChanged((value) => onValueChanged());
    }

	public override string ToString()
	{
		return Value.ToString();
	}
}
