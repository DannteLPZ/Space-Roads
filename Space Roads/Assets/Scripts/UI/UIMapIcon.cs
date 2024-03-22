using UnityEngine;
using UnityEngine.UI;

public class UIMapIcon : MonoBehaviour
{
    [Header("Visualization")]
    [SerializeField] private Color _activatedColor;
    [SerializeField] private Color _deactivatedColor;

    [Header("Event")]
    [SerializeField] private GameEvent _iconEvent;

    private Image _image;
    private Button _button;

    [HideInInspector]
    public GameObject ParentIcon;
    [HideInInspector]
    public UIMapGenerator Generator;
    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        ToggleBehaviour(false);
    }

    public void ToggleBehaviour(bool active)
    {
        _image.color = active ? _activatedColor : _deactivatedColor;
        _button.enabled = active;
    }

    public void SelectIcon() => Generator.SelectNewIcon(gameObject);

    public void InvokeEvent() => _iconEvent.Invoke();
}
