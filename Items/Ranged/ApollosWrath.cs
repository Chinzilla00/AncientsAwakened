using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class ApollosWrath : BaseAAItem
    {

        public override void SetDefaults()
        {
            item.damage = 78;
            item.noMelee = true;
            item.ranged = true;
            item.width = 24;
            item.height = 52;
            item.useTime = 14;
            item.useAnimation = 14;
            item.useStyle = 5;
            item.shoot = 294;
            item.knockBack = 2;
            item.value = Terraria.Item.sellPrice(0, 5, 0, 0);
            item.rare = 7;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shootSpeed = 4f;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_GUN; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Apollo's Wrath");
            Tooltip.SetDefault(@"Shoots Shadow beams
Doesn't use Ammo");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Apollo", 1);
            recipe.AddIngredient(ItemID.PulseBow, 1);
            recipe.AddIngredient(ItemID.SoulofMight, 20);
			recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
    }
}
