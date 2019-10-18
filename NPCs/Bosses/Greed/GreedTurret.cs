using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Greed
{
    public class GreedTurret : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Singularity of Desire");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 4500;
            npc.defense = 100;
            npc.width = 60;
            npc.height = 60;
            npc.aiStyle = -1;
            npc.HitSound = new LegacySoundStyle(21, 1);
            npc.DeathSound = new LegacySoundStyle(2, 14);
            npc.knockBackResist = 0f;
            npc.noGravity = true;
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            int damage = 34;
            Vector2 npcCenter = new Vector2(npc.Center.X, npc.Center.Y);

            if (npc.ai[0] == 0)
            {
                npc.ai[0] = Main.rand.Next(1, 4);
            }

            if (npc.ai[0] == 1)
            {
                int type = ModContent.ProjectileType<GreedLaser>();
                float Speed = 8f;
                float rotation = (float)Math.Atan2(npcCenter.Y - (player.position.Y + (player.height * 0.5f)), npcCenter.X - (player.position.X + (player.width * 0.5f)));

                if (++npc.ai[1] >= 80)
                {
                    Main.PlaySound(SoundID.DD2_BetsyFireballShot, (int)npc.position.X, (int)npc.position.Y);
                    int proj = Projectile.NewProjectile(npcCenter.X, npcCenter.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                    Main.projectile[proj].netUpdate = true;
                    npc.ai[1] = 0;
                }
            }
            if (npc.ai[0] == 2)
            {
                int type = ModContent.ProjectileType<GreedLaser>();
                float Speed = 7f;
                float rotation = (float)Math.Atan2(npcCenter.Y - (player.position.Y + (player.height * 0.5f)), npcCenter.X - (player.position.X + (player.width * 0.5f)));

                if (++npc.ai[1] >= 120)
                {
                    Main.PlaySound(SoundID.DD2_BetsyFireballShot, (int)npc.position.X, (int)npc.position.Y);
                    int proj = Projectile.NewProjectile(npcCenter.X, npcCenter.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                    int proj2 = Projectile.NewProjectile(npcCenter.X, npcCenter.Y, (float)((Math.Cos(rotation) * Speed) * -1) + 2, (float)((Math.Sin(rotation) * Speed) * -1) + 2, type, damage, 0f, 0);
                    int proj3 = Projectile.NewProjectile(npcCenter.X, npcCenter.Y, (float)((Math.Cos(rotation) * Speed) * -1) - 2, (float)((Math.Sin(rotation) * Speed) * -1) - 2, type, damage, 0f, 0);
                    Main.projectile[proj].netUpdate = true;
                    npc.ai[1] = 0;
                }
            }
            if (npc.ai[0] >= 3)
            {
                int type = ModContent.ProjectileType<DesireBeam>();
                float Speed = 10f;
                float rotation = (float)Math.Atan2(npcCenter.Y - (player.Center.Y), npcCenter.X - (player.Center.X));

                if (++npc.ai[1] >= 200)
                {
                    Main.PlaySound(SoundID.DD2_BetsysWrathShot, (int)npc.position.X, (int)npc.position.Y);
                    int proj = Projectile.NewProjectile(npcCenter.X, npcCenter.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                    Main.projectile[proj].netUpdate = true;
                    npc.ai[1] = 0;
                }
            }

        }
    }
}