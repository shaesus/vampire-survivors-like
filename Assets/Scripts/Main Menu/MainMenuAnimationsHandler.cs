using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MainMenuAnimationsHandler : MonoBehaviour
{
    public event Action FadeEnded;
    
    [SerializeField] private Animator[] _mainMenuEnemiesAnimators;
    [SerializeField] private Animator _fadingPanelAnimator;
    
    [SerializeField] private float _fadeDelay = 0.8f;
    [SerializeField] private float _fadeDuration = 0.5f;

    public void BeginStartRoutine()
    {
        StartEnemiesAnimation();
        StartCoroutine(StartFade());
    }
    
    private void StartEnemiesAnimation()
    {
        foreach (var animator in _mainMenuEnemiesAnimators)
        {
            animator.SetTrigger("Attack");
        }
    }

    private IEnumerator StartFade()
    {
        yield return new WaitForSeconds(_fadeDelay);

        _fadingPanelAnimator.gameObject.SetActive(true);
        _fadingPanelAnimator.SetTrigger("Fade");

        yield return new WaitForSeconds(_fadeDuration);
        
        FadeEnded?.Invoke();
    }
}
