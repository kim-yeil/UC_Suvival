using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Tilemaps;

public class LevelUpSelectionUI : MonoBehaviour
{
    [SerializeField] private PlayerExperience playerExperience;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerAutoAttack playerAutoAttack;

    [SerializeField] private GameObject selectionPanel;
    [SerializeField] private TMP_Text pendingCount;

    [SerializeField] private float moveSpeedIncrease = 0.5f;
    [SerializeField] private int healAmount = 2;
    [SerializeField] private int damageIncrease = 1;
    [SerializeField] private float attackIntervalDecrease = 0.1f;
    [SerializeField] private float projectileSpeedIncrease = 0.5f;
    [SerializeField] private int maxHealthIncrease = 2;
    [SerializeField] private int spreadProjectileCountIncrease = 1;

    private int[] buttons = new int[3];
    private string[] buttonText = new string[3];
    private int[] upgradeLevels = 
        new int[] {0, 0, 0, 0, 0, 0, 0};
    private static int upgradeKind = 7;
    private int randomIndex;
    private List<int> nums;
    private List<int> selectedNums;

    private int pendingSelections;

    private void Awake()
    {
        selectionPanel.SetActive(false);
        pendingSelections = 0;
    }

    /// <summary>
    /// 오브젝트가 활성화 될 때 유니티가 자동으로 호출하는 함수. <-> OnDisable()
    /// </summary>
    private void OnEnable()
    {
        nums = Enumerable.Range(1, upgradeKind).ToList();
        playerExperience.onLevelUp += ShowSelection;
    }

    private void OnDisable()
    {
        playerExperience.onLevelUp -= ShowSelection;
    }

    void ShowSelection()
    {
        pendingSelections++;
        selectionPanel.SetActive(true);
        pendingCount.text = "Select Count Left : " + pendingSelections;
        Time.timeScale = 0;
    }

    void ShowThreeOptions()
    {
        for (int i = 0; i < 3; i++) SelectRandomNum();
        for (int i = 0; i < 3; i++) buttons[i] = selectedNums[i];


    }

    void SelectRandomNum()
    {
        randomIndex = Random.Range(1, upgradeKind + 1);
        switch (randomIndex) {
            case 1:
                {
                    if (upgradeLevels[randomIndex - 1] == 4)
                    {
                        nums.RemoveAt(randomIndex - 1);
                        SelectRandomNum();
                    }
                    break;
                }
            case 2:
                {
                    if (upgradeLevels[randomIndex - 1] == 6)
                    {
                        nums.RemoveAt(randomIndex - 1);
                        SelectRandomNum();
                    }
                    break;
                }
            case 3:
                {
                    if (upgradeLevels[randomIndex - 1] == 6)
                    {
                        nums.RemoveAt(randomIndex - 1);
                        SelectRandomNum();
                    }
                    break;
                }
            case 4:
                {
                    if (upgradeLevels[randomIndex - 1] == 6)
                    {
                        nums.RemoveAt(randomIndex - 1);
                        SelectRandomNum();
                    }
                    break;
                }
            case 5:
                {
                    
                    break;
                }
            case 6:
                {
                    if (upgradeLevels[randomIndex - 1] == 6)
                    {
                        nums.RemoveAt(randomIndex - 1);
                        SelectRandomNum();
                    }
                    break;
                }
            case 7:
                {
                    if (upgradeLevels[randomIndex - 1] == 6)
                    {
                        nums.RemoveAt(randomIndex - 1);
                        SelectRandomNum();
                    }
                    break;
                }
        }
        ;

        selectedNums.Add(randomIndex);
    }

    public void ChooseSpreadProjectile() // 1
    {
        playerAutoAttack.IncreaseSpreadProjectile(spreadProjectileCountIncrease);

        FinishSelection();
    }

    public void ChooseProjectileSpeed() // 2
    {
        playerAutoAttack.ProjectileSpeedIncrease(projectileSpeedIncrease);

        FinishSelection();
    }

    public void ChooseMaxHealthIncrease() // 3
    {
        playerHealth.MaxHealthIncrease(maxHealthIncrease);

        FinishSelection();
    }

    public void ChooseMoveSpeed() // 4
    {
        playerMovement.IncreaseMoveSpeed(moveSpeedIncrease);

        // 선택 종료.
        FinishSelection();
    }

    public void ChooseHeal() // 5
    {
        playerHealth.Heal(healAmount);

        // 선택 종료.
        FinishSelection();
    }

    public void ChooseDamage() // 6
    {
        playerAutoAttack.IncreaseDamage(damageIncrease);

        FinishSelection();
    }

    public void ChooseAttackSpeed() // 7
    {
        playerAutoAttack.IncreaseAttackSpeed(attackIntervalDecrease);

        FinishSelection();
    }

    void FinishSelection()
    {
        pendingSelections--;
        if (pendingSelections > 0) return;

        selectionPanel.SetActive(false);
        Time.timeScale = 1;
        nums = Enumerable.Range(1, upgradeKind).ToList();
    }
}
