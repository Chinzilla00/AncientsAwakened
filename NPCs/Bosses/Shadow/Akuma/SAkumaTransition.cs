using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shadow.Akuma
{
    public class SAkumaTransition : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Of Fury");
        }
        public override void SetDefaults()
        {
            projectile.width = 144;
            projectile.height = 144;
            projectile.friendly = false;
        }
        public int timer;
        public override void AI()
        {
            timer++;
            if (timer < 900)
            {
                Dust dust1;
                Dust dust2;
                Dust dust3;
                Dust dust4;
                Vector2 position = projectile.position;
                dust1 = Main.dust[Dust.NewDust(position, 1, 1, mod.DustType<Dusts.AkumaDust>(), 0, 0, 0, default(Color), 1f)];
                dust2 = Main.dust[Dust.NewDust(position, 1, 1, mod.DustType<Dusts.AkumaDust>(), 0, 0, 0, default(Color), 1f)];
                dust3 = Main.dust[Dust.NewDust(position, 1, 1, mod.DustType<Dusts.AkumaDust>(), 0, 0, 0, default(Color), 1f)];
                dust4 = Main.dust[Dust.NewDust(position, 1, 1, mod.DustType<Dusts.AkumaDust>(), 0, 0, 0, default(Color), 1f)];
                dust1.noGravity = false;
                dust2.noGravity = false;
                dust3.noGravity = false;
                dust4.noGravity = false;
            }
            if (timer >= 900)
            {
                Dust dust1;
                Dust dust2;
                Dust dust3;
                Dust dust4;
                Vector2 position = projectile.position;
                dust1 = Main.dust[Dust.NewDust(position, 1, 1, mod.DustType<Dusts.AkumaADust>(), 0, 0, 0, default(Color), 1f)];
                dust2 = Main.dust[Dust.NewDust(position, 1, 1, mod.DustType<Dusts.AkumaADust>(), 0, 0, 0, default(Color), 1f)];
                dust3 = Main.dust[Dust.NewDust(position, 1, 1, mod.DustType<Dusts.AkumaADust>(), 0, 0, 0, default(Color), 1f)];
                dust4 = Main.dust[Dust.NewDust(position, 1, 1, mod.DustType<Dusts.AkumaADust>(), 0, 0, 0, default(Color), 1f)];
                dust1.noGravity = false;
                dust2.noGravity = false;
                dust3.noGravity = false;
                dust4.noGravity = false;
            }

            if (timer == 1125)
            {
                projectile.Kill();
            }

        }

        public override void Kill(int timeleft)
        {
            Dust dust1;
            Dust dust2;
            Dust dust3;
            Dust dust4;
            Dust dust5;
            Dust dust6;
            Vector2 position = projectile.position;
            dust1 = Main.dust[Dust.NewDust(position, projectile.width, projectile.height, mod.DustType<Dusts.AkumaADust>(), 0, 0, 0, default(Color), 1f)];
            dust2 = Main.dust[Dust.NewDust(position, projectile.width, projectile.height, mod.DustType<Dusts.AkumaADust>(), 0, 0, 0, default(Color), 1f)];
            dust3 = Main.dust[Dust.NewDust(position, projectile.width, projectile.height, mod.DustType<Dusts.AkumaADust>(), 0, 0, 0, default(Color), 1f)];
            dust4 = Main.dust[Dust.NewDust(position, projectile.width, projectile.height, mod.DustType<Dusts.AkumaADust>(), 0, 0, 0, default(Color), 1f)];
            dust5 = Main.dust[Dust.NewDust(position, projectile.width, projectile.height, mod.DustType<Dusts.AkumaADust>(), 0, 0, 0, default(Color), 1f)];
            dust6 = Main.dust[Dust.NewDust(position, projectile.width, projectile.height, mod.DustType<Dusts.AkumaADust>(), 0, 0, 0, default(Color), 1f)];
            dust1.noGravity = false;
            dust2.noGravity = false;
            dust3.noGravity = false;
            dust4.noGravity = false;
            dust5.noGravity = false;
            dust6.noGravity = false;

            Main.NewText("The Shadow of Akuma has been Awakened!", Color.Silver.R, Color.Silver.G, Color.Silver.B);

            AAMod.AkumaMusic = false;

            NPC.NewNPC((int)projectile.position.X, (int)projectile.position.Y, mod.NPCType<SAkumaA>());
        }
        
    }
}