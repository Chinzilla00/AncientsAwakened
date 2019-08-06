using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Yamata
{
    public class GreedSpawn : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spark of Desire");
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
        }

        public override void AI()
        {
			npc.TargetClosest();			
            Player player = Main.player[npc.target];
			
			if(Main.netMode != 2) //clientside stuff
			{
				npc.frameCounter++;

                npc.rotation += .1f;
                Rotation += .05f;


                if (npc.ai[0] > 375)
				{
					npc.alpha -= 5;
					if (npc.alpha < 0)
					{
						npc.alpha = 0;
					}
				}

                if (npc.ai[0] >= 1100)
                {
                    music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Greed");
                }

            }
			if(Main.netMode != 1)
			{
				npc.ai[0]++;

				if (npc.ai[0] == 375)    
				{
					if (Main.netMode != 1) BaseUtility.Chat("Who disturbs me from my coin-counting?! I'm busy--", Color.Goldenrod);
					npc.netUpdate = true;
				}else
				if (npc.ai[0] == 650)
				{
					if (Main.netMode != 1) BaseUtility.Chat("...Oooooh...is that...?", Color.Goldenrod);
				}else
				if (npc.ai[0] == 900)
				{
					if (Main.netMode != 1) BaseUtility.Chat("You brought me my favorite dish..!", Color.Goldenrod);
                    npc.netUpdate = true;
				}else
				if (npc.ai[0] == 1100)
				{
					if (Main.netMode != 1) BaseUtility.Chat("Golden Grub...", Color.Goldenrod);
				}else
				if (npc.ai[0] >= 1455 && !NPC.AnyNPCs(mod.NPCType("YamataA")))
				{
					AAModGlobalNPC.SpawnBoss(player, mod.NPCType("YamataA"), true, npc.Center, "Greed", false);
					if (Main.netMode != 1) BaseUtility.Chat("WITH A SIDE OF TERRARIAN!!!", Color.Goldenrod);

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
            return true;
        }

        public float Rotation = 0;
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Rectangle SunFrame = new Rectangle(0, 0, 60, 60);
            BaseDrawing.DrawTexture(spriteBatch, mod.GetTexture("NPCs/Bosses/Greed/GreedSpawn2"), 0, npc.position + new Vector2(0, npc.gfxOffY), npc.width, npc.height, npc.scale, -npc.rotation, npc.spriteDirection, 1, SunFrame, AAColor.COLOR_WHITEFADE1, true);
            BaseDrawing.DrawTexture(spriteBatch, mod.GetTexture("NPCs/Bosses/Greed/GreedSpawn1"), 0, npc.position + new Vector2(0, npc.gfxOffY), npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, 1, SunFrame, AAColor.COLOR_WHITEFADE1, true);
            BaseDrawing.DrawTexture(spriteBatch, mod.GetTexture("NPCs/Bosses/Greed/GreedSpawn"), 0, npc.position + new Vector2(0, npc.gfxOffY), npc.width, npc.height, npc.scale, Rotation, npc.spriteDirection, 1, SunFrame, AAColor.COLOR_WHITEFADE1, true);
            return false;
        }
    }
}