using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

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
        public Player player;
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

            SpawnBoss(player, "YamataA", "Yamata Awakened");
            Main.NewText("Yamata has been Awakened!", Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
            Main.NewText("...MY TRUE ABYSSAL WRATH, YOU LITTLE WEALP!!!", new Color(146, 30, 68));

            AAMod.YamataMusic = false;

            
        }

        public void SpawnBoss(Player player, string name, string displayName)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-100f, 100f, (float)Main.rand.NextDouble()), 800f);
                Main.npc[npcID].netUpdate2 = true;
                string npcName = (!string.IsNullOrEmpty(Main.npc[npcID].GivenName) ? Main.npc[npcID].GivenName : displayName);
                if (Main.netMode == 0) { Main.NewText(Language.GetTextValue("Announcement.HasAwoken", npcName), 175, 75, 255, false); }
                else
                if (Main.netMode == 2)
                {
                    NetMessage.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", new object[]
                    {
                        NetworkText.FromLiteral(npcName)
                    }), new Color(175, 75, 255), -1);
                }
            }
        }

    }
}