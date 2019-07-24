using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Projectiles
{
    public class DragonP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.HornetStinger);
			projectile.magic = true;
            projectile.penetrate = 3;  
            projectile.width = 16;
            projectile.height = 16;
        }

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			List<NPC> list = new List<NPC>();
			for (int i = 0; i < 200; i++)
			{
				NPC nPC = Main.npc[i];
				if (nPC.CanBeChasedBy(this, false) && projectile.Distance(nPC.Center) < 800f)
				{
					list.Add(nPC);
				}
			}
			Vector2 center = projectile.Center;
			Vector2 value = Vector2.Zero;
			if (list.Count > 0)
			{
				NPC expr_94 = list[Main.rand.Next(list.Count)];
				center = expr_94.Center;
				value = expr_94.velocity;
			}
			int num = Main.rand.Next(2) * 2 - 1;
			Vector2 vector = new Vector2((float)num * (4f + (float)Main.rand.Next(3)), 0f);
			Vector2 vector2 = center + new Vector2((float)(-(float)num * 120), 0f);
			vector += (center + value * 15f - vector2).SafeNormalize(Vector2.Zero) * 2f;
			int p = Projectile.NewProjectile(vector2, vector, 700, projectile.damage/2, 0f, projectile.owner, 0f, 0f);
			Main.projectile[p].melee = false;
			Main.projectile[p].magic = true;
		}

        public override void Kill(int timeleft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, 6, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 0, new Color(50, 200, 0), 1f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, 6, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 0, new Color(50, 200, 0), 1f);
                Main.dust[num469].velocity *= 2f;
            }
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("DragonP");
    }

    }
}
