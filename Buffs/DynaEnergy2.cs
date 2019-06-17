using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class DynaEnergy2 : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Dyna-Energy");
			Main.debuff[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
            npc.GetGlobalNPC<AAModGlobalNPC>(mod).DynaEnergy2 = true;
        }
	}
}
