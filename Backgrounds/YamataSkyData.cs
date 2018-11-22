using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace AAMod.Backgrounds
{
    public class YamataSkyData : ScreenShaderData
    {
        private int YamataIndex;

        public YamataSkyData(string passName) : base(passName)
        {
        }

        private void UpdateYamataIndex()
        {
            int YamataType = ModLoader.GetMod("AAMod").NPCType("YamataA");
            if (YamataIndex >= 0 && Main.npc[YamataIndex].active && Main.npc[YamataIndex].type == YamataType)
            {
                return;
            }
            YamataIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == YamataType)
                {
                    YamataIndex = i;
                    break;
                }
            }
        }

        public override void Apply()
        {
            UpdateYamataIndex();
            if (YamataIndex != -1)
            {
                UseTargetPosition(Main.npc[YamataIndex].Center);
            }
            base.Apply();
        }
    }
}