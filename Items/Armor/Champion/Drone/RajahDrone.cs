using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;


namespace AAMod.Items.Armor.Champion.Drone
{
    public class RajahDrone : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Legendary Rainbow Cat");
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.width = 248;
            projectile.height = 142;
            projectile.penetrate = -1;
            projectile.hostile = false;
            projectile.friendly = false;
            projectile.alpha = 255;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.timeLeft = 480;
        }

        public Entity target = null;

        public override void AI()
        {
            if (++projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }

            projectile.ai[0]++;

            if (projectile.ai[0] < 450)
            {
                if (projectile.alpha > 0)
                {
                    projectile.alpha -= 4;
                }
                else
                {
                    projectile.alpha = 0;
                }
                projectile.velocity = default;

                Target();

                if (target != null && Main.netMode != 2 && projectile.owner == Main.myPlayer)
                {
                    projectile.localAI[0]--;
                    if (projectile.localAI[0] <= 0)
                    {
                        projectile.localAI[0] = 90f;
                        Vector2 velocity = BaseUtility.RotateVector(default, new Vector2(10f, 0f), BaseUtility.RotationTo(projectile.Center, target.Center));
                        int projID = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 4f, 0f, 0f, ModContent.ProjectileType<RajahDroneShot>(), projectile.damage, 0f, projectile.owner);
                        Main.projectile[projID].velocity = velocity;
                        Main.projectile[projID].netUpdate = true;
                    }
                }
            }
            else
            {
                if (projectile.alpha < 255)
                {
                    projectile.alpha += 4;
                }

                if (projectile.ai[0] >= 480)
                {
                    Main.player[projectile.owner].AddBuff(mod.BuffType("DroneCool"), 900);
                    projectile.Kill();
                }
            }
        }

        public bool CanTarget(Entity codable, Vector2 startPos)
        {
            if (codable is NPC npc)
            {
                return npc.active && npc.life > 0 && !npc.friendly && !npc.dontTakeDamage && npc.lifeMax > 5 && Vector2.Distance(startPos, npc.Center) < 800f;
            }
            return false;
        }

        public void Target()
        {
            Vector2 startPos = projectile.Center;
            if (target != null && !CanTarget(target, startPos)) target = null;
            if (target == null)
            {
                int[] npcs = BaseAI.GetNPCs(startPos, -1, default, 800f);
                float prevDist = 800f;
                foreach (int i in npcs)
                {
                    NPC npc = Main.npc[i];
                    float dist = Vector2.Distance(startPos, npc.Center);
                    if (CanTarget(npc, startPos) && dist < prevDist) { target = npc; prevDist = dist; }
                }
            }
        }
    }
}
