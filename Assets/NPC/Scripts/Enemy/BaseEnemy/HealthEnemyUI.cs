using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthEnemyUI : MonoBehaviour
{
    
    [SerializeField] BaseEnemy _enemy;
    [SerializeField] Transform _healthbarCanvas;

    private Transform _playerCamera;
    private Image _Image;

    private void Start()
    {
        _Image = GetComponent<Image>();
        _playerCamera = FindObjectOfType<CameraMovement>().transform;
        _enemy.onHealthChanged += HealthChanged;
    }

    private void OnDestroy()
    {
        _enemy.onHealthChanged -= HealthChanged;
    }

    private void LateUpdate()
    {
        _healthbarCanvas.LookAt(_playerCamera);
    }

    private void HealthChanged(float percent)
    {
        _Image.fillAmount = 1-percent;
    }
}
