using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowdrop : MonoBehaviour
{
    // Start is called before the first frame update
    private int flatDeal = 1;
    private int dealIncreasePerSkillLevel = 1;
    private int dealIncreasePerPower = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            CharacterState state = collision.transform.GetComponentInChildren<CharacterState>();
            state.ProcessSkill(0, "arrow_rain", flatDeal, dealIncreasePerSkillLevel, dealIncreasePerPower);
        }

    }
}
