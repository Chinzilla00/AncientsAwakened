using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
	public class PrettyPony : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Pretty Pony");
			Description.SetDefault("Its a horse.");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(mod.MountType<Mounts.BegPony>(), player);
			player.buffTime[buffIndex] = 10;
		}
	}
}
