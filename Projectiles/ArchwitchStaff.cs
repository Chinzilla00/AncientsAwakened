
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class ArchwitchStaff : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Archwitch Staff");
        }
        public override void SetDefaults()
        {
            projectile.width = 110;
            projectile.height = 110;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;       
            projectile.magic = true;
        }

        public override void AI()
        {
            projectile.soundDelay--;
            if (projectile.soundDelay <= 0)
            {
                Main.PlaySound(SoundID.Item, (int)projectile.Center.X, (int)projectile.Center.Y, 15);
                projectile.soundDelay = 45;
            }
            Player player = Main.player[projectile.owner];
            if (Main.myPlayer == projectile.owner)
            {
                if (!player.channel || player.noItems || player.CCed)
                {
                    projectile.Kill();
                }
            }
            Lighting.AddLight(projectile.Center, .6f, 0.6f, .7f);
            projectile.Center = player.MountedCenter;
            projectile.position.X += player.width / 2 * player.direction;
            projectile.spriteDirection = player.direction;
            projectile.rotation += 0.3f * player.direction;
            if (projectile.rotation > MathHelper.TwoPi)
            {
                projectile.rotation -= MathHelper.TwoPi;
            }
            else if (projectile.rotation < 0)
            {
                projectile.rotation += MathHelper.TwoPi;
            }
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = projectile.rotation;
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Shadowflame);
            Main.dust[dust].velocity /= 1f;

            if (player.ownedProjectileCounts[mod.ProjectileType("ArchwitchStar")] < 1)
            {
                for (int a = 0; a < 8; a++)
                {
                    Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("ArchwitchStar"), (int)(100 * player.magicDamage), 4, Main.myPlayer);
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, null, Color.White, projectile.rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }
    }
}