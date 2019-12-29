using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace AAMod.Items.Magic.SpellBook
{
    public class SpellBookofEbontten : EffectSpellBook
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SpellBook of Ebontten");
			Tooltip.SetDefault(@"Fires unholy fire balls
If this book is copied and consumed by the SpellBook of Sefer, you get the following effects:
In this shooting turn, your magic projectiles will split after a while");
		}

		public override void SetDefaults()
		{
			item.damage = 40;
			item.mana = 12;
			item.useStyle = 5;
			item.shootSpeed = 10f;
			item.shoot = 95;
			item.width = 26;
			item.height = 28;
			item.UseSound = SoundID.Item117;
			item.useTime = 20;
            item.useAnimation = 20;
			item.autoReuse = true;
			item.noMelee = true;
			item.knockBack = 2f;
			item.rare = 12;
			item.value = Item.sellPrice(0, 3, 34, 0);
			item.magic = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SpellTome, 1);
			recipe.AddIngredient(ItemID.Book, 1);
			recipe.AddIngredient(ItemID.CursedFlames, 1);
			recipe.AddIngredient(ItemID.SoulofNight, 50);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void Sustainedeffect(Projectile projectile)
		{
			if(projectile.type != 250 && projectile.type != 251 && projectile.GetGlobalProjectile<Items.Magic.SpellBook.spellbookproj>().SpellbookprojCounter % 90 == 30 && projectile.GetGlobalProjectile<Items.Magic.SpellBook.spellbookproj>().SplitCounter < 1)
			{
				Vector2 oldvelocity = projectile.velocity;
				Vector2 newvelocity = new Vector2();
				Vector2 smallvector = new Vector2(0.5f, 0.5f);
				projectile.velocity = oldvelocity + smallvector;
				newvelocity = oldvelocity - smallvector;
				int p = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, newvelocity.X, newvelocity.Y, projectile.type, projectile.damage, projectile.knockBack, projectile.owner);
				Main.projectile[p].magic = true;
				Main.projectile[p].timeLeft = projectile.timeLeft;
				Main.projectile[p].noDropItem = projectile.noDropItem;
				projectile.GetGlobalProjectile<Items.Magic.SpellBook.spellbookproj>().SplitCounter++;
				Main.projectile[p].GetGlobalProjectile<Items.Magic.SpellBook.spellbookproj>().SplitCounter = projectile.GetGlobalProjectile<Items.Magic.SpellBook.spellbookproj>().SplitCounter;
				Main.projectile[p].GetGlobalProjectile<Items.Magic.SpellBook.spellbookproj>().SpellbookprojCounter = projectile.GetGlobalProjectile<Items.Magic.SpellBook.spellbookproj>().SpellbookprojCounter;
			}
		}
	}
}
