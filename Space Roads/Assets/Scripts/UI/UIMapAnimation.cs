using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMapAnimation : MonoBehaviour
{
    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();
    public void ShowMap() => _animator.SetBool("OpenMap", true);
    public void CloseMap() => _animator.SetBool("OpenMap", false);
}
