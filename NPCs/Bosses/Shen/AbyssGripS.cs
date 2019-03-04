using System;
using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Grips;

namespace AAMod.NPCs.Bosses.Shen
{
    [AutoloadBossHead]
    public class AbyssGripS : BaseGripOfChaos
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grip of Abyssal Wrath");
            Main.npcFrameCount[npc.type] = 4;
        }

	    public override void SetDefaults()
        {
			base.SetDefaults();
			npc.lifeMax = 70000;
            npc.damage = 250;
            npc.defense = 110;
            npc.buffImmune[BuffID.Poisoned] = true;	
            bossBag = mod.ItemType("GripSBag");			

			offsetBasePoint = new Vector2(240f, 0f);
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
                int dust1 = mod.DustType<Dusts.YamataDust>();
                int dust2 = mod.DustType<Dusts.YamataDust>();
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


        public int chargeWidth = 50;
        public int normalWidth = 250;

        public static bool NOTRELEASED = true;

        public override void AI()
        {
            if (NOTRELEASED)
            {
                npc.active = false; return;
            }
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))       //Chances for it to inflict the debuff
            {
                target.AddBuff(mod.BuffType<Buffs.HydraToxin>(), Main.rand.Next(180, 250));       //Main.rand.Next part is the length of the buff, so 8.3 seconds to 16.6 seconds
            }
        }
    }
}
