using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class AtlanteanBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Atlantean Power");
            Description.SetDefault("Your battle abilities are increased");
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.magicDamage += 0.15f;
			player.magicCrit += 10;
			player.manaCost -= 0.15f;
			player.statDefense += 10;
			player.endurance += 0.15f;
        }
    }
}