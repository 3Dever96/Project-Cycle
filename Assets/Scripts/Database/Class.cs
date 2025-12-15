using UnityEngine;

namespace ProjectCycle.Database
{
    [CreateAssetMenu(fileName = "Class", menuName = "Scriptable Objects/Class")]
    public class Class : ScriptableObject
    {
        public string className;

        [Range(10, 16)] public int baseHp = 13;
        [Range(10, 16)] public int baseMp = 13;
        [Range(10, 16)] public int baseSp = 13;
        [Range(10, 16)] public int baseAtk = 13;
        [Range(10, 16)] public int baseDef = 13;
        [Range(10, 16)] public int baseMAtk = 13;
        [Range(10, 16)] public int baseMDef = 13;
        [Range(10, 16)] public int baseAgi = 13;
        [Range(10, 16)] public int baseCrit = 13;
        [Range(10, 16)] public int baseWis = 13;
        [Range(10, 16)] public int baseCha = 13;
        [Range(10, 16)] public int baseInt = 13;
    }
}
