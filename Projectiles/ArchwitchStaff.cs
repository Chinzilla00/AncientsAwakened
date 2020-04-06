using System;
using System.Collections.Generic;
using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class ArchwitchStaff : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Archwitch Staff");
        }
        public override void SetDefaults()
        {
            projectile.width = 110;
            projectile.height = 110;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;       
            projectile.melee = true;
        }

        public override void AI()
        {
            projectile.soundDelay--;
            if (projectile.soundDelay <= 0)
            {
                Main.PlaySound(2, (int)projectile.Center.X, (int)projectile.Center.Y, 15);
                projectile.soundDelay = 45;
            }
            Player player = Main.player[projectile.owner];
            if (Main.myPlayer == projectile.owner)
            {
                if (!player.channel || player.noItems || player.CCed)
                {
                    projectile.Kill();
                }
            }
            Lighting.AddLight(projectile.Center, .6f, 0.6f, .7f);
            projectile.Center = player.MountedCenter;
            projectile.position.X += player.width / 2 * player.direction;
            projectile.spriteDirection = player.direction;
            projectile.rotation += 0.3f * player.direction;
            if (projectile.rotation > MathHelper.TwoPi)
            {
                projectile.rotation -= MathHelper.TwoPi;
            }
            else if (projectile.rotation < 0)
            {
                projectile.rotation += MathHelper.TwoPi;
            }
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = projectile.rotation;
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Shadowflame);
            Main.dust[dust].velocity /= 1f;

            int[] Targets = GetNPCs(projectile.Center, -1, 600f);

            if (Targets.Length != 0)
            {
                for (int e = 0; e < Targets.Length; e++)
                {
                    int pID;
                    bool properSide = projectile.owner == Main.myPlayer;
                    if (properSide)
                    {
                        NPC target = Main.npc[Targets[e]];
                        Vector2 targetCenter = projectile.position + new Vector2(target.width * 0.5f, target.height * 0.5f);
                        projectile.ai[1]--;
                        if (projectile.ai[1] <= 0)
                        {
                            Vector2 fireTarget = projectile.Center;
                            float rot = BaseUtility.RotationTo(projectile.Center, targetCenter);
                            fireTarget = BaseUtility.RotateVector(projectile.Center, fireTarget, rot);
                            pID = BaseAI.FireProjectile(targetCenter, fireTarget, ModContent.ProjectileType<ArchwitchStar>(), projectile.damage, 0f, 10);
                            Main.projectile[pID].ai[1] = target.whoAmI;
                            projectile.ai[1] = 40;
                        }
                    }
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, null, Color.White, projectile.rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }

        public static int[] GetNPCs(Vector2 center, int npcType = -1, float distance = 500F)
        {
            List<int> allNPCs = new List<int>();
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (npc != null && npc.active && npc.life > 0 && (npcType == -1 || npc.type == npcType) && npc.type != NPCID.TargetDummy && !npc.friendly && (distance == -1 || npc.Distance(center) < distance))
                {
                    bool add = true;
                    if (add) { allNPCs.Add(i); }
                }
            }
            return allNPCs.ToArray();
        }
    }
}