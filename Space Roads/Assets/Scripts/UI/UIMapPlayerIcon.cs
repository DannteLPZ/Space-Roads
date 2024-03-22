using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMapPlayerIcon : MonoBehaviour
{
    [SerializeField] private GameObject _playerIcon;
    [SerializeField] private float _iconSpeed;

    [SerializeField] private GameEvent _onMapTraversed;

    private bool _isMoving;

    public void SetIconPosition(Vector2 position) => _playerIcon.transform.position = position;

    public void TravelToPoint(Vector2 point)
    {
        if (_isMoving == true) return;
        StartCoroutine(Travel(point));
    }

    private IEnumerator Travel(Vector2 point)
    {
        _isMoving = true;
        Vector2 direction = (point - (Vector2)_playerIcon.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;
        _playerIcon.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angle);
        while (Vector2.Distance(_playerIcon.transform.position, point) >= 0.01)
        {
            _playerIcon.transform.Translate(_iconSpeed * Time.deltaTime * Vector2.up);
            yield return null;
        }
        _playerIcon.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0);
        _isMoving = false;
        _onMapTraversed.Invoke();
    }
}
