using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.GripsShen
{
    public class BlazeClawM : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blaze Claw");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void SetDefaults()
        {
            aiType = NPCID.DemonEye;
            animationType = NPCID.DemonEye;
            npc.width = 28;
            npc.height = 24;
            npc.friendly = false;
            npc.damage = 60;
            npc.defense = 60;
            npc.lifeMax = 900;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 0f;
            npc.knockBackResist = 0.6f;
            npc.aiStyle = -1;
            npc.noGravity = true; 
        }

        public override void AI()
        {
            if (!NPC.AnyNPCs(mod.NPCType<AbyssGrip>()) && !NPC.AnyNPCs(mod.NPCType<BlazeGrip>()))
            {
                npc.alpha += 10;
                if (npc.alpha > 255)
                {
                    npc.active = false;
                }
            }
            AAAI.AIClaw(npc, ref npc.ai, false, true, 0.1f, 0.04f, 8f, 3f, 1f, 1f);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)          //this make so when the npc has 0 life(dead) he will spawn this
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/DragonClawGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/DragonClawGore2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/DragonClawGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/DragonClawGore4"), 1f);
            }
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            target.AddBuff(mod.BuffType<Buffs.DragonFire>(), 180);
        }
    }
}