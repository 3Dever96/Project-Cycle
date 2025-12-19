using UnityEngine;
using ProjectCycle.Database;

namespace ProjectCycle.GameSystems
{
    public class PlayerManager : MonoBehaviour
    {
        public string characterName;
        public Class characterClass;
        public int characterLevel;
        public Color skinColor;
        public ArmorData armor;

        [Header("Stats")]
        public float maxHp;
        public float currentHp;
        public float maxMp;
        public float currentMp;
        public float maxSp;
        public float currentSp;
        public float atk;
        public float def;
        public float mAtk;
        public float mDef;
        public float agi;
        public float crit;
        public float wis;
        public float cha;
        public float wit;

        public void SetStats()
        {
            maxHp = characterClass.baseHp;
            currentHp = maxHp;
            maxMp = characterClass.baseMp;
            currentMp = maxMp;
            maxSp = characterClass.baseSp;
            currentSp = maxSp;
            atk = characterClass.baseAtk;
            def = characterClass.baseDef;
            mAtk = characterClass.baseMAtk;
            mDef = characterClass.baseMDef;
            agi = characterClass.baseAgi;
            crit = characterClass.baseCrit;
            wis = characterClass.baseWis;
            cha = characterClass.baseCha;
            wit = characterClass.baseInt;
        }

        public void TakeDamage(float attack)
        {
            float damage = Mathf.Max(attack - def, 1f);

            currentHp = Mathf.Clamp(currentHp - damage, 0f, maxHp);
        }
    }
}
