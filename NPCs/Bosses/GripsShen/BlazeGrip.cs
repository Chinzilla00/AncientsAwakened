using System;
using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Grips;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.NPCs.Bosses.GripsShen
{
    [AutoloadBossHead]
    public class BlazeGrip : BaseGripOfChaos
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grip of Blazing Fury");
            Main.npcFrameCount[npc.type] = 4;
        }

	    public override void SetDefaults()
        {
			base.SetDefaults();
			npc.lifeMax = 80000;
            npc.damage = 200;
            npc.defense = 110;
            npc.buffImmune[BuffID.OnFire] = true;	
            bossBag = mod.ItemType("GripSBag");			

			offsetBasePoint = new Vector2(-240f, 0f);	
			shenGrips = true;			
        }	

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0) //this make so when the npc has 0 life(dead) he will spawn this
            {
                npc.position.X = npc.position.X + (float)(npc.width / 2);
                npc.position.Y = npc.position.Y + (float)(npc.height / 2);
                npc.width = 44;
                npc.height = 78;
                npc.position.X = npc.position.X - (float)(npc.width / 2);
                npc.position.Y = npc.position.Y - (float)(npc.height / 2);
                int dust1 = mod.DustType<Dusts.AkumaDust>();
                int dust2 = mod.DustType<Dusts.AkumaDust>();
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust2, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            if (npc.alpha > 0)
            {
                return AAColor.Akuma;
            }
            return lightColor;
        }


        public static Texture2D glowTex = null;

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            if (glowTex == null)
            {
                glowTex = mod.GetTexture("Glowmasks/BlazeGrip_Glow");
            }
            BaseMod.BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseMod.BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, Color.White);
            return false;
        }

        public override void NPCLoot()
        {
            int GripRed = NPC.CountNPCS(mod.NPCType("AbyssGrip"));
            if (GripRed == 0)
            {
                AAWorld.downedGripsS = true;
                if (Main.expertMode)
                {
                    npc.DropBossBags();
                }
            }
            if (!Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DaybreakIncineriteOre"), Main.rand.Next(30, 44));
            }
        }


        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            target.AddBuff(mod.BuffType<Buffs.DragonFire>(), 180);
        }
    }
}
