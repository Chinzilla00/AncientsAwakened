
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;

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
                dust1.scale *= 1.3f;
                dust1.noGravity = true;
                dust1.velocity.Y -= 6;
                dust2.scale *= 1.3f;
                dust2.noGravity = true;
                dust2.velocity.Y -= 6;
                dust3.scale *= 1.3f;
                dust3.noGravity = true;
                dust3.velocity.Y -= 6;
                dust4.scale *= 1.3f;
                dust4.noGravity = true;
                dust4.velocity.Y -= 6;
            }
            if (timer == 375)    
            {
                Main.NewText("NYEHEHEHEHEHEHEHEH~!", new Color(45, 46, 70));
                AAMod.YamataMusic = true;
            }
            if (timer == 650)
            {
                Main.NewText("You thought I was DONE..?!", new Color(45, 46, 70));
            }
            if (timer == 900)
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
                dust1.scale *= 1.3f;
                dust1.noGravity = true;
                dust1.velocity.Y -= 9;
                dust2.scale *= 1.3f;
                dust2.noGravity = true;
                dust2.velocity.Y -= 9;
                dust3.scale *= 1.3f;
                dust3.noGravity = true;
                dust3.velocity.Y -= 9;
                dust4.scale *= 1.3f;
                dust4.noGravity = true;
                dust4.velocity.Y -= 9;
            }
            if (timer == 1100)
            {
                Main.NewText("I HOPE YOU ARE READY...", new Color(45, 46, 70));
            }
            if (timer == 1455)
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

            SpawnBoss(projectile.Center, "YamataA", "Yamata Awakened");
            Main.NewText("Yamata has been Awakened!", Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
            Main.NewText("...TO FACE MY TRUE ABYSSAL WRATH, YOU LITTLE WHELP!!!", new Color(146, 30, 68));
            AAMod.YamataMusic = false; 
        }

        public void SpawnBoss(Vector2 center, string name, string displayName)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)center.X, (int)center.Y, bossType, 0);
                Main.npc[npcID].Center = center - new Vector2(MathHelper.Lerp(-100f, 100f, (float)Main.rand.NextDouble()), 0f);
                Main.npc[npcID].netUpdate2 = true;			
                string npcName = (!string.IsNullOrEmpty(Main.npc[npcID].GivenName) ? Main.npc[npcID].GivenName : displayName);
                /*f (Main.netMode == 0) { Main.NewText(Language.GetTextValue("Announcement.HasAwoken", npcName), 175, 75, 255, false); }
                else
                if (Main.netMode == 2)
                {
                    NetMessage.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", new object[]
                    {
                        NetworkText.FromLiteral(npcName)
                    }), new Color(175, 75, 255), -1);
                }*/
            }
        }

    }
}