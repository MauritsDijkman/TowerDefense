using UnityEngine;
using TMPro;

[RequireComponent(typeof(BasicEnemy))]

public class MoneyDropBehaviour : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] private float _worth = 0f;

    [Header("UI")]
    private GameObject valueUI = null;


    public void SetWealth(float pWorth)
    {
        _worth = pWorth;
    }

    public void ExecuteBehaviour()
    {
        // Add the value of the enemy to the current money
        if (PlayerMoney.instance != null)
            PlayerMoney.instance.currentMoney += _worth;

        // Spawn the worth UI at the position of the enemy
        if (GameManager.instance != null)
            if (GameManager.instance.GetWorthUI() != null)
                valueUI = (GameObject)Instantiate(GameManager.instance.GetWorthUI(), transform.position, transform.rotation);

        // Set the text of the value UI to the worth of the enemy
        if (valueUI.GetComponentInChildren<TMP_Text>().text != null)
            valueUI.GetComponentInChildren<TMP_Text>().text = "+$" + _worth.ToString();

        // Checks the length of the animation
        if (valueUI.GetComponentInChildren<Animator>().runtimeAnimatorController.animationClips != null)
        {
            AnimationClip[] animation = valueUI.GetComponentInChildren<Animator>().runtimeAnimatorController.animationClips;
            float animationTime = animation[0].length;
            //Debug.Log($"Length of animation : {animationTime}");
            Destroy(valueUI, animationTime);
        }
    }
}
