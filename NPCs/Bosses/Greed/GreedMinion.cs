/*using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Greed
{
    public class GreedMinion : ModNPC
	{

        public static int MinionType = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ore Construct");
			Main.npcFrameCount[npc.type] = 12;
		}

		public override void SetDefaults()
		{
            npc.lifeMax = 200;
            npc.defense = 20;
            npc.damage = 50;
            npc.width = 26;
            npc.height = 20;
            npc.aiStyle = -1;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.4f;
            npc.noTileCollide = true;
            npc.noGravity = true;
        }

        public override void AI()
        {
            BaseAI.AIFlier(npc, ref npc.ai, true, 0.4f, 0.04f, 6f, 1.5f, true, 300);
            Player player = Main.player[npc.target];
            if (player.Center.X > npc.Center.X)
            {
                npc.spriteDirection = 1;
            }
            else
            {
                npc.spriteDirection = -1;
            }
        }

        Color bodyColor;
        Color glowColor;

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter >= 10)
            {
                npc.frameCounter = 0;
                npc.frame.Y += frameHeight;
                if (npc.frame.Y > (frameHeight * 11))
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = 0;
                }
            }
        }

        public void SetColor()
        {
            switch (MinionType)
            {
                case 0: bodyColor = new Color(150, 67, 22); glowColor = new Color(255, 229, 183); break;
                case 1: bodyColor = new Color(87, 92, 80); glowColor = new Color(187, 165, 124); break;
                case 2: bodyColor = new Color(87, 60, 60); glowColor = new Color(189, 159, 139); break;
                case 3: bodyColor = new Color(47, 62, 87); glowColor = new Color(104, 140, 150); break;
                case 4: bodyColor = new Color(61, 72, 73); glowColor = new Color(122, 140, 144); break;
                case 5: bodyColor = new Color(39, 70, 40); glowColor = new Color(154, 190, 155); break;
                case 6: bodyColor = new Color(148, 126, 24); glowColor = new Color(255, 249, 183); break;
                case 7: bodyColor = new Color(72, 73, 114); glowColor = new Color(190, 222, 222); break;
                case 8: bodyColor = new Color(68, 69, 114); glowColor = new Color(120, 117, 179); break;
                case 9: bodyColor = new Color(76, 76, 76); glowColor = new Color(216, 59, 63); break;
                case 10: bodyColor = new Color(52, 36, 88); glowColor = new Color(18, 103, 92); break;
                case 11: bodyColor = new Color(150, 67, 22); glowColor = new Color(255, 229, 183); break;
                case 12: bodyColor = new Color(150, 67, 22); glowColor = new Color(255, 229, 183); break;
                case 13: bodyColor = new Color(150, 67, 22); glowColor = new Color(255, 229, 183); break;
                case 14: bodyColor = new Color(150, 67, 22); glowColor = new Color(255, 229, 183); break;
                case 15: bodyColor = new Color(150, 67, 22); glowColor = new Color(255, 229, 183); break;
                case 16: bodyColor = new Color(150, 67, 22); glowColor = new Color(255, 229, 183); break;
                case 17: bodyColor = new Color(150, 67, 22); glowColor = new Color(255, 229, 183); break;
                case 18: bodyColor = new Color(150, 67, 22); glowColor = new Color(255, 229, 183); break;
                case 19: bodyColor = new Color(150, 67, 22); glowColor = new Color(255, 229, 183); break;
                case 20: bodyColor = new Color(150, 67, 22); glowColor = new Color(255, 229, 183); break;
                case 21: bodyColor = new Color(150, 67, 22); glowColor = new Color(255, 229, 183); break;
                case 22: bodyColor = new Color(150, 67, 22); glowColor = new Color(255, 229, 183); break;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SetColor();
            Texture2D glowTex = mod.GetTexture("Glowmasks/GreedMinion_Glow");
            BaseDrawing.DrawTexture(spriteBatch, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 24, npc.frame, bodyColor, true);
            BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 24, npc.frame, glowColor, true);
            return false;
        }
    }
}
*/