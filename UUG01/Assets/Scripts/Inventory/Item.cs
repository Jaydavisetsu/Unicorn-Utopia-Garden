using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Item : MonoBehaviour
{
    public ItemData data;

    [HideInInspector]
    public Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
}
// Source: https://www.youtube.com/watch?v=_eaMfRepWNY&list=PL4PNgDjMajPN51E5WzEi7cXzJ16BCHZXl&index=13
