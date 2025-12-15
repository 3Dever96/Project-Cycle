using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ProjectCycle.GameSystems
{
    public class AvatarRenderer : MonoBehaviour
    {
        private PlayerManager playerManager;

        [SerializeField] private SkinnedMeshRenderer body;

        [SerializeField] private Transform neckParent;
        [SerializeField] private Transform sleeveParent;
        [SerializeField] private Transform torsoParent;
        [SerializeField] private Transform legParent;

        private List<GameObject> necks = new List<GameObject>();
        private List<GameObject> sleeves = new List<GameObject>();
        private List<GameObject> torsos = new List<GameObject>();
        private List<GameObject> legs = new List<GameObject>();

        private void Start()
        {
            playerManager = GameManager.instance.PlayerManager;

            PopulateList(necks, neckParent);
            PopulateList(sleeves, sleeveParent);
            PopulateList(torsos, torsoParent);
            PopulateList(legs, legParent);
        }

        private void Update()
        {
            if (playerManager != null)
            {
                body.materials[0].SetColor("_PrimaryColor", playerManager.skinColor);
                UpdateAppearance();
            }
            else
            {
                playerManager = GameManager.instance.PlayerManager;
            }
        }

        private void PopulateList(List<GameObject> list, Transform parent)
        {
            for (var i = 0; i < parent.childCount; i++)
            {
                list.Add(parent.GetChild(i).gameObject);
            }
        }

        private void UpdateAppearance()
        {
            UpdateList(torsos, playerManager.armor.underTorsoLength, playerManager.armor.underShirtColor, Color.white, Color.white);
            if (playerManager.armor.underTorsoLength != Length.None)
            {
                UpdateList(necks, playerManager.armor.underNeckLength, playerManager.armor.underShirtColor, Color.white, Color.white);
            }
            else
            {
                foreach (GameObject go in necks)
                {
                    go.SetActive(false);
                }
            }
            UpdateList(sleeves, playerManager.armor.underSleeveLength, playerManager.armor.underShirtColor, Color.white, Color.white);
            UpdateList(legs, playerManager.armor.underLegLength, playerManager.armor.underPantsColor, Color.white, Color.white);
        }

        private void UpdateList(List<GameObject> list, Length length, Color primaryColor, Color secondaryColor, Color thirdiaryColor)
        {
            foreach (GameObject go in list)
            {
                Material m = go.GetComponent<SkinnedMeshRenderer>().materials[0];
                m.SetColor("_PrimaryColor", primaryColor);
                m.SetColor("_SecondaryColor", secondaryColor);
                m.SetColor("_ThirdiaryColor", thirdiaryColor);
                go.SetActive(false);
            }

            switch (length)
            {
                case Length.Full:
                    list[0].SetActive(true);
                    break;
                case Length.Medium:
                    list[1].SetActive(true);
                    break;
                case Length.Short:
                    list[2].SetActive(true);
                    break;
            }

            if (list.Count == 4)
            {
                list[3].SetActive(true);
            }
        }
    }
}
