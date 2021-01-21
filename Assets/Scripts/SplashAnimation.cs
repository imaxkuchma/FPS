using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashAnimation : MonoBehaviour
{
    private Image _curtainImage;
    private float _showDuration = 1.0f;

    [SerializeField]
    [Range(0, 10)]
    private float Duration = 1.0f;

    void Start()
    {
        _curtainImage = gameObject.GetComponent<Image>();

        if (_curtainImage != null)
            StartCoroutine(PlayAnimation(_curtainImage, Duration, delegate { SceneManager.LoadScene(1); }));          
    }

    IEnumerator PlayAnimation(Image _image, float _duration, Action _action = null)
    {
        _duration = (_duration - _showDuration) / 2;

        Color currentColor = _image.color;

        Color visibleColor = _image.color;
        visibleColor.a = 0f;

        float counter = 0;

        while (counter < _duration)
        {
            counter += Time.deltaTime;
            _image.color = Color.Lerp(currentColor, visibleColor, counter / _duration);
            yield return null;
        }

        yield return new WaitForSeconds(_showDuration);

        counter = 0;

        while (counter < _duration)
        {
            counter += Time.deltaTime;
            _image.color = Color.Lerp(visibleColor, currentColor, counter / _duration);
            yield return null;
        }

        _action?.Invoke();
    }
}
