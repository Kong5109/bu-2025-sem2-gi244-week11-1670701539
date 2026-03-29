using System;
using System.Collections;
using UnityEngine;

public class StunPowerUp : MonoBehaviour
{
    public event Action<bool> OnStunEnemy;
    public float duration = 5f;

    private Enemy[] enemies;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController playerController))
        {
            enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
            foreach (Enemy enemy in enemies)
            {
                enemy.DoStun(duration);
            }
        }
    }
}
