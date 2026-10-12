using Unity.VisualScripting;
using UnityEngine;

public class LevelUpSelectionUI : MonoBehaviour
{
    [SerializeField] private PlayerExperience playerExperience;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private GameObject selectionPanel;

    [SerializeField] private float moveSpeedIncrease = 0.5f;
    [SerializeField] private int healAmount = 2;

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
        Time.timeScale = 0;
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

    void FinishSelection()
    {
        pendingSelections--;
        if (pendingSelections > 0) return;

        selectionPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
