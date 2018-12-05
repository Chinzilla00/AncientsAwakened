using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Retriever
{
    [AutoloadBossHead]
    public class Retriever : ModNPC
    {
        private Player player;
        private float speed;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Retriever");
            Main.npcFrameCount[npc.type] = 4;    //boss frame/animation 

        }
        public override void SetDefaults()
        {
            npc.aiStyle = 5;  //5 is the flying AI
            npc.lifeMax = 25000;   //boss life
            npc.damage = 80;  //boss damage
            npc.defense = 20;    //boss defense
            npc.knockBackResist = 0f;
            npc.width = 92;
            npc.height = 54;
            npc.friendly = false;
            npc.value = Item.buyPrice(0, 10, 50, 0);
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = new LegacySoundStyle(3, 4, Terraria.Audio.SoundType.Sound);
            npc.DeathSound = new LegacySoundStyle(4, 14, Terraria.Audio.SoundType.Sound);
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Boss6");
            npc.buffImmune[BuffID.Ichor] = true;
            npc.netAlways = true;
            bossBag = mod.ItemType("RetrieverBag");
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            {
                SpriteEffects spriteEffects = SpriteEffects.None;
                if (npc.spriteDirection == 1)
                {
                    spriteEffects = SpriteEffects.FlipHorizontally;
                }
                spriteBatch.Draw(mod.GetTexture("Glowmasks/Retriever_Glow"), new Vector2(npc.Center.X - Main.screenPosition.X, npc.Center.Y - Main.screenPosition.Y),
                npc.frame, Color.White, npc.rotation,
                new Vector2(npc.width * 0.5f, npc.height * 0.5f), 1f, spriteEffects, 0f);
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RetrieverTrophy"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SoulofSight, Main.rand.Next(20, 40));
            }
            else
            {

                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SoulofSight, Main.rand.Next(25, 40));
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RetrieverMask"));
                }
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FulguriteBar"), Main.rand.Next(30, 64));
                }
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
            else
            {
                npc.frame.Y = 3 * frameHeight;
            }
        }


        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;   //boss drops
            AAWorld.downedRetriever = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 0.8f);  //boss damage increase in expermode
        }
        public int timer;
        private bool switchMove = false;  //Creates a bool for this .cs only
        public override void AI()
        {
            if (Main.dayTime)
            {
                npc.position.Y -= 300;  //disappears at night
            }
            Target();
            DespawnHandler();
            if (switchMove)
            {
                Move(new Vector2(-240, 0));   //240 is the X axis, so its to the right of the player, -240 will be to the left
            }
            npc.ai[0]++;
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            npc.netUpdate = true;
            if (Main.rand.Next(450) == 0) // The lower the value, the higher chance of a grippy boi spawning
            {
                NPC.NewNPC((int)npc.position.X + 70, (int)npc.position.Y + 70, mod.NPCType("CyberClaw")); //Change name AAAAAAAAAAAAAAAAAAAA
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
                npc.aiStyle = 5;        //Reverts back to the original Flying AI Style
                timer = 0;              //Sets the timer back to 0 to repeat
            }
        }
        private void Move(Vector2 offset)
        {
            if (switchMove)             //If the switchMove is on, all of this happens, if its off, all of this doesnt happen
            {
                npc.spriteDirection = 1;
                if (Main.expertMode)
                {
                    speed = 60f; // Increased movement speed in expert mode (The Keeper only thing, change if you wish)
                }
                else
                {
                    speed = 90f; // Sets the max speed of the npc.
                }
                Vector2 moveTo = player.Center + offset; // Gets the point that the npc will be moving to.
                Vector2 move = moveTo - npc.Center;
                float magnitude = Magnitude(move);
                if (magnitude > speed)
                {
                    move *= speed / magnitude;
                }
                float turnResistance = 15f; // The larget the number the slower the npc will turn.
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
                if (!player.active || player.dead)        // If the player is dead and not active, the npc flies off-screen and despawns
                {
                    npc.velocity = new Vector2(0f, -10f);
                    if (npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                    return;
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
                target.AddBuff(BuffID.Electrified, Main.rand.Next(100, 180));       //Main.rand.Next part is the length of the buff, so 8.3 seconds to 16.6 seconds
            }
            /*if (Main.rand.Next(9) == 0 || (Main.expertMode && Main.rand.Next(7) == 0))
            {
                target.AddBuff(BuffID.Poisoned, Main.rand.Next(250, 500));                 //there is no need for this, unless it inflicts a different debuff
            }*/
        }
    }
}