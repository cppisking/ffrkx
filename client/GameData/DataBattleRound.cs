using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    class DataBattleRound
    {
        [JsonProperty("enemy")]
        public List<DataEnemy> Enemies;

        [JsonProperty("round")]
        public uint Index;

        [JsonProperty("drop_materias")]
        public List<DataMateria> Materias;

        [JsonProperty("drop_item_list")]
        public List<DataPotion> Potions;

        [JsonExtensionData]
        public Dictionary<string, JToken> UnknownValues;

        public IEnumerable<DropEvent> Drops
        {
            get
            {
                foreach (DataEnemy enemy in Enemies)
                {
                    foreach (DropEvent drop in enemy.Drops)
                    {
                        drop.Round = Index;
                        yield return drop;
                    }
                }
                foreach (DataMateria materia in Materias)
                {
                    DropEvent dropMateria = new DropEvent();
                    dropMateria.MateriaName = materia.Name;
                    dropMateria.Round = Index;
                    dropMateria.EnemyName = "";
                    dropMateria.ItemType = DataEnemyDropItem.DropItemType.Materia;
                    yield return dropMateria;
                }
                foreach (DataPotion potion in Potions)
                {
                    DropEvent dropPotion = new DropEvent();
                    if (potion.Type == 21) { dropPotion.PotionName = "Potion"; }
                    else if (potion.Type == 22) { dropPotion.PotionName = "Hi-Potion"; }
                    else if (potion.Type == 31) { dropPotion.PotionName = "Ether"; }
                    else { dropPotion.PotionName = "Unknown Potion"; }
                    dropPotion.Round = Index;
                    dropPotion.EnemyName = "";
                    dropPotion.ItemType = DataEnemyDropItem.DropItemType.Potion;
                    yield return dropPotion;
                }
            }
        }
    }
}
