using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.AH
{
    public class HeartOfSorrow: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heart of Sorrow");
            Tooltip.SetDefault(@"Your melee and ranged attacks grow stronger the less health you have
Melee and Ranged inflict Hydratoxin
Below 2/3 of your maximum life, Your movement speed is doubled
Below 1/3 of your maximum life, your melee and ranged attacks inflict Moonraze instead of Hydratoxin");
        }

        public override void SetDefaults()
        {
            item.width = 66;
            item.height = 78;
            item.value = Item.sellPrice(0, 40, 0, 0);
            item.rare = 11;
            item.accessory = true;
            item.expert = true;
            item.defense = 3;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage += ( 1 - player.statLife / player.statLifeMax);
            player.rangedDamage += (1 - player.statLife / player.statLifeMax);
            player.GetModPlayer<AAPlayer>(mod).HeartS = true;

            if (player.statLife > (player.statLifeMax * (2/3)))
            {
                player.moveSpeed += 1f;
            }
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == mod.ItemType<HeartOfPassion>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}