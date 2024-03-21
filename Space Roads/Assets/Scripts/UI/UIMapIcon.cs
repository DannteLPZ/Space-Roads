using UnityEngine;
using UnityEngine.UI;

public class UIMapIcon : MonoBehaviour
{
    [SerializeField] private Color _activatedColor;
    [SerializeField] private Color _deactivatedColor;
    [SerializeField] private Animator _animator;

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
        //_animator.SetFloat("_Speed", active ? 0.5f : 0.0f);
    }

    public void SelectIcon()
    {
        Generator.SelectNewIcon(gameObject);

        GameManager.Instance.IncreaseLevel();
        Generator.ActivatePaths(GameManager.Instance.CurrentLevel);
    }
}
