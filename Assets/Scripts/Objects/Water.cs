using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public void ConfigureFor(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
    }
}
