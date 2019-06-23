using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
    public class LunaticJavelin : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 120;           //this is the item damage
            item.melee = true;             //this make the item do throwing damage
            item.noMelee = true;
            item.width = 20;
            item.height = 20;
            item.useTime = 8;       //this is how fast you use the item
            item.useAnimation = 8;   //this is how fast the animation when the item is used
            item.useStyle = 1;      
            item.knockBack = 6;
            item.value = 1;
            item.rare = 8;
            item.reuseDelay = 0;    //this is the item delay
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;       //this make the item auto reuse
            item.shoot = mod.ProjectileType("LJavelinP");  //javelin projectile
            item.shootSpeed = 18f;     //projectile speed
            item.useTurn = true;
            item.maxStack = 999;       //this is the max stack of this item
            item.consumable = true;  //this make the item consumable when used
            item.noUseGraphic = true;
                       
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Lunatic Javelin");
      Tooltip.SetDefault("");
    }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 250);
            recipe.AddRecipe();
        }
    }
}
