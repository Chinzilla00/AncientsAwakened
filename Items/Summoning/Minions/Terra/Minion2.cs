using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Items.Summoning.Minions.Terra
{
	public class Minion2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Terr Crawler");
		}		
		
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 30;
            projectile.aiStyle = -1;
            projectile.timeLeft = 300;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
			projectile.damage = 1;
            projectile.penetrate = -1;
            projectile.netImportant = true;
            projectile.minionSlots = 1f;
            projectile.minion = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;		
        }

        public static int frameWidth = 34, frameHeight = 40;
        public float frameSubCounter = 0f;
        public int frameCount = 0, textureAlt = -1;
        public Rectangle frame;
        public bool syncSpawn = false;

        public Entity target = null;

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            player.AddBuff(mod.BuffType("TerraSummon"), 3600);
            if (player.dead)
            {
                modPlayer.TerraSummon = false;
            }
            if (!modPlayer.TerraSummon)
            {
                projectile.active = false;
            }
            if (Main.netMode == 1 && Main.myPlayer == projectile.owner && !syncSpawn) { syncSpawn = projectile.netUpdate2 = true; }
            if (!player.active || player.dead) { projectile.Kill(); return; }
            Target();
            bool playerTarget = (target != null && target.Equals(player));
            int maxDistBeforeReturn = playerTarget ? 950 : 1100;
            BaseAI.AIMinionFighter(projectile, ref projectile.ai, Main.player[projectile.owner], false, 14, 20, 20, 900, maxDistBeforeReturn, target == player ? -1f : .2f, target == player ? -1f : 12, 10, (proj, owner) => { return (target == player ? null : target); });
        }

        public override bool OnTileCollide(Vector2 value2)
        {
            return false;
        }

        public override void PostAI()
        {
            if (projectile.velocity.X != 0 && projectile.velocity.Y == 0)
            {
                if (projectile.frameCounter++ > 5)
                {
                    projectile.frame++;
                    projectile.frameCounter = 0;
                }
                if (projectile.frame > 4)
                {
                    projectile.frame = 0;
                }
            }
            else
            {
                projectile.frame = 0;
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            projectile.position.Y -= projectile.height - 3;
            BaseDrawing.DrawTexture(sb, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.spriteDirection, 5, frame, AAColor.COLOR_WHITEFADE1);
            projectile.position.Y += projectile.height - 3;
            return false;
        }

        public void Target()
        {
            Vector2 startPos = Main.player[projectile.owner].Center;
            if (target != null && target != Main.player[projectile.owner] && !CanTarget(target, startPos))
            {
                target = null;
            }
            if (target == null || target == Main.player[projectile.owner])
            {
                int[] npcs = BaseAI.GetNPCs(startPos, -1, default(int[]), 900);
                float prevDist = 900;
                foreach (int i in npcs)
                {
                    NPC npc = Main.npc[i];
                    float dist = Vector2.Distance(startPos, npc.Center);
                    if (CanTarget(npc, startPos) && dist < prevDist) { target = npc; prevDist = dist; }
                }
            }
            if (target == null) { target = Main.player[projectile.owner]; }
        }

        public bool CanTarget(Entity codable, Vector2 startPos)
        {
            if (codable is NPC)
            {
                NPC npc = (NPC)codable;
                return npc.active && npc.life > 0 && !npc.friendly && !npc.dontTakeDamage && npc.lifeMax > 5 && Vector2.Distance(startPos, npc.Center) < 900 && Math.Abs(npc.Center.Y - startPos.Y) < (16f * (20 - 1)) && (BaseUtility.CanHit(projectile.Hitbox, npc.Hitbox) || BaseUtility.CanHit(Main.player[projectile.owner].Hitbox, npc.Hitbox));
            }
            return false;
        }
    }
}



