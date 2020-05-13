using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Serpent
{
    public class IceBall2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Ball");
            Main.projFrames[projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.BoulderStaffOfEarth);
            projectile.penetrate = 1;  
            projectile.width = 30;
            projectile.height = 30;
            projectile.timeLeft = 300;
            projectile.hostile = true;
            projectile.friendly = false;
        }

        public override bool PreAI()
        {
            projectile.frame = (int)projectile.ai[1];
            return true;
        }

        public override bool PreKill(int timeLeft)
        {
            Main.PlaySound(2, projectile.position, 50);
            int pieCut = 20;
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.IceDust>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / pieCut * 6.28f);
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.IceDust>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m /pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
            }
            return true;
        }
    }
}
