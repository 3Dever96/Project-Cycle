using System.Collections.Generic;
using UnityEngine;

namespace ProjectCycle.GameSystems
{
    [System.Serializable]
    public class ArmorData
    {
        public Length underNeckLength;
        public Length underSleeveLength;
        public Length underTorsoLength;
        public Length underLegLength;

        public Color underShirtColor;
        public Color underPantsColor;

        public void GenerateNewOutfit()
        {
            List<Color> colors = GenerateColorList();

            underShirtColor = colors[Random.Range(0, colors.Count)];
            underPantsColor = colors[Random.Range(0, colors.Count)];

            underNeckLength = (Length)Random.Range(0, 4);
            underSleeveLength = (Length)Random.Range(0, 4);
            underTorsoLength = (Length)Random.Range(0, 4);
            underLegLength = (Length)Random.Range(0, 4);
        }

        private List<Color> GenerateColorList()
        {
            Color primaryColor = GeneratePrimaryColor();
            Color secondaryColor = GenerateComplementColor(primaryColor, 0.33f);
            Color thirdiaryColor = GenerateComplementColor(primaryColor, 0.66f);

            List<Color> colors = new List<Color>();

            colors.Add(primaryColor);
            colors.Add(secondaryColor);
            colors.Add(thirdiaryColor);

            return colors;
        }

        private Color GeneratePrimaryColor()
        {
            float hue = Random.Range(0f, 1f);
            float saturation = 1f;
            float value = 1f;

            Color randomColor = Color.HSVToRGB(hue, saturation, value);
            return randomColor;
        }

        private Color GenerateComplementColor(Color baseColor, float complementHue)
        {
            Color.RGBToHSV(baseColor, out float hue, out float saturation, out float value);

            float complementaryHue = (hue + complementHue) % 1f;

            Color complementaryColor = Color.HSVToRGB(complementaryHue, saturation, value);
            return complementaryColor;
        }

    }
}
