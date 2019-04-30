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

namespace AAMod.NPCs.Bosses.Truffle
{
    [AutoloadBossHead]
    public class TechnoTruffle : ModNPC
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
            Main.npcFrameCount[npc.type] = 12;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 25000;
            npc.damage = 50;
            npc.defense = 40;
            npc.knockBackResist = 0f;   //this boss will behavior like the DemonEye  //boss frame/animation 
            npc.value = Item.buyPrice(0, 12, 0, 0);
            npc.aiStyle = 0;
            npc.width = 66;
            npc.height = 104;
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.buffImmune[46] = true;
            npc.buffImmune[47] = true;
            npc.netAlways = true;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            bossBag = mod.ItemType("FungusBag");
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Siege");
            npc.alpha = 255;
            npc.netAlways = true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public static int AISTATE_HOVER = 0, AISTATE_FLIER = 1, AISTATE_SHOOT = 2, AISTATE_ROCKET = 3;
		public float[] internalAI = new float[4];
        bool HasStopped = false;
        bool SelectPoint = false;
        Vector2 MovePoint = new Vector2(0, 0);


        public override void AI()
        {
            Player player = Main.player[npc.target];
             
            if (Main.dayTime)
            {
                npc.active = false;
                Projectile.NewProjectile(npc.Center, new Vector2(0f, 0f), mod.ProjectileType("TruffleBookIt"), 0, 0);
                return;
            }
            npc.frameCounter++;
            if (npc.frameCounter >= 10)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 90;
                if (npc.frame.Y > (90 * 7))
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = 0;
                }
            }

            if (!Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
            {
                internalAI[0]++;
                MoveToPoint(new Vector2(player.Center.X, player.Center.Y - 170f));
            }

            if (Main.netMode != 1 && internalAI[1] != AISTATE_SHOOT)
			{
                internalAI[0]++;
                if (internalAI[0] >= 180)
                {
                    internalAI[0] = 0;
                    internalAI[1] = Main.rand.Next(3);
                    if (internalAI[1] == AISTATE_ROCKET)
                    {
                        SelectPoint = true;
                    }
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }
			if(internalAI[1] == AISTATE_HOVER) 
            {
                BaseAI.AISpaceOctopus(npc, ref npc.ai, player.Center, 0.15f, 8f, 170, 56f, FireMagic);
            }
            else if (internalAI[1] == AISTATE_FLIER) 
            {
                BaseAI.AIFlier(npc, ref npc.ai, true, 0.2f, 0.1f, 8f, 6f, false, 1);
            }
            else if (internalAI[1] == AISTATE_SHOOT)
            {

                if (HasStopped)
                {
                    internalAI[0]++;
                    npc.rotation = 0;
                }
                if (internalAI[0] >= 60)
                {
                    int attack = Main.rand.Next(3);
                    internalAI[1] = Main.rand.Next(3);
                    internalAI[0] = 0;
                    FungusAttack(attack);
                    npc.netUpdate = true;
                }

                npc.velocity *= 0.7f;

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
            else if (internalAI[1] == AISTATE_ROCKET)
            {
                if (SelectPoint)
                {
                    float Point = 500 * npc.direction;
                    MovePoint = player.Center + new Vector2(Point, 500f);
                    SelectPoint = false;
                    npc.netUpdate = true;
                }
                MoveToPoint(MovePoint);
                if (Vector2.Distance(npc.Center, MovePoint) <= 0)
                {
                    internalAI[1] = Main.rand.Next(3);
                    internalAI[0] = 0;
                    npc.netUpdate = true;
                }
                npc.netUpdate = true;
            }
            if (internalAI[1] == AISTATE_ROCKET)
            {
                npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
            }
            else
            {
                npc.rotation = 0;
            }
        }


        public float[] shootAI = new float[4];

        public void FireMagic(NPC npc, Vector2 velocity)
        {
            Player player = Main.player[npc.target];
            BaseAI.ShootPeriodic(npc, player.position, player.width, player.height, mod.ProjType("TruffleShot"), ref shootAI[0], 5, (int)(npc.damage * (Main.expertMode ? 0.25f : 0.5f)), 8f, true, new Vector2(20f, 15f));
        }

        public override void BossLoot(ref string name, ref int potionType)
        {   //boss drops
            AAWorld.downedTruffle = true;
            Projectile.NewProjectile(npc.Center, npc.velocity, mod.ProjectileType("TruffleBookIt"), 0, 0, 255, npc.scale);
            if (Main.expertMode == true)
            {
                npc.DropBossBags();
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SoulofSight, Main.rand.Next(25, 40));
            }
            else
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SoulofFright, Main.rand.Next(25, 40));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FulguriteBar"), Main.rand.Next(30, 64));
            }
        }

        public void FungusAttack(int Attack)
        {
            Player player = Main.player[npc.target];

            if (Attack == 0)
            {
                if (NPC.CountNPCS(mod.NPCType<Truffling>()) < 4)
                {
                    for (int i = 0; i < (Main.expertMode ? 3 : 2); i++)
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<Truffling>());
                    }
                }
                else
                { Attack = 2; }
            }
            else if (Attack == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<TruffleProbe>());
                }
            }
            else if (Attack == 2)
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
        }

        public void MoveToPoint(Vector2 point, bool goUpFirst = false)
        {
            float moveSpeed = 14f;
            if (moveSpeed == 0f || npc.Center == point) return; //don't move if you have no move speed
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float length = (dist == Vector2.Zero ? 0f : dist.Length());
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
            npc.velocity = (length == 0f ? Vector2.Zero : Vector2.Normalize(dist));
            npc.velocity *= moveSpeed;
            npc.velocity *= velMultiplier;
        }


        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/TechnoTruffle_Glow1");
            Texture2D glowTex1 = mod.GetTexture("Glowmasks/TechnoTruffle_Glow2");
            Color color = BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, BaseDrawing.GetLightColor(npc.position), BaseDrawing.GetLightColor(npc.position), Color.Violet, BaseDrawing.GetLightColor(npc.position), Color.Violet, BaseDrawing.GetLightColor(npc.position));

            if (internalAI[1] == AISTATE_ROCKET)
            {
                BaseDrawing.DrawAfterimage(spritebatch, Main.npcTexture[npc.type], 0, npc, 1.5f, 1f, 5, false, 0f, 0f, Color.LightCyan);
            }
            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, color);
            BaseDrawing.DrawTexture(spritebatch, glowTex1, 0, npc, Color.White);
            return false;
        }
    }

    
}


