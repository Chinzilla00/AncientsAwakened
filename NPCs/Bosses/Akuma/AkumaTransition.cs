using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Akuma
{
    public class AkumaTransition : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Of Fury");
        }
        public override void SetDefaults()
        {
            projectile.width = 144;
            projectile.height = 144;
            projectile.friendly = false;
        }
        public int timer;
        public bool ATransitionActive = false;
        public override void AI()
        {
            timer++;
            ATransitionActive = true;
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
            if (timer == 375)          //if the timer has gotten to 7.5 seconds, this happens (60 = 1 second)
            {
                Main.NewText("Heh...", new Color(180, 41, 32));
                AAMod.AkumaMusic = true;
            }
            if (timer == 750)
            {
                Main.NewText("You know, kid...", new Color(180, 41, 32));
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

            if (timer == 900)
            {
                Main.NewText("fanning the flames doesn't put them out...", new Color(180, 41, 32));
            }

            if (timer == 1165)
            {
                projectile.Kill();
            }

        }

        public override void Kill(int timeleft)
        {
            ATransitionActive = false;
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

            Main.NewText("Akuma has been Awakened!", Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
            Main.NewText("IT ONLY MAKES THEM STRONGER", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);

            AAMod.AkumaMusic = false;

            NPC.NewNPC((int)projectile.position.X, (int)projectile.position.Y, mod.NPCType<AkumaA>());
        }
        
    }
}