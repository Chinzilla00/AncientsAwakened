using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class TrueTerraRose : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Terra Rose");
            Tooltip.SetDefault(@"Some say this staff was used by the legendary hero themselves
Projectiles explode on hit
Projectiles go through walls
Right Clicking fires a piercing rose
Terra Rose EX");
			Item.staff[item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults()
		{
			item.damage = 500;
			item.magic = true;
			item.mana = 15;
			item.width = 68;
			item.height = 60;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 6;
			item.value = 500000;
			item.rare = ItemRarityID.Purple;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("TerraRoseShotEX");
			item.shootSpeed = 20f;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.shoot = mod.ProjectileType("TrueTerraRose");
                item.damage = 70;
                item.useTime = 30;
                item.useAnimation = 30;
                item.knockBack = 2;
            }
            else
            {
                item.shoot = mod.ProjectileType("TerraRoseShotEX");
                item.damage = 500;
                item.useTime = 10;
                item.useAnimation = 10;
                item.knockBack = 6;
            }
            return base.CanUseItem(player);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(mod.ItemType("TerraRose"));
			recipe.AddIngredient(mod.ItemType("EXSoul"));
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}