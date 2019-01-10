using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Graphics.Shaders;
using AAMod;
using BaseMod;

namespace AAMod.Projectiles.Djinn
{
	public class DustDevil : AAProjectile
	{
        public override void SetDefaults()
        {
            projectile.width = 44;
            projectile.height = 44;
            projectile.aiStyle = -1;
            projectile.timeLeft = 300;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.penetrate = -1;
            projectile.netImportant = true;	
        }

		public static int frameWidth = 44, frameHeight = 44;
		public int frameTimer = 0;
		public int frameCount = 5;
		public bool invertFrame = false;
		public Rectangle frame;
		public static Texture2D tex = null;
		public static Texture2D glowTex = null;
		public bool checkedMinPos = false;
        public virtual void SetMaster(params object[] args) { }
        public float maxDistToAttack = 360f;
		public Entity target = null;

		public override void AI()
		{
            Player player = Main.player[projectile.owner];
            bool flag64 = projectile.type == mod.ProjectileType("DustDevil");
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            player.AddBuff(mod.BuffType("DustDevil"), 3600);
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.dustDevil = false;
                }
                if (modPlayer.dustDevil)
                {
                    projectile.timeLeft = 2;
                }
            };
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
			return target != null && target is NPC && BaseMod.BaseUtility.CanHit(projectile.Hitbox, new Rectangle((int)target.Center.X, (int)target.Center.Y, 1, 1)) && Vector2.Distance(projectile.Center, target.Center) < 350;
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
						Vector2 velocity = BaseMod.BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), BaseMod.BaseUtility.RotationTo(projectile.Center, target.Center));
						int projID = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjType("SandSpray"), projectile.damage, 0f, projectile.owner);
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
			frameTimer--;
			if (frameTimer <= 0)
			{
				frameTimer = 2;
				if (invertFrame) { frameCount--; if (frameCount < 0) { frameCount = 1; invertFrame = false; } }else
				{ frameCount++; if (frameCount > 2) { frameCount = 1; invertFrame = true; } }
			}
			frame = BaseDrawing.GetFrame(frameCount, frameWidth, frameHeight, 0, 2);
		}

		public override bool PreDraw(SpriteBatch sb, Color dColor)
		{
			if (tex == null)
			{
				tex = Main.projectileTexture[projectile.type];
			}
			for (int m = projectile.oldPos.Length - 1; m > 0; m--){ projectile.oldPos[m] = projectile.oldPos[m - 1]; }
			projectile.oldPos[0] = projectile.position;
			bool applyDye = Main.player[projectile.owner].dye[1] != null && !Main.player[projectile.owner].dye[1].IsBlank();
			if (applyDye)
			{
				sb.End();
				sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
				GameShaders.Armor.ApplySecondary((int)Main.player[projectile.owner].dye[1].dye, Main.player[projectile.owner], null);				
			}
			BaseDrawing.DrawTexture(sb, tex, 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.spriteDirection, 3, frame, lightColor);
			if (applyDye)
			{
				sb.End();
				sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
			}
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
				int[] npcs = BaseMod.BaseAI.GetNPCs(startPos, -1, default(int[]), maxDistToAttack);
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
			if (codable is NPC)
			{
				NPC npc = (NPC)codable;
				return npc.active && npc.life > 0 && !npc.friendly && !npc.dontTakeDamage && npc.lifeMax > 5 && Vector2.Distance(startPos, npc.Center) < maxDistToAttack && BaseMod.BaseUtility.CanHit(projectile.Hitbox, npc.Hitbox);
			}
			return false;
		}
	}
}



