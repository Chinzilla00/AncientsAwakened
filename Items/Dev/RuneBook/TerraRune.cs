using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev.RuneBook
{
    public class TerraRune : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Rune");
            ProjectileID.Sets.DontAttachHideToAlpha[projectile.type] = true;
            Main.projPet[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.width = 12;
            projectile.height = 22;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.timeLeft = 18000;
            projectile.timeLeft *= 5;
            projectile.minion = true;
            projectile.minionSlots = 0f;
            projectile.damage = 1;
            projectile.tileCollide = false;
            projectile.timeLeft *= 5;
        }

        public override void AI()
        {
            Lighting.AddLight((int)(projectile.position.X + projectile.width / 2) / 16, (int)(projectile.position.Y + projectile.height / 2) / 16, 1f, 0.95f, 0.8f);
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            player.AddBuff(mod.BuffType("CCRune"), 3600);
            if (!modPlayer.CCBookEX)
            {
                projectile.active = false;
                return;
            }
            if (player.dead)
            {
                modPlayer.CCRune = false;
            }
            if (player.HasBuff(mod.BuffType("CCRune")))
            {
                projectile.timeLeft = 2;
            }

            projectile.timeLeft ++;

            float num633 = 700f;
            float num634 = 800f;
            for (int num638 = 0; num638 < 1000; num638++)
            {
                bool flag23 = Main.projectile[num638].type == mod.ProjectileType("TerraRune");
                if (num638 != projectile.whoAmI && Main.projectile[num638].active && Main.projectile[num638].owner == projectile.owner && flag23 && Math.Abs(projectile.position.X - Main.projectile[num638].position.X) + Math.Abs(projectile.position.Y - Main.projectile[num638].position.Y) < projectile.width)
                {
                    if (projectile.position.X < Main.projectile[num638].position.X)
                    {
                        projectile.velocity.X = projectile.velocity.X - 0.02f;
                    }
                    else
                    {
                        projectile.velocity.X = projectile.velocity.X + 0.02f;
                    }
                    if (projectile.position.Y < Main.projectile[num638].position.Y)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - 0.02f;
                    }
                    else
                    {
                        projectile.velocity.Y = projectile.velocity.Y + 0.02f;
                    }
                }
            }
            if (Vector2.Distance(player.Center, projectile.Center) > 400f)
			{
				projectile.ai[0] = 1f;
				projectile.tileCollide = false;
				projectile.netUpdate = true;
			}
			Vector2 vector = player.Center - projectile.Center - new Vector2(0, 50f);
            float num639 = 7f;
			if (vector.Length() > 200f && num639 < 10f)
			{
				num639 = 10f;
			}
			if (vector.Length() < 100f && (projectile.ai[0] == 1f) && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
			{
				projectile.ai[0] = 0f;
				projectile.netUpdate = true;
			}
			if (vector.Length() > 2000f)
			{
				projectile.position.X = Main.player[projectile.owner].Center.X - projectile.width / 2;
				projectile.position.Y = Main.player[projectile.owner].Center.Y - projectile.height / 2;
				projectile.netUpdate = true;
			}
			if (vector.Length() > 150f)
			{
				vector.Normalize();
				vector *= num639;
				projectile.velocity = (projectile.velocity * 40f + vector) / 41f;
			}
            else if (vector.Length() > 40f)
			{
				vector.Normalize();
				vector *= num639;
				projectile.velocity = (projectile.velocity * 40f + vector) / 41f;
			}
			if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
			{
				projectile.velocity.X = -0.04f;
				projectile.velocity.Y = -0.02f;
			}

            if (projectile.ai[1] > 0f)
			{
				projectile.ai[1] += Main.rand.Next(1, 4);
			}
			if (projectile.ai[1] > 220f)
			{
				projectile.ai[1] = 0f;
				projectile.netUpdate = true;
			}
            if (projectile.localAI[0] < 120f)
			{
				projectile.localAI[0] += 1f;
			}
            if (projectile.ai[0] == 0f)
            {
                if (projectile.ai[1] == 0f && projectile.localAI[0] >= 120f)
                {
                    projectile.ai[1] += 1f;
                    if (Main.myPlayer == projectile.owner && Main.player[projectile.owner].statLife < Main.player[projectile.owner].statLifeMax2)
					{
                        Main.player[projectile.owner].HealEffect(20, false);
                        Main.player[projectile.owner].statLife += 20;
                        if (Main.player[projectile.owner].statLife > Main.player[projectile.owner].statLifeMax2)
                        {
                            Main.player[projectile.owner].statLife = Main.player[projectile.owner].statLifeMax2;
                        }
                        NetMessage.SendData(66, -1, -1, null, projectile.owner, 1, 0f, 0f, 0, 0, 0);
                        projectile.netUpdate = true;
                    }
                }
            }
        }
    }
}
