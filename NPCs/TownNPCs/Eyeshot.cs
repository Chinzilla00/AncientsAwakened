using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.TownNPCs

{
    public class EyeShot : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lovecraftian Eye");
		}

		public override void SetDefaults()
		{
            projectile.penetrate = 1;
            projectile.width = 10;
            projectile.height = 10;
            projectile.tileCollide = true;
            projectile.hostile = false;
            projectile.friendly = true;
            
		}

        public override void Kill(int timeleft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, Terraria.ModLoader.ModContent.DustType<Dusts.CthulhuAuraDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, new Color(191, 86, 188), 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, Terraria.ModLoader.ModContent.DustType<Dusts.CthulhuAuraDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, new Color(191, 86, 188));
                Main.dust[num469].velocity *= 2f;
            }
        }

        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            if (NPC.downedMoonlord)
            {
                projectile.damage = 200;
                return;
            }
            if (Main.hardMode)
            {
                projectile.damage = 90;
                return;
            }
            if (!Main.hardMode)
            {
                projectile.damage = 20;
                return;
            }
        }
    }
}
