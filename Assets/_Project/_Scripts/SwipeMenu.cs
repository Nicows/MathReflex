using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{
    [SerializeField] private GameObject scrollbar;
    [Range(1f, 1.5f)]
    [SerializeField] private float selectedSize = 1.1f;
    [Range(0.01f, 0.5f)]
    [SerializeField] private float animationSpeed = 0.1f;
    private float _scrollPosition;
    private float[] _position;
    private float _distance;
    private float _distanceThreshold;
    private Scrollbar _scrollbarComponent;
    private Vector3 _selectedSizeVerVector3;

    private void Start()
    {
        _position = new float[transform.childCount];
        _distance = 1f / (_position.Length - 1f);
        _distanceThreshold = _distance / 2;
        _selectedSizeVerVector3 = new Vector3(selectedSize, selectedSize);
        _scrollbarComponent = scrollbar.GetComponent<Scrollbar>();
        for (var i = 0; i < _position.Length; i++)
        {
            _position[i] = _distance * i;
        }
    }
    private void Update() => Swipe();
    
    private void Swipe()
    {
        if (Input.GetMouseButton(0))
        {
            _scrollPosition = _scrollbarComponent.value;
        }
        else
        {
            foreach (var currentSwipePosition in _position)
            {
                if (_scrollPosition < currentSwipePosition + _distanceThreshold && _scrollPosition > currentSwipePosition - _distanceThreshold)
                {
                    _scrollbarComponent.value = Mathf.Lerp(_scrollbarComponent.value, currentSwipePosition, animationSpeed);
                }
            }
        }

        for (var i = 0; i < _position.Length; i++)
        {
            var selected = transform.GetChild(i);
            if (_scrollPosition < _position[i] + _distanceThreshold && _scrollPosition > _position[i] - _distanceThreshold)
            {
                selected.localScale = Vector3.Lerp(selected.localScale, _selectedSizeVerVector3, animationSpeed);
                for (var j = 0; j < _position.Length; j++)
                {
                    if (j == i)
                    {
                        continue;
                    }

                    var unselected = transform.GetChild(j);
                    unselected.localScale = Vector3.Lerp(unselected.localScale, Vector3.one, animationSpeed);
                }
            }
        }
    }



}
