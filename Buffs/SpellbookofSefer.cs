using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class SpellbookofSefer : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Spellbook of Sefer");
			Description.SetDefault("You get the power of spellbook");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();

            player.GetModPlayer<Items.Magic.SpellBook.spellbookplayer>().effectSpellBook = true;
        }
	}
}
