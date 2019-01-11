using System;
using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Grips
{
    [AutoloadBossHead]
    public class GripOfChaosBlue : ModNPC
    {
        private Player player;
        private float speed;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grip of Chaos");
            Main.npcFrameCount[npc.type] = 4;    //boss frame/animation 

        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;  //5 is the flying AI
            npc.lifeMax = 1400;   //boss life
            npc.damage = 25;  //boss damage
            npc.defense = 10;    //boss defense
            npc.knockBackResist = 0f;
            npc.width = 66;
            npc.height = 60;
            npc.friendly = false;
            npc.value = Item.buyPrice(0, 4, 50, 0);
            npc.npcSlots = 1f;
            npc.boss = true;  
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/GripsTheme");
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.netAlways = true;
            bossBag = mod.ItemType("GripBag");
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)          //this make so when the npc has 0 life(dead) he will spawn this
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/MireGripGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/MireGripGore2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/MireGripGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/MireGripGore4"), 1f);
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter < 5)
            {
                npc.frame.Y = 0 * frameHeight;
            }
            else if (npc.frameCounter < 10)
            {
                npc.frame.Y = 1 * frameHeight;
            }
            else if (npc.frameCounter < 15)
            {
                npc.frame.Y = 2 * frameHeight;
            }
            else if (npc.frameCounter < 20)
            {
                npc.frame.Y = 3 * frameHeight;
            }
            else
            {
                npc.frameCounter = 0;
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void NPCLoot()
        {
            int GripRed = NPC.CountNPCS(mod.NPCType("GripOfChaosRed"));
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GripTrophyBlue"));
            }
            if (GripRed == 0)
            {
                if (Main.rand.Next(4) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ClawBaton"));
                }

                AAWorld.downedGrips = true;
                if (Main.expertMode)
                {
                    npc.DropBossBags();
                }
            }
            else
            {
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GripMaskBlue"));
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Abyssium"), Main.rand.Next(30, 44));
                
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.LesserHealingPotion;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 0.8f);  //boss damage increase in expermode
        }
        public int timer;
        private bool switchMove = false; //Creates a bool for this .cs only
        public override void AI()
        {
            if (Main.dayTime)
            {
                npc.position.Y -= 10;  //disappears at night
            }
            Target();
            DespawnHandler();
            if (switchMove)
            {
                Move(new Vector2(240, 0));   //240 is the X axis, so its to the right of the player, -240 will be to the left
            }
            npc.ai[0]++;
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            npc.netUpdate = true;
            if (Main.rand.Next(700) == 0) // The lower the value, the higher chance of a grippy boi spawning
            {
                NPC.NewNPC((int)npc.position.X + 70, (int)npc.position.Y + 70, mod.NPCType("HydraClaw")); //Change name AAAAAAAAAAAAAAAAAAAA
            }
            timer++;                //Makes the int start
            if (timer == 450)          //if the timer has gotten to 7.5 seconds, this happens (60 = 1 second)
            {
                switchMove = true;     //Makes the switch turn on, making the AI change to nothing
                npc.aiStyle = -1;      //So the AI doesnt mix with the flying AI Style
                npc.rotation = 0;      // I think this is the right rotation, if not change it tooooo 180 or something
            }
            if (timer >= 900)          //After 15 seconds this happens
            {
                switchMove = false;     //Turns the switch off so the void Move stuff is disabled
                BaseAI.AIEye(npc, ref npc.ai, false, true, 0.1f, 0.04f, 6f, 5f, 1f, 1f);
                timer = 0;              //Sets the timer back to 0 to repeat
            }
            if (switchMove)
            {
                float num4 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2);
                float num5 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2);
                npc.rotation = (float)Math.Atan2((double)num5, (double)num4) - 1.57f;
            }
        }
        private void Move(Vector2 offset)
        {
            if (switchMove)             //If the switchMove is on, all of this happens, if its off, all of this doesnt happen
            {
                if (Main.expertMode)
                {
                    speed = 30f; // Increased movement speed in expert mode (The Keeper only thing, change if you wish)
                }
                else
                {
                    speed = 30f; // Sets the max speed of the npc.
                }
                Vector2 moveTo = player.Center + offset; // Gets the point that the npc will be moving to.
                Vector2 move = moveTo - npc.Center;
                float magnitude = Magnitude(move);
                if (magnitude > speed)
                {
                    move *= speed / magnitude;
                }
                float turnResistance = 35f; // The larger the number the slower the npc will turn.
                move = ((npc.velocity * turnResistance) + move) / (turnResistance + 1f);
                magnitude = Magnitude(move);
                if (magnitude > speed)
                {
                    move *= speed / magnitude;
                }
                npc.velocity = move;
            }
        }

        private void Target()
        {
            player = Main.player[npc.target]; // This will get the player target.
        }
        private void DespawnHandler()
        {
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead || Main.dayTime)        // If the player is dead and not active, the npc flies off-screen and despawns
                {
                    npc.alpha -= 10;
                    npc.velocity.X = 0;
                    npc.velocity.Y = 0;
                    if (npc.alpha >= 255)
                    {
                        npc.active = false;
                    }
                }
            }
        }
        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt((mag.X * mag.X) + (mag.Y * mag.Y));      //No idea, leave this
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))       //Chances for it to inflict the debuff
            {
                target.AddBuff(BuffID.Poisoned, Main.rand.Next(180, 250));       //Main.rand.Next part is the length of the buff, so 8.3 seconds to 16.6 seconds
            }
            /*if (Main.rand.Next(9) == 0 || (Main.expertMode && Main.rand.Next(7) == 0))
            {
                target.AddBuff(BuffID.Poisoned, Main.rand.Next(250, 500));                 //there is no need for this, unless it inflicts a different debuff
            }*/
        }
    }
}
