using Terraria;
using Terraria.Graphics.Shaders;

namespace AAMod.Backgrounds
{
    public class InfernoSkyData : ScreenShaderData
    {
        private int DataIndex;

        public InfernoSkyData(string passName) : base(passName)
        {
        }

        private void UpdateInfernoSky()
        {
            AAPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<AAPlayer>();
            if (AAWorld.infernoTiles < 100)
            {
                return;
            }
            DataIndex = -1;
        }

        public override void Apply()
        {
            UpdateInfernoSky();
            base.Apply();
        }
    }
}