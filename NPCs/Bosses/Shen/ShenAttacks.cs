using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System;
using Terraria;

namespace AAMod
{
    public class ShenAttacks
	{
        public static void Dragonfire(NPC npc, Mod mod)
        {
            Player player = Main.player[npc.target];
            Vector2 vector12 = new Vector2(player.Center.X, player.Center.Y);
            float num75 = 20f;
            for (int num120 = 0; num120 < 3; num120++)
            {
                Vector2 vector2 = player.Center + new Vector2(-(float)Main.rand.Next(0, 401) * player.direction, -600f);
                vector2.Y -= 120 * num120;
                Vector2 vector13 = vector12 - vector2;
                if (vector13.Y < 0f)
                {
                    vector13.Y *= -1f;
                }
                if (vector13.Y < 20f)
                {
                    vector13.Y = 20f;
                }
                vector13.Normalize();
                vector13 *= num75;
                float num82 = vector13.X;
                float num83 = vector13.Y;
                float speedX5 = num82;
                float speedY6 = num83 + Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(vector2.X, vector2.Y, speedX5, speedY6, mod.ProjectileType<NPCs.Bosses.Shen.DiscordianInferno>(), npc.damage / 4, 6, Main.myPlayer, 0, 0);
            }
        }

        public static void Thunderstrike(NPC npc, Mod mod)
        {
            Player player = Main.player[npc.target];
            for (int num120 = 0; num120 < 3; num120++)
            {
                Vector2 vector12 = new Vector2(player.Center.X, player.Center.Y);
                float num75 = 20f;
                Vector2 vector2 = player.Center + new Vector2(-(float)Main.rand.Next(0, 401) * player.direction, -600f);
                vector2.Y -= 100 * num120;
                Vector2 vector13 = vector12 - vector2;
                if (vector13.Y < 0f)
                {
                    vector13.Y *= -1f;
                }
                if (vector13.Y < 20f)
                {
                    vector13.Y = 20f;
                }
                vector13.Normalize();
                vector13 *= num75;
                float num82 = vector13.X;
                float num83 = vector13.Y;
                float speedX5 = num82;
                float speedY5 = num83 + Main.rand.Next(-5, 5) * 0.02f;
                int L = Projectile.NewProjectile(vector2.X, vector2.Y, speedX5, speedY5 * 2, mod.ProjectileType<NPCs.Bosses.Shen.ChaosLightning>(), npc.damage / 2, 1, player.whoAmI, vector13.ToRotation());
                Main.projectile[L].penetrate = -1;
                Main.projectile[L].hostile = false;
                Main.projectile[L].friendly = true;
            }
        }

        public static void Eruption(NPC npc, Mod mod)
        {
            Player player = Main.player[npc.target];
            float num72 = 18;
            Vector2 vector2 = new Vector2(player.position.X + (player.width * 0.5f) + (Main.rand.Next(201) * -(float)player.direction) + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
            vector2.X = ((vector2.X + player.Center.X) / 2f) + Main.rand.Next(-200, 201);
            vector2.Y -= 100;
            float num78 = player.Center.X + Main.screenPosition.X - vector2.X + ((float)Main.rand.Next(-40, 41) * 0.03f);
            float num79 = player.Center.Y + Main.screenPosition.Y - vector2.Y;
            if (num79 < 0f)
            {
                num79 *= -1f;
            }
            if (num79 < 20f)
            {
                num79 = 20f;
            }
            float num80 = (float)Math.Sqrt((double)((num78 * num78) + (num79 * num79)));
            num80 = num72 / num80;
            num79 *= num80;
            float num115 = num79 + (Main.rand.Next(-40, 41) * 0.02f);
            Projectile.NewProjectile(vector2.X, vector2.Y, 0, num115 * 2, mod.ProjectileType("ShenMeteor1"), (int)(npc.damage / 1.3f), 0, player.whoAmI, 0f, 1f);
        }
    }
}