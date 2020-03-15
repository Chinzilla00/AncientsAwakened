using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public abstract class ArmorBounsLeech : ModProjectile
    {
        protected int dust = 0;
        bool runOnce = true;
        int effectPotency = 0;
        protected float potencyFactor = 1f;
        public virtual void PlayerBenifit(int potency, Player player)
        {
           
        }

        public override void AI()
        {
            if (runOnce)
            {
                runOnce = false;
                projectile.localNPCImmunity[(int)projectile.ai[0]] = 0;
            }
            if (effectPotency > 0)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, dust);
                Player player = Main.player[projectile.owner];
                projectile.velocity = (player.Center - projectile.Center).SafeNormalize(-Vector2.UnitY) * 12f;
                if (Collision.CheckAABBvAABBCollision(player.position, player.Size, projectile.position, projectile.Size))
                {
                    PlayerBenifit(effectPotency, player);
                    projectile.Kill();
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 0;
            projectile.localNPCImmunity[target.whoAmI] = -1;
            effectPotency = (int)(damage * potencyFactor);
            if (effectPotency > 0)
            {
                projectile.timeLeft = 120;
            }


        }
    }
    public class DarkLeech : ArmorBounsLeech
    {

        public override void SetDefaults()
        {
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.width = projectile.height = 4;
            projectile.usesLocalNPCImmunity = true;
            for (int i = 0; i < projectile.localNPCImmunity.Length; i++)
            {
                projectile.localNPCImmunity[i] = -1;
            }
            projectile.timeLeft = 3;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.magic = true;
            dust = mod.DustType("DarkmatterDust");
            potencyFactor = .02f;
        }
        public override void PlayerBenifit(int potency, Player player)
        {
            player.statLife += potency;
            player.HealEffect(potency, true);
        }

    }
    public class SunSiphon : ArmorBounsLeech
    {

        public override void SetDefaults()
        {
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.width = projectile.height = 4;
            projectile.usesLocalNPCImmunity = true;
            for (int i = 0; i < projectile.localNPCImmunity.Length; i++)
            {
                projectile.localNPCImmunity[i] = -1;
            }
            projectile.timeLeft = 3;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.magic = true;
            dust = mod.DustType("RadiumDust");
            potencyFactor = .25f;
        }
        public override void PlayerBenifit(int potency, Player player)
        {
            int manaCount = 0;
            int overloadCount = 0;
            for(; potency > 0; potency--)
            {
                if(player.statManaMax2 > player.statMana )
                {
                    player.statMana++;
                    manaCount++;
                }
                else
                {
                    overloadCount++;
                    
                }
            }
            if(manaCount>0)
            {
                player.ManaEffect(manaCount);
            }
            if (overloadCount >0)
            {
                if (player.HasBuff(mod.BuffType("ManaOverload")))
                {

                    player.buffTime[player.FindBuffIndex(mod.BuffType("ManaOverload"))] += overloadCount * 2;
                    if (player.buffTime[player.FindBuffIndex(mod.BuffType("ManaOverload"))] > 600)
                    {
                        player.buffTime[player.FindBuffIndex(mod.BuffType("ManaOverload"))] = 600;
                    }
                }
                else
                {
                    player.AddBuff(mod.BuffType("ManaOverload"), overloadCount * 2);
                }
                CombatText.NewText(player.Hitbox, Color.Purple, overloadCount * 2);
            }
            
        }

    }
}
