using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Greed
{
    public class Nebula : ModProjectile
	{
        public override string Texture => "AAMod/BlankTex";

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 20;
			projectile.friendly = false; 
			projectile.hostile = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 240;
			projectile.alpha = 20;
			projectile.ignoreWater = true;
            projectile.tileCollide = true;          
		}

        public override void AI()
        {
            if (projectile.frameCounter++ > 5)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
        }

        Texture2D t;
        Color c;

        public void Setstuff()
        {
            if (projectile.ai[0] == 0)
            {
                t = mod.GetTexture("NPCs/Bosses/Greed/NebulaA");
                c = Color.HotPink;
            }
            else if (projectile.ai[0] == 1)
            {
                t = mod.GetTexture("NPCs/Bosses/Greed/NebulaD");
                c = Color.Blue;
            }
            else
            {
                t = mod.GetTexture("NPCs/Bosses/Greed/NebulaH");
                c = Color.Red;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Setstuff();
            Rectangle frame = BaseDrawing.GetFrame(projectile.frame, t.Width, t.Height / 3, 0, 0);
            BaseDrawing.DrawTexture(spriteBatch, t, 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.direction, 4, frame, ColorUtils.COLOR_GLOWPULSE, false);
            return false;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, ModContent.ProjectileType<NebulaBoost>(), 0, 0, Main.myPlayer, projectile.ai[0]);
            projectile.Kill();
        }

        public override void Kill(int timeleft)
        {
            Setstuff();
            int pieCut = 20;
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.AbyssDust>(), 0f, 0f, 100, c, 1.6f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.AbyssDust>(), 0f, 0f, 100, c, 2f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
        }
    }

    public class NebulaBoost : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;
            projectile.friendly = true;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.damage = 0;
            projectile.timeLeft = 130;
            projectile.alpha = 20;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
        }

        public override void AI()
        {
            if (projectile.frameCounter++ > 5)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }

            const int aislotHomingCooldown = 0;
            const int homingDelay = 0;
            const float desiredFlySpeedInPixelsPerFrame = 30;
            const float amountOfFramesToLerpBy = 20;

            projectile.ai[aislotHomingCooldown]++;
            if (projectile.ai[aislotHomingCooldown] > homingDelay)
            {
                projectile.ai[aislotHomingCooldown] = homingDelay;

                int foundTarget = BaseAI.GetNPC(projectile.Center, ModContent.NPCType<GreedA>(), -1);
                if (foundTarget != -1)
                {
                    NPC n = Main.npc[foundTarget];
                    Vector2 desiredVelocity = projectile.DirectionTo(n.Center) * desiredFlySpeedInPixelsPerFrame;
                    projectile.velocity = Vector2.Lerp(projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
                }
            }
        }

        Texture2D t;
        Color c;

        public void Setstuff()
        {
            if (projectile.ai[0] == 0)
            {
                t = mod.GetTexture("NPCs/Bosses/Greed/NebBoostA");
                c = Color.HotPink;
            }
            else if (projectile.ai[0] == 1)
            {
                t = mod.GetTexture("NPCs/Bosses/Greed/NebBoostD");
                c = Color.Blue;
            }
            else
            {
                t = mod.GetTexture("NPCs/Bosses/Greed/NebBoostH");
                c = Color.Red;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Setstuff();
            Rectangle frame = BaseDrawing.GetFrame(projectile.frame, t.Width, t.Height / 3, 0, 0);
            BaseDrawing.DrawTexture(spriteBatch, t, 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.direction, 4, frame, ColorUtils.COLOR_GLOWPULSE, false);
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.type == ModContent.NPCType<GreedA>())
            {
                if (projectile.ai[0] == 0)
                {
                    target.AddBuff(ModContent.BuffType<BuffA>(), 180);
                }
                else if (projectile.ai[0] == 1)
                {
                    target.AddBuff(ModContent.BuffType<BuffD>(), 180);
                }
                else
                {
                    target.life += 1000;
                    CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), CombatText.HealLife, 1000, false, false);
                }
            }
            projectile.Kill();
        }


        public override void Kill(int timeleft)
        {
            Setstuff();
            int pieCut = 20;
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.AbyssDust>(), 0f, 0f, 100, c, 1.6f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.AbyssDust>(), 0f, 0f, 100, c, 2f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
        }
    }

    public class BuffA : ModBuff
    {
        public override void SetDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }
    }
    public class BuffD : ModBuff
    {
        public override void SetDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }
    }
}
