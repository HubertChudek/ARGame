using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameManager.TARGET_OBJECT_TAG)
        {
            GameManager.Instance.RemoveBlock(other.gameObject);
        }
    }
}
