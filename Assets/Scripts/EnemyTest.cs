using UnityEngine;

public class EnemyTest : MonoBehaviour, IDamagable
{
    private int _Health = 4;

    public void ApplyDamage()
    {
        if (_Health > 0)
            _Health--;
        if (_Health == 0)
            Death();
        else
            Debug.Log("POPAL PIDOR");
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
