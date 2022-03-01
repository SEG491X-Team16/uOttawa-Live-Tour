using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ToggleSwitch : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private bool _isOn = false;
    public bool isOn{
        get{
            return _isOn;
        }
    }

    [SerializeField]
    private RectTransform toggleIndicator;
    [SerializeField]
    private Image backgroundImage;
    [SerializeField]
    private Color onColor;
    [SerializeField]
    private Color offColor;
    private float offX;
    private float onX;
    [SerializeField]
    private float tweenTime =0.25f;
    private AudioSource audioSource;

    public delegate void ValueChanged(bool value);
    public event ValueChanged valueChanged;


    // Start is called before the first frame update
    void Start()
    {
        offX = toggleIndicator.anchoredPosition.x;
        onX = backgroundImage.rectTransform.rect.width - toggleIndicator.rect.width;
        audioSource = this.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Toggle(isOn);
    }

    private void Toggle(bool value, bool playSFX= true)
    {
        if(value != isOn)
        {
            _isOn = value;
            ToggleColor(isOn);
            MoveIndicator(isOn);

            if(playSFX)
                audioSource.Play();
            if(valueChanged != null)
                valueChanged(isOn);
        }
    }
    
    private void ToggleColor(bool value)
    {
        if (value)
            backgroundImage.DOColor(onColor,tweenTime);
        else
            backgroundImage.DOColor(offColor,tweenTime);

    }

    private void MoveIndicator (bool value)
    {
        if (value)
            toggleIndicator.DOAnchorPosX(onX,tweenTime);
        else
            toggleIndicator.DOAnchorPosX(offX,tweenTime);
        
    }

    public void OnPointerDown (PointerEventData eventData)
    {
        Toggle(!isOn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

