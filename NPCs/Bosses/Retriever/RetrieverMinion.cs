using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
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

        public float[] shootAI = new float[4];
        public bool Shoot = false;
        public int ShootTimer = 0;

        public override void AI()
        {
            ShootTimer++;
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            float num = 6f;
            Vector2 vector = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
            float num4 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2);
            float num5 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2);
            num4 = (float)((int)(num4 / 8f) * 8);
            num5 = (float)((int)(num5 / 8f) * 8);
            vector.X = (float)((int)(vector.X / 8f) * 8);
            vector.Y = (float)((int)(vector.Y / 8f) * 8);
            num4 -= vector.X;
            num5 -= vector.Y;
            float num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
            float num7 = num6;
            if (num6 == 0f)
            {
                num4 = npc.velocity.X;
                num5 = npc.velocity.Y;
            }
            else
            {
                num6 = num / num6;
                num4 *= num6;
                num5 *= num6;
            }
            int num8 = 25;
            if (Main.expertMode)
            {
                num8 = 22;
            }
            BaseAI.AIEater(npc, ref npc.ai, 0.022f, 4.2f, 0.7f, false, false);

            if (ShootTimer >= 300)
            {
                npc.frame.Y = (38 * 4);
            }
            else if (ShootTimer >= 307)
            {
                npc.frame.Y = (38 * 5);
            }
            else if (ShootTimer >= 314)
            {
                npc.frame.Y = (38 * 6);
            }
            else if (ShootTimer >= 321)
            {
                npc.frame.Y = (38 * 7);
            }
            else if (ShootTimer >= 328)
            {
                npc.frame.Y = (38 * 8);
                if (ShootTimer >= 332)
                {
                    Main.PlaySound(SoundID.Item12, npc.position);
                    Projectile.NewProjectile(vector.X, vector.Y, num4, num5, mod.ProjectileType("RetrieverShot"), num8, 0f, Main.myPlayer, 0f, 0f);
                }
            }
            else if (ShootTimer >= 335)
            {
                npc.frame.Y = (38 * 9);
            }
            else if (ShootTimer >= 342)
            {
                npc.frame.Y = (38 * 10);
                ShootTimer = 0;
            }
            else
            {
                npc.frameCounter++;
                if (npc.frameCounter >= 10)
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
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TerraWizardGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TerraWizardGore2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TerraWizardGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TerraWizardGore4"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TerraWizardGore5"), 1f);
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
