using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Assassin
{
    public class AssassinHurt : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("AssassinHirt");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = false;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<AAModGlobalNPC>().AssassinHurt = true;
		}
	}
}
