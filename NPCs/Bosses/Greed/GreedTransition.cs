using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Greed
{
    public class GreedTransition : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spark of Greed");
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
			
			if(Main.netMode != 2) //clientside stuff
			{
				npc.frameCounter++;

                npc.rotation += .1f;
                Rotation += .05f;


                if (npc.ai[0] > 175)
				{
					npc.alpha -= 3;
					if (npc.alpha < 0)
					{
						npc.alpha = 0;
					}
				}
            }
			if(Main.netMode != 1)
			{
				npc.ai[0]++;

				if (npc.ai[0] == 175)    
				{
					if (Main.netMode != 1) BaseUtility.Chat("YOU..! YOU LITTLE--", Color.Goldenrod);
					npc.netUpdate = true;
				}else
				if (npc.ai[0] == 350)
				{
					if (Main.netMode != 1) BaseUtility.Chat("THATS IT, I'VE HAD IT!", Color.Goldenrod);
				}else
				if (npc.ai[0] == 500)
				{
					if (Main.netMode != 1) BaseUtility.Chat("YOU TRY TO STEAL MY SHINIES, AND NOW...!", Color.Goldenrod);
                    npc.netUpdate = true;
				}else
				if (npc.ai[0] >= 610)
                {
                    AAModGlobalNPC.SpawnBoss(player, mod.NPCType("GreedA"), true, npc.Center, "Greed", false);
                    if (Main.netMode != 1) BaseUtility.Chat("I WILL STEAL YOUR LIFE! HAHAHAHAHAHAHAHAHAHA!!!", Color.Goldenrod);

                    npc.netUpdate = true;
                    npc.active = false;
                }
			}
        }

        public override bool CheckActive()
        {
            if (!NPC.AnyNPCs(mod.NPCType("GreedA")))
            {
                return false;
            }
            npc.active = false;
            return true;
        }

        public float Rotation = 0;
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Rectangle SunFrame = new Rectangle(0, 0, 60, 60);
            BaseDrawing.DrawTexture(spriteBatch, mod.GetTexture("NPCs/Bosses/Greed/GreedSpawn2"), 0, npc.position + new Vector2(0, npc.gfxOffY), npc.width, npc.height, npc.scale, -npc.rotation, npc.spriteDirection, 1, SunFrame, npc.GetAlpha(AAColor.COLOR_WHITEFADE1), true);
            BaseDrawing.DrawTexture(spriteBatch, mod.GetTexture("NPCs/Bosses/Greed/GreedSpawn1"), 0, npc.position + new Vector2(0, npc.gfxOffY), npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, 1, SunFrame, npc.GetAlpha(AAColor.COLOR_WHITEFADE1), true);
            BaseDrawing.DrawTexture(spriteBatch, mod.GetTexture("NPCs/Bosses/Greed/GreedSpawn"), 0, npc.position + new Vector2(0, npc.gfxOffY), npc.width, npc.height, npc.scale, Rotation, npc.spriteDirection, 1, SunFrame, npc.GetAlpha(AAColor.COLOR_WHITEFADE1), true);
            return false;
        }
    }
}