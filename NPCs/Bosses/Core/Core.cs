using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.IO;

namespace AAMod.NPCs.Bosses.Core
{
    [AutoloadBossHead]
    public class Core : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Biomite Core");
            Main.npcFrameCount[npc.type] = 8;
		}

		public override void SetDefaults()
        {
            npc.lifeMax = 6000;
            npc.boss = true;
            npc.defense = 0;
            npc.damage = 40;
            npc.width = 74;
            npc.height = 70;
            npc.aiStyle = -1;
            npc.value = Item.sellPrice(0, 16, 0, 0);
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Core");
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            bossBag = mod.ItemType("CoreBag");
        }

        public float[] internalAI = new float[5];
        public float[] shootAI = new float[1];
        
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
                writer.Write(internalAI[4]);
                writer.Write(shootAI[0]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
                internalAI[4] = reader.ReadFloat();
                shootAI[0] = reader.ReadFloat();
            }
        }
       
        public override void AI()
        {

        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                int dust1 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 107, 0f, 0f, 0);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
            }
        }

        public override void NPCLoot()
        {
            AAWorld.downedSag = true;
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CoreTrophy"));
            }
            if (!Main.expertMode)
            {
                if (Main.rand.Next(7) == 0)
                {
                    npc.DropLoot(mod.ItemType("CoreMask"));
                }
                string[] lootTable = {  };
                int loot = Main.rand.Next(lootTable.Length);
                npc.DropLoot(mod.ItemType(lootTable[loot]));
                Item.NewItem(npc.Center, ModContent.ItemType<Items.Materials.TerraCrystal>(), Main.rand.Next(1, 4));
            }
            else
            {
                npc.DropBossBags();
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter > 10)
            {
                npc.frameCounter = 0;
                npc.frame.Y += frameHeight;
                if (npc.frame.Y > frameHeight * 7)
                {
                    npc.frame.Y = 0;
                }
            }
        }

        public int frameShell = 0;

        public float RingRoatation = 0;

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            Texture2D CoreBack = mod.GetTexture("NPCs/Bosses/Core/CoreBack");
            Texture2D Core = Main.npcTexture[npc.type];
            Texture2D CoreShell = mod.GetTexture("NPCs/Bosses/Core/CoreShell");
            Texture2D Glow = mod.GetTexture("NPCs/Bosses/Core/CoreGlow");

            Rectangle ShellFrame = BaseDrawing.GetFrame(frameShell, 128, 128, 0, 0);
            Rectangle GlowFrame = BaseDrawing.GetFrame((int)npc.ai[3], 128, 128, 0, 0); ;

            BaseDrawing.DrawTexture(sb, CoreBack, 0, npc.position, npc.width, npc.height, 1, 0, 0, 1, new Rectangle(0, 0, 88, 90), dColor, true);
            BaseDrawing.DrawTexture(sb, Core, 0, npc.position, npc.width, npc.height, 1, 0, 0, 8, npc.frame, dColor, true);
            BaseDrawing.DrawTexture(sb, CoreShell, 0, npc.position, npc.width, npc.height, 1, 0, 9, 1, ShellFrame, dColor, true);
            BaseDrawing.DrawTexture(sb, Glow, 0, npc.position, npc.width, npc.height, 1, npc.rotation, 0, 16, GlowFrame, Color.White, true);
            
            return false;
        }

        public Color GlowColor()
        {
            switch ((int)npc.ai[3])
            {
                case 1:
                    return Color.Green;
                case 2:
                    return Color.LightGreen;
                case 3:
                    return Color.Pink;
                case 4:
                    return Color.Purple;
                case 5:
                    return Color.OrangeRed;
                case 6:
                    return Color.LightBlue;
                case 7:
                    return Color.Brown;
                case 8:
                    return Color.Yellow;
                case 9:
                    return new Color(30, 30, 30);
                case 10:
                    return Color.DarkSlateBlue;
                case 11:
                    return Color.Orange;
                case 12:
                    return Color.Red;
                case 13:
                    return Color.Indigo;
                case 14:
                    return Color.Blue;
                case 15:
                    return Color.SkyBlue;
                case 16:
                    return Color.White;
                default:
                    return Color.Green;
            }
        }
    }
}
