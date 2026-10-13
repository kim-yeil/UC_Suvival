using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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
        Time.timeScale = 0;
    }
    
    public void ChooseProjectileSpeed()
    {
        playerAutoAttack.ProjectileSpeedIncrease(projectileSpeedIncrease);

        FinishSelection();
    }

    public void ChooseMaxHealthIncrease()
    {
        playerHealth.MaxHealthIncrease(maxHealthIncrease);

        FinishSelection();
    }

    public void ChooseMoveSpeed()
    {
        playerMovement.IncreaseMoveSpeed(moveSpeedIncrease);

        // 선택 종료.
        FinishSelection();
    }

    public void ChooseHeal()
    {
        playerHealth.Heal(healAmount);

        // 선택 종료.
        FinishSelection();
    }

    public void ChooseDamage()
    {
        playerAutoAttack.IncreaseDamage(damageIncrease);

        FinishSelection();
    }

    public void ChooseAttackSpeed()
    {
        playerAutoAttack.IncreaseAttackSpeed(attackIntervalDecrease);

        FinishSelection();
    }

    void ShowFourOptions()
    {

    }

    void FinishSelection()
    {
        pendingSelections--;
        if (pendingSelections > 0) return;

        selectionPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
