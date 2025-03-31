using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Healtj System Configuration")]
    [SerializeField] int maxHealth; //Vida máxima del enemigo
    [SerializeField] int currentHealth; //Vida actual del enemigo

    [Header("Feedback Configuration")]
    [SerializeField] Material baseMat; //Material base enemigo (aspecto normal)
    [SerializeField] Material damagedMat; //Material feedback de recibir daño
    [SerializeField] GameObject deathEffect; //VFX que se activa al morir

    //Autoreferencias privadas
    MeshRenderer enemyRend; //Ref a la mesh del enemigo, nos permite acceder a su material y modificarlo


    private void Awake()
    {
        enemyRend = GetComponent<MeshRenderer>();
        baseMat = enemyRend.material; //Al inicio del juego se almacena el material base del enemigo
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0; //La vida no puede bajar de cero
            deathEffect.SetActive(true); //Al morir se activa el VFX de muerte
            deathEffect.transform.position = transform.position; //Se mueve el VFX de muerte a la posición correcta
            gameObject.SetActive(false); //Se apaga el modelo visual del enemigo
        }
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage; //Se aplica el daño recibido desde fuera
        enemyRend.material = damagedMat; //Se aplica el feedback visual del daño
        Invoke(nameof(ResetDamageMaterial), 0.1f);
    }


    void ResetDamageMaterial()
    {
        //Devuelve al enemigo su aspecto original después de ser dañado
        enemyRend.material = baseMat;
    }
}
