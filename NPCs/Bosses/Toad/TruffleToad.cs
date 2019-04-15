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

namespace AAMod.NPCs.Bosses.Toad
{
    [AutoloadBossHead]
    public class TruffleToad : ModNPC
    {
        public float bossLife;

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
            DisplayName.SetDefault("Truffle Toad");
            Main.npcFrameCount[npc.type] = 12;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 14000;   //boss life
            npc.damage = 30;  //boss damage
            npc.defense = 20;    //boss defense
            npc.knockBackResist = 0f;   //this boss will behavior like the DemonEye  //boss frame/animation 
            npc.value = Item.buyPrice(0, 1, 0, 0);
            npc.aiStyle = -1;
            npc.width = 98;
            npc.height = 72;
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = false;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/TODE");
            npc.netAlways = true;
            bossBag = mod.ItemType("ToadBag");
        }
      
		public static int AISTATE_JUMP = 0, AISTATE_BARF = 1, AISTATE_TONGUE = 2;
		public float[] internalAI = new float[4];
        public int NOM = 0;
        public bool tonguespawned = false;
        public bool TongueAttack = false;
        
        private float JumpX = 5;
        private float JumpY = -5;
        private float HighX = 13;
        private float HighY = -13;

        public override void AI()
        {
            Player player = Main.player[npc.target]; // makes it so you can reference the player the npc is targetting
            AAModGlobalNPC.Toad = npc.whoAmI;


            if (npc.velocity.X < 0)
            {
                npc.spriteDirection = -1;
            }
            else
            {
                npc.spriteDirection = 1;
            }

            if (internalAI[0] == AISTATE_JUMP)
            {
                BaseAI.AISlime(npc, ref npc.ai, false, 60, JumpX, JumpY, HighX, HighY);
                if (npc.velocity.Y == 0)
                {
                    internalAI[1]++;
                }
                if (internalAI[1] >= 180)
                {
                    internalAI[1] = 0;
                    internalAI[0] = Main.rand.Next(2);
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }
            if (internalAI[0] == AISTATE_BARF)
            {
                internalAI[1]++;
                npc.velocity.X = 0;
                if (internalAI[1] >= 35)
                {
                    internalAI[2]++;
                    if (internalAI[2] > 5)
                    {
                        internalAI[2] = 0;
                        if (npc.direction == -1)
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2(-6 + Main.rand.Next(-6, 0), -4 + Main.rand.Next(-4, 0)), mod.ProjectileType("ToadBomb"), 35, 3);
                        }
                        else
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2(6 + Main.rand.Next(0, 6), -4 + Main.rand.Next(-4, 0)), mod.ProjectileType("ToadBomb"), 35, 3);
                        }
                    }
                }
                if (internalAI[1] >= 100)
                {
                    internalAI[0] = AISTATE_JUMP;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.velocity.Y == 0)
            {
                npc.frame.Y = 0;
            }
            else
            {
                if (npc.velocity.Y < 0)
                {
                    if (npc.frameCounter >= 10)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y += 72;
                        if (npc.frame.Y > (108 * 3))
                        {
                            npc.frameCounter = 0;
                            npc.frame.Y = 108 * 3;
                        }
                    }
                }
                else if (npc.velocity.Y > 0)
                {
                    npc.frame.Y = 108 * 4;
                }
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            AAWorld.downedToad = true;
            if (Main.expertMode == true)
            {
                npc.DropBossBags();
            }
            else
            {

            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 1.1f);  //boss damage increase in expermode
        }
    }
}


