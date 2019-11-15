using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Yamata.Awakened
{
    public class YamataHarukaProj : ModProjectile
    {
        public override string Texture => "AAMod/NPCs/Bosses/Yamata/Awakened/HarukaY";

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Haruka Yamata");
            Main.projFrames[projectile.type] = 28;
		}
    	
        public override void SetDefaults()
        {
            projectile.width = 50;
            projectile.height = 60;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 480;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
        }

        public override void AI()
        {
            int ai0 = (int)projectile.ai[0];
            if (ai0 < 0 || ai0 >= Main.maxPlayers)
            {
                projectile.Kill();
                return;
            }

            Player player = Main.player[ai0];
            const float dashTime = 90;
            if (++projectile.ai[1] <= dashTime) //move beside player
            {
                Vector2 target = player.Center;
                target.X += projectile.Center.X < player.Center.X ? -400 : 400;
                MoveToPoint(target);
                if (projectile.ai[1] == dashTime) //dash
                {
                    projectile.velocity = projectile.DirectionTo(player.Center) * 22;
                    projectile.netUpdate = true;
                }
            }
            else
            {
                projectile.velocity *= 0.98f;
                if (projectile.ai[1] > dashTime + 60)
                {
                    projectile.ai[1] = 0;
                    projectile.netUpdate = true;
                }
            }
        }

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 16f;
            if (Vector2.Distance(projectile.Center, point) > 500)
                moveSpeed = 25f;
            float velMultiplier = 1f;
            Vector2 dist = point - projectile.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            if (length < 200f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                moveSpeed *= 0.5f;
            }
            projectile.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            projectile.velocity *= moveSpeed;
            projectile.velocity *= velMultiplier;
        }

        public override void Kill(int timeLeft)
        {
            Main.NewText("insert haruka smoke cloud despawn dust here");
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Buffs.HydraToxin>(), 300);
        }
    }
}