using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Rajah
{
    public class BunnyBrawler : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bunny Brawler");
            Main.npcFrameCount[npc.type] = 7;
        }

        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.Derpling);
            npc.life = 500;
            npc.damage = 70;
            npc.alpha = 255;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            bool isDead = npc.life <= 0;
            if (isDead)          //this make so when the npc has 0 life(dead) he will spawn this
            {

            }
            for (int m = 0; m < (isDead ? 35 : 6); m++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, DustID.Fire, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 100, default(Color), (isDead ? 2f : 1.5f));
            }
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override void PostAI()
        {
            if (NPC.AnyNPCs(mod.NPCType<Rajah>()))
            {
                if (npc.alpha > 0)
                {
                    npc.alpha -= 5;
                }
                else
                {
                    npc.alpha = 0;
                }
            }
            else
            {
                npc.dontTakeDamage = true;
                if (npc.alpha < 255)
                {
                    npc.alpha += 5;
                }
                else
                {
                    npc.active = false;
                }
            }
        }
    }
}