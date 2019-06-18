using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class Nightmare_Katana : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nightmare Katana");
			Tooltip.SetDefault("Inflicts Bleeding upon contact.");
		}

		public override void SetDefaults()
		{
			item.damage = 24;           //The damage of your weapon
			item.melee = true;          //Is your weapon a melee weapon?
			item.width = 38;            //Weapon's texture's width
			item.height = 44;           //Weapon's texture's height
			item.useTime = 15;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			item.useAnimation = 15;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			item.knockBack = 2;         //The force of knockback of the weapon. Maximum is 20
			item.value = Item.sellPrice(0, 1, 68, 0);           //The value of the weapon
			item.rare = 1;              //The rarity of the weapon, from -1 to 13
			item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			item.useTurn = true;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Nightmare_Bar", 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Bleeding, 180);
		}
	}
}
