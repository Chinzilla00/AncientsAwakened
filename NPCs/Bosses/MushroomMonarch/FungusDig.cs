using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.MushroomMonarch
{
    public class FungusDig: ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Feudal Fungus");
            Main.npcFrameCount[npc.type] = 9;
        }
        public override void SetDefaults()
        {
            npc.damage = 24;
            npc.width = 74;
            npc.height = 80;
            npc.friendly = false;
            npc.timeLeft = 900;
        }
        public override void AI()
        {
            if (npc.velocity.Y == 0)
            {
                if (++npc.ai[0] >= 9)
                {
                    npc.ai[0] = 0;
                    if (npc.frame.Y >= 108 * 9)
                    {
                        npc.active = false;
                    }
                }
            }
            else
            {
                npc.frame.Y = 0;
            }
        }
    }
}