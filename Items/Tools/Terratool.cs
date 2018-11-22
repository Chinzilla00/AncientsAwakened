using AAMod.UI;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class Terratool : ModItem
    {
        public static bool AxeBool = false;
        public static bool PickBool = false;
        public static bool HammerBool = false;
        public static short customGlowMask = 0;

        public override void SetDefaults()
        {

            item.damage = 40;
            item.melee = true;
            item.width = 54;
            item.height = 60;
            item.useTime = 5;
            item.useAnimation = 20;
            item.axe = 300;
            item.pick = 300;
            item.tileBoost += 20;
            item.hammer = 0;
            item.useStyle = 1;
            item.knockBack = 3;
            item.value = 1000000;
            item.rare = 11;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.glowMask = customGlowMask;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terratool");
            Tooltip.SetDefault("Not Working Yet...");
            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Items/Tools/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
        }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 20);
            recipe.AddTile(TileID.Anvils);   //at work bench
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }

        public override bool AltFunctionUse(Player player)
        {
            if (!TerratoolUI.visible)
            {
                Main.PlaySound(SoundID.MenuOpen);
                TerratoolUI.visible = true;
                AAMod.instance.UserInterface.SetState(AAMod.instance.TerratoolUI);
            }
            else
            {
                Main.PlaySound(SoundID.MenuClose);
                TerratoolUI.visible = false;
                AAMod.instance.UserInterface.SetState(null);
            }
            return true;
        }
    }
}
