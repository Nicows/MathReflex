using UnityEngine;

public class AnimationOpen : MonoBehaviour
{
    private bool _isOpen = false;
    private bool _animationFinished = true;
    private Animator _animator;

    private void Start() {
        _animator = GetComponent<Animator>();
    }
    public void AnimationPlayStart(){
        _animationFinished = false;
    }
    public void AnimationPlayFinished()
    {
       _isOpen = !_isOpen;
       _animationFinished = true;
    }
    public void TriggerAnimation()
    {
        if (!_animationFinished) return;
         if(!_isOpen)
            _animator.SetTrigger("Open");
        else
            _animator.SetTrigger("Close");
    }

}
