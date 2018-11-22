using AAMod.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
	public class DragonFire : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Dragon Fire");
			Description.SetDefault("Damage reduced by 10");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.GetModPlayer<AAPlayer>(mod).dragonFire = true;
        }

		public override void Update(NPC npc, ref int buffIndex)
		{
            npc.GetGlobalNPC<AAModGlobalNPC>(mod).Dragonfire = true;
        }
	}
}
