using Terraria;
using Terraria.Graphics.Shaders;

namespace AAMod.Backgrounds
{
    public class MireSkyData : ScreenShaderData
    {
        public MireSkyData(string passName) : base(passName)
        {
        }

        private void UpdateMireSky()
        {
            AAPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<AAPlayer>();
            if (AAWorld.mireTiles < 100)
            {
                return;
            }
        }

        public override void Apply()
        {
            UpdateMireSky();
            base.Apply();
        }
    }
}