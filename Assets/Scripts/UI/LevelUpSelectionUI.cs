using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public enum Upgrades
{
    SpreadProjectile,
    ProjectileSpeed,
    MaxHealth,
    MoveSpeed,
    Heal,
    ProjectileDamage,
    AttackCooldown,
    HpRegen
}

public class LevelUpSelectionUI : MonoBehaviour
{
    [SerializeField] private PlayerExperience playerExperience;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerAutoAttack playerAutoAttack;
    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private GameObject selectionPanel;
    [SerializeField] private TMP_Text pendingCount;

    [SerializeField] private TMP_Text[] buttonTexts = new TMP_Text[3];

    private string[] upgradeTexts = new string[]
        {"Spread Projectile + 1",
        "Projectile Speed + 0.5",
        "Max Health + 2",
        "Move Speed + 0.5",
        "Heal (+ 2 HP)",
        "Projectile Damage + 1",
        "Attack Cooldown - 0.1",
        "HP Regen (+ 2 HP / 5s)"};

    [SerializeField] private float moveSpeedIncrease = 0.5f;
    [SerializeField] private int healAmount = 2;
    [SerializeField] private int damageIncrease = 1;
    [SerializeField] private float attackIntervalDecrease = 0.1f;
    [SerializeField] private float projectileSpeedIncrease = 0.5f;
    [SerializeField] private int maxHealthIncrease = 4;
    [SerializeField] private int hpRegenIncrease = 1;
    [SerializeField] private int spreadProjectileCountIncrease = 1;
        
    private int[] buttons = new int[3];
    private int[] upgradeLevels = 
        new int[] {0, 0, 0, 0, 0, 0, 0, 0};
    private int[] maxUpgradeLevels =
        new int[] {4, 6, 10000, 6, 10000, 10000, 5, 3};
    private static int upgradeKind = 8;
    private List<int> nums;
    private List<int> shuffledNums;
    private int selectedIndex;

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
        ShowThreeOptions();
        Time.timeScale = 0;
    }

    void ShowThreeOptions()
    {
        nums = Enumerable.Range(0, upgradeKind).ToList();
        shuffledNums = nums.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < 3; i++)
        {
            buttons[i] = SelectRandomNum();
            buttonTexts[i].text = upgradeTexts[buttons[i]];
            if (upgradeLevels[buttons[i]] == maxUpgradeLevels[buttons[i]] - 1)
                buttonTexts[i].text += " (MAX)";

            string test = "";
            for (int j = 0; j < shuffledNums.Count; j++)
            {
                test += shuffledNums[j] + " ";
            }
            Debug.Log(test);
        }
    }

    public void Button1Pressed()
    {
        upgradeLevels[buttons[0]]++;
        ExecuteUpgrade(buttons[0]);
    }

    public void Button2Pressed()
    {
        upgradeLevels[buttons[1]]++;
        ExecuteUpgrade(buttons[1]);
    }

    public void Button3Pressed()
    {
        upgradeLevels[buttons[2]]++;
        ExecuteUpgrade(buttons[2]);
    }

    void ExecuteUpgrade(int index)
    {
        switch (index)
        {
            case 0: ChooseSpreadProjectile(); break;
            case 1: ChooseProjectileSpeed(); break;
            case 2: ChooseMaxHealthIncrease(); break;
            case 3: ChooseMoveSpeed(); break;
            case 4: ChooseHeal(); break;
            case 5: ChooseDamage(); break;
            case 6: ChooseAttackSpeed(); break;
            case 7: ChooseHPRegen(); break;
        }
    }

    int SelectRandomNum()
    {
        while(true)
        {
            selectedIndex = shuffledNums[^1];
            shuffledNums.RemoveAt(shuffledNums.Count - 1);

            if (upgradeLevels[selectedIndex] == maxUpgradeLevels[selectedIndex])
            {
                continue;
            }
            else
                break;
        }

        return selectedIndex;
    }

    public void ChooseSpreadProjectile() // 0
    {
        playerAutoAttack.IncreaseSpreadProjectile(spreadProjectileCountIncrease);

        FinishSelection();
    }

    public void ChooseProjectileSpeed() // 1
    {
        playerAutoAttack.ProjectileSpeedIncrease(projectileSpeedIncrease);

        FinishSelection();
    }

    public void ChooseMaxHealthIncrease() // 2
    {
        playerStats.MaxHealthIncrease(maxHealthIncrease);

        FinishSelection();
    }

    public void ChooseMoveSpeed() // 3
    {
        playerMovement.IncreaseMoveSpeed(moveSpeedIncrease);

        // 선택 종료.
        FinishSelection();
    }

    public void ChooseHeal() // 4
    {
        playerHealth.Heal(healAmount);

        // 선택 종료.
        FinishSelection();
    }

    public void ChooseDamage() // 5
    {
        playerAutoAttack.IncreaseDamage(damageIncrease);

        FinishSelection();
    }

    public void ChooseAttackSpeed() // 6
    {
        playerAutoAttack.IncreaseAttackSpeed(attackIntervalDecrease);

        FinishSelection();
    }

    public void ChooseHPRegen() // 7
    {
        playerStats.HPRegenIncrease(hpRegenIncrease);

        FinishSelection();
    }

    void FinishSelection()
    {
        pendingSelections--;
        if (pendingSelections > 0) return;

        selectionPanel.SetActive(false);
        Time.timeScale = 1;

        string test = "";
        for (int i = 0; i < upgradeKind; i++)
        {
            test += upgradeLevels[i] + " ";
        }
        Debug.Log(test);
    }
}
