using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class BaseCounter : ScriptableObject
{
    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact()!");
    }
}
