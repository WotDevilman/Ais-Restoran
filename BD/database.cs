using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD
{
    class database
    {
        //Запросы
        public string selectSostavBlydo = "SELECT СоставБлюд.ID_зап, Продукты.Название, СоставБлюд.Количество, ЕдИзмер.ЕдИзмерения FROM ЕдИзмер INNER JOIN(Блюда INNER JOIN (Продукты INNER JOIN СоставБлюд ON Продукты.ID_продукта = СоставБлюд.ID_продукт) ON Блюда.ID_блюда = СоставБлюд.ID_блюд) ON ЕдИзмер.ID_ед = СоставБлюд.Eд WHERE СоставБлюд.ID_блюд = ";
        public string selectBlydo = "SELECT Блюда.ID_блюда, Блюда.Название, Блюда.Вес_порции, Блюда.Цена, Блюда.СрокГодности, Категория.Категория FROM Категория INNER JOIN Блюда ON Категория.ID_категории = Блюда.ID_категор ";
        public string selectSclad = "SELECT Склад.ID_записи, Продукты.Название, ЕдИзмер.ЕдИзмерения, Склад.Количество FROM ЕдИзмер INNER JOIN(Продукты INNER JOIN Склад ON Продукты.ID_продукта = Склад.ID_продукта) ON ЕдИзмер.ID_ед = Склад.ID_ЕдИзмерний ";
        public string selectMenu = "SELECT Меню.ID_запМеню, Блюда.Название, Меню.Дата, Меню.Количество FROM Блюда INNER JOIN Меню ON Блюда.ID_блюда = Меню.ID_блюд ";
       //public string selectBlydo = "SELECT Блюда.ID_блюда, Блюда.Название, Блюда.Вес_порции, Блюда.Цена, Блюда.СрокГодности, Категория.Категория FROM Категория INNER JOIN Блюда ON Категория.ID_категории = Блюда.ID_категор ";

        //НекиеЧасти
        public string selectRecept = "SELECT СоставБлюд.Рецепт FROM СоставБлюд WHERE СоставБлюд.ID_блюд = ";

        //Запрос на подключение
        public OleDbConnection connect = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=database.mdb");
    }
}
