using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LuaComponent : MonoBehaviour
{
    public Action LuaStart;
    public Action LuaUpdate;
    public Action LuaOnEnable;
    public Action LuaOnDisable;
    public Action LuaOnDestroy;
    public Action<Collision2D> LuaOnCollisionEnter2D;
    public Action<Collision2D> LuaOnCollisionExit2D;
    public Action<Collider2D> LuaOnTriggerEnter2D;
    public Action<Collider2D> LuaOnTriggerExit2D;

    List<Action> _luaFunctions = new List<Action>();

    public void AddButtonClick(string path, Action click)
    {
        Button button = null;
        if (string.IsNullOrEmpty(path))
        {
            button = GetComponent<Button>();
        }
        else
        {
            button = transform.Find(path).GetComponent<Button>();
        }
        if (null == button)
        {
            Debug.LogWarningFormat("Button component not found, path = {0}", path);
            return;
        }
        var index = _luaFunctions.Count;
        _luaFunctions.Add(click);
        button.onClick.AddListener(() => _luaFunctions[index]());
    }

    public void AddEventTrigger(string path, int triggerType, Action triggerFunction)
    {
        EventTrigger trigger = null;
        if (string.IsNullOrEmpty(path))
        {
            trigger = GetComponent<EventTrigger>();
        }
        else
        {
            trigger = transform.Find(path).GetComponent<EventTrigger>();
        }
        if (null == trigger)
        {
            Debug.LogWarningFormat("EventTrigger component not found, path = {0}", path);
            return;
        }
        var index = _luaFunctions.Count;
        _luaFunctions.Add(triggerFunction);

        var entry = new EventTrigger.Entry();
        entry.eventID = (EventTriggerType)triggerType;
        entry.callback.AddListener((data) => _luaFunctions[index]());
        trigger.triggers.Add(entry);
    }

    void Start()
    {
        if (null != LuaStart)
        {
            LuaStart();
        }
    }

    void Update()
    {
        if (null != LuaUpdate)
        {
            LuaUpdate();
        }
    }

    void OnEnable()
    {
        if (null != LuaOnEnable)
        {
            LuaOnEnable();
        }
    }

    void OnDisable()
    {
        if (null != LuaOnDisable)
        {
            LuaOnDisable();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (null != LuaOnCollisionEnter2D)
        {
            LuaOnCollisionEnter2D(other);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (null != LuaOnCollisionExit2D)
        {
            LuaOnCollisionExit2D(other);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (null != LuaOnTriggerEnter2D)
        {
            LuaOnTriggerEnter2D(other);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (null != LuaOnTriggerExit2D)
        {
            LuaOnTriggerExit2D(other);
        }
    }

    void OnDestroy()
    {
        if (null != LuaOnDestroy)
        {
            LuaOnDestroy();
        }
        LuaStart = null;
        LuaUpdate = null;
        LuaOnEnable = null;
        LuaOnDisable = null;
        LuaOnDestroy = null;
        LuaOnCollisionEnter2D = null;
        LuaOnCollisionExit2D = null;
        LuaOnTriggerEnter2D = null;
        LuaOnTriggerExit2D = null;

        for (int i = 0; i < _luaFunctions.Count; i++)
        {
            _luaFunctions[i] = null;
        }
        _luaFunctions.Clear();
    }
}
