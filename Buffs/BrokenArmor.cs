using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class BrokenArmor : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Broken Armor");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = false;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<AAModGlobalNPC>().BrokenArmor = true;
        }
    }
}
