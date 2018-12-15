using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Zero
{
    [AutoloadBossHead]
    public class Zero : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zero");
            Main.npcFrameCount[npc.type] = 2; 
        }

        public override void SetDefaults()
        {

            npc.damage = 100;
            npc.defense = 90;
            npc.lifeMax = 95000;
            if (Main.expertMode)
            {
                npc.value = 0;
            }
            else
            {
                npc.value = 120000f;
            }
            npc.width = 206;
            npc.height = 208;
            npc.aiStyle = -1;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCHit4;
            npc.noGravity = true;
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Zero");
            npc.noTileCollide = true;
            
            npc.knockBackResist = -1f;
            npc.boss = true;
            npc.friendly = false;
            animationType = NPCID.SkeletronPrime;
            npc.npcSlots = 1f;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.lavaImmune = true;
            npc.netAlways = true;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((short)npc.localAI[0]);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            npc.localAI[0] = reader.ReadInt16();
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.type == mod.NPCType<Zero>() && (NPC.AnyNPCs(mod.NPCType<VoidStar>()) || NPC.AnyNPCs(mod.NPCType<Taser>()) || NPC.AnyNPCs(mod.NPCType<RealityCannon>()) || NPC.AnyNPCs(mod.NPCType<RiftShredder>())))
            {
                npc.dontTakeDamage = true;
                npc.chaseable = false;
            }
            if (npc.life <= 0 && npc.type == mod.NPCType<Zero>())
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore3"), 1f);
                npc.position.X = npc.position.X + (npc.width / 2);
                npc.position.Y = npc.position.Y + (npc.height / 2);
                npc.width = 100;
                npc.height = 100;
                npc.position.X = npc.position.X - (npc.width / 2);
                npc.position.Y = npc.position.Y - (npc.height / 2);
                Vector2 spawnAt = npc.Center + new Vector2(0f, npc.height / 2f);
                if (Main.expertMode)
                {
                    NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("ZeroAwakened"));
                }
                if (!Main.expertMode)
                {
                    Main.NewText("D00MSDAY PR0T0CALL MALFUNCTI0N. MAIN.EXPERT M0DE = FALSE.", Color.Red.R, Color.Red.G, Color.Red.B);
                }
            }
        }

        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                npc.DropLoot(mod.ItemType("ApocalyptitePlate"), 2, 4);
            }
            else
            {
                npc.DropLoot(mod.ItemType("ApocalyptitePlate"), 2, 4);
                npc.DropLoot(mod.ItemType("UnstableSingularity"), 25, 35);
                string[] lootTable =
                {
                    "Battery",
                    "ZeroArrow",
                    "Vortex",
                    "EventHorizon",
                    "RealityCannon",
                    "RiftShredder",
                    "VoidStar",
                    "TeslaHand",
                    "ZeroStar",
                    "Neutralizer",
                    "ZeroTerratool"
                };
                int loot = Main.rand.Next(lootTable.Length);
                npc.DropLoot(mod.ItemType(lootTable[loot]));
                npc.DropLoot(Items.Vanity.Mask.ZeroMask.type, 1f / 7);
                npc.DropLoot(Items.Boss.Zero.ZeroTrophy.type, 1f / 10);
                npc.DropLoot(Items.Boss.EXSoul.type, 1f / 10);
                if (Main.rand.NextFloat() < 0.05f && AAWorld.RealityDropped == false)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RealityStone"));
                    AAWorld.RealityDropped = true;
                }
            }
        }
        
        public override void BossLoot(ref string name, ref int potionType)
        {
            if (!Main.expertMode)
            {
                potionType = ItemID.SuperHealingPotion;   //boss drops
                AAWorld.downedZero = true;
            }
        }

        public Color GetGlowAlpha()
        {
            return new Color(233, 53, 53) * (Main.mouseTextColor / 255f);
        }

        public static Texture2D glowTex = null;
        public float auraPercent = 0f;
        public bool auraDirection = true;
        public bool saythelinezero = false;

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            if (glowTex == null)
            {
                glowTex = mod.GetTexture("Glowmasks/Zero_Glow");
            }
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseMod.BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseMod.BaseDrawing.DrawAura(spritebatch, glowTex, 0, npc, auraPercent, 1f, 0f, 0f, GetGlowAlpha());
            BaseMod.BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, GetGlowAlpha());
            if (!saythelinezero)
            {
                BaseMod.BaseDrawing.DrawAura(spritebatch, Main.npcTexture[npc.type], 0, npc, auraPercent, 1f, 0f, 0f, GetGlowAlpha());
                BaseMod.BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, Color.White);
            }
            return false;
        }

        /*public bool ChargeAttack //actually charging the player
        {
            get
            {
                return npc.ai[1] == 1;
            }
            set
            {
                float oldValue = npc.ai[1];
                npc.ai[1] = (value ? 1f : 0f);
                if (npc.ai[1] != oldValue) npc.netUpdate = true;
            }
        }
        public bool Charging //preparing to charge the player
        {
            get
            {
                return npc.ai[1] == 1.5f;
            }
            set
            {
                float oldValue = npc.ai[1];
                npc.ai[1] = (value ? 1.5f : 0f);
                if (npc.ai[1] != oldValue) npc.netUpdate = true;
            }
        }*/
        public int chargeTimer = 0;
        public int movementtimer = 0;
        public bool direction = false;
        public int chargeTime = 100;



        public int MinionTimer = 0;
        public int LineStopper = 180;
        public override void AI()
        {
            MinionTimer++;
            LineStopper--;
            if (MinionTimer == 180 && NPC.CountNPCS(mod.NPCType<SearcherZero>()) < 8)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<SearcherZero>());

                MinionTimer = 0;
            }
            
            npc.damage = npc.defDamage;
            npc.defense = npc.defDefense;
            bool expert = Main.expertMode;
            if (npc.ai[0] == 0 && Main.netMode != 1)
            {
                npc.TargetClosest(true);
                npc.ai[0]++;
                int index1 = NPC.NewNPC((int)(npc.position.X + (double)(npc.width / 2)), (int)npc.position.Y + (npc.height / 2), mod.NPCType("VoidStar"), npc.whoAmI, 0.0f, 0.0f, 0.0f, 0.0f, byte.MaxValue);
                Main.npc[index1].ai[0] = -1f;
                Main.npc[index1].ai[1] = npc.whoAmI;
                Main.npc[index1].target = npc.target;
                Main.npc[index1].netUpdate = true;
                int index2 = NPC.NewNPC((int)(npc.position.X + (double)(npc.width / 2)), (int)npc.position.Y + (npc.height / 2), mod.NPCType("RiftShredder"), npc.whoAmI, 0.0f, 0.0f, 0.0f, 0.0f, byte.MaxValue);
                Main.npc[index2].ai[0] = 1f;
                Main.npc[index2].ai[1] = npc.whoAmI;
                Main.npc[index2].target = npc.target;
                Main.npc[index2].netUpdate = true;
                int index3 = NPC.NewNPC((int)(npc.position.X + (double)(npc.width / 2)), (int)npc.position.Y + (npc.height / 2), mod.NPCType("Taser"), npc.whoAmI, 0.0f, 0.0f, 0.0f, 0.0f, byte.MaxValue);
                Main.npc[index3].ai[0] = -1f;
                Main.npc[index3].ai[1] = npc.whoAmI;
                Main.npc[index3].target = npc.target;
                Main.npc[index3].ai[3] = 150f;
                Main.npc[index3].netUpdate = true;
                int index4 = NPC.NewNPC((int)(npc.position.X + (double)(npc.width / 2)), (int)npc.position.Y + (npc.height / 2), mod.NPCType("RealityCannon"), npc.whoAmI, 0.0f, 0.0f, 0.0f, 0.0f, byte.MaxValue);
                Main.npc[index4].ai[0] = 1f;
                Main.npc[index4].ai[1] = npc.whoAmI;
                Main.npc[index4].target = npc.target;
                Main.npc[index4].netUpdate = true;
                Main.npc[index4].ai[3] = 150f;
                
            }

            if (npc.type == mod.NPCType<Zero>() && (!NPC.AnyNPCs(mod.NPCType<VoidStar>()) && !NPC.AnyNPCs(mod.NPCType<Taser>()) && !NPC.AnyNPCs(mod.NPCType<RealityCannon>()) && !NPC.AnyNPCs(mod.NPCType<RiftShredder>())))
            {
                saythelinezero = true;
                npc.dontTakeDamage = false;
                npc.chaseable = true;
                if (!Main.expertMode && !AAWorld.downedZero)
                {
                    npc.damage = 100;
                }
                if (!Main.expertMode && AAWorld.downedZero)
                {
                    npc.damage = 110;
                }
                if (Main.expertMode && !AAWorld.downedZero)
                {
                    npc.damage = 110;
                }
                if (Main.expertMode && AAWorld.downedZero)
                {
                    npc.damage = 120;
                }
            }

            if (saythelinezero && LineStopper == 0)
            {
                saythelinezero = true;
                Main.NewText("CRITICAL ERR0R: ARM UNITS NOT FOUND. SHIELDS L0WERED. RER0UTING RES0RCES TO OFFENSIVE PR0T0C0LS", Color.Red.R, Color.Red.G, Color.Red.B);
            }
            if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
                    npc.ai[1] = 3f;
                }
            }
            else
            {
                npc.defense = 70;
            }
            if (npc.ai[1] == 0f)
            {
                npc.ai[2] += 1f;
                if (npc.ai[2] >= 600f)
                {
                    npc.ai[2] = 0f;
                    npc.ai[1] = 1f;
                    npc.TargetClosest(true);
                    npc.netUpdate = true;
                }
                npc.rotation = npc.velocity.X / 15f;
                if (npc.position.Y > Main.player[npc.target].position.Y - 200f)
                {
                    if (npc.velocity.Y > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y * 0.98f;
                    }
                    npc.velocity.Y = npc.velocity.Y - 0.1f;
                    if (npc.velocity.Y > 2f)
                    {
                        npc.velocity.Y = 2f;
                    }
                }
                else if (npc.position.Y < Main.player[npc.target].position.Y - 500f)
                {
                    if (npc.velocity.Y < 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y * 0.98f;
                    }
                    npc.velocity.Y = npc.velocity.Y + 0.1f;
                    if (npc.velocity.Y < -2f)
                    {
                        npc.velocity.Y = -2f;
                    }
                }
                if (npc.position.X + (float)(npc.width / 2) > Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) + 100f)
                {
                    if (npc.velocity.X > 0f)
                    {
                        npc.velocity.X = npc.velocity.X * 0.98f;
                    }
                    npc.velocity.X = npc.velocity.X - 0.1f;
                    if (npc.velocity.X > 8f)
                    {
                        npc.velocity.X = 8f;
                    }
                }
                if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - 100f)
                {
                    if (npc.velocity.X < 0f)
                    {
                        npc.velocity.X = npc.velocity.X * 0.98f;
                    }
                    npc.velocity.X = npc.velocity.X + 0.1f;
                    if (npc.velocity.X < -8f)
                    {
                        npc.velocity.X = -8f;
                        return;
                    }
                }
            }
            else
            {
                if (npc.ai[1] == 1f)
                {
                    npc.defense *= 2;
                    npc.damage *= 2;
                    npc.ai[2] += 1f;
                    if (npc.ai[2] == 2f)
                    {
                        Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0, 1f, 0f);
                    }
                    if (npc.ai[2] >= 400f)
                    {
                        npc.ai[2] = 0f;
                        npc.ai[1] = 0f;
                    }
                    npc.rotation += (float)npc.direction * 0.7f;
                    Vector2 vector44 = new Vector2(npc.position.X + ((float)npc.width * 0.5f), npc.position.Y + ((float)npc.height * 0.5f));
                    float num441 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector44.X;
                    float num442 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector44.Y;
                    float num443 = (float)Math.Sqrt((double)((num441 * num441) + (num442 * num442)));
                    num443 = 2f / num443;
                    npc.velocity.X = num441 * num443;
                    npc.velocity.Y = num442 * num443;
                    return;
                }
                if (npc.ai[1] == 3f)
                {
                    npc.velocity.Y = npc.velocity.Y + 0.1f;
                    if (npc.velocity.Y < 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y * 0.95f;
                    }
                    npc.velocity.X = npc.velocity.X * 0.95f;
                    if (npc.timeLeft > 500)
                    {
                        npc.timeLeft = 500;
                        return;
                    }
                }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            //npc.frameCounter++;
            //if (ChargeAttack || Charging)
            //{
                npc.frame.Y = 1 * frameHeight;
            //}
            //else
            //{
                //npc.frame.Y = 0;
                //npc.frameCounter = 0;
            //}
        }
    }
}







