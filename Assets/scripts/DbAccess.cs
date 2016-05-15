using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections;
using Mono.Data.Sqlite;
using UnityEngine;

public class DbAccess
    {

        private SqliteConnection dbConnection;
        private SqliteCommand dbCommand;
        private SqliteDataReader reader;

        public DbAccess(string connectionString)
        {
            OpenDB(connectionString);
        }
        public DbAccess()
        {

        }

        /// <summary>
        /// 数据库的打开
        /// </summary>
        /// <param name="connectionString"></param>
        public void OpenDB(string connectionString)
        {
            try
            {
                dbConnection = new SqliteConnection(connectionString);
                dbConnection.Open();
            }
            catch (Exception e)
            {
                string temp1 = e.ToString();
                Console.WriteLine(temp1);
             Debug.Log(temp1);
        }
        }

        /// <summary>
        /// 数据库的关闭
        /// </summary>
        public void CloseSqlConnection()
        {
            if (dbCommand != null)
            {
                dbCommand.Dispose();
            }
            dbCommand = null;
            if (reader != null)
            {
                reader.Dispose();
            }
            reader = null;
            if (dbConnection != null)
            {
                dbConnection.Close();
            }
            dbConnection = null;
        }

        /// <summary>
        /// 语句的执行
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public SqliteDataReader ExecuteQuery(string sqlQuery)
        {
            dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = sqlQuery;
            reader = dbCommand.ExecuteReader();
            return reader;

        }

        /// <summary>
        /// 数据的读取
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public SqliteDataReader ReadFullTable(string tableName)
        {
            string query = "SELECT * FROM " + tableName;
            return ExecuteQuery(query);
        }

        /// <summary>
        /// 数据的插入
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public SqliteDataReader InsertInto(string tableName, string[] values)
        {
            string query = "INSERT INTO " + tableName + " VALUES ('" + values[0] + "'";
            for (int i = 1; i < values.Length; ++i)
            {
                query += ",'" + values[i] + "'";
            }
            query += ")";
            return ExecuteQuery(query);
        }

        /// <summary>
        /// 数据库的更新
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="cols"></param>
        /// <param name="colsvalues"></param>
        /// <param name="selectkey"></param>
        /// <param name="selectvalue"></param>
        /// <returns></returns>
        public SqliteDataReader UpdateInto(string tableName, string[] cols, string[] colsvalues, string selectkey, string selectvalue)
        {
            string query = "UPDATE " + tableName + " SET " + cols[0] + " = " + colsvalues[0];
            for (int i = 1; i < colsvalues.Length; ++i)
            {
                query += ", " + cols[i] + " =" + colsvalues[i];
            }
            query += " WHERE " + selectkey + " = " + selectvalue + " ";
            return ExecuteQuery(query);
        }

        /// <summary>
        /// 数据的多删除
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="cols"></param>
        /// <param name="colsvalues"></param>
        /// <returns></returns>
        public SqliteDataReader Delete(string tableName, string[] cols, string[] colsvalues)
        {
            string query = "DELETE FROM " + tableName + " WHERE " + cols[0] + " = " + colsvalues[0];
            for (int i = 1; i < colsvalues.Length; ++i)
            {
                query += " or " + cols[i] + " = " + colsvalues[i];
            }
            return ExecuteQuery(query);
        }

        /// <summary>
        /// 数据的单删除
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="cols"></param>
        /// <param name="colsvalues"></param>
        /// <returns></returns>
        public SqliteDataReader SimpleDelete(string tableName, string cols, string colsvalues)
        {
            string query = "DELETE FROM " + tableName + " WHERE " + cols + " = " + colsvalues;
            return ExecuteQuery(query);
        }

        /// <summary>
        /// 条件插入
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="cols"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public SqliteDataReader InsertIntoSpecific(string tableName, string[] cols, string[] values)
        {
            if (cols.Length != values.Length)
            {
                throw new SqliteException("columns.Length != values.Length");
            }
            string query = "INSERT INTO " + tableName + "(" + cols[0];
            for (int i = 1; i < cols.Length; ++i)
            {
                query += ", " + cols[i];
            }
            query += ") VALUES (" + values[0];
            for (int i = 1; i < values.Length; ++i)
            {
                query += ", " + values[i];
            }
            query += ")";
            return ExecuteQuery(query);
        }

        /// <summary>
        /// 表的删除
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public SqliteDataReader DeleteContents(string tableName)
        {
            string query = "DELETE FROM " + tableName;
            return ExecuteQuery(query);
        }

        /// <summary>
        /// 表的创建
        /// </summary>
        /// <param name="name"></param>
        /// <param name="col"></param>
        /// <param name="colType"></param>
        /// <returns></returns>
        public SqliteDataReader CreateTable(string name, string[] col, string[] colType)
        {
            if (col.Length != colType.Length)
            {
                throw new SqliteException("columns.Length != colType.Length");
            }
            string query = "CREATE TABLE " + name + " (" + col[0] + " " + colType[0];
            for (int i = 1; i < col.Length; ++i)
            {
                query += ", " + col[i] + " " + colType[i];
            }
            query += ")";
            return ExecuteQuery(query);
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="items"></param>
        /// <param name="col"></param>
        /// <param name="operation"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public SqliteDataReader SelectWhere(string tableName, string[] items, string[] col, string[] operation, string[] values)
        {
            if (col.Length != operation.Length || operation.Length != values.Length)
            {
                throw new SqliteException("col.Length != operation.Length != values.Length");
            }
            string query = "SELECT " + items[0];
            for (int i = 1; i < items.Length; ++i)
            {
                query += ", " + items[i];
            }
            query += " FROM " + tableName + " WHERE " + col[0] + operation[0] + "'" + values[0] + "' ";
            for (int i = 1; i < col.Length; ++i)
            {
                query += " AND " + col[i] + operation[i] + "'" + values[0] + "' ";
            }
            return ExecuteQuery(query);
        }
    }
