using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//With a scrollview and differents gameobjects, this script will allow to swipe between them and clamp the position of the scrollview
public class SwipeScrollView : MonoBehaviour
{

    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private RectTransform[] _content;
    private int _currentIndex;
    private bool _isSwiping;

    //if scrollrect velocity is under a certain value, focus on the nearest object
    private const float _minVelocity = 500f;
    private void Start()
    {
        // InvokeRepeating("DisplayVelocity", 0, 0.5f);
        // _scrollRect.content = _content;
    }
    private void DisplayVelocity()
    {
        Debug.Log(_scrollRect.velocity.x);
        // yield return null;
    }

    private void Update()
    {
        //if the screen is touched
        if (Input.touchCount > 0 || Input.GetMouseButton(0)) _isSwiping = true;
        else _isSwiping = false;

        StartToClampToNearestObject();
        // //check if the scrollrect is moving
        // if (_scrollRect.velocity.x > _minVelocity || _scrollRect.velocity.x < -_minVelocity)
        // {
        //     _isSwiping = true;
        // }
        // else
        // {
        //     _isSwiping = false;
        // }


    }
    public void StartToClampToNearestObject()
    {
        if(_isSwiping) return;
        if (_scrollRect.velocity.x < _minVelocity || _scrollRect.velocity.x > -_minVelocity)
        {
            //anchor the current index in middle of the screen
            // _content.anchoredPosition = new Vector2(_content.anchoredPosition.x, 0);
            //clamp the position of the scrollview
            _scrollRect.velocity = Vector2.zero;

        }
    }


}
