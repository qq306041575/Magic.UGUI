using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnController : MonoBehaviour
{
    const int ColumnCount = 4;
    const float ColumnInterval = 576f;
    const float SpeedStep = 1f;

    List<ColumnComponent> _columns;

    void Awake()
    {
        var go = Resources.Load<GameObject>("Column");
        _columns = new List<ColumnComponent>();
        for (int i = 0; i < ColumnCount; i++)
        {
            var column = Instantiate(go);
            column.transform.SetParent(transform, false);
            _columns.Add(column.GetComponent<ColumnComponent>());
        }
        StartCoroutine(MoveColumn());
    }

    IEnumerator MoveColumn()
    {
        for (int i = 0; i < _columns.Count; i++)
        {
            _columns[i].enabled = true;
            if (i + 1 == _columns.Count)
            {
                break;
            }
            var panel = _columns[i].GetComponent<RectTransform>();
            while (panel.anchoredPosition.x + ColumnInterval > 0)
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    void OnDisable()
    {
        for (int i = 0; i < _columns.Count; i++)
        {
            _columns[i].enabled = false;
        }
    }

    public void AddSpeed()
    {
        for (int i = 0; i < _columns.Count; i++)
        {
            _columns[i].MoveSpeed += SpeedStep;
        }
    }
}
