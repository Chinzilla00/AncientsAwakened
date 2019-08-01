using AAMod.NPCs.Enemies.Sky;
using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Athena
{
	public class AthenaDefeat : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 15;
        }

        public override void SetDefaults()
        {
            npc.width = 152;
            npc.height = 114;
            npc.npcSlots = 1000;
            npc.aiStyle = -1;
            npc.defense = 1;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.boss = true;
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
            npc.damage = 0;
            npc.value = 0;
            npc.noTileCollide = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/silence");
        }

        public override void AI()
        {
            Vector2 Origin = new Vector2((int)(Main.maxTilesX * 0.65f), 100) * 16;
            Vector2 Acropolis = new Vector2(Origin.X + (76 * 16), Origin.Y + (72 * 16));
            npc.TargetClosest();
            if (Main.netMode != 2)
            {
                npc.frameCounter++;
                if (npc.frameCounter >= 6)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += Main.npcTexture[npc.type].Height / 4;
                }
                if (npc.frame.Y > Main.npcTexture[npc.type].Height / 4 * 3)
                {
                    npc.frame.Y = 0;
                }
            }
            if (Main.netMode != 1)
            {
                if (Vector2.Distance(npc.Center, Acropolis) < 100 && Main.netMode != 1)
                {
                    npc.velocity.X *= 0;
                    npc.ai[1] = 1;
                    npc.noTileCollide = false;
                    npc.noGravity = false;
                    npc.netUpdate = true;
                }
                if (npc.ai[1] == 0)
                {
                    MoveToPoint(Acropolis);
                }
                else
                {
                    if (Main.netMode != 1)
                    {
                        npc.ai[0]++;
                    }
                    if (npc.ai[0] == 90)
                    {
                        if (Main.netMode != 1) BaseUtility.Chat("...hah...hah...", Color.CornflowerBlue);
                        npc.netUpdate = true;
                    }
                    else
                    if (npc.ai[0] == 180)
                    {
                        if (Main.netMode != 1) BaseUtility.Chat("...I still lost.", Color.CornflowerBlue);
                        npc.netUpdate = true;
                    }
                    else
                    if (npc.ai[0] == 270)
                    {
                        if (Main.netMode != 1) BaseUtility.Chat("...", Color.CornflowerBlue);
                        npc.netUpdate = true;
                    }
                    else
                    if (npc.ai[0] == 360)
                    {
                        music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/AthenaA");
                        if (Main.netMode != 1) BaseUtility.Chat("No.", Color.CornflowerBlue);
                        npc.netUpdate = true;
                    }
                    else
                    if (npc.ai[0] == 450)
                    {
                        if (Main.netMode != 1) BaseUtility.Chat("I'm not giving up that easilly.", Color.CornflowerBlue);
                        npc.netUpdate = true;
                    }
                    else
                    if (npc.ai[0] == 540)
                    {
                        if (Main.netMode != 1) BaseUtility.Chat("There's a phrase my people live by, earthwalker.", Color.CornflowerBlue);
                        npc.netUpdate = true;
                    }
                    else
                    if (npc.ai[0] == 630)
                    {
                        if (Main.netMode != 1) BaseUtility.Chat("Brightest of dawn...", Color.CornflowerBlue);
                        npc.netUpdate = true;
                    }
                    else
                    if (npc.ai[0] == 720)
                    {
                        if (Main.netMode != 1) BaseUtility.Chat("Darkest of night...", Color.CornflowerBlue);
                        npc.netUpdate = true;
                    }
                    else
                    if (npc.ai[0] == 810)
                    {
                        if (Main.netMode != 1) BaseUtility.Chat("Even in defeat...", Color.CornflowerBlue);
                        npc.netUpdate = true;
                    }
                    else
                    if (npc.ai[0] >= 900)
                    {
                        if (Main.netMode != 1) BaseUtility.Chat("A VARIAN ALWAYS PUTS UP ONE LAST FIGHT!!!", Color.Silver);
                        AAModGlobalNPC.SpawnBoss(Main.player[npc.target], mod.NPCType<AthenaA>(), false, npc.Center);

                        int b = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("ShockwaveBoom"), 0, 1, Main.myPlayer, 5, 15);
                        Main.projectile[b].Center = npc.Center;

                        npc.active = false;
                        npc.netUpdate = true;
                    }
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.ai[1] == 0)
            {
                if (npc.frameCounter++ >= 6)
                {
                    npc.frame.Y += frameHeight;
                    npc.frameCounter = 0;
                }
                if (npc.frame.Y >= frameHeight * 7)
                {
                    npc.frame.Y = 0;
                }
            }
            else
            {
                if (npc.ai[0] < 270)
                {
                    if (npc.frameCounter++ >= 15)
                    {
                        npc.frame.Y += frameHeight;
                        npc.frameCounter = 0;
                        if (npc.frame.Y < frameHeight * 8 || npc.frame.Y >= frameHeight * 12)
                        {
                            npc.frame.Y = 8;
                        }
                    }
                }
                else if (npc.ai[0] >= 270 && npc.ai[0] < 450)
                {
                    if (npc.frame.Y < frameHeight * 8 || npc.frame.Y >= frameHeight * 12)
                    {
                        npc.frame.Y = 11;
                    }
                }
                else if (npc.ai[0] <= 450)
                {
                    if (npc.frameCounter++ >= 15)
                    {
                        npc.frame.Y += frameHeight;
                        npc.frameCounter = 0;
                        if (npc.frame.Y < frameHeight * 12 || npc.frame.Y >= frameHeight * 15)
                        {
                            npc.frame.Y = 12;
                        }
                    }
                }
            }
        }

        public void MoveToPoint(Vector2 point, bool goUpFirst = false)
        {
            float moveSpeed = 14f;
            if (moveSpeed == 0f || npc.Center == point) return; //don't move if you have no move speed
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            if (length < 200f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                moveSpeed *= 0.5f;
            }
            npc.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            npc.velocity *= moveSpeed;
            npc.velocity *= velMultiplier;
        }
    }
}