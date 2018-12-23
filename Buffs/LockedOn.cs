using Terraria;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Infinity;
using AAMod.NPCs.Bosses.Shen;

namespace AAMod.Buffs
{
    public class LockedOn : ModBuff
    {
        public string SBHP()
        {
            string Stringy = "";
            if (IZ)
            {
                Stringy = Stringy + @"
Infinity Zero: " + IZHP;
            }
            if (NPC.AnyNPCs(mod.NPCType<ShenDoragon>()) || NPC.AnyNPCs(mod.NPCType<ShenA>()))
            {
                Stringy = Stringy + @"
Shen Doragon: " + ShenHP;
            }
            return Stringy;
        }

    	public bool IZ = false;
    	public int IZHP = 2000000;
    	public int ShenHP = 1600000;
        public Infinity Inf = null;
        public ShenDoragon Shen = null;
        public ShenA ShenA = null;

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Locked On");
            Description.SetDefault("");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = "A Super Boss has you locked on!" + SBHP();
            rare = 10;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 60;
            player.GetModPlayer<AAPlayer>(mod).LockedOn = true;

            if (IZ && mod.GetNPC<Infinity>().Dead)
            {
            	IZ = false;
            	player.GetModPlayer<AAPlayer>(mod).InfZ = false;     		
            	player.GetModPlayer<AAPlayer>(mod).GetIZHealth = 2000000;
            }
            if (NPC.AnyNPCs(mod.NPCType<Infinity>()) || NPC.AnyNPCs(mod.NPCType<IZSpawn1>()) || IZ)
            {
            	IZ = true;
            	player.GetModPlayer<AAPlayer>(mod).InfZ = true;
            	if (NPC.AnyNPCs(mod.NPCType<Infinity>()))
            	{
                    if (Inf == null)
                    {
                        Inf = (Infinity)Main.npc[NPC.FindFirstNPC(mod.NPCType<Infinity>())].modNPC;
                    }
                    if (Inf.npc.life != 0)
                    {
                        IZHP = Inf.npc.life;
                        player.GetModPlayer<AAPlayer>(mod).GetIZHealth = IZHP;
                    }
            	}
                else
                {
                    Inf = null;
                }
            }
            if (NPC.AnyNPCs(mod.NPCType<ShenDoragon>()))
            {
                if (Shen == null)
                {
                    Shen = (ShenDoragon)Main.npc[NPC.FindFirstNPC(mod.NPCType<ShenDoragon>())].modNPC;
                }
                ShenHP = Shen.npc.life;
            }
            if (NPC.AnyNPCs(mod.NPCType<ShenA>()))
            {
                if (ShenA == null)
                {
                    ShenA = (ShenA)Main.npc[NPC.FindFirstNPC(mod.NPCType<ShenA>())].modNPC;
                }
                ShenHP = ShenA.npc.life;
            }

            if (!IZ && !NPC.AnyNPCs(mod.NPCType<ShenDoragon>()) && !NPC.AnyNPCs(mod.NPCType<ShenA>()))
            {
            	player.ClearBuff(Type);
            	buffIndex--;
            }
        }
    }
}
    
