using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Inferno
{
    public abstract class InfernoSandShark : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sand Shark");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.SandShark];
        }

        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.SandShark);
            animationType = NPCID.SandShark;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 10; i++)
            {
                int dustType = Main.rand.Next(139, 143);
                int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.AbyssiumDust>(), 0f, 0f, 200, default, 0.8f);
                Main.dust[dustIndex].velocity *= 0.3f;
            }
        }
    }
}


