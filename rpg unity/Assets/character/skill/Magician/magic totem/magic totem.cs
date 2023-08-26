using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTotem : MonoBehaviour
{
    float dropTime = 0.3f;
    float _time = 0f;

    private float flatDeal = 1;
    private float dealIncreasePerSkillLevel = 1;
    private float dealIncreasePerPower = 1;

    private float flatPower = 1;
    private float powerIncreasePerSkillLevel = 1;

    public GameObject aura;

    private float duration = 10f;

    public float caseterPower = 1f;
    public float casterSkillLevel = 1f;
    public float casterCriticalPercent = 1f;
    public float casterCriticalDamage = 1f;
    public float Deal;
    public float Power;

    private void Awake()
    {
        StartCoroutine(Vanish(duration));
        Deal = SkillManager.instance.CaculateCharacterSkillDamage(casterSkillLevel, caseterPower,
            flatDeal, dealIncreasePerSkillLevel, dealIncreasePerPower,
            casterCriticalPercent, casterCriticalDamage, true);
        Power = SkillManager.instance.CaculateCharacterSkillDamage(casterSkillLevel, caseterPower,
            flatPower, powerIncreasePerSkillLevel, 0);
    }

    IEnumerator Vanish(float duration)
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        GetComponent<Animator>().SetTrigger("vanish");
        Destroy(gameObject, 0.45f);
    }

    void Start()
    {
        StartCoroutine(clock());
    }
    
    IEnumerator clock()
    {
        while(_time <= dropTime)
        {
            _time += Time.deltaTime;
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            if (_time <= dropTime)
            {
                CharacterState state = collision.transform.GetComponentInChildren<CharacterState>();
                state.ProcessSkill(0, Deal);
            }
        }
        else if (collision.CompareTag("Player"))
        {
            CharacterState state = collision.transform.GetComponentInChildren<CharacterState>();
            state.ProcessSkill(3, Power);
        }
        Debug.Log(collision.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CharacterState state = collision.transform.GetComponentInChildren<CharacterState>();
            state.ProcessSkill(3, -Power);
        }
    }
    
}