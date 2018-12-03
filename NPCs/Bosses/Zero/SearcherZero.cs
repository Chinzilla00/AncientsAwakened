using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Zero
{
    public class SearcherZero : ModNPC
	{
		public int timer = 0;
		public bool start = true;
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Searcher");
            Main.npcFrameCount[npc.type] = 1;
        }
		
		public override void SetDefaults()
		{
            npc.CloneDefaults(NPCID.CursedSkull);
            npc.aiStyle = 5;
			npc.width = 30;
			npc.height = 30;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.chaseable = false;
			npc.damage = 30;
			npc.defense = 20;
            npc.value = 0;
			npc.lifeMax = 1000;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath14;
		}

        public override void AI()
        {
            timer++;
            float num373 = 8.25f;
            int num375 = 1;
            if (npc.position.X + (npc.width / 2) < Main.player[npc.target].position.X + Main.player[npc.target].width)
            {
                num375 = -1;
            }
            Vector2 vector36 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
            float num376 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) + (num375 * 300) - vector36.X;
            float num377 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - 300f - vector36.Y;
            float num378 = (float)Math.Sqrt((num376 * num376) + (num377 * num377));
            float num379 = num378;
            num378 = num373 / num378;
            num376 *= num378;
            num377 *= num378;
            if (Main.netMode != 1)
            {
                float num380 = 9f;
                int num381 = npc.damage / 8;
                int num382 = mod.ProjectileType<DeathLaser>();
                if (Main.expertMode)
                {
                    num380 = 10.5f;
                    num381 = npc.damage / 8;
                }
                num378 = (float)Math.Sqrt((num376 * num376) + (num377 * num377));
                num378 = num380 / num378;
                num376 *= num378;
                num377 *= num378;
                num376 += Main.rand.Next(-40, 41) * 0.08f;
                num377 += Main.rand.Next(-40, 41) * 0.08f;
                vector36.X += num376 * 15f;
                vector36.Y += num377 * 15f;
                if (timer == 180)
                {
                    Projectile.NewProjectile(vector36.X, vector36.Y, num376, num377, num382, num381, 0f, Main.myPlayer, 0f, 0f);
                    timer = 0;
                }
            }
        }

        public Color GetGlowAlpha()
        {
            return new Color(233, 53, 53) * (Main.mouseTextColor / 255f);
        }

        public static Texture2D glowTex = null;
        public float auraPercent = 0f;
        public bool auraDirection = true;



        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (glowTex == null)
            {
                glowTex = mod.GetTexture("Glowmasks/SearcherZero_Glow");
            }
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseMod.BaseDrawing.DrawAura(spriteBatch, glowTex, 0, npc, auraPercent, 1f, 0f, 0f, GetGlowAlpha());
            BaseMod.BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, npc, GetGlowAlpha());
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SearcherGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SearcherGore2"), 1f);
            }
        }
    }
}