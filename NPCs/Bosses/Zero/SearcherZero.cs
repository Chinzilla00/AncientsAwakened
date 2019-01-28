using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Bosses.Zero
{
	public class SearcherZero : ModNPC
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Searcher");	
		}		

        public override void SetDefaults()
        {
            npc.width = 35;
            npc.height = 35;
            npc.value = BaseMod.BaseUtility.CalcValue(0, 0, 5, 50);
            npc.npcSlots = 1;
            npc.aiStyle = -1;
            npc.lifeMax = 250;
            npc.defense = 30;
            npc.damage = 65;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
			npc.noGravity = true;
            
        }

		public int Shoot = 150;
		public int ShootMax = 150;

        public override void HitEffect(int hitDirection, double damage)
		{
			if (Main.netMode == 2) { return; }
			if(npc.life <= 0)
			{
				for (int m = 0; m < 50; m++)
				{
					int dustID = Dust.NewDust(npc.position, npc.width, npc.height, 5, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, mod.DustType<Dusts.VoidDust>(), Color.White, m % 3 == 0 ? 3f : 1f);
					if (m % 3 == 0) { Main.dust[dustID].noGravity = true; }
				}
			}else
			{
				for (int m = 0; m < 12; m++)
				{
					int dustID = Dust.NewDust(npc.position, npc.width, npc.height, 5, npc.velocity.X * 0.2f * hitDirection, npc.velocity.Y * 0.2f, mod.DustType<Dusts.VoidDust>(), Color.White, 1f);
				}
			}
		}

		public override void AI()
		{
			BaseAI.AIEater(npc, ref npc.ai, 0.022f, 4.2f, 0.7f, false, false);
			Player player = Main.player[npc.target];
			Shoot = Math.Max(0, Shoot - 1);
			if (Main.netMode != 1 && Shoot == 0 && Main.rand.Next(10) == 0 && Vector2.Distance(npc.Center, Main.player[npc.target].Center) > 250f && Collision.CanHit(npc.position, npc.width, npc.height, player.position, player.width, player.height))
			{
                Shoot = ShootMax;

                int num429 = 1;
                if (npc.position.X + (npc.width / 2) < Main.player[npc.target].position.X + Main.player[npc.target].width)
                {
                    num429 = -1;
                }
                Vector2 vector38 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                float num391 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector38.X;
                float num392 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - 300f - vector38.Y;
                float num393 = (float)Math.Sqrt((num391 * num391) + (num392 * num392));
                vector38 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                num391 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector38.X;
                num392 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector38.Y;
                Vector2 PlayerDistance = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) + (num429 * 180) - PlayerDistance.X;
                float PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - PlayerDistance.Y;
                float PlayerPos = (float)Math.Sqrt((PlayerPosX * PlayerPosX) + (PlayerPosY * PlayerPosY));
                float num433 = 6f;
                PlayerDistance = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - PlayerDistance.X;
                PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - PlayerDistance.Y;
                PlayerPos = (float)Math.Sqrt((PlayerPosX * PlayerPosX + PlayerPosY * PlayerPosY));
                PlayerPos = num433 / PlayerPos;
                PlayerPosX *= PlayerPos;
                PlayerPosY *= PlayerPos;
                PlayerPosY += Main.rand.Next(-40, 41) * 0.01f;
                PlayerPosX += Main.rand.Next(-40, 41) * 0.01f;
                PlayerPosY += npc.velocity.Y * 0.5f;
                PlayerPosX += npc.velocity.X * 0.5f;
                PlayerDistance.X -= PlayerPosX * 1f;
                PlayerDistance.Y -= PlayerPosY * 1f;
                npc.localAI[1] = 0f;
                float num394 = 10f;
                int num395 = npc.damage / 8;
                if (Main.expertMode)
                {
                    num394 = 12.5f;
                    num395 = npc.damage / 8;
                }
                num393 = (float)Math.Sqrt((num391 * num391) + (num392 * num392));
                num393 = num394 / num393;
                num391 *= num393;
                num392 *= num393;
                vector38.X += num391 * 15f;
                vector38.Y += num392 * 15f;
                int damage = npc.damage;
                for (int i = 0; i < 5; ++i)
                {
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(PlayerDistance.X, PlayerDistance.Y, PlayerPosX, PlayerPosY, mod.ProjectileType<DeathLaser>(), (int)(damage * 1.5f), 0f, Main.myPlayer);
                    }
                }
            }
		}

		public override void PostAI()
		{
			for (int m = npc.oldPos.Length - 1; m > 0; m--)
			{
				npc.oldPos[m] = npc.oldPos[m - 1];
			}
			npc.oldPos[0] = npc.position;
		}

        public static Texture2D glowTex = null;
        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            if (glowTex == null)
            {
                glowTex = mod.GetTexture("Glowmasks/SearcherZero_Glow");
            }
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseDrawing.DrawAura(spritebatch, glowTex, 0, npc, auraPercent, 1f, 0f, 0f, Color.Red);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, Color.Red);
            return false;
        }
	}
}