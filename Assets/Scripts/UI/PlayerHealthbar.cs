using UnityEngine;
using UnityEngine.UI;
using ProjectCycle.GameSystems;

namespace ProjectCycle.UI
{
    public class PlayerHealthbar : MonoBehaviour
    {
        [SerializeField] private Slider hpBar;
        [SerializeField] private Slider mpBar;
        [SerializeField] private Slider spBar;

        private void Update()
        {
            hpBar.value = GameManager.instance.PlayerManager.currentHp / GameManager.instance.PlayerManager.maxHp;
            mpBar.value = GameManager.instance.PlayerManager.currentMp / GameManager.instance.PlayerManager.maxMp;
            spBar.value = GameManager.instance.PlayerManager.currentSp / GameManager.instance.PlayerManager.maxSp;
        }
    }
}
