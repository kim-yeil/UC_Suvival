using UnityEngine;
using TMPro;
using Unity.UI;
using UnityEngine.UI;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField] private int startLevel = 1;
    [SerializeField] private int startRequiredEXP = 5;
    [SerializeField] private int expIncrement = 5;

    private int currentLevel;
    private int currentEXP;
    private int requiredEXP;

    [SerializeField] private TMP_Text expText;
    [SerializeField] private Image expImage;

    private void Awake()
    {
        currentLevel = startLevel;
        currentEXP = 0;
        requiredEXP = startRequiredEXP;
        expImage.fillAmount = 0;
    }

    private void Update()
    {
        expText.text = $"Lv.{currentLevel} : {currentEXP} / {requiredEXP}";
    }

    public void AddEXP(int amount)
    {
        currentEXP += amount;

        // LevelUp Check
        CheckLevelUp();
        expImage.fillAmount = (float)currentEXP / (float)requiredEXP;
    }

    void CheckLevelUp()
    {
        while (currentEXP >= requiredEXP)
        {
            currentEXP -= requiredEXP;
            currentLevel++;
            requiredEXP += expIncrement;
            Debug.Log("LevelUp! " + (currentLevel - 1) + " -> " + currentLevel);
        }
    }
}
