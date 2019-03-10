using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Yamata
{
    public class YamataBlast : ModProjectile
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Venom");     //The English name of the projectile
            Main.projFrames[projectile.type] = 5;     //The recording mode
		}

		public override void SetDefaults()
		{
			projectile.width = 14;               //The width of projectile hitbox
			projectile.height = 14;              //The height of projectile hitbox
			projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
			projectile.friendly = false;         //Can the projectile deal damage to enemies?
			projectile.hostile = true;        //Is the projectile shoot by a ranged weapon?
			projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 600;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.alpha = 20;              //How much light emit around the projectile
			projectile.ignoreWater = true;
			projectile.tileCollide = true;        //Set to above 0 if you want the projectile to update multiple time in a frame
			aiType = ProjectileID.WoodenArrowFriendly;           
            
		}

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType<Buffs.HydraToxin>(), 300);
        }

        public override void Kill(int timeleft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.YamataDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, new Color(86, 191, 188), 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.YamataDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, new Color(86, 191, 188));
                Main.dust[num469].velocity *= 2f;
            }
            Main.PlaySound(new LegacySoundStyle(2, 89, Terraria.Audio.SoundType.Sound));
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 51 + 8, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("Shock"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
        }
    }
}
