using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHitTrigger : MonoBehaviour
{
    BaseProjectileSpell sourceSpell;
    // Start is called before the first frame update
    void Start()
    {
        // ...
    }

    // Update is called once per frame
    void Update()
    {
        // ...
    }

    void OnTriggerEnter (Collider other)
    {
        this.sourceSpell.HandleCollision( this.gameObject, other );
    }
    public void SetSourceSpell(BaseProjectileSpell source){
        this.sourceSpell = source;
    }
}
