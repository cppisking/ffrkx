using FFRKInspector.Database;
using FFRKInspector.GameData;
using FFRKInspector.GameData.Party;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Proxy
{
    class HandlePartyList : IResponseHandler
    {
        public bool CanHandle(string RequestPath)
        {
            return RequestPath.Equals("/dff/party/list", StringComparison.CurrentCultureIgnoreCase);
        }

        public void Handle(string RequestPath, string ResponseJson)
        {
            DataPartyDetails party = JsonConvert.DeserializeObject<DataPartyDetails>(ResponseJson);
            DbOpInsertItems insert_request = new DbOpInsertItems();
            foreach (DataEquipmentInformation equip in party.Equipments)
            {
                DbOpInsertItems.ItemRecord record = new DbOpInsertItems.ItemRecord();
                record.EquipCategory = equip.Category;
                record.Id = equip.EquipmentId;
                record.Name = equip.Name.TrimEnd(' ', '+', '＋');
                record.Type = equip.Type;
                record.Rarity = equip.BaseRarity;
                record.Series = equip.SeriesId;
                insert_request.Items.Add(record);
            }
            FFRKProxy.Instance.Database.BeginExecuteRequest(insert_request);

            FFRKProxy.Instance.GameState.PartyDetails = party;
            FFRKProxy.Instance.RaisePartyList(party);
        }
    }
}
