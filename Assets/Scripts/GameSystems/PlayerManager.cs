using System.Collections.Generic;
using UnityEngine;

namespace ProjectCycle.GameSystems
{
	public class PlayerManager : MonoBehaviour
	{
		[Header("General Information")]
		public string characterName;
		// public Class characterClass;
		public int characterLevel;
		public int characterExp;
		
		[Header("Stats")]
		public float maxVitality;
		public float strength;
		public float defense;
		public float maxMagic;
		public float magicStrength;
		public float magicDefense;
		public float maxEndurance;
		public float agility;
		public float focus;
		public float wisdom;
		public float charisma;
		public float intellect;
		
		// [Header("Appearance")]
		// public int hairStyle;
		// public Color hairColor;
		// public int teethStyle;
		// public Color skinTone;
		// public Color eyeColor;
		// public int height;
		
		// [Header("Equipment")]
		// public Weapon weapon;
		// public ArmorData armor;
		/*
			Armor Data:
				public Armor armor;
				
				public int underNeckLength;
				public int underSleeveLength;
				public int underTorsoLength;
				public int underLegLength;
				
				public int armorNeckLength;
				public int armorSleeveLength;
				public int armorTorsoLength;
				public int armorLegLength;
				
				public int shoeType;
				public int gloveType;
				
				public Color shirtColor;
				public Color pantsColor;
				public Color armorColor;
				public Color shoeColor;
				public Color gloveColor;
		*/
		// public Shield shield;
		// public Accessory accessory;
		
		// [Header("Skills")]
		// public List<Skill> skills;
	}
}
