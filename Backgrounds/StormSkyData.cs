using Terraria;
using Terraria.Graphics.Shaders;

namespace AAMod.Backgrounds
{
    public class StormSkyData : ScreenShaderData
    {
        private int DataIndex;

        public StormSkyData(string passName) : base(passName)
        {
        }

        private void UpdateStormSky()
        {
            AAPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<AAPlayer>();
            if (AAWorld.stormTiles < 1)
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