using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Pets
{
	public class Glowmoss : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glowmoss");
			Main.projFrames[projectile.type] = 1;
			Main.projPet[projectile.type] = true;
			ProjectileID.Sets.LightPet[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			projectile.penetrate = -1;
			projectile.netImportant = true;
			projectile.timeLeft *= 5;
			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.scale = 0.8f;
			projectile.tileCollide = false;
		}

		public override void AI()
        {
            Lighting.AddLight((int)(projectile.Center.X + (float)(projectile.width / 2)) / 16, (int)(projectile.position.Y + (float)(projectile.height / 2)) / 16, 0f, 0.5f, 0.2f);
            Player player = Main.player[projectile.owner];
            projectile.rotation += 0.02f;
            if (Main.myPlayer == projectile.owner)
            {
                if (player.GetModPlayer<AAPlayer>(mod).Glowmoss)
                {
                    projectile.timeLeft = 2;
                }
            }
            if (Main.player[projectile.owner].dead)
            {
                projectile.Kill();
                return;
            }
            float num146 = 3.3f;
            Vector2 vector13 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
            float num147 = Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2) - vector13.X;
            float num148 = Main.player[projectile.owner].position.Y + (float)(Main.player[projectile.owner].height / 2) - vector13.Y;
            int num149 = 70;
            if (Main.player[projectile.owner].controlUp)
            {
                num148 = Main.player[projectile.owner].position.Y - 40f - vector13.Y;
                num147 -= 6f;
                num149 = 4;
            }
            else if (Main.player[projectile.owner].controlDown)
            {
                num148 = Main.player[projectile.owner].position.Y + (float)Main.player[projectile.owner].height + 40f - vector13.Y;
                num147 -= 6f;
                num149 = 4;
            }
            float num150 = (float)Math.Sqrt((double)(num147 * num147 + num148 * num148));
            if (num150 > 800f)
            {
                projectile.position.X = Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2) - (float)(projectile.width / 2);
                projectile.position.Y = Main.player[projectile.owner].position.Y + (float)(Main.player[projectile.owner].height / 2) - (float)(projectile.height / 2);
                return;
            }
            if (num150 > num149)
            {
                num150 = num146 / num150;
                num147 *= num150;
                num148 *= num150;
                projectile.velocity.X = num147;
                projectile.velocity.Y = num148;
                return;
            }
            projectile.velocity.X = 0f;
            projectile.velocity.Y = 0f;
            return;
        }
    }
}