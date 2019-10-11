using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class SunLance : ModProjectile
	{
		public static Color lightColor = new Color(82, 138, 206);
		public static Vector2[] spearPos = new Vector2[]{ new Vector2(0, 0), new Vector2(50, -25), new Vector2(100, -50), new Vector2(100, 0), new Vector2(100, 50), new Vector2(50, 25), new Vector2(30, 0), new Vector2(120, 0), new Vector2(120, 0), new Vector2(30, 0) };
	
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Sun Halberd");
		}	

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.aiStyle = -1;
            projectile.timeLeft = 600;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.damage = 1;
            projectile.penetrate = -1;
            projectile.hide = true;
            projectile.ownerHitCheck = true;
            projectile.melee = true;
			projectile.alpha = 254;
        }

		public override void AI()
		{
			AIArcStabSpear(projectile, ref projectile.ai, false);
			if (Main.rand.Next(3) != 0)
			{
				int dustID = Dust.NewDust(projectile.Center, 0, 0, ModContent.DustType<Dusts.CthulhuDust>(), 0f, 0f, 0);
				Main.dust[dustID].noGravity = true;
			}			
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 5;
		}

		public override bool PreDraw(SpriteBatch sb, Color dColor)
		{
			BaseMod.BaseDrawing.DrawProjectileSpear(sb, Main.projectileTexture[projectile.type], 0, projectile, null, 0f, 0f);
			return false;
		}

        public static void AIArcStabSpear(Projectile p, ref float[] ai, bool overrideKill = false)
        {
            Player plr = Main.player[p.owner];
            Item item = plr.inventory[plr.selectedItem];
            if (Main.myPlayer == p.owner && item != null && item.autoReuse && plr.itemAnimation == 1) { p.Kill(); return; } //prevents a bug with autoReuse and spears
            Main.player[p.owner].heldProj = p.whoAmI;
            Main.player[p.owner].itemTime = Main.player[p.owner].itemAnimation;
			Vector2 gfxOffset = new Vector2(0, plr.gfxOffY);
            AIArcStabSpear(p, ref ai, plr.Center + gfxOffset, BaseMod.BaseUtility.RotationTo(p.Center, p.Center + p.velocity), plr.direction, plr.itemAnimation, plr.itemAnimationMax, overrideKill, plr.frozen);
        }

        public static void AIArcStabSpear(Projectile p, ref float[] ai, Vector2 center, float itemRot, int ownerDirection, int itemAnimation, int itemAnimationMax, bool overrideKill = false, bool frozen = false)
        {
			if(p.timeLeft < 598) p.alpha -= 70; if(p.alpha < 0) p.alpha = 0;
            p.direction = ownerDirection;
			Vector2 oldCenter = p.Center;
            p.position.X = center.X - p.width * 0.5f;
            p.position.Y = center.Y - p.height * 0.5f;
			p.position += BaseMod.BaseUtility.RotateVector(default, BaseMod.BaseUtility.MultiLerpVector(1f - itemAnimation / (float)itemAnimationMax, spearPos), itemRot);		
            if (!overrideKill && Main.player[p.owner].itemAnimation == 0){ p.Kill(); }
            p.rotation = BaseMod.BaseUtility.RotationTo(center, oldCenter) + 2.355f;				
			if (p.direction == -1) { p.rotation -= 0f; }else
			if (p.direction == 1) { p.rotation -= 1.57f; }		
		}
	}
}