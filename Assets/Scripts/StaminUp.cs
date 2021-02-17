using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminUp : MonoBehaviour
{
    public Slider staminaBar;
    //Serialized by WG for Tweaking Gameplay
    [SerializeField] private float maxStamina = 100;
    //Serialized by WG for Tweaking Gameplay
    [SerializeField] private float smallRegen = 0.1f;
    private float currentStamina;

    ButterMovement butterMovement;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);

    public static StaminUp Instance;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
        butterMovement = FindObjectOfType<ButterMovement>();
    }

    public void UseStamina(float amount)
    {

        if (currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            staminaBar.value = currentStamina;
            //StartCoroutine(RegenStamina()); - don't un-comment
        }
        else { Debug.Log("Not enough stamina!"); }
    }

    public float StaminaRemaining()
    {
        return currentStamina;
    }

    public void PowerStamina(float boost)
    {
        currentStamina += boost;
    }

    public void Regenerate()
    {
        StartCoroutine(RegenStamina());
    }

    public void StopRegenerate()
    {
        StopCoroutine(RegenStamina());
    }

    //Small delay before you regen stamina
    public IEnumerator RegenStamina()
    {
        // May need to tweak this slightly - seeing that the BF regenerates even when flying for the first small section of the stamina bar
        yield return new WaitForSeconds(2f);

        while (currentStamina < maxStamina)
        {
            currentStamina += smallRegen;
            staminaBar.value = currentStamina;
            yield return regenTick;
        }
    }
    //Different
}
