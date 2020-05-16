using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AAMod.NPCs.Enemies.Terrarium.PostEquinox
{
    public class TerraKraken : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Terra Kraken");
      Main.npcFrameCount[npc.type] = Main.npcFrameCount[4];
		}

		public override void SetDefaults()
	{
            npc.lifeMax = 2000;
            npc.defense = 20;
            npc.damage = 75;
            npc.width = 80;
            npc.height = 66;
            npc.aiStyle = -1;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.4f;
            npc.alpha = 255;
            npc.noTileCollide = false;
            npc.noGravity = true;
            banner = npc.type;
	    bannerItem = mod.ItemType("PuritySquidBanner");
        }

        public override Color? GetAlpha(Color drawColor)
        {
            return Color.White;
        }

        public int[] orbiters = new int[5];
        int someIntegerYoullHaveToWriteLaterOnSorryImLazy;
        public override void AI()
        {
            if (npc.frameCounter++ > 5)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 66;
                if (npc.frame.Y >= 66 * 4)
                {
                    npc.frame.Y = 0;
                }
            }
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            npc.velocity = MoveTowardsPlayer(5, npc.velocity.X, npc.velocity.Y, player, npc.Center, npc.direction);
            npc.rotation = npc.velocity.X / 30;
            npc.ai[1]++;
            if (npc.ai[2] != 0)
            {
                orbiters[(int)npc.ai[2] - 1] = 0;
                npc.ai[2] = 0;
            }
            if (npc.ai[1] % 72 == 0 && npc.ai[1] <= 360 && orbiters[(int)(npc.ai[1] / 72) - 1] == 0 && npc.ai[1] != 0)
            {
                Main.NewText((int)Math.Round(npc.ai[1] / 72) - 1);
                orbiters[(int)Math.Round(npc.ai[1] / 72) - 1] = 1;
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("TerraKrakenOrbiter"), 0, npc.whoAmI,0,((int)npc.ai[1] / 72));
            }
            if (npc.ai[1] > 360)
            {
                npc.ai[1] = 0;
            }
            npc.ai[3] = 0;
            for (int i = 0; i<orbiters.Length; i++)
            {
                if (orbiters[i] == 1)
                    npc.ai[3]++;
            }
            if (npc.ai[3] == orbiters.Length)
                someIntegerYoullHaveToWriteLaterOnSorryImLazy++;
            if(someIntegerYoullHaveToWriteLaterOnSorryImLazy % 72 == 0 && npc.ai[3] == orbiters.Length)
            {
                float Speed = 20;
                Vector2 vector8 = new Vector2(npc.position.X, npc.position.Y + (npc.height / 2));
                int damage = 40;
                int type = mod.ProjectileType("TerraKrakenProj");
                Main.PlaySound(SoundID.Item33, npc.position);
                float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
                Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
            }
            if (npc.ai[0] == 2f)
            {
                npc.alpha += 12;
                if (npc.alpha > 255)
                {
                    npc.alpha = 255;
                }
            }
            else
            {
                npc.alpha -= 12;
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                }
            }

            npc.rotation = npc.velocity.X / 15f;

        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(4) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Items.Materials.TerraShard>());
            }
        }
        public static Vector2 MoveTowardsPlayer(float speed, float currentX, float currentY, Player player, Vector2 issue, int direction)
        {
            float num1 = speed;

            Vector2 vector3 = new Vector2(issue.X + (float)(direction * 20), issue.Y + 6f);

            float num2 = player.position.X + (float)player.width * 0.5f - vector3.X;

            float num3 = player.Center.Y - vector3.Y;

            float num4 = (float)Math.Sqrt((double)(num2 * num2 + num3 * num3));

            float num5 = num1 / num4;
            num2 *= num5;
            num3 *= num5;

            currentX = (currentX * 58f + num2) / 58.8f;
            currentY = (currentY * 58f + num3) / 58.8f;

            return new Vector2(currentX, currentY);
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 107, 0f, 0f, 0);
                }
            }
        }
    }
}
