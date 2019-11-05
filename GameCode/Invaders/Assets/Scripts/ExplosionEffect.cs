using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Animator>().Play("Explosion");
        Destroy(this.gameObject, .6f);
    }
}
