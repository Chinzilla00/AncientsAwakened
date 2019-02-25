using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Bosses.MushroomMonarch
{
    [AutoloadBossHead]
    public class FeudalFungus : ModNPC
    {
		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if((Main.netMode == 2 || Main.dedServ))
			{
				writer.Write((float)internalAI[0]);
				writer.Write((float)internalAI[1]);
                writer.Write((float)internalAI[2]);
                writer.Write((float)internalAI[3]);
            }
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if(Main.netMode == 1)
			{
				internalAI[0] = reader.ReadFloat();
				internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
            }	
		}	

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Feudal Fungus");
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 1200;   //boss life
            npc.damage = 12;  //boss damage
            npc.defense = 12;    //boss defense
            npc.knockBackResist = 0f;   //this boss will behavior like the DemonEye  //boss frame/animation 
            npc.value = Item.buyPrice(0, 0, 75, 45);
            npc.aiStyle = 26;
            npc.width = 74;
            npc.height = 108;
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.buffImmune[46] = true;
            npc.buffImmune[47] = true;
            npc.netAlways = true;
            npc.noGravity = true;
            bossBag = mod.ItemType("FungusBag");
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/TODE");
            npc.alpha = 255;
        }

        public static int AISTATE_HOVER = 0, AISTATE_FLIER = 1, AISTATE_SHOOT = 2;
		public float[] internalAI = new float[4];
        bool HasStopped = false;
		
        public override void AI()
        {
            Player player = Main.player[npc.target]; // makes it so you can reference the player the npc is targetting

            if ((Main.dayTime && player.position.Y < Main.worldSurface && !player.ZoneGlowshroom) || (player.position.Y > Main.worldSurface && !player.ZoneGlowshroom))
            {
                npc.velocity *= 0;

                if (npc.velocity.X <= .1f && npc.velocity.X >= -.1f)
                {
                    npc.velocity.X = 0;
                }
                if (npc.velocity.Y <= .1f && npc.velocity.Y >= -.1f)
                {
                    npc.velocity.Y = 0;
                }

                npc.alpha += 10;

                if (npc.alpha >= 255)
                {
                    npc.active = false;
                }
                return;
            }
            npc.alpha -= 10;

            npc.frameCounter++;
            if (!HasStopped)
            {
				if (npc.frameCounter >= 10)
				{
					npc.frameCounter = 0;
					npc.frame.Y += 90;
					if (npc.frame.Y > (90 * 4))
					{
						npc.frameCounter = 0;
						npc.frame.Y = 0;
					}
				}
            }
            else
            {
                internalAI[2]++;
                if (internalAI[2] > 8)
                {
                    npc.frame.Y += 90;
                    internalAI[3] += 1;
                    internalAI[2] = 0;
                }
                if (internalAI[3] == 3)
                {
                    int attack = Main.rand.Next(4);
                    FungusAttack(attack);
                }
                if (internalAI[3] > 4)
                {
                    internalAI[0] = 0;
                    internalAI[1] = Main.rand.Next(3);
                    HasStopped = false;
                    npc.netUpdate = true;
                }
            }

			if(Main.netMode != 1 && internalAI[1] != AISTATE_SHOOT)
			{
                internalAI[0]++;
                if (internalAI[0] >= 180)
                {
                    internalAI[0] = 0;
                    internalAI[1] = Main.rand.Next(3);
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }
			if(internalAI[1] == AISTATE_HOVER) 
            {
                BaseAI.AISpaceOctopus(npc, ref npc.ai, player.Center, 0.15f, 4f, 200, 56f, FireMagic);
            }
            else if (internalAI[1] == AISTATE_FLIER) 
            {
                BaseAI.AIFlier(npc, ref npc.ai, true, 0.1f, 0.04f, 5f, 3f, false, 1);
            }
            else 
            {
                npc.velocity *= 0;

                if (npc.velocity.X <= .1f && npc.velocity.X >= -.1f)
                {
                    npc.velocity.X = 0;
                }
                if (npc.velocity.Y <= .1f && npc.velocity.Y >= -.1f)
                {
                    npc.velocity.Y = 0;
                }
                if (npc.velocity == new Vector2(0, 0))
                {
                    HasStopped = true;
                }
            }
        }


        public float[] shootAI = new float[4];

        public void FireMagic(NPC npc, Vector2 velocity)
        {
            Player player = Main.player[npc.target];
            BaseAI.ShootPeriodic(npc, player.position, player.width, player.height, mod.ProjType("MagicBlast"), ref shootAI[0], 5, (int)(npc.damage * (Main.expertMode ? 0.25f : 0.5f)), 24f, true, new Vector2(20f, 15f));
        }

        public override void BossLoot(ref string name, ref int potionType)
        {   //boss drops
            AAWorld.downedMonarch = true;
            Projectile.NewProjectile(npc.Center, npc.velocity, mod.ProjectileType("FungusIGoNow"), 0, 0);
            if (Main.expertMode == true)
            {
                npc.DropBossBags();
            }
            else
            {

                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GlowingMushium"), Main.rand.Next(25, 35));
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 1.1f);  //boss damage increase in expermode
        }

        public void FungusAttack(int Attack)
        {
            Player player = Main.player[npc.target];

            if (Attack == 0)
            {
                for (int i = 0; i < (Main.expertMode ? 3 : 2); i++)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<Mushling>());
                }
            }
            if (Attack == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<FungusFlier>());
                }
            }
            if (Attack == 2)
            {
                float spread = 12f * 0.0174f;
                double startAngle = Math.Atan2(npc.velocity.X, npc.velocity.Y) - spread / 2;
                double deltaAngle = spread / (Main.expertMode ? 5 : 4);
                double offsetAngle;
                for (int i = 0; i < (Main.expertMode ? 5 : 4); i++)
                {
                    offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * 6f), (float)(Math.Cos(offsetAngle) * 6f), mod.ProjectileType("FungusCloud"), npc.damage / 2, 0, Main.myPlayer, 0f, 0f);
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<FungusSpore>(), 255, i);
                }
            }
        }

        public override void PostDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/FeudalFungus_Glow");
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, AAColor.Glow);
        }
    }

    
}


