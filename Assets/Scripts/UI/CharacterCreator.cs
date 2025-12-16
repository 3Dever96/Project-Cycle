using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectCycle.Database;
using ProjectCycle.GameSystems;

namespace ProjectCycle.UI
{
    public class CharacterCreator : MonoBehaviour
    {
        [System.Serializable]
        public class ClassData
        {
            public Class role;
            public ArmorData armor;
        }

        [SerializeField] private List<ClassData> classes;
        [SerializeField] private TMPro.TMP_InputField nameField;
        [SerializeField] private Slider hue;
        [SerializeField] private Slider saturation;
        [SerializeField] private Slider brightness;

        [SerializeField] private TMPro.TMP_Text className;
        [SerializeField] private TMPro.TMP_Text hpValue;
        [SerializeField] private TMPro.TMP_Text mpValue;
        [SerializeField] private TMPro.TMP_Text spValue;
        [SerializeField] private TMPro.TMP_Text atkValue;
        [SerializeField] private TMPro.TMP_Text defValue;
        [SerializeField] private TMPro.TMP_Text mAtkValue;
        [SerializeField] private TMPro.TMP_Text mDefValue;
        [SerializeField] private TMPro.TMP_Text agiValue;
        [SerializeField] private TMPro.TMP_Text critValue;
        [SerializeField] private TMPro.TMP_Text wisValue;
        [SerializeField] private TMPro.TMP_Text chaValue;
        [SerializeField] private TMPro.TMP_Text intValue;

        private int index;

        private void Start()
        {
            PopulateArmors();

            hue.value = Random.value;
            saturation.value = Random.value;
            brightness.value = Random.value;

            UpdatePlayerManager();
        }

        private void Update()
        {
            className.text = classes[index].role.className;
            hpValue.text = classes[index].role.baseHp.ToString();
            mpValue.text = classes[index].role.baseMp.ToString();
            spValue.text = classes[index].role.baseSp.ToString();
            atkValue.text = classes[index].role.baseAtk.ToString();
            defValue.text = classes[index].role.baseDef.ToString();
            mAtkValue.text = classes[index].role.baseMAtk.ToString();
            mDefValue.text = classes[index].role.baseMDef.ToString();
            agiValue.text = classes[index].role.baseAgi.ToString();
            critValue.text = classes[index].role.baseCrit.ToString();
            wisValue.text = classes[index].role.baseWis.ToString();
            chaValue.text = classes[index].role.baseCha.ToString();
            intValue.text = classes[index].role.baseInt.ToString();
        }

        private void PopulateArmors()
        {
            foreach (ClassData c in classes)
            {
                c.armor.GenerateNewOutfit();
            }
        }

        public void UpdatePlayerManager()
        {
            GameManager.instance.PlayerManager.characterName = nameField.text;

            GameManager.instance.PlayerManager.skinColor = Color.HSVToRGB(hue.value, saturation.value, brightness.value);

            GameManager.instance.PlayerManager.characterClass = classes[index].role;
            GameManager.instance.PlayerManager.armor = classes[index].armor;
        }

        public void IncreaseIndex()
        {
            index++;
            if (index > classes.Count - 1)
            {
                index = 0;
            }
            UpdatePlayerManager();
        }

        public void DecreaseIndex()
        {
            index--;
            if (index < 0)
            {
                index = classes.Count - 1;
            }
            UpdatePlayerManager();
        }

        public void OnContinue()
        {
            GameManager.instance.DungeonManager.BeginningDungeon(false);
            GameManager.instance.SceneHandler.MoveToScene(2);
        }
    }
}
