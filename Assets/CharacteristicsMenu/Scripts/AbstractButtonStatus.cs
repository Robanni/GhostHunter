using UnityEngine.UI;
using UnityEngine;


public abstract class AbstractButtonStatus : MonoBehaviour
{
    protected Button _btn;

    private void Awake()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(ButtonIsSelected);
    }

    protected abstract void ButtonIsSelected();
}
