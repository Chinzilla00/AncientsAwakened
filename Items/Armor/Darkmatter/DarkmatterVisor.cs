using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Darkmatter
{
    [AutoloadEquip(EquipType.Head)]
    public class DarkmatterVisor : ModItem
    {
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
        {

            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Items/Armor/Darkmatter/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
            item.glowMask = customGlowMask;
            DisplayName.SetDefault("Darkmatter Visor");
            Tooltip.SetDefault(@"10% increased Ranged damage
Dark, yet still barely visible");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 14;
            item.value = 300000;
            item.rare = 11;
            item.defense = 26;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += 0.10f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("DarkmatterBreastplate") && legs.type == mod.ItemType("DarkmatterGreaves");
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = true;
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = @"25% decreased ammo consumption
20% increased ranged critical chance
Your ranged ammunition electrocute enemies";

            player.ammoCost75 = true;
            player.rangedCrit += 20;
            player.GetModPlayer<AAPlayer>(mod).darkmatterSetRa = true;
            player.armorEffectDrawShadowLokis = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DarkMatter", 25);
            recipe.AddIngredient(null, "DarkEnergy", 10);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}