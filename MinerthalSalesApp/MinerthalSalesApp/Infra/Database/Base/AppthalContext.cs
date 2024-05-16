﻿using Dapper;
using Microsoft.Data.Sqlite;
using MinerthalSalesApp.Infra.Database.Tables;
using SQLite;

namespace MinerthalSalesApp.Infra.Database.Base
{
    public class AppthalContext : IAppthalContext
    {
        private readonly string _dbPath;
        private SQLiteAsyncConnection conn;
        private readonly SqliteConnectionStringBuilder connectionStringBuilder;

        public AppthalContext(string dbPath)
        {
            _dbPath = dbPath;
            connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource =dbPath
            };
        }


        public int ExcecutarComandoCrud(string command)
        {

            var cont = 0;
            using var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);
            if (connection.State!=System.Data.ConnectionState.Open)
                connection.Open();

            using var transaction = connection.BeginTransaction();
            try
            {
                cont =connection.Execute(command);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                if (ex.Message.Contains("no such table: Log"))
                    App.LogRepository.CriarTabela();
                else if (ex.Message.Contains("no such table: Atualizacoes"))
                    App.AtualizacaoRepository.CriarTabela();

                var _command = command.Contains("'") ? command.Replace("'", "") : command;
                App.LogRepository.Add(new Log
                {
                    Data=DateTime.Now,
                    ErrorDetail =ex.Message,
                    Command = _command
                });
                cont=1;
            }
            return cont;

        }

        public IEnumerable<dynamic> ExcecutarSelect(string command)
        {
            try
            {
                using var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);
                if (connection.State!=System.Data.ConnectionState.Open)
                    connection.Open();

                return connection.Query<dynamic>(command);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dynamic ExcecutarSelectFirstOrDefault(string command)
        {
            try
            {
                using var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);
                if (connection.State!=System.Data.ConnectionState.Open)
                    connection.Open();

                return connection.QueryFirstOrDefault<dynamic>(command);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
