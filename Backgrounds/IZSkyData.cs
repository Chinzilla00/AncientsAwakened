using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace AAMod.Backgrounds
{
    public class IZSkyData : ScreenShaderData
    {
        private int IZIndex;

        public IZSkyData(string passName) : base(passName)
        {
        }

        private void UpdateIZIndex()
        {
            int IZType = ModLoader.GetMod("AAMod").NPCType("Infinity");

            int IZSpawmType = ModLoader.GetMod("AAMod").NPCType("IZSpawn1");
            if (IZIndex >= 0 && Main.npc[IZIndex].active && (Main.npc[IZIndex].type == IZType || Main.npc[IZIndex].type == IZSpawmType))
            {
                return;
            }
            IZIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == IZType)
                {
                    IZIndex = i;
                    break;
                }
                if (Main.npc[i].active && Main.npc[i].type == IZSpawmType)
                {
                    IZIndex = i;
                    break;
                }
            }
        }

        public override void Apply()
        {
            UpdateIZIndex();
            if (IZIndex != -1)
            {
                UseTargetPosition(Main.npc[IZIndex].Center);
            }
            base.Apply();
        }
    }
}