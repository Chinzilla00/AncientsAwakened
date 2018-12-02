using Terraria;
using Terraria.Graphics.Shaders;

namespace AAMod.Backgrounds
{
    public class FogSkyData : ScreenShaderData
    {
        private int DataIndex;

        public FogSkyData(string passName) : base(passName)
        {
        }

        private void UpdateFogSky()
        {
            AAPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<AAPlayer>();
            if (AAWorld.mireTiles < 100 && Main.dayTime && ((!AAWorld.downedYamata && !Main.expertMode) || (!AAWorld.downedYamataA && Main.expertMode)))
            {
                return;
            }
            DataIndex = -1;
        }

        public override void Apply()
        {
            UpdateFogSky();
            base.Apply();
        }
    }
}