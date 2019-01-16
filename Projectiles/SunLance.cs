using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class SunLance : ModProjectile
    {
        public static Vector2[] spearPos = new Vector2[] { new Vector2(0, 0), new Vector2(50, -25), new Vector2(100, -50), new Vector2(100, 0), new Vector2(100, 50), new Vector2(50, 25), new Vector2(30, 0), new Vector2(150, 0), new Vector2(150, 0), new Vector2(30, 0) };
        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;
            projectile.scale = 1.1f;
            projectile.aiStyle = 19;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.ownerHitCheck = true;
            projectile.melee = true;
            projectile.timeLeft = 60;
            projectile.hide = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sun Lance");
        }
        public float MovementFactor // Change this value to alter how fast the spear moves
        {
            get { return projectile.ai[0]; }
            set { projectile.ai[0] = value; }
        }


        public override void AI()
        {
            AIArcStabSpear(projectile, ref projectile.ai, false);
            if (Main.rand.Next(3) != 0)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.DragonflameDust>(), projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
                Dust dust = Main.dust[dustIndex];
                dust.noGravity = true;
                dust.scale *= 1.75f;
                dust.velocity.X = dust.velocity.X * 2f;
                dust.velocity.Y = dust.velocity.Y * 2f;
                dust.scale *= 0.2f;
            }
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
            if (p.timeLeft < 598) p.alpha -= 70; if (p.alpha < 0) p.alpha = 0;
            p.direction = ownerDirection;
            Vector2 oldCenter = p.Center;
            p.position.X = center.X - (float)(p.width * 0.5f);
            p.position.Y = center.Y - (float)(p.height * 0.5f);
            p.position += BaseMod.BaseUtility.RotateVector(default(Vector2), BaseMod.BaseUtility.MultiLerpVector(1f - (float)(itemAnimation / (float)itemAnimationMax), spearPos), itemRot);
            if (!overrideKill && Main.player[p.owner].itemAnimation == 0) { p.Kill(); }
            p.rotation = BaseMod.BaseUtility.RotationTo(center, oldCenter) + 2.355f;
            if (p.direction == -1) { p.rotation -= 0f; }
            else
            if (p.direction == 1) { p.rotation -= 1.57f; }
        }
    }
}
