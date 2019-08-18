using Terraria;
using Terraria.Graphics.Shaders;

namespace AAMod.Backgrounds
{
    public class InfernoSkyData : ScreenShaderData
    {
        public InfernoSkyData(string passName) : base(passName)
        {

        }

        private void UpdateInfernoSky()
        {
            AAPlayer modPlayer = Main.LocalPlayer.GetModPlayer<AAPlayer>();
            if (AAWorld.infernoTiles < 100)
            {
                return;
            }
        }

        public override void Apply()
        {
            UpdateInfernoSky();
            base.Apply();
        }
    }
}