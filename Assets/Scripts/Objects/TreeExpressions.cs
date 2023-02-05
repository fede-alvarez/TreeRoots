using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeExpressions : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    void Start()
    {
        EventManager.TreeDamaged += OnTreeDamaged;
    }

    private void OnTreeDamaged(int healthPercentage)
    {
        animator.SetInteger("HealthPercentage", healthPercentage);
    }

    private void OnDestroy() {
        EventManager.TreeDamaged -= OnTreeDamaged;
    }
}
