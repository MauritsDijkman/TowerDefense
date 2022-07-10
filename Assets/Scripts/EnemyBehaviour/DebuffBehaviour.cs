using System.Collections;
using UnityEngine;

public class DebuffBehaviour : MonoBehaviour
{
    private bool debuffActive = false;
    private CommandStack _commandStack = new CommandStack();

    public void ExecuteDebuff(float pDebuffTime)
    {
        if (pDebuffTime > 0)
        {
            if (!debuffActive)
                StartCoroutine(Debuff(pDebuffTime));
        }
        else
            throw new System.ArgumentOutOfRangeException("pDebuffTime", "Only positive number is allowed for ExecuteBehaviour!");
    }

    private IEnumerator Debuff(float pDebuffTime)
    {
        debuffActive = true;

        _commandStack.ExecuteCommand(new DebuffToggle(gameObject));

        yield return new WaitForSeconds(pDebuffTime);

        _commandStack.UndoLastCommand();

        debuffActive = false;
    }
}
