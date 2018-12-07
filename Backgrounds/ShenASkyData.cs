using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace AAMod.Backgrounds
{
    public class ShenASkyData : ScreenShaderData
    {
        private int ShenIndex;

        public ShenASkyData(string passName) : base(passName)
        {
        }

        private void UpdateShenIndex()
        {
            int ShenType = ModLoader.GetMod("AAMod").NPCType("ShenA");
            if (ShenIndex >= 0 && Main.npc[ShenIndex].active && Main.npc[ShenIndex].type == ShenType)
            {
                return;
            }
            ShenIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == ShenType)
                {
                    ShenIndex = i;
                    break;
                }
            }
        }

        public override void Apply()
        {
            UpdateShenIndex();
            if (ShenIndex != -1)
            {
                UseTargetPosition(Main.npc[ShenIndex].Center);
            }
            base.Apply();
        }
    }
}