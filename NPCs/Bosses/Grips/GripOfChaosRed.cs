using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Grips
{
    [AutoloadBossHead]
    public class GripOfChaosRed : BaseGripOfChaos
    {
        public override void SetDefaults()
        {
			base.SetDefaults();
			npc.lifeMax = 1600;
            npc.damage = 32;
            npc.defense = 20;	
            npc.buffImmune[BuffID.OnFire] = true;			

			offsetBasePoint = new Vector2(-240f, 0f);			
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0) //this make so when the npc has 0 life(dead) he will spawn this
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/InfernoGripGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/InfernoGripGore2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/InfernoGripGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/InfernoGripGore4"), 1f);
            }
        }

        public static Texture2D glowTex = null;

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            if (glowTex == null)
            {
                glowTex = mod.GetTexture("Glowmasks/GripOfChaosRed_Glow");
            }
            BaseMod.BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseMod.BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, Color.White);
            return false;
        }

        public override void NPCLoot()
        {
            int blueGripExists = NPC.CountNPCS(mod.NPCType("GripOfChaosBlue"));
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GripTrophyRed"));
            }
            if (blueGripExists == 0)
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
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GripMaskRed"));
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Incinerite"), Main.rand.Next(30, 44));
            }
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))       //Chances for it to inflict the debuff
            {
                target.AddBuff(BuffID.OnFire, Main.rand.Next(180, 250));       //Main.rand.Next part is the length of the buff, so 8.3 seconds to 16.6 seconds
            }
            /*if (Main.rand.Next(9) == 0 || (Main.expertMode && Main.rand.Next(7) == 0))
            {
                target.AddBuff(BuffID.Poisoned, Main.rand.Next(250, 500));                 //there is no need for this, unless it inflicts a different debuff
            }*/
        }
    }
}
