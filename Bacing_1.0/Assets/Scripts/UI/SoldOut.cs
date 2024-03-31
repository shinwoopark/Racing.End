using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldOut : MonoBehaviour
{
    private Image _image;

    public void Awake()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {
        _image.fillAmount += 3 * Time.unscaledDeltaTime;
    }
}
