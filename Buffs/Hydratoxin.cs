using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class HydraToxin : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Hydra Toxin");
			Description.SetDefault("The longer you have it, the faster it festers and eats you away");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.GetModPlayer<AAPlayer>().hydraToxin = true;
        }

		public override void Update(NPC npc, ref int buffIndex)
		{
            npc.GetGlobalNPC<AAModGlobalNPC>().Hydratoxin = true;
        }
	}
}
