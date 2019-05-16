using BaseMod;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System;
using Terraria;
using Terraria.ID;

namespace AAMod
{
    public class ShenAttacks
	{
        static AAPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<AAPlayer>();
        
        public static void Dragonfire(NPC npc, Mod mod)
        {
            Player player = Main.player[npc.target];
            Vector2 vector12 = new Vector2(player.Center.X, player.Center.Y);
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num75 = 20f;
            float num119 = vector12.Y;
            if (num119 > player.Center.Y - 200f)
            {
                num119 = player.Center.Y - 200f;
            }
            for (int num120 = 0; num120 < 3; num120++)
            {
                vector2 = player.Center + new Vector2(-(float)Main.rand.Next(0, 401) * player.direction, -600f);
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
                float speedY6 = num83 + Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(vector2.X, vector2.Y, speedX5, speedY6, mod.ProjectileType<NPCs.Bosses.Shen.DiscordianInferno>(), npc.damage / 4, 6, Main.myPlayer);
            }
        }

        public static void Eruption(NPC npc, Mod mod)
        {
            Player player = Main.player[npc.target];
            float num72 = 18;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num78 = player.Center.X + Main.screenPosition.X - vector2.X;
            float num79 = player.Center.Y + Main.screenPosition.Y - vector2.Y;
            if (player.gravDir == -1f)
            {
                num79 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector2.Y;
            }
            float num80 = (float)Math.Sqrt((double)((num78 * num78) + (num79 * num79)));
            float num81 = num80;
            if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
            {
                num78 = (float)npc.direction;
                num79 = 0f;
                num80 = num72;
            }
            else
            {
                num80 = num72 / num80;
            }
            num78 *= num80;
            num79 *= num80;
            vector2 = new Vector2(player.position.X + ((float)player.width * 0.5f) + (float)(Main.rand.Next(201) * -(float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
            vector2.X = ((vector2.X + player.Center.X) / 2f) + (float)Main.rand.Next(-200, 201);
            vector2.Y -= (float)(100);
            num78 = player.Center.X + Main.screenPosition.X - vector2.X + ((float)Main.rand.Next(-40, 41) * 0.03f);
            num79 = player.Center.Y + Main.screenPosition.Y - vector2.Y;
            if (num79 < 0f)
            {
                num79 *= -1f;
            }
            if (num79 < 20f)
            {
                num79 = 20f;
            }
            num80 = (float)Math.Sqrt((double)((num78 * num78) + (num79 * num79)));
            num80 = num72 / num80;
            num78 *= num80;
            num79 *= num80;
            float num114 = num78;
            float num115 = num79 + ((float)Main.rand.Next(-40, 41) * 0.02f);
            Projectile.NewProjectile(vector2.X, vector2.Y, 0, num115 * 1.5f, mod.ProjectileType("ShenMeteor1"), (int)(npc.damage / 1.3f), 0, player.whoAmI, 0f, 1f);
        }
    }
}