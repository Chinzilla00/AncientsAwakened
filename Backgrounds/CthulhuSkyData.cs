using Terraria;
using Terraria.Graphics.Shaders;
using BaseMod;

namespace AAMod.Backgrounds
{
    public class CthulhuSkyData : ScreenShaderData
    {
        private int DataIndex;

        public CthulhuSkyData(string passName) : base(passName)
        {
        }

        private void UpdateStormSky()
        {
            AAPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<AAPlayer>();
            if (Main.player[Main.myPlayer].InZone("Ocean") && !AAWorld.downedSoC && AAWorld.downedAllAncients)
            {
                return;
            }
            DataIndex = -1;
        }

        public override void Apply()
        {
            UpdateStormSky();
            base.Apply();
        }
    }
}