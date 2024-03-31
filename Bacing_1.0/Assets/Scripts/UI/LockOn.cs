using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    private Image _image;

    public bool bLockOff;

    public void Awake()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {
        if (!bLockOff)
            return;

        _image.fillAmount -= 3 * Time.unscaledDeltaTime;

        if (_image.fillAmount >= 0)
            Destroy(gameObject);
    }
}
