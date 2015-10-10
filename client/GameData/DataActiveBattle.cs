using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    class DataActiveBattle
    {
        [JsonProperty("rounds")]
        public List<DataBattleRound> Rounds;

        [JsonProperty("battle_id")]
        public uint BattleId;

        [JsonExtensionData]
        public Dictionary<string, JToken> UnknownValues;

        public IEnumerable<DropEvent> Drops
        {
            get
            {
                foreach (DataBattleRound Round in Rounds)
                    foreach (DropEvent drop in Round.Drops)
                        yield return drop;
            }
        }


        public IEnumerable<BasicEnemyInfo> Enemies
        {
            get
            {
                HashSet<BasicEnemyInfo> infos = new HashSet<BasicEnemyInfo>();
                foreach (DataBattleRound Round in Rounds)
                {
                    foreach (DataEnemy enemy in Round.Enemies)
                    {
                        foreach (DataEnemyChild child in enemy.Children)
                        {
                            // Not sure what the deal is with child and params, or why an enemy can theoretically
                            // have more than one name.  I assume it has something to do with enemies that have
                            // different body parts, but haven't figured it out yet.
                            if (child.Params.Count == 0) { continue; }
                            else
                            {
                                foreach (DataEnemyParam param in child.Params)
                                {
                                    List<string> elemweakness = new List<string>();
                                    List<string> elemresist = new List<string>();
                                    List<string> elemnull = new List<string>();
                                    List<string> elemabsorb = new List<string>();
                                    List<string> statusimmune = new List<string>();
                                    foreach (DataDefAttributes defattributes in param.DefAttributes)
                                    {
                                        if (defattributes.Id < 200) //If it's an elemental weakness.
                                        {
                                            if (defattributes.Factor == (int)SchemaConstants.ElementVulnerability.Vulnerable)
                                            {
                                                //elemweakness.Add(((SchemaConstants.ElementID)defattributes.Id).ToString());
                                                elemweakness.Add(Enum.GetName(typeof(SchemaConstants.ElementID), defattributes.Id));
                                            }
                                            else if (defattributes.Factor == (int)SchemaConstants.ElementVulnerability.Absorb)
                                            {
                                                elemabsorb.Add(Enum.GetName(typeof(SchemaConstants.ElementID), defattributes.Id));
                                            }
                                            else if (defattributes.Factor == (int)SchemaConstants.ElementVulnerability.Null)
                                            {
                                                elemnull.Add(Enum.GetName(typeof(SchemaConstants.ElementID), defattributes.Id));
                                            }
                                            else if (defattributes.Factor == (int)SchemaConstants.ElementVulnerability.Resist)
                                            {
                                                elemresist.Add(Enum.GetName(typeof(SchemaConstants.ElementID), defattributes.Id));
                                            }
                                        }
                                        else if ((defattributes.Id >= 200) && (defattributes.Id < 216)) //If >=200 then it must be a status effect.
                                        {
                                            if (defattributes.Factor == (int)SchemaConstants.StatusVulnerability.Immune)
                                            {
                                                statusimmune.Add(Enum.GetName(typeof(SchemaConstants.StatusID), defattributes.Id));
                                            }

                                        }
                                    }
                                    //List<string> elemweakness = new List<string>();
                                    //elemweakness.Add(Enum.GetName(typeof(SchemaConstants.StatusID), param.DefAttributes[0].Id));
                                    //elemweakness.Add(Enum.GetName(typeof(SchemaConstants.StatusID), param.DefAttributes[1].Id));
                                    infos.Add(new BasicEnemyInfo
                                    {
                                        EnemyId = param.Id,
                                        EnemyName = param.Name,
                                        EnemyMaxHp = param.MaxHp,
                                        //EnemyElemWeakness = elemweakness
                                        EnemyElemResist = elemresist,
                                        EnemyElemWeakness = elemweakness,
                                        EnemyElemAbsorb = elemabsorb,
                                        EnemyElemNull = elemnull,
                                        EnemyStatusImmunity = statusimmune
                                    });
                                    //break;
                                }
                            }
                        }
                    }
                }
                return infos;
            }
        }
    }
}
