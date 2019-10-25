using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Bosses.Anubis.Forsaken
{
	public class CurseGlyphs : ModProjectile
	{				
		public override void SetStaticDefaults()
		{
            Main.projFrames[projectile.type] = 9;
		}

        public override void SetDefaults()
        {
            projectile.tileCollide = false;
            projectile.width = 30;
            projectile.height = 30;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.ignoreWater = true;
        }

        public int body = -1;
		public float rotValue = -1f;
		public bool spawnedDust = false;

		public override void AI()
        {
            if (body == -1)
            {
                int npcID = BaseAI.GetNPC(projectile.Center, ModContent.NPCType<ForsakenAnubis>(), 400f, null);
                if (npcID >= 0) body = npcID;
            }
            if (body == -1) return;
            NPC anubis = Main.npc[body];
            if (anubis == null || anubis.life <= 0 || !anubis.active || anubis.type != ModContent.NPCType<ForsakenAnubis>()) { projectile.active = false; return; }

            Player player = Main.player[anubis.target];

            projectile.rotation += .1f;

            int glyph = ((ForsakenAnubis)anubis.modNPC).LocustCount;

            if (rotValue == -1f) rotValue = projectile.ai[0] % glyph * ((float)Math.PI * 2f / glyph);
            rotValue += 0.04f;
            while (rotValue > (float)Math.PI * 2f) rotValue -= (float)Math.PI * 2f;

            projectile.Center = BaseUtility.RotateVector(anubis.Center, anubis.Center + new Vector2(160, 0f), rotValue);

        }

		public override bool PreDraw(SpriteBatch sb, Color dColor)
		{
            BaseDrawing.DrawTexture(sb, Main.projectileTexture[projectile.type], 0, projectile, ColorUtils.COLOR_GLOWPULSE);
			return false;
		}		
	}
}