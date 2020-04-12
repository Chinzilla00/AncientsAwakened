using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using Microsoft.Xna.Framework;

namespace AAMod.NPCs.Enemies.Mire
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/blushiemagic/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class Soulsucker : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soulsucker");
			Main.npcFrameCount[npc.type] = 3;
		}

		public override void SetDefaults()
		{
            npc.aiStyle = 1;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.width = 64;
			npc.height = 64;
			npc.damage = 70;
			npc.defense = 30;
			npc.lifeMax = 1000;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 6000f;
            npc.lavaImmune = false;
            npc.knockBackResist = 0.5f;
            banner = npc.type;
			bannerItem = mod.ItemType("SoulsuckerBanner");
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.frameCounter++ > 3)
            {
                npc.frame.Y += frameHeight;
                npc.frameCounter = 0;
                if (npc.frame.Y >= frameHeight * 2)
                {
                    npc.frame.Y = 0;
                }
            }
        }

        public override void AI()
        {
            BaseAI.AIFlier(npc, ref npc.ai, false, 0.2f, 0.1f, 3, 2.5f, true, 250);
            npc.rotation = npc.velocity.X * 0.05f;
            if (npc.velocity.X > 0)
            {
                npc.spriteDirection = 1;
            }
            else
            {
                npc.spriteDirection = -1;
            }

            if (Collision.SolidCollision(new Vector2(npc.Center.X, npc.Center.Y), npc.width, npc.height))
            {
                if (npc.alpha < 100)
                {
                    npc.alpha += 2;
                }
            }
            else
            {
                if (npc.alpha > 0)
                {
                    npc.alpha -= 3;
                }
            }
        }

        public override Color? GetAlpha(Color drawColor)
        {
            if (Collision.SolidCollision(new Vector2(npc.Center.X, npc.Center.Y), npc.width, npc.height))
            {
                return Color.PaleVioletRed;
            }
            else
            {
                return drawColor;
            }
        }

        public override void NPCLoot()
        {
            BaseAI.DropItem(npc, mod.ItemType("TerrorSoul"), Main.expertMode ? 1 + Main.rand.Next(2) : Main.rand.Next(1), 3, 100, true);
        }
	}
}
