using UnityEngine;
using ProjectCycle.Database;

namespace ProjectCycle.Combat
{
    public class EnemyStats : MonoBehaviour
    {
        [SerializeField] private Class enemyClass;

        public float maxHp;
        public float currentHp;
        public float maxMp;
        public float currentMp;
        public float maxSp;
        public float currentSp;
        public float atk;
        public float def;
        public float mATk;
        public float mDef;
        public float agi;
        public float crit;
        public float wis;
        public float cha;
        public float wit;

        private void Start()
        {
            if (enemyClass != null)
            {
                maxHp = enemyClass.baseHp;
                currentHp = maxHp;
                maxMp = enemyClass.baseMp;
                currentMp = maxMp;
                maxSp = enemyClass.baseSp;
                currentSp = maxSp;
                atk = enemyClass.baseAtk;
                def = enemyClass.baseDef;
                mATk = enemyClass.baseMAtk;
                mDef = enemyClass.baseMDef;
                agi = enemyClass.baseAgi;
                crit = enemyClass.baseCrit;
                wis = enemyClass.baseWis;
                cha = enemyClass.baseCha;
                wit = enemyClass.baseInt;
            }
        }

        public void TakeDamage(float attack)
        {
            float damage = Mathf.Max(attack - def, 1f);

            currentHp = Mathf.Clamp(currentHp - damage, 0f, maxHp);

            if (currentHp == 0f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
