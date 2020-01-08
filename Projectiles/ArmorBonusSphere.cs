using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public abstract class ArmorBonusSphere : ModProjectile
    {
        protected int useDust = 0;
        public virtual void InflictBuffs(NPC target)
        {

        }

        bool runOnce = true;
        int shader;
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if(runOnce)
            {
                runOnce = false;
                shader = player.dye[1].dye;
            }
            projectile.frameCounter++;
            if (projectile.frameCounter % 10 == 0)
            {
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }
            if (projectile.timeLeft == 3)
            {
                Explode();
            }
            if (Main.rand.Next(4) == 0)
            {
                Dust dyeMe = Dust.NewDustPerfect(projectile.Center, useDust);
                if (shader != 0)
                {
                    dyeMe.shader = GameShaders.Armor.GetSecondaryShader(shader, player);
                }
                    
            }

        }
       
        
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Player player = Main.player[projectile.owner];

            if (shader != 0)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

                GameShaders.Armor.GetSecondaryShader(shader, player).Apply(null);
            }
            return projectile.timeLeft > 2;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            
            Player player = Main.player[projectile.owner];
            if (shader != 0)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.EffectMatrix);
            }
        }
        
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.localNPCImmunity[target.whoAmI] = -1;
            target.immune[projectile.owner] = 0;
            InflictBuffs(target);
            Explode();
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Explode();
            return false;
        }
        void Explode()
        {
            if (projectile.timeLeft > 2)
            {
                projectile.position.X -= 50;
                projectile.position.Y -= 50;
                projectile.width = projectile.height = 100;
                projectile.tileCollide = false;
                for (int d = 0; d < 40; d++)
                {
                    Dust dyeMe = Dust.NewDustPerfect(projectile.Center, useDust, PolarVector(Main.rand.NextFloat(6f), Main.rand.NextFloat((float)Math.PI * 2)));
                    if (shader != 0)
                    {
                        Player player = Main.player[projectile.owner];
                        dyeMe.shader = GameShaders.Armor.GetSecondaryShader(shader, player);
                    }
                }
                projectile.timeLeft = 2;
            }
        }
        public static Vector2 PolarVector(float radius, float theta)
        {
            return new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta)) * radius;
        }
    }
    public class DarkmatterSphere : ArmorBonusSphere
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.width = projectile.height = 22;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.timeLeft = 600;
            projectile.extraUpdates = 1;
            useDust = mod.DustType("DarkmatterDust");

        }
        public override void InflictBuffs(NPC target)
        {
            target.AddBuff(mod.BuffType("DarkCurse"), 600);
            if(!target.boss)
            {
                target.AddBuff(mod.BuffType("DarkLock"), 120);
            }
        }
    }
    public class SunSphere : ArmorBonusSphere
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.width = projectile.height = 22;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.timeLeft = 600;
            projectile.extraUpdates = 1;
            useDust = mod.DustType("RadiumDust");

        }
        public override void InflictBuffs(NPC target)
        {
            target.AddBuff(mod.BuffType("RadiumInferno"), 600);
        }
    }
}
