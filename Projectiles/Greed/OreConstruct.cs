using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Projectiles.Greed
{
	public class OreConstruct : AAProjectile
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("OreConstruct");
		}
		
        public override void SetDefaults()
        {
            projectile.width = 25;
            projectile.height = 25;
            projectile.aiStyle = -1;
            projectile.timeLeft = 300;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.penetrate = -1;
            projectile.netImportant = true;	
        }

		public bool checkedMinPos = false;
		public float maxDistToAttack = 360f;
		public Entity target = null;

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			int[] projs = BaseAI.GetProjectiles(player.Center, projectile.type, player.whoAmI, -1);
			if(!checkedMinPos)
			{
				for (int m = 0; m < projs.Length; m++) { if (projs[m] == projectile.identity) { projectile.minionPos = m; } }
				if (Main.myPlayer == projectile.owner) { projectile.netUpdate = true; }
			}
			if (!Main.player[projectile.owner].active || Main.player[projectile.owner].dead || projs.Length > 2) { projectile.Kill(); return; }
			Target();
            BaseAI.AIMinionFlier(projectile, ref projectile.ai, player, false, false, false, 40, 40, 400, 800, 1f, 10f, 10f, !CanShoot(target), false, (proj, owner) => { return (target == player ? null : target); }, Shoot);
			projectile.position -= (player.oldPosition - player.position);
			if (CanShoot(target)) { projectile.spriteDirection = (projectile.Center.X > target.Center.X ? -1 : 1); }
		}

		public bool CanShoot(Entity target)
		{
			return target != null && target is NPC && BaseUtility.CanHit(projectile.Hitbox, new Rectangle((int)target.Center.X, (int)target.Center.Y, 1, 1)) && Vector2.Distance(projectile.Center, target.Center) < 350;
		}

		public bool Shoot(Entity proj, Entity owner, Entity target)
		{
			if(CanShoot(target))
			{
				if (Main.myPlayer == projectile.owner)
				{
					projectile.localAI[0]--;
					if (projectile.localAI[0] <= 0)
					{
						projectile.localAI[0] = 30;
						Vector2 velocity = BaseUtility.RotateVector(default, new Vector2(5f, 0f), BaseUtility.RotationTo(projectile.Center, target.Center));
						int projID = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjType("Gold"), projectile.damage, 0f, projectile.owner, 0, 1);
						((AAProjectile)Main.projectile[projID].modProjectile).SetMaster(2, projectile.identity, 1, 0f, 450f, false);	
						Main.projectile[projID].velocity = velocity;
						Main.projectile[projID].netUpdate = true;
					}
				}
				return true;
			}
			projectile.localAI[0] = 0;
			return false;
		}

		public override bool OnTileCollide(Vector2 velocityChange) 
		{
			projectile.velocity *= -1f;
			return false; 
		}

		public override void PostAI()
		{
			projectile.rotation = projectile.velocity.X * 0.1f;
            if (projectile.frameCounter++ > 5)
            {
                projectile.frame += 1;
                projectile.frameCounter = 0;
            }
            if (projectile.frame > 14)
            {
                projectile.frame = 3;
            }
		}

        Color GlowColor = Color.Brown;

		public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(frameCount, 60, 60, 0, 0);
            Texture2D tex = Main.projectileTexture[projectile.type];
            Texture2D glowTex = mod.GetTexture("Glowmasks/GreedMinion_Glow");
            Color lightColor = BaseDrawing.GetLightColor(projectile.Center);
            BaseDrawing.DrawTexture(sb, tex, 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.spriteDirection, 15, frame, lightColor);
            BaseDrawing.DrawTexture(sb, glowTex, 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.spriteDirection, 15, frame, Color.Goldenrod);
			return false;
		}

        public void SetType()
        {
            switch (projectile.ai[1])
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
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
				int[] npcs = BaseAI.GetNPCs(startPos, -1, default, maxDistToAttack);
				float prevDist = maxDistToAttack;
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
            if (codable is NPC npc)
            {
                return npc.active && npc.life > 0 && !npc.friendly && !npc.dontTakeDamage && npc.lifeMax > 5 && Vector2.Distance(startPos, npc.Center) < maxDistToAttack && BaseUtility.CanHit(projectile.Hitbox, npc.Hitbox);
            }
            return false;
		}
	}
}



