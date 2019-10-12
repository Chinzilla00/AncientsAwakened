using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class SockPuppet : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Sock Puppet");
			Description.SetDefault("Summons a Sock Puppet to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[mod.ProjectileType("SockPuppet")] > 0)
			{
				modPlayer.Sock = true;
			}
			if (!modPlayer.Sock)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else
			{
				player.buffTime[buffIndex] = 18000;
			}
		}
	}
}