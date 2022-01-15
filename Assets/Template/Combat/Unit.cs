using UnityEngine;

public delegate void VoidDelegate();

public class Unit : MonoBehaviour {

    public VoidDelegate onTakeDamage;
    public VoidDelegate onDeath;
    public Rigidbody2D rb;
    public float maxHp = 5, currentHP = 5;
    public bool canBeHurt = true;
    public void TakeDamage(float amt) {

        if (canBeHurt) {
            currentHP -= amt;
            if (currentHP <= 0) {
                currentHP = 0;
                if (onDeath != null) onDeath.Invoke();
            }
            if (onTakeDamage != null) onTakeDamage.Invoke();
        }


    }

}
