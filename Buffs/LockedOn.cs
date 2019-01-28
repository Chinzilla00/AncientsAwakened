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
            return Stringy;
        }

    	public bool IZ = false;
    	public int IZHP = 2000000;
    	public int ShenHP = 1600000;
        public Infinity Inf = null;

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
            tip = "Target Locked." + SBHP();
            rare = 10;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 60;
            player.GetModPlayer<AAPlayer>(mod).LockedOn = true;

            if (IZ && Inf != null && Inf.Dead)
            {
            	IZ = false;
            	player.GetModPlayer<AAPlayer>(mod).InfZ = false;     		
            	player.GetModPlayer<AAPlayer>(mod).GetIZHealth = 2000000;
                player.ClearBuff(Type);
                buffIndex--;
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
        }
    }
}
    
