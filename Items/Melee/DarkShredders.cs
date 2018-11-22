using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Melee     //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class DarkShredders : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 115;    //The damage stat for the Weapon.
            item.melee = true;     //This defines if it does Melee damage and if its effected by Melee increasing Armor/Accessories.
            item.width = 80;    //The size of the width of the hitbox in pixels.
            item.height = 80;    //The size of the height of the hitbox in pixels.

            item.useTime = 6;   //How fast the Weapon is used.
            item.useAnimation = 6;     //How long the Weapon is used for.
            item.channel = true;
            item.useStyle = 100;    //The way your Weapon will be used, 1 is the regular sword swing for example
            item.knockBack = 3f;    //The knockback stat of your Weapon.
            item.value = Item.buyPrice(1, 0, 0, 0); // How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 10gold)
            item.rare = 9;   //The color the title of your Weapon when hovering over it ingame                    
            item.shoot = mod.ProjectileType("DarkShredders");  //This defines what type of projectile this weapon will shoot  
            item.noUseGraphic = true; // this defines if it does not use graphic
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DarkMatter", 12);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reality Shredders");
            Tooltip.SetDefault("Blades made out of Dark matter. Inflicts the Electified debuff");
        }

 
        public override bool UseItemFrame(Player player)     //this defines what frame the player use when this weapon is used
        {
            player.bodyFrame.Y = 3 * player.bodyFrame.Height;
            return true;
        }
    }
}
