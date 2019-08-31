using System;
using System.IO;
using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class ChaosChainEX : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Perfect Chaos Chain");
		}

        public override void SetDefaults()
        {
            projectile.width = 58;
            projectile.height = 58;
            projectile.friendly = true;
            projectile.penetrate = -1; 
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 5;
            projectile.extraUpdates = 1;
        }

        public float[] InternalAI = new float[2];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(InternalAI[0]);
                writer.Write(InternalAI[1]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                InternalAI[0] = reader.ReadFloat();
                InternalAI[1] = reader.ReadFloat();
            }
        }

        float Rot = 0;
        int Dir = 1;
		
		public override void AI()
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == mod.ProjectileType<ChaosChainEXSaw>() && projectile.ai[0] == 1f)
                {
                    InternalAI[1] = 1;
                }
            }
            if (projectile.velocity.X < 0)
            {
                Dir = -1;
            }
            Rot += 0.03f * projectile.direction;

            if (projectile.timeLeft == 120)
            {
                projectile.ai[0] = 1f;
            }

            if (Main.player[projectile.owner].dead)
            {
                projectile.Kill();
                return;
            }

            Main.player[projectile.owner].itemAnimation = 5;
            Main.player[projectile.owner].itemTime = 5;

            if (projectile.alpha == 0)
            {
                if (projectile.position.X + (projectile.width / 2) > Main.player[projectile.owner].position.X + (Main.player[projectile.owner].width / 2))
                {
                    Main.player[projectile.owner].ChangeDir(1);
                }
                else
                {
                    Main.player[projectile.owner].ChangeDir(-1);
                }
            }
            projectile.rotation += .4f;
            Vector2 vector14 = new Vector2(projectile.position.X + (projectile.width * 0.5f), projectile.position.Y + (projectile.height * 0.5f));
            float num166 = Main.player[projectile.owner].position.X + (Main.player[projectile.owner].width / 2) - vector14.X;
            float num167 = Main.player[projectile.owner].position.Y + (Main.player[projectile.owner].height / 2) - vector14.Y;
            float num168 = (float)Math.Sqrt((num166 * num166) + (num167 * num167));
            if (projectile.ai[0] == 0f)
            {
                if (num168 > 1000)
                {
                    Projectile.NewProjectile(projectile.position, projectile.velocity, mod.ProjectileType<ChaosChainEXSaw>(), projectile.damage, 0, Main.myPlayer);
                    projectile.ai[0] = 1f;
                }
                else if (num168 > 500f)
                {
                    Projectile.NewProjectile(projectile.position, projectile.velocity, mod.ProjectileType<ChaosChainEXSaw>(), projectile.damage, 0, Main.myPlayer);
                    projectile.ai[0] = 1f;
                }
                projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
                projectile.ai[1] += 1f;
                if (projectile.ai[1] > 5f)
                {
                    projectile.alpha = 0;
                }
                if (projectile.ai[1] > 8f)
                {
                    projectile.ai[1] = 8f;
                }
                if (projectile.ai[1] >= 10f)
                {
                    projectile.ai[1] = 15f;
                    projectile.velocity.Y = projectile.velocity.Y + 0.3f;
                }
                if (projectile.velocity.X < 0f)
                {
                    projectile.spriteDirection = -1;
                }
                else
                {
                    projectile.spriteDirection = 1;
                }
            }
            else if (projectile.ai[0] == 1f)
            {
                projectile.tileCollide = false;
                projectile.rotation = (float)Math.Atan2(num167, num166) - 1.57f;
                float num169 = 30f;

                if (num168 < 50f)
                {
                    projectile.Kill();
                }
                num168 = num169 / num168;
                num166 *= num168;
                num167 *= num168;
                projectile.velocity.X = num166;
                projectile.velocity.Y = num167;
                if (projectile.velocity.X < 0f)
                {
                    projectile.spriteDirection = 1;
                }
                else
                {
                    projectile.spriteDirection = -1;
                }

            }
        }
		
		public override void OnHitNPC (NPC target, int damage, float knockback, bool crit)
		{
            if (projectile.ai[0] == 0f)
            {
                Projectile.NewProjectile(projectile.position, projectile.velocity, mod.ProjectileType<ChaosChainEXSaw>(), projectile.damage, 0, Main.myPlayer);
            }
            target.AddBuff(mod.BuffType<Buffs.DiscordInferno>(), 240);
        }
		
        // chain voodoo
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = mod.GetTexture("Projectiles/ChaosChainEX_Chain");
            BaseDrawing.DrawChain(spriteBatch, texture, 0, projectile.Center, Main.player[projectile.owner].Center, 0f, lightColor, 1f, true);
            Texture2D Tex = mod.GetTexture("Projectiles/ChaosChainEXSaw");
            Texture2D Tex2 = mod.GetTexture("Projectiles/ChaosChainEXSphere");
            Rectangle frame = new Rectangle(0, 0, Tex.Width, Tex.Height);
            for (int i = 0; i < 1000; ++i)
            {
                if (projectile.ai[0] == 0f)
                {
                    BaseDrawing.DrawTexture(spriteBatch, Tex2, 0, projectile.position, projectile.width, projectile.height, projectile.scale, Rot, Dir, 1, frame, lightColor, true);
                }
                else
                {
                    BaseDrawing.DrawTexture(spriteBatch, Tex, 0, projectile.position, projectile.width, projectile.height, projectile.scale, Rot, Dir, 1, frame, lightColor, true);
                }
            }
            return true;
        }
    }
}