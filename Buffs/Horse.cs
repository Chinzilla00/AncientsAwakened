using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
	public class Horse : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Ur a horse now");
			Description.SetDefault("Neigh.");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			canBeCleared = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer p = player.GetModPlayer<AAPlayer>();
            
			if (p.BegAccessoryPrevious)
			{
				p.HorseBuff = true;
			}
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
