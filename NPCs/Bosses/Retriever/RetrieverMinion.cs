using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using BaseMod;
using Terraria.Audio;

namespace AAMod.NPCs.Bosses.Retriever
{
    public class RetrieverMinion : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Storm Claw");
            Main.npcFrameCount[npc.type] = 11;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 400;
            npc.defense = 40;
            npc.damage = 40;
            npc.width = 36;
            npc.height = 36;
            npc.aiStyle = -1;
            npc.HitSound = new LegacySoundStyle(3, 4, Terraria.Audio.SoundType.Sound);
            npc.DeathSound = new LegacySoundStyle(4, 14, Terraria.Audio.SoundType.Sound);
            npc.knockBackResist = 0f;
            npc.noGravity = true;
        }

        public int shootAI = 0;
        public bool Shoot = false;
        public int ShootTimer = 0;

        public override void AI()
        {
            BaseAI.AISkull(npc, ref npc.ai, true, 6f, 350f, 0.1f, 0.15f);
            Player player = Main.player[npc.target];
            bool playerActive = player != null && player.active && !player.dead;
            BaseAI.LookAt((playerActive ? player.Center : (npc.Center + npc.velocity)), npc, 0);
            if (Main.netMode != 1 && playerActive)
            {
                shootAI++;
                if (shootAI >= 130)
                {
                    shootAI = 0;
                    int projType = (!Main.dayTime ? mod.ProjType("RetrieverShot") : mod.ProjType("RetrieverShot"));
                    if (Collision.CanHit(npc.position, npc.width, npc.height, player.position, player.width, player.height))
                        BaseAI.FireProjectile(player.Center, npc, projType, (int)(npc.damage * 0.25f), 0f, 2f);
                }
            }
            npc.frameCounter++;
            if (npc.frameCounter >= 8)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 38;
                if (npc.frame.Y > (38 * 3))
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = 0;
                }
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                npc.position.X = npc.position.X + (float)(npc.width / 2);
                npc.position.Y = npc.position.Y + (float)(npc.height / 2);
                npc.width = 44;
                npc.height = 78;
                npc.position.X = npc.position.X - (float)(npc.width / 2);
                npc.position.Y = npc.position.Y - (float)(npc.height / 2);
                int dust1 = mod.DustType<Dusts.TMagicDust>();
                int dust2 = mod.DustType<Dusts.TMagicDust>();
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust2, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;
            }
        }
    }
}
