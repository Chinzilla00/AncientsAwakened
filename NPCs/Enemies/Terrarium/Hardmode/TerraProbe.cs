using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Terrarium.Hardmode
{
    public class TerraProbe : ModNPC
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unity Probe");
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            npc.width = 35;
            npc.height = 35;
            npc.value = BaseUtility.CalcValue(0, 0, 5, 50);
            npc.npcSlots = 1;
            npc.aiStyle = -1;
            npc.lifeMax = 300;
            npc.defense = 30;
            npc.damage = 65;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            npc.noGravity = true;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            bool isDead = npc.life <= 0;
            for (int m = 0; m < (isDead ? 25 : 5); m++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 107, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 100, Color.White, (isDead ? 2f : 1.1f));
            }
        }

        public override void AI()
        {
            BaseAI.AISkull(npc, ref npc.ai, false, 6f, 350f, 0.1f, 0.15f);
            Player player = Main.player[npc.target];
            bool playerActive = player != null && player.active && !player.dead;
            BaseAI.LookAt((playerActive ? player.Center : (npc.Center + npc.velocity)), npc, 0);
            if (Main.netMode != 1 && playerActive)
            {
                npc.ai[2]++;
                if (npc.ai[2] >= 90)
                {
                    npc.ai[2] = 0;
                    int projType = mod.ProjType("PurityShot");
                    if (Collision.CanHit(npc.position, npc.width, npc.height, player.position, player.width, player.height))
                        BaseAI.FireProjectile(player.Center, npc, projType, (int)(npc.damage * 0.25f), 0f, 2f);
                    npc.netUpdate2 = true;
                }
            }
            if (npc.frameCounter++ > 7)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 26;
                if (npc.frame.Y > 26 * 2)
                {
                    npc.frame.Y = 0;
                }
            }
        }
    }
}