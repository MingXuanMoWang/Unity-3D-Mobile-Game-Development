using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocket : Rocket
{

    private void OnTriggerEnter(Collider other)
    {
        if(!other.tag.Equals("Player"))
        {
            return;
        }
        Destroy(this.gameObject);
    }
}
