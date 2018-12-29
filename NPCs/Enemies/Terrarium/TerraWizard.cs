using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Enemies.Terrarium
{
    public class TerraWizard : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Terra Knight");
			Main.npcFrameCount[npc.type] = 6;
		}

		public override void SetDefaults()
		{
            npc.lifeMax = 600;
            npc.defense = 40;
            npc.damage = 90;
            npc.width = 22;
            npc.height = 56;
            npc.aiStyle = -1;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.4f;
            npc.noGravity = true;
        }
        

        public override void AI()
        {
            BaseAI.AIFlier(npc, ref npc.ai, true, 0.4f, 0.04f, 6f, 1.5f, false, 300);
            if (Main.netMode != 1)
            {
                npc.localAI[0] += 1f;
                if (npc.localAI[0] >= (float)(360 + Main.rand.Next(360)) && npc.Distance(Main.player[npc.target].Center) < 400f && Math.Abs(npc.DirectionTo(Main.player[npc.target].Center).Y) < 0.5f && Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
                {
                    npc.localAI[0] = 0f;
                    Vector2 vector13 = npc.Center + new Vector2((float)(npc.direction * 30), 2f);
                    Vector2 vector14 = npc.DirectionTo(Main.player[npc.target].Center) * 7f;
                    if (vector14.HasNaNs())
                    {
                        vector14 = new Vector2((float)(npc.direction * 8), 0f);
                    }
                    int num85 = Main.expertMode ? 50 : 75;
                    for (int num86 = 0; num86 < 4; num86++)
                    {
                        Vector2 vector15 = vector14 + Utils.RandomVector2(Main.rand, -0.8f, 0.8f);
                        Projectile.NewProjectile(vector13.X, vector13.Y, vector15.X, vector15.Y, mod.ProjectileType<MagicBlast>(), num85, 1f, Main.myPlayer, 0f, 0f);
                    }
                }
            }
            npc.frameCounter++;
            if (npc.frameCounter >= 10)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 58;
                if (npc.frame.Y > (58 * 5))
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

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType<Items.Materials.TerraCrystal>());
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType<Buffs.Terrablaze>(), 300);
        }
    }
}
