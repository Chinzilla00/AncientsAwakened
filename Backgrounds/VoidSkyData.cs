using Terraria;
using Terraria.Graphics.Shaders;

namespace AAMod.Backgrounds
{
    public class VoidSkyData : ScreenShaderData
    {
        public VoidSkyData(string passName) : base(passName)
        {
        }

        private void UpdateVoidSky()
        {
            AAPlayer modPlayer = Main.LocalPlayer.GetModPlayer<AAPlayer>();
            if (AAWorld.voidTiles < 100)
            {
                return;
            }
        }

        public override void Apply()
        {
            UpdateVoidSky();
            base.Apply();
        }
    }
}