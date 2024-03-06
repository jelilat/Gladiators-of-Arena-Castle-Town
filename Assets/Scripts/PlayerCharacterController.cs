using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacterController : MonoBehaviour
{
    public GameObject character1;
    public GameObject character2;
    public Slider c1HpBar;
    public Slider c1EnergyBar;
    public Slider c2HpBar;
    public Slider c2EnergyBar;

    private Animator c1Animator;
    private Animator c2Animator;

    public Vector3 character1InitialPosition;
    public Vector3 character2InitialPosition;

    private enum BattleAction
    {
        QuickAttack = 1,
        PreciseAttack = 2,
        HeavyAttack = 3,
        MoveRight = 4,
        MoveLeft = 5,
        Rest = 6
    }

    void Start()
    {
        c1Animator = character1.GetComponent<Animator>();
        c2Animator = character2.GetComponent<Animator>();

        character1InitialPosition = character1.transform.position;
        if (character1InitialPosition.x < 0)
        {
            character1InitialPosition.x *= -1;
        }
        character2InitialPosition = character2.transform.position;
    }

    public void StartAnimation()
    {
        Debug.Log("StartAnimation");
        ProcessGameData(GameData.data);
    }

    private void ProcessGameData(List<string> data)
    {
        StartCoroutine(ProcessGameDataCoroutine(data));
    }

    private IEnumerator ProcessGameDataCoroutine(List<string> data)
    {
        int numberOfTurns = Convert.ToInt32(data[0], 16);
        Debug.Log($"Number of Turns: {numberOfTurns}");

        // Remove the first element
        data.RemoveAt(0);

        for (int i = 0; i < numberOfTurns; i++)
        {
            Debug.Log($"Processing turn {i}");
            bool shouldContinue = true;

            try
            {
                int index = 1 + (i * 12);

                // Parse the hex values into integers
                int c1Hp = Convert.ToInt32(data[index + 1], 16);
                int c2Hp = Convert.ToInt32(data[index + 2], 16);
                float c1Position = Convert.ToInt32(data[index + 3], 16) * 0.2f;
                float c2Position = Convert.ToInt32(data[index + 4], 16) * 0.2f;
                int c1Energy = Convert.ToInt32(data[index + 5], 16);
                int c2Energy = Convert.ToInt32(data[index + 6], 16);
                int c1Action = Convert.ToInt32(data[index + 7], 16);
                int c2Action = Convert.ToInt32(data[index + 8], 16);

                // Update the UI bars
                c1HpBar.value = c1Hp;
                c2HpBar.value = c2Hp;
                c1EnergyBar.value = c1Energy;
                c2EnergyBar.value = c2Energy;

                if (c1Position != 0)
                {
                    ExecuteAction(c1Animator, BattleAction.MoveRight);
                }
                if (c2Position != 0)
                {
                    ExecuteAction(c2Animator, BattleAction.MoveLeft);
                }

                Debug.Log(c1Position);
                Debug.Log(c2Position);

                // Move the characters
                character1.transform.position = new Vector3(c1Position + character1InitialPosition.x, character1InitialPosition.y, character1InitialPosition.z);
                character2.transform.position = new Vector3(c2Position + character1InitialPosition.x, character2.transform.position.y, character2.transform.position.z);

                // Execute actions for each character
                ExecuteAction(c1Animator, (BattleAction)c1Action);
                ExecuteAction(c2Animator, (BattleAction)c2Action);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error processing turn {i}: {ex.Message}");
                shouldContinue = false;
            }

            // Wait for 5 seconds outside the try-catch block
            if (shouldContinue)
            {
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                yield break; // Exit the coroutine on error
            }
        }
    }

    private void ExecuteAction(Animator animator, BattleAction action)
    {
        Debug.Log(action);
        ResetAnimator(animator);
        switch (action)
        {
            case BattleAction.QuickAttack:
                animator.SetBool("Rest", false);
                animator.SetTrigger("QuickAttack");
                // animator.SetBool("QuickAttack", true);
                break;
            case BattleAction.PreciseAttack:
                animator.SetTrigger("PreciseAttack");
                break;
            case BattleAction.HeavyAttack:
                animator.SetTrigger("HeavyAttack");
                break;
            case BattleAction.MoveRight:
                // Move the character right
                animator.SetBool("Move", true);
                break;
            case BattleAction.MoveLeft:
                // Move the character left
                animator.SetBool("Move", true);
                break;
            case BattleAction.Rest:
                animator.SetBool("Rest", true);
                break;
        }
    }

    private void ResetAnimator(Animator animator)
    {
        animator.ResetTrigger("QuickAttack");
        // animator.ResetTrigger("PreciseAttack");
        // animator.ResetTrigger("HeavyAttack");
        // animator.SetBool("Move", false);
    }
}
