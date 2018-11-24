using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using AAMod;

namespace AAMod.NPCs.Bosses.Yamata
{
    public class YamataTransition : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirit of Wrath");
        }
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
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
                dust1 = Main.dust[Dust.NewDust(position, 1, 1, mod.DustType<Dusts.YamataDust>(), 0, 0, 0, default(Color), 1f)];
                dust2 = Main.dust[Dust.NewDust(position, 1, 1, mod.DustType<Dusts.YamataDust>(), 0, 0, 0, default(Color), 1f)];
                dust3 = Main.dust[Dust.NewDust(position, 1, 1, mod.DustType<Dusts.YamataDust>(), 0, 0, 0, default(Color), 1f)];
                dust4 = Main.dust[Dust.NewDust(position, 1, 1, mod.DustType<Dusts.YamataDust>(), 0, 0, 0, default(Color), 1f)];
                dust1.noGravity = true;
                dust1.velocity.Y -= 6;
                dust2.noGravity = true;
                dust2.velocity.Y -= 6;
                dust3.noGravity = true;
                dust3.velocity.Y -= 6;
                dust4.noGravity = true;
                dust4.velocity.Y -= 6;
            }
            if (timer == 375)          //if the timer has gotten to 7.5 seconds, this happens (60 = 1 second)
            {
                Main.NewText("NYEHEHEHEHEHEHEHEH~!", new Color(45, 46, 70));
                AAMod.YamataMusic = true;
            }
            if (timer == 550)
            {
                Main.NewText("You thought I was DONE..?!", new Color(45, 46, 70));
            }

            if (timer == 725)
            {
                Main.NewText("HAH! AS IF!", new Color(45, 46, 70));
            }
            if (timer >= 900)
            {
                Dust dust1;
                Dust dust2;
                Dust dust3;
                Dust dust4;
                Vector2 position = projectile.position;
                dust1 = Main.dust[Dust.NewDust(position, 1, 1, mod.DustType<Dusts.YamataADust>(), 0, 0, 0, default(Color), 1f)];
                dust2 = Main.dust[Dust.NewDust(position, 1, 1, mod.DustType<Dusts.YamataADust>(), 0, 0, 0, default(Color), 1f)];
                dust3 = Main.dust[Dust.NewDust(position, 1, 1, mod.DustType<Dusts.YamataADust>(), 0, 0, 0, default(Color), 1f)];
                dust4 = Main.dust[Dust.NewDust(position, 1, 1, mod.DustType<Dusts.YamataADust>(), 0, 0, 0, default(Color), 1f)];
                dust1.noGravity = true;
                dust1.velocity.Y -= 9;
                dust2.noGravity = true;
                dust2.velocity.Y -= 9;
                dust3.noGravity = true;
                dust3.velocity.Y -= 9;
                dust4.noGravity = true;
                dust4.velocity.Y -= 9;
            }

            if (timer == 900)
            {
                Main.NewText("YOU SHALL BE FACING...", new Color(45, 46, 70));
            }

            if (timer == 1080)
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
            dust1 = Main.dust[Dust.NewDust(position, projectile.width, projectile.height, mod.DustType<Dusts.YamataADust>(), 0, 0, 0, default(Color), 1f)];
            dust2 = Main.dust[Dust.NewDust(position, projectile.width, projectile.height, mod.DustType<Dusts.YamataADust>(), 0, 0, 0, default(Color), 1f)];
            dust3 = Main.dust[Dust.NewDust(position, projectile.width, projectile.height, mod.DustType<Dusts.YamataADust>(), 0, 0, 0, default(Color), 1f)];
            dust4 = Main.dust[Dust.NewDust(position, projectile.width, projectile.height, mod.DustType<Dusts.YamataADust>(), 0, 0, 0, default(Color), 1f)];
            dust5 = Main.dust[Dust.NewDust(position, projectile.width, projectile.height, mod.DustType<Dusts.YamataADust>(), 0, 0, 0, default(Color), 1f)];
            dust6 = Main.dust[Dust.NewDust(position, projectile.width, projectile.height, mod.DustType<Dusts.YamataADust>(), 0, 0, 0, default(Color), 1f)];
            dust1.noGravity = true;
            dust1.velocity.Y -= 1;
            dust2.noGravity = true;
            dust2.velocity.Y -= 1;
            dust3.noGravity = true;
            dust3.velocity.Y -= 1;
            dust4.noGravity = true;
            dust4.velocity.Y -= 1;
            dust5.noGravity = true;
            dust5.velocity.Y -= 1;
            dust6.noGravity = true;
            dust6.velocity.Y -= 1;

            Main.NewText("Yamata has been Awakened!", Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
            Main.NewText("...MY TRUE ABYSSAL WRATH, YOU LITTLE WEALP!!!", new Color(146, 30, 68));

            AAMod.YamataMusic = false;

            NPC.NewNPC((int)projectile.position.X, (int)projectile.position.Y, mod.NPCType("YamataA"));
        }
        
    }
}