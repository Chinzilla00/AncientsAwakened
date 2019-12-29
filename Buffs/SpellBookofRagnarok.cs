using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class SpellBookofRagnarok : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Spellbook of Ragnarok");
			Description.SetDefault("You get the full power of spellbook");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            Items.Magic.SpellBook.spellbookplayer modPlayer = player.GetModPlayer<Items.Magic.SpellBook.spellbookplayer>();
			modPlayer.effectRagnarok = true;

            if(modPlayer.spellbooknum >= 5)
			{
				player.GetModPlayer<AAPlayer>().spellbookDamage += .3f;
			}
			if(modPlayer.spellbooknum >= 10)
			{
				SpawnRagnarok(player);
			}
        }
		private void SpawnRagnarok(Player player)
		{
			bool flag = false;
			for (int j = 0; j < 1000; j ++)
			{
				if (Main.projectile[j].active && Main.projectile[j].owner == player.whoAmI && Main.projectile[j].type ==  mod.ProjectileType("SpellBookofRagnarokProj"))
				{
					flag = true;
					Main.projectile[j].timeLeft = 600;
				}
			}
			if (!flag)
			{
				Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, mod.ProjectileType("SpellBookofRagnarokProj"), (int)((300 + player.inventory[player.selectedItem].damage) * player.magicDamage), 0f, Main.myPlayer, 0f, 0f);
			}
		}
	}
}
