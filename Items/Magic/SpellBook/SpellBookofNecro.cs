using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Magic.SpellBook
{
    public class SpellBookofNecro : EffectSpellBook
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SpellBook of Necro");
			Tooltip.SetDefault(@"Fires a lost soul to chase your foes
If this book is copied and consumed by the SpellBook of Sefer, you get the following effects:
In this shooting turn, your magic damage can release the soul of the enemy.");
		}

		public override void SetDefaults()
		{
			item.damage = 76;
			item.mana = 12;
			item.useStyle = 5;
			item.shootSpeed = 10f;
			item.shoot = 297;
			item.width = 26;
			item.height = 28;
			item.UseSound = SoundID.Item117;
			item.useTime = 20;
            item.useAnimation = 20;
			item.autoReuse = true;
			item.noMelee = true;
			item.knockBack = 2f;
			item.rare = 9;
			item.value = Item.sellPrice(0, 12, 13, 0);
			item.magic = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SpellTome, 1);
			recipe.AddIngredient(ItemID.Book, 1);
			recipe.AddIngredient(ItemID.SpectreStaff, 1);
			recipe.AddIngredient(ItemID.SpectreBar, 30);
            recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void Sustainedeffect(Projectile projectile)
		{
			projectile.GetGlobalProjectile<Items.Magic.SpellBook.spellbookproj>().SpellBookofNecro = true;
		}
	}
}
