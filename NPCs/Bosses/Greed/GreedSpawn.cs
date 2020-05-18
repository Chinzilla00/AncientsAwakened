
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.NPCs.Bosses.Greed
{
    public class GreedSpawn : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spark of Desire");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 100;
            npc.friendly = false;
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.timeLeft = 10;
            npc.alpha = 255;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/silence");
        }

        public override void AI()
        {
			npc.TargetClosest();			
            Player player = Main.player[npc.target];
			
			if(Main.netMode != NetmodeID.Server)
			{
                if (npc.ai[0] > 175)
				{
					npc.alpha -= 3;
					if (npc.alpha < 0)
					{
						npc.alpha = 0;
					}
				}

                if (npc.ai[0] >= 570)
                {
                    music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Greed");
                }

            }
			if(Main.netMode != NetmodeID.MultiplayerClient)
			{
				npc.ai[0]++;

				if (npc.ai[0] == 175)    
				{
					if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("Greed1"), Color.Goldenrod);
					npc.netUpdate = true;
				}else
				if (npc.ai[0] == 350)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("Greed2"), Color.Goldenrod);
				}else
				if (npc.ai[0] == 500)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("Greed3"), Color.Goldenrod);
                    npc.netUpdate = true;
				}else
				if (npc.ai[0] == 610)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("Greed4"), Color.Goldenrod);
				}else
				if (npc.ai[0] >= 755 && !NPC.AnyNPCs(mod.NPCType("Greed")))
				{
					AAModGlobalNPC.SpawnBoss(player, mod.NPCType("Greed"), true, npc.Center, Lang.BossChat("GreedName"), false);
					if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("Greed5"), Color.Goldenrod);

                    npc.netUpdate = true;
					npc.active = false;				
				}
			}
        }

        public override bool CheckActive()
        {
            if (!NPC.AnyNPCs(mod.NPCType("Greed")))
            {
                return false;
            }
            npc.active = false;
            return true;
        }

        public override void FindFrame(int frameHeight)
        {
            if (++npc.frameCounter >= 4)
            {
                npc.frameCounter = 0;
                npc.frame.Y += frameHeight;
                if (npc.frame.Y >= frameHeight * 3)
                {
                    npc.frame.Y = 0;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Rectangle SunFrame = new Rectangle(0, 0, 70, 70);
            BaseDrawing.DrawTexture(spriteBatch, mod.GetTexture("NPCs/Bosses/Greed/GreedSpawn"), 0, npc.position + new Vector2(0, npc.gfxOffY), npc.width, npc.height, npc.scale, 0, npc.spriteDirection, 4, SunFrame, npc.GetAlpha(AAColor.COLOR_WHITEFADE1), true);
            return false;
        }
    }
}