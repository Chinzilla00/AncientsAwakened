using Terraria;
using Terraria.Graphics.Shaders;
using BaseMod;
using Terraria.ModLoader;

namespace AAMod.Backgrounds
{
    public class CthulhuSkyData : ScreenShaderData
    {
        private int SoCIndex;

        public CthulhuSkyData(string passName) : base(passName)
        {
        }

        private void UpdateCthulhuSky()
        {

            AAPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<AAPlayer>();
            int SoCType = ModLoader.GetMod("AAMod").NPCType("SoC");
            if (SoCIndex >= 0 && Main.npc[SoCIndex].active && Main.npc[SoCIndex].type == SoCType)
            {
                return;
            }
            SoCIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == SoCType)
                {
                    SoCIndex = i;
                    break;
                }
            }
            if (Main.player[Main.myPlayer].InZone("Ocean") && !AAWorld.downedSoC && AAWorld.downedAllAncients)
            {
                return;
            }
        }

        public override void Apply()
        {
            UpdateCthulhuSky();
            if (SoCIndex != -1)
            {
                UseTargetPosition(Main.npc[SoCIndex].Center);
            }
            base.Apply();
        }
    }
}