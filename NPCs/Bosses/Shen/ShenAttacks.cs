
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

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
                Projectile.NewProjectile(vector2.X, vector2.Y, speedX5 * 2, speedY6 * 2, mod.ProjectileType<NPCs.Bosses.Shen.DiscordianInferno>(), npc.damage / 6, 6, Main.myPlayer, 0, 0);
            }
        }
				
        public static void Thunderstrike(NPC npc, Mod mod)
        {
			
					Player player = Main.player[npc.target];
				
                int indexer = 1;
                float Speed = 25f;  //projectile speed
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 10;  //projectile damage
                int type = mod.ProjectileType("Arrow");  //put your projectile
                Main.PlaySound(23, (int)npc.position.X, (int)npc.position.Y, 17);
                float rot = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));


             

                    indexer = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rot) * Speed) * -1), (float)((Math.Sin(rot) * Speed) * -1), type, damage, 0f, 0);

             

                    for (indexer = 0; indexer < 200; indexer++)
                    {
                        if (Main.projectile[indexer].type == mod.ProjectileType("Arrow"))
                        {
							

            for (int num119 = 0; num119 < 2; num119++)
            {
                for (int num120 = 0; num120 < 3; num120++)
                {
                    Vector2 vector12 = new Vector2(Main.projectile[indexer].position.X, Main.projectile[indexer].position.Y);
                    float num75 = 20f;
                    Vector2 vector2 = Main.projectile[indexer].Center + new Vector2(-(float)Main.rand.Next(0, 401) * Main.projectile[indexer].direction, -600f);
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
                    int L = Projectile.NewProjectile(vector2.X, vector2.Y, speedX5 * 2, speedY5 * 2, mod.ProjectileType<NPCs.Bosses.Shen.ChaosLightning>(), npc.damage / 6, 1, Main.myPlayer, vector13.ToRotation());
                    Main.projectile[L].penetrate = -1;
                    Main.projectile[L].hostile = false;
                    Main.projectile[L].hostile = true;
                }
            }
		
        
    }
					}
				}
			}
			
}
