using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Fulgurite
{
    public class FulguriteRing : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 102;
            projectile.height = 102;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.timeLeft = 180;
            projectile.knockBack = 10;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
        }

        public override void AI()
        {

            Player player = Main.player[projectile.owner];
            player.GetModPlayer<AAPlayer>(mod).ringActive = true;
            projectile.Center = player.Center;
            projectile.direction = player.direction;
        }
        

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            player.GetModPlayer<AAPlayer>(mod).ringActive = false;
            Main.PlaySound(SoundID.Item94, projectile.position);
        }

        

        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
            return true;
        }
    }
}