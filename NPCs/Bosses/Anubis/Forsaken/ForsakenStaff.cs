using BaseMod;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Anubis.Forsaken
{
    public class ForsakenStaff : ModProjectile
	{
        public override void SetDefaults()
        {
            projectile.width = 50;
            projectile.height = 50;
            projectile.aiStyle = -1;
            projectile.timeLeft = 3600;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
        }

        public int master = -1;

		public override void AI()
		{
            if (master >= 0 && (Main.npc[master] == null || !Main.npc[master].active || Main.npc[master].type != mod.NPCType("ForsakenAnubis"))) master = -1;
            if (master == -1)
            {
                master = BaseAI.GetNPC(projectile.Center, mod.NPCType("ForsakenAnubis"), -1, null);
                if (master == -1) master = -2;
            }
            if (master == -1) { return; }
			if (master < 0 || !Main.npc[master].active || Main.npc[master].life <= 0) { projectile.Kill(); return; }

            if (Main.rand.Next(2) == 0)
            {
                int dustnumber = Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 200, default, 0.5f);
                Main.dust[dustnumber].velocity *= 0.3f;
            }

            BaseAI.AIBoomerang(projectile, ref projectile.ai, Main.npc[master].position, Main.npc[master].width, Main.npc[master].height, true, 40, 45, 10f, 1f, true);

            ReflectProjectiles(projectile.Hitbox);
        }

        public void ReflectProjectiles(Rectangle myRect)
        {
            for (int i = 0; i < 1000; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].CanReflect())
                {
                    Rectangle hitbox = Main.projectile[i].Hitbox;
                    if (myRect.Intersects(hitbox))
                    {
                        Main.PlaySound(SoundID.NPCHit4, Main.projectile[i].position);
                        for (int j = 0; j < 3; j++)
                        {
                            int num = Dust.NewDust(Main.projectile[i].position, Main.projectile[i].width, Main.projectile[i].height, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 0, default, 1f);
                            Main.dust[num].velocity *= 0.3f;
                        }
                        Main.projectile[i].hostile = true;
                        Main.projectile[i].friendly = false;
                        Vector2 vector = Main.player[Main.projectile[i].owner].Center - Main.projectile[i].Center;
                        vector.Normalize();
                        vector *= Main.projectile[i].oldVelocity.Length();
                        Main.projectile[i].velocity = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                        Main.projectile[i].velocity.Normalize();
                        Main.projectile[i].velocity *= vector.Length();
                        Main.projectile[i].velocity += vector * 20f;
                        Main.projectile[i].velocity.Normalize();
                        Main.projectile[i].velocity *= vector.Length();
                        Main.projectile[i].damage /= 2;
                        Main.projectile[i].penetrate = 1;
                    }
                }
            }
        }
    }
}