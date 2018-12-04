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
            npc.aiStyle = -1;  //5 is the flying AI
            npc.lifeMax = 25000;   //boss life
            npc.damage = 80;  //boss damage
            npc.defense = 20;    //boss defense
            npc.knockBackResist = 0f;
            npc.width = 92;
            npc.height = 54;
            npc.friendly = false;
            animationType = NPCID.DemonEye;   //this boss will behavior like the DemonEye
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
                switchMove = true;     //So the AI doesnt mix with the flying AI Style
                npc.rotation = 0;      // I think this is the right rotation, if not change it tooooo 180 or something
            }
            if (timer >= 900)          //After 15 seconds this happens
            {
                switchMove = false;       //Reverts back to the original Flying AI Style
                timer = 0;              //Sets the timer back to 0 to repeat
            }
            if (!switchMove)
            {
                npc.TargetClosest(false);
                npc.rotation = npc.velocity.ToRotation();
                if (Math.Sign(npc.velocity.X) != 0)
                {
                    npc.spriteDirection = -Math.Sign(npc.velocity.X);
                }
                if (npc.rotation < -1.57079637f)
                {
                    npc.rotation += 3.14159274f;
                }
                if (npc.rotation > 1.57079637f)
                {
                    npc.rotation -= 3.14159274f;
                }
                float num997 = 0.4f;
                float num998 = 10f;
                float scaleFactor3 = 200f;
                float num999 = 750f;
                float num1000 = 30f;
                float num1001 = 30f;
                float scaleFactor4 = 0.95f;
                int num1002 = 50;
                float scaleFactor5 = 14f;
                float num1003 = 30f;
                float num1004 = 100f;
                float num1005 = 20f;
                float num1006 = 0f;
                float num1007 = 7f;
                bool flag63 = true;
                num1006 *= num1005;
                if (Main.expertMode)
                {
                    num997 *= Main.expertKnockBack;
                }
                if (npc.ai[0] != 3f)
                {
                    int num1008 = Dust.NewDust(npc.position, npc.width, npc.height, 226, 0f, 0f, 100, default(Color), 0.5f);
                    Main.dust[num1008].noGravity = true;
                    Main.dust[num1008].velocity = npc.velocity / 5f;
                    Vector2 vector125 = new Vector2(-10f, 10f);
                    if (npc.spriteDirection == 1)
                    {
                        vector125.X *= -1f;
                    }
                    vector125 = vector125.RotatedBy((double)npc.rotation, default(Vector2));
                    Main.dust[num1008].position = npc.Center + vector125;
                }
                if (npc.ai[0] == 0f)
                {
                    npc.knockBackResist = num997;
                    float scaleFactor6 = num998;
                    Vector2 center4 = npc.Center;
                    Vector2 center5 = Main.player[npc.target].Center;
                    Vector2 vector126 = center5 - center4;
                    Vector2 vector127 = vector126 - Vector2.UnitY * scaleFactor3;
                    float num1013 = vector126.Length();
                    vector126 = Vector2.Normalize(vector126) * scaleFactor6;
                    vector127 = Vector2.Normalize(vector127) * scaleFactor6;
                    bool flag64 = Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1);
                    if (npc.ai[3] >= 120f)
                    {
                        flag64 = true;
                    }
                    float num1014 = 8f;
                    flag64 = (flag64 && vector126.ToRotation() > 3.14159274f / num1014 && vector126.ToRotation() < 3.14159274f - 3.14159274f / num1014);
                    if (num1013 > num999 || !flag64)
                    {
                        npc.velocity.X = (npc.velocity.X * (num1000 - 1f) + vector127.X) / num1000;
                        npc.velocity.Y = (npc.velocity.Y * (num1000 - 1f) + vector127.Y) / num1000;
                        if (!flag64)
                        {
                            npc.ai[3] += 1f;
                            if (npc.ai[3] == 120f)
                            {
                                npc.netUpdate = true;
                            }
                        }
                        else
                        {
                            npc.ai[3] = 0f;
                        }
                    }
                    else
                    {
                        npc.ai[0] = 1f;
                        npc.ai[2] = vector126.X;
                        npc.ai[3] = vector126.Y;
                        npc.netUpdate = true;
                    }
                }
                else if (npc.ai[0] == 1f)
                {
                    npc.knockBackResist = 0f;
                    npc.velocity *= scaleFactor4;
                    npc.ai[1] += 1f;
                    if (npc.ai[1] >= num1001)
                    {
                        npc.ai[0] = 2f;
                        npc.ai[1] = 0f;
                        npc.netUpdate = true;
                        Vector2 velocity = new Vector2(npc.ai[2], npc.ai[3]) + new Vector2((float)Main.rand.Next(-num1002, num1002 + 1), (float)Main.rand.Next(-num1002, num1002 + 1)) * 0.04f;
                        velocity.Normalize();
                        velocity *= scaleFactor5;
                        npc.velocity = velocity;
                    }
                    if (Main.rand.Next(4) == 0)
                    {
                        int num1015 = Dust.NewDust(npc.position, npc.width, npc.height, 226, 0f, 0f, 100, default(Color), 0.5f);
                        Main.dust[num1015].noGravity = true;
                        Main.dust[num1015].velocity *= 2f;
                        Main.dust[num1015].velocity = Main.dust[num1015].velocity / 2f + Vector2.Normalize(Main.dust[num1015].position - npc.Center);
                    }
                }
                else if (npc.ai[0] == 2f)
                {
                    npc.knockBackResist = 0f;
                    float num1016 = num1003;
                    npc.ai[1] += 1f;
                    bool flag65 = Vector2.Distance(npc.Center, Main.player[npc.target].Center) > num1004 && npc.Center.Y > Main.player[npc.target].Center.Y;
                    if ((npc.ai[1] >= num1016 && flag65) || npc.velocity.Length() < num1007)
                    {
                        npc.ai[0] = 0f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                        npc.ai[3] = 0f;
                        npc.velocity /= 2f;
                        npc.netUpdate = true;
                    }
                    else
                    {
                        Vector2 center6 = npc.Center;
                        Vector2 center7 = Main.player[npc.target].Center;
                        Vector2 vec2 = center7 - center6;
                        vec2.Normalize();
                        if (vec2.HasNaNs())
                        {
                            vec2 = new Vector2((float)npc.direction, 0f);
                        }
                        npc.velocity = (npc.velocity * (num1005 - 1f) + vec2 * (npc.velocity.Length() + num1006)) / num1005;
                    }
                    if (flag63 && Collision.SolidCollision(npc.position, npc.width, npc.height))
                    {
                        npc.ai[0] = 3f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                        npc.ai[3] = 0f;
                        npc.netUpdate = true;
                    }
                }
                else if (npc.ai[0] == 4f)
                {
                    npc.ai[1] -= 3f;
                    if (npc.ai[1] <= 0f)
                    {
                        npc.ai[0] = 0f;
                        npc.ai[1] = 0f;
                        npc.netUpdate = true;
                    }
                    npc.velocity *= 0.95f;
                }
                if (flag63 && npc.ai[0] != 3f && Vector2.Distance(npc.Center, Main.player[npc.target].Center) < 64f)
                {
                    npc.ai[0] = 3f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    npc.netUpdate = true;
                }
                if (npc.ai[0] == 3f)
                {
                    npc.position = npc.Center;
                    npc.width = (npc.height = 192);
                    npc.position.X = npc.position.X - (float)(npc.width / 2);
                    npc.position.Y = npc.position.Y - (float)(npc.height / 2);
                    npc.velocity = Vector2.Zero;
                    npc.damage = (int)(80f * Main.damageMultiplier);
                    npc.alpha = 255;
                    Lighting.AddLight((int)npc.Center.X / 16, (int)npc.Center.Y / 16, 0.2f, 0.7f, 1.1f);
                    for (int num1017 = 0; num1017 < 10; num1017++)
                    {
                        int num1018 = Dust.NewDust(npc.position, npc.width, npc.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                        Main.dust[num1018].velocity *= 1.4f;
                        Main.dust[num1018].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + npc.Center;
                    }
                    for (int num1019 = 0; num1019 < 40; num1019++)
                    {
                        int num1020 = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.FulguriteDust>(), 0f, 0f, 100, default(Color), 0.5f);
                        Main.dust[num1020].noGravity = true;
                        Main.dust[num1020].velocity *= 2f;
                        Main.dust[num1020].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + npc.Center;
                        Main.dust[num1020].velocity = Main.dust[num1020].velocity / 2f + Vector2.Normalize(Main.dust[num1020].position - npc.Center);
                        if (Main.rand.Next(2) == 0)
                        {
                            num1020 = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.FulguriteDust>(), 0f, 0f, 100, default(Color), 0.9f);
                            Main.dust[num1020].noGravity = true;
                            Main.dust[num1020].velocity *= 1.2f;
                            Main.dust[num1020].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + npc.Center;
                            Main.dust[num1020].velocity = Main.dust[num1020].velocity / 2f + Vector2.Normalize(Main.dust[num1020].position - npc.Center);
                        }
                        if (Main.rand.Next(4) == 0)
                        {
                            num1020 = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.FulguriteDust>(), 0f, 0f, 100, default(Color), 0.7f);
                            Main.dust[num1020].velocity *= 1.2f;
                            Main.dust[num1020].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + npc.Center;
                            Main.dust[num1020].velocity = Main.dust[num1020].velocity / 2f + Vector2.Normalize(Main.dust[num1020].position - npc.Center);
                        }
                    }
                    npc.ai[1] += 1f;
                }
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