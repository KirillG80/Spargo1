using Spargo.DAL;
using Spargo.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Spargo.UI_Command
{
    public class InputCommands
    {
        //        private Dictionary<EntityType, Dictionary<CommandType, Func<EntityBase>>> _entityTypeMapper;
        private Dictionary<EntityType, Dictionary<CommandType, Func<string[], Task>>> _entityTypeMapper;
        private readonly string _connStr;
 
        public InputCommands(string connStr)
        {
            _connStr = connStr;

            _entityTypeMapper = new Dictionary<EntityType, Dictionary<CommandType, Func<string[], Task>>>();
            _entityTypeMapper.Add(EntityType.Goods, new Dictionary<CommandType, Func<string[], Task>>());
            _entityTypeMapper[EntityType.Goods].Add(CommandType.Select, goodsSelect);
            _entityTypeMapper[EntityType.Goods].Add(CommandType.Add, goodsAdd);
            _entityTypeMapper[EntityType.Goods].Add(CommandType.Delete, goodsDelete);
            _entityTypeMapper[EntityType.Goods].Add(CommandType.SelectAll, goodsSelectAll);

            _entityTypeMapper.Add(EntityType.Pharmacy, new Dictionary<CommandType, Func<string[], Task>>());
            _entityTypeMapper[EntityType.Pharmacy].Add(CommandType.Select, pharmSelect);
            _entityTypeMapper[EntityType.Pharmacy].Add(CommandType.Add, pharmAdd);
            _entityTypeMapper[EntityType.Pharmacy].Add(CommandType.Delete, pharmDelete);
            _entityTypeMapper[EntityType.Pharmacy].Add(CommandType.SelectAll, pharmSelectAll);
            _entityTypeMapper[EntityType.Pharmacy].Add(CommandType.SelectQnts, pharmSelectQnts);

            _entityTypeMapper.Add(EntityType.Storage, new Dictionary<CommandType, Func<string[], Task>>());
            _entityTypeMapper[EntityType.Storage].Add(CommandType.Select, storSelect);
            _entityTypeMapper[EntityType.Storage].Add(CommandType.Add, storAdd);
            _entityTypeMapper[EntityType.Storage].Add(CommandType.Delete, storDelete);
            _entityTypeMapper[EntityType.Storage].Add(CommandType.SelectAll, storSelectAll);

            _entityTypeMapper.Add(EntityType.Party, new Dictionary<CommandType, Func<string[], Task>>());
            _entityTypeMapper[EntityType.Party].Add(CommandType.Select, partySelect);
            _entityTypeMapper[EntityType.Party].Add(CommandType.Add, partyAdd);
            _entityTypeMapper[EntityType.Party].Add(CommandType.Delete, partyDelete);
            _entityTypeMapper[EntityType.Party].Add(CommandType.SelectAll, partySelectAll);
        }

        private async Task goodsSelect(string[] prmtrs)
        {
            if (prmtrs.Length > 2)
            {
                GoodsRepository _rep = new GoodsRepository(_connStr);
                int id;
                bool res = Int32.TryParse(prmtrs[2], out id);
                if (!res)
                {
                    Console.WriteLine("Параметр 'Id' некорректно задан!");
                }
                else
                {
                    var goods = await _rep.GetById(id);
                    if(goods!=null)Console.WriteLine(goods.EntityToString());
                    else Console.WriteLine("Товара с таким Id нет!");
                }
            }
            else
            {
                Console.WriteLine("Параметр 'Id' не задан!");
            }
        }
        private async Task goodsSelectAll(string[] prmtrs)
        {
            GoodsRepository _rep = new GoodsRepository(_connStr);
            
            var goodsList = await _rep.GetAll();
            foreach (var goods in goodsList)
            {
                Console.WriteLine(goods.EntityToString());
            }
        }
        private async Task goodsAdd(string[] prmtrs)
        {
            if (prmtrs.Length == 4)
            {
                GoodsRepository _rep = new GoodsRepository(_connStr);
                string name = prmtrs[2];
                decimal price;
                bool res = Decimal.TryParse(prmtrs[3], out price);
                if (!res)
                {
                    Console.WriteLine("Параметр 'Цена' некорректно задан!");
                }
                else
                {
                    var goods = new Goods(0, name, price);
                    await _rep.Create(goods);

                    Console.WriteLine("Товар добавлен!");
                }
            }
            else
            {
                Console.WriteLine("Параметры некорректно заданы!");
            }
        }
        private async Task goodsDelete(string[] prmtrs)
        {
            GoodsRepository _rep = new GoodsRepository(_connStr);
            int id;
            bool res = Int32.TryParse(prmtrs[2], out id);
            if (!res)
            {
                Console.WriteLine("Параметр 'Id' некорректно задан!");
                return;
            }
            await _rep.Delete(id);

            Console.WriteLine("Товар удалён!");
        }
        private async Task pharmSelectAll(string[] prmtrs)
        {
            PharmacyRepository _rep = new PharmacyRepository(_connStr);

            var pharms = await _rep.GetAll();
            foreach (var pharm in pharms)
            {
                Console.WriteLine(pharm.EntityToString());
            }
        }
        private async Task pharmSelect(string[] prmtrs)
        {
            PharmacyRepository _rep = new PharmacyRepository(_connStr);
            int id;
            bool res = Int32.TryParse(prmtrs[2], out id);
            if (!res)
            {
                Console.WriteLine("Параметр 'Id' некорректно задан!");
                return;
            }
            var pharm = await _rep.GetById(id);
            Console.WriteLine(pharm.EntityToString());
        }
        private async Task pharmSelectQnts(string[] prmtrs)
        {
            PharmacyRepository _rep = new PharmacyRepository(_connStr);
            int id;
            bool res = Int32.TryParse(prmtrs[2], out id);
            if (!res)
            {
                Console.WriteLine("Параметр 'Id' некорректно задан!");
                return;
            }
            var pharms = await _rep.GetQntInPharmById(id);
            foreach (var pharm in pharms)
            {
                Console.WriteLine(pharm.EntityToString());
            }
        }
        private async Task pharmAdd(string[] prmtrs)
        {
            if (prmtrs.Length == 5)
            {
                string name = prmtrs[2];
                string address = prmtrs[3];
                string phone = prmtrs[4];
                const string PHONE_REG = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$";
                if (!Regex.IsMatch(phone, PHONE_REG))
                {
                    Console.WriteLine("Параметр 'phone' некорректно задан!");
                }
                else
                {
                    PharmacyRepository _rep = new PharmacyRepository(_connStr);
                    var pharm = new Pharmacy(0, name, address, phone);
                    await _rep.Create(pharm);
                    Console.WriteLine("Аптека добавлена!");
                }
            }
            else
            {
                Console.WriteLine("Параметры некорректно заданы!");
            }
        }
        private async Task pharmDelete(string[] prmtrs)
        {
            PharmacyRepository _rep = new PharmacyRepository(_connStr);
            int id;
            bool res = Int32.TryParse(prmtrs[2], out id);
            if (!res)
            {
                Console.WriteLine("Параметр 'Id' некорректно задан!");
                return;
            }
            await _rep.Delete(id);

            Console.WriteLine("Аптека добавлена!");
        }
        private async Task storSelect(string[] prmtrs)
        {
            if (prmtrs.Length > 2)
            {
                StorageRepository _rep = new StorageRepository(_connStr);
                int id;
                bool res = Int32.TryParse(prmtrs[2], out id);
                if (!res)
                {
                    Console.WriteLine("Параметр 'Id' некорректно задан!");
                }
                else
                {
                    var stor = await _rep.GetById(id);
                    if (stor != null) Console.WriteLine(stor.EntityToString());
                    else Console.WriteLine("Склада с таким Id нет!");
                }
            }
            else
            {
                Console.WriteLine("Параметр 'Id' не задан!");
            }
        }
        private async Task storSelectAll(string[] prmtrs)
        {
            StorageRepository _rep = new StorageRepository(_connStr);

            var storList = await _rep.GetAll();
            foreach (var storage in storList)
            {
                Console.WriteLine(storage.EntityToString());
            }
        }
        private async Task storAdd(string[] prmtrs)
        {
            if (prmtrs.Length == 4)
            {
                StorageRepository _rep = new StorageRepository(_connStr);
                string name = prmtrs[2];
                int pharm_Id;
                bool res = Int32.TryParse(prmtrs[3], out pharm_Id);
                if (!res)
                {
                    Console.WriteLine("Параметр 'Аптека ID' некорректно задан!");
                }
                else
                {
                    var stor = new Storage(0, name, pharm_Id);
                    await _rep.Create(stor);

                    Console.WriteLine("Склад добавлен!");
                }
            }
            else
            {
                Console.WriteLine("Параметры некорректно заданы!");
            }
        }
        private async Task storDelete(string[] prmtrs)
        {
            StorageRepository _rep = new StorageRepository(_connStr);
            int id;
            bool res = Int32.TryParse(prmtrs[2], out id);
            if (!res)
            {
                Console.WriteLine("Параметр 'Id' некорректно задан!");
                return;
            }
            await _rep.Delete(id);

            Console.WriteLine("Склад удалён!");
        }
        private async Task partySelect(string[] prmtrs)
        {
            if (prmtrs.Length > 2)
            {
                PartyRepository _rep = new PartyRepository(_connStr);
                int id;
                bool res = Int32.TryParse(prmtrs[2], out id);
                if (!res)
                {
                    Console.WriteLine("Параметр 'Id' некорректно задан!");
                }
                else
                {
                    var party = await _rep.GetById(id);
                    if (party != null) Console.WriteLine(party.EntityToString());
                    else Console.WriteLine("Партии с таким Id нет!");
                }
            }
            else
            {
                Console.WriteLine("Параметр 'Id' не задан!");
            }
        }
        private async Task partySelectAll(string[] prmtrs)
        {
            PartyRepository _rep = new PartyRepository(_connStr);

            var partyList = await _rep.GetAll();
            foreach (var party in partyList)
            {
                Console.WriteLine(party.EntityToString());
            }
        }
        private async Task partyAdd(string[] prmtrs)
        {
            if (prmtrs.Length == 6)
            {
                PartyRepository _rep = new PartyRepository(_connStr);
                string name = prmtrs[2];
                int goods_Id;
                if(!Int32.TryParse(prmtrs[3], out goods_Id))
                {
                    Console.WriteLine("Параметр 'Товар ID' некорректно задан!");
                    return;
                }
                int stor_Id;
                if(!Int32.TryParse(prmtrs[4], out stor_Id))
                {
                    Console.WriteLine("Параметр 'Склад ID' некорректно задан!");
                    return;
                }
                int qnt;
                if(!Int32.TryParse(prmtrs[5], out qnt))
                {
                    Console.WriteLine("Параметр 'Количество' некорректно задан!");
                    return;
                }

                    var party = new Party(0, goods_Id, stor_Id, qnt);
                    await _rep.Create(party);

                    Console.WriteLine("Партия добавлена!");
            }
            else
            {
                Console.WriteLine("Параметры некорректно заданы!");
            }
        }
        private async Task partyDelete(string[] prmtrs)
        {
            PartyRepository _rep = new PartyRepository(_connStr);
            int id;
            bool res = Int32.TryParse(prmtrs[2], out id);
            if (!res)
            {
                Console.WriteLine("Параметр 'Id' некорректно задан!");
                return;
            }
            await _rep.Delete(id);

            Console.WriteLine("Партия удалёна!");
        }

        public async Task GetDataAsync(EntityType eType, CommandType cType, string[] prmtrs)
        {
            await _entityTypeMapper[eType][cType](prmtrs);
        }
    }
}
