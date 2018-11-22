using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class ChairMinion : Summoning.Minions.Chair
    {
        private int chairdeath = 0;

        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.width = 16;
            projectile.height = 34;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.minionSlots = 1f;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.ignoreWater = false;
            projectile.tileCollide = false;
            projectile.restrikeDelay = 0;
            projectile.localNPCHitCooldown = 0;
            projectile.damage = 1;
            projectile.alpha = 0;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
            Main.projPet[projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chair Minion");
            Main.projFrames[projectile.type] = 9;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = true;
			return true;
		}

        public override void AI()
        {
            if (projectile.timeLeft == 10)
            {
                projectile.timeLeft = 180;
            }
            if (chairdeath > 0)
            {
                chairdeath--;
            }
            if (projectile.frame == 9 && chairdeath == 0)
            {
                projectile.Kill();
            }
            if ((projectile.wet || projectile.lavaWet || projectile.honeyWet || projectile.frame > 0) && chairdeath == 0)
            {
                projectile.frame++;
                chairdeath = 10;
            }
            Player player = Main.player[projectile.owner];
            Vector2 targetPos = projectile.position;
            float targetDist = 400f;
            bool target = false;
            projectile.tileCollide = true;
            if (player.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];
                if (Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height))
                {
                    targetDist = Vector2.Distance(projectile.Center, targetPos);
                    targetPos = npc.Center;
                    target = true;
                }
            }
            else for (int k = 0; k < 200; k++)
            {
                NPC npc = Main.npc[k];
                if (npc.CanBeChasedBy(this, false))
                {
                    float distance = Vector2.Distance(npc.Center, projectile.Center);
                    if ((distance < targetDist || !target) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height))
                    {
                        targetDist = distance;
                        targetPos = npc.Center;
                        target = true;
                    }
                }
            }
            if (Vector2.Distance(player.Center, projectile.Center) > (target ? 1000f : 500f))
            {
                projectile.ai[0] = 1f;
                projectile.netUpdate = true;
            }
            if (projectile.ai[0] == 1f)
            {
                projectile.tileCollide = false;
            }
            if (target && projectile.ai[0] == 0f)
            {
                Vector2 direction = targetPos - projectile.Center;
                if (direction.Length() != 0f)
                {
                    direction.Normalize();
                    projectile.velocity = ((projectile.velocity * 40f) + (direction * 6f)) / (40f + 1);
                }
            }
            else
            {
                if (!Collision.CanHitLine(projectile.Center, 1, 1, player.Center, 1, 1))
                {
                    projectile.ai[0] = 1f;
                }
                float speed = 6f;
                if (projectile.ai[0] == 1f)
                {
                    speed = 15f;
                }
                Vector2 center = projectile.Center;
                Vector2 direction = player.Center - center;
                projectile.ai[1] = 3600f;
                projectile.netUpdate = true;
                int num = 1;
                for (int k = 0; k < projectile.whoAmI; k++)
                {
                    if (Main.projectile[k].active && Main.projectile[k].owner == projectile.owner && Main.projectile[k].type == projectile.type)
                    {
                        num++;
                    }
                }
                direction.X -= (float)((10 + (num * 40)) * player.direction);
                direction.Y -= 70f;
                float distanceTo = direction.Length();
                if (distanceTo > 200f && speed < 9f)
                {
                    speed = 9f;
                }
                if (distanceTo < 100f && projectile.ai[0] == 1f && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                {
                    projectile.ai[0] = 0f;
                    projectile.netUpdate = true;
                }
                if (distanceTo > 2000f)
                {
                    projectile.Center = player.Center;
                }
                if (distanceTo > 48f)
                {
                    direction.Normalize();
                    direction *= speed;
                    float temp = 40f / 2f;
                    projectile.velocity = ((projectile.velocity * temp) + direction) / (temp + 1);
                }
                else
                {
                    projectile.direction = Main.player[projectile.owner].direction;
                    projectile.velocity *= (float)Math.Pow(0.9, 40.0 / 40f);
                }
            }
            if (!player.HasBuff(mod.BuffType("ChairMinionBuff")))
            {
                projectile.Kill();
            }
            if (projectile.spriteDirection != player.direction)
            {
                projectile.spriteDirection = player.direction;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.type == NPCID.Crab)
            {
                target.life = 1;
                target.StrikeNPC(99999, 0, 0);
                NPC.NewNPC((int)(target.Center.X), (int)(target.Center.Y), mod.NPCType<CrabGuardian>());
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.tileCollide = false;
            projectile.position += projectile.velocity;
            projectile.velocity = Vector2.Zero;
            projectile.timeLeft = 180;
            return false;
        }

        public override bool MinionContactDamage()
        {
            return true;
        }

        public override void CheckActive()
        {
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = (AAPlayer)player.GetModPlayer(mod, "AAPlayer");
            if (player.dead)
            {
                modPlayer.ChairMinion = false;
            }
        }
    }
    internal class CrabGuardian : ModNPC
    {
        private int soundTimer = 0;
        private bool Cheating = false;
        private bool highATK = false;

        public override string Texture { get { return "AAMod/Items/Dev/CrabGuardian"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crab Guardian");
            npc.CloneDefaults(NPCID.DungeonGuardian);
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 999999;
            npc.scale = 3f;
            npc.defense = 999999;
            npc.damage = 999999;
            npc.noGravity = true;
            npc.noTileCollide = true;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (damage > 100)
            {
                Main.NewText("What Weapon Is That?!", Color.Red.R, Color.Red.G, Color.Red.B);
                npc.immortal = true;
                highATK = true;
            }
        }

        public override void AI()
        {
            Mod Mod1 = ModLoader.GetMod("CheatSheet");
            Mod Mod2 = ModLoader.GetMod("HEROsMOD");
            if ((Mod1 != null || Mod2 != null) && !Cheating)
            {
                Main.NewText("Cheat Mod Active!", Color.Red.R, Color.Red.G, Color.Red.B);
                npc.immortal = true;
                Cheating = true;
            }
            if (soundTimer > 0)
            {
                soundTimer--;
            }
            if (soundTimer == 0)
            {
                Main.PlaySound(SoundID.MoonLord, npc.Center, 0);
                soundTimer = 3600;
            }
            if (npc.ai[1] != 3f && npc.ai[1] != 2f)
            {
                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0, 1f, 0f);
                npc.ai[1] = 2f;
            }
            if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 2000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 2000f)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 2000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 2000f)
                {
                    npc.ai[1] = 3f;
                }
            }
            if (npc.ai[1] == 2f)
            {
                npc.rotation += (float)npc.direction * 0.3f;
                Vector2 vector18 = new Vector2(npc.position.X + ((float)npc.width * 0.5f), npc.position.Y + ((float)npc.height * 0.5f));
                float num174 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector18.X;
                float num175 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector18.Y;
                float num176 = (float)Math.Sqrt((double)((num174 * num174) + (num175 * num175)));
                num176 = 8f / num176;
                npc.velocity.X = num174 * num176;
                npc.velocity.Y = num175 * num176;
                if (npc.timeLeft < 50)
                {
                    npc.timeLeft = 180;
                }
            }
            else if (npc.ai[1] == 3f)
            {
                npc.velocity.Y = npc.velocity.Y + 0.1f;
                if (npc.velocity.Y < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y * 0.95f;
                }
                npc.velocity.X = npc.velocity.X * 0.95f;
                if (npc.timeLeft > 50)
                {
                    npc.timeLeft = 50;
                }
            }
        }

        public override void NPCLoot()
        {
            if (!AAWorld.Chairlol && !highATK)
            {
                AAWorld.Chairlol = true;
            }
        }
    }
}