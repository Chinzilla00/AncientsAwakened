using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Enemies.Inferno
{
    public class BlazePhoenix : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blaze Phoenix");
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {
			npc.width = 30;
			npc.height = 30;
            npc.aiStyle = -1;
            npc.npcSlots = 1;
            npc.value = BaseUtility.CalcValue(0, 1, 25, 0);
            npc.lifeMax = 200;
            npc.defense = 5;
            npc.noGravity = true;
			npc.noTileCollide = true;
			npc.knockBackResist = 0f;
            npc.lavaImmune = true;
			npc.buffImmune[BuffID.OnFire] = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.damage = 70;
        }

        public override void AI()
        {
            Lighting.AddLight(npc.Center, Color.DarkOrange.R / 255, Color.DarkOrange.G / 255, Color.DarkOrange.B / 255);
			AAAI.AIShadowflameGhost(npc, ref npc.ai, false, 660f, 0.3f, 10f, 0.2f, 6f, 5f, 10f, 0.4f, 0.4f, 0.95f, 5f);
			npc.spriteDirection = (npc.velocity.X > 0 ? -1 : 1);
			BaseAI.LookAt(npc.Center + npc.velocity, npc, 0);
            npc.frameCounter++;
            if (npc.frameCounter > 3)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 76;
                if (npc.frame.Y > 76 * 7)
                {
                    npc.frame.Y = 0;
                }
            }
            float num1276 = 120f;
            if (npc.localAI[0] < num1276)
            {
                npc.localAI[0] += 1f;
                float num1279 = 1f - npc.localAI[0] / num1276;
                float num1280 = num1279 * 20f;
                int num1281 = 0;
                while ((float)num1281 < num1280)
                {
                    if (Main.rand.Next(5) == 0)
                    {
                        int num1282 = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.DragonflameDust>(), 0f, 0f, 0, default(Color), 1f);
                        Main.dust[num1282].alpha = 100;
                        Main.dust[num1282].velocity *= 0.3f;
                        Main.dust[num1282].velocity += npc.velocity * 0.75f;
                        Main.dust[num1282].noGravity = true;
                    }
                    num1281++;
                }
            }
        }
		
				

        public Color GetGlowAlpha()
        {
            return new Color(220, 150, 150) * ((float)Main.mouseTextColor / 255f);
        }
        
        public override void NPCLoot()
        {
			if(Main.netMode != 1)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DragonFire"), 1 + Main.rand.Next(2));
			}
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseDrawing.DrawAfterimage(spritebatch, Main.npcTexture[npc.type], 0, npc, 0.8f, 1f, 4, false, 0f, 0f, GetGlowAlpha());
            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, Color.White);			
            return false;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("DragonFire"), 600);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
			bool isDead = npc.life <= 0;
            if (isDead)
            {
				for (int m = 0; m < 30; m++)
				{
					int dustID = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, 1, DustID.Fire, -npc.velocity.X * 0.2f,
						-npc.velocity.Y * 0.2f, 100, default(Color), 2f);
					Main.dust[dustID].velocity *= 2f;
					dustID = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, npc.height, mod.DustType<Dusts.BroodmotherDust>(), -npc.velocity.X * 0.2f,
						-npc.velocity.Y * 0.2f, 100, default(Color));
					Main.dust[dustID].velocity *= 2f;
				}
            }
			for (int m = 0; m < 5; m++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, DustID.Fire, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 100, Color.White, 1.3f);
			}
        }	
    }
}
