using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace AAMod.Backgrounds
{
    public class AkumaSkyData : ScreenShaderData
    {
        private int AkumaIndex;

        public AkumaSkyData(string passName) : base(passName)
        {
        }

        private void UpdateAkumaIndex()
        {
            int AkumaType = ModLoader.GetMod("AAMod").NPCType("AkumaA");
            if (AkumaIndex >= 0 && Main.npc[AkumaIndex].active && Main.npc[AkumaIndex].type == AkumaType)
            {
                return;
            }
            AkumaIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == AkumaType)
                {
                    AkumaIndex = i;
                    break;
                }
            }
        }

        public override void Apply()
        {
            UpdateAkumaIndex();
            if (AkumaIndex != -1)
            {
                UseTargetPosition(Main.npc[AkumaIndex].Center);
            }
            base.Apply();
        }
    }
}