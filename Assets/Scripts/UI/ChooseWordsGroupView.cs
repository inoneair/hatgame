using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseWordsGroupView : MonoBehaviour
{
    [SerializeField] WordsGroupView _wordsGroupViewPrefab;
    [SerializeField] RectTransform _contentTransform;

    private Dictionary<string, WordsGroupView> _contentItems = new Dictionary<string, WordsGroupView>();
    private WordsGroupView _currentItem;

    public Action<string> _onChooseWordsGroup;

    public void SetGroups(string[] groups)
    {
        if (groups == null)
            return;

        ClearGroups();

        foreach (var group in groups)
        {
            var item = Instantiate(_wordsGroupViewPrefab, _contentTransform);
            item.text = group;
            item.OnChooseItem += OnChooseItemHandler;
            _contentItems[group] = item;
        }

        var contentHeight = _wordsGroupViewPrefab.GetComponent<RectTransform>().rect.height * groups.Length;
        _contentTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, contentHeight);
    }

    public void SubscribeOnChooseWordsGroup(Action<string> handler)
    {
        _onChooseWordsGroup += handler;
    }

    private void ClearGroups()
    {
        foreach (var item in _contentItems)
        {
            item.Value.OnChooseItem -= OnChooseItemHandler;
            Destroy(item.Value.gameObject);
        }

        _contentItems.Clear();
        _currentItem = null;
    }

    private void OnChooseItemHandler(WordsGroupView item)
    {
        if (item.isOn)
        {
            _currentItem?.SetIsOnWithoutNotify(false);
            _currentItem = item;
        }
        else
        {
            if (_currentItem == item)
            {
                _currentItem.SetIsOnWithoutNotify(false);
                _currentItem = null;
            }
        }

        _onChooseWordsGroup?.Invoke(_currentItem == null ? null : _currentItem.text);
    }
}
