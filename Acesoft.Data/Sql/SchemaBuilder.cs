using System;
using System.Collections.Generic;
using System.Data;

using Dapper;
using Acesoft.Data.Sql.Schema;

namespace Acesoft.Data.Sql
{
    public class SchemaBuilder
    {
        private ICommandInterpreter _builder;
        private string _tablePrefix;
        private ISqlDialect _dialect;
        public ISession Session { get; private set; }
        public bool ThrowOnError { get; set; } = true;

        public SchemaBuilder(ISession session)
        {
            Session = session;
            _builder = CommandInterpreterFactory.For(session.Connection);
            //_dialect = session.Dialect;
            //_tablePrefix = session.Store.Configuration.TablePrefix;
        }

        public SchemaBuilder(IDbConnection connection, IDbTransaction transaction, string tablePrefix)
        {
            _builder = CommandInterpreterFactory.For(connection);
            _dialect = SqlDialectFactory.For(connection);
            _tablePrefix = tablePrefix;
            Connection = connection;
            Transaction = transaction;
        }

        private void Execute(IEnumerable<string> statements)
        {
            foreach (var statement in statements)
            {
                Connection.Execute(statement, null, Transaction);
            }
        }

        private string Prefix(string table)
        {
            return _tablePrefix + table;
        }

        public SchemaBuilder CreateTable(string name, Action<CreateTableCommand> table)
        {
            try
            {
                var createTable = new CreateTableCommand(Prefix(name));
                table(createTable);
                Execute(_builder.CreateSql(createTable));
            }
            catch
            {
                if (ThrowOnError)
                {
                    throw;
                }
            }

            return this;
        }

        public SchemaBuilder AlterTable(string name, Action<AlterTableCommand> table)
        {
            try
            {
                var alterTable = new AlterTableCommand(Prefix(name), _dialect, _tablePrefix);
                table(alterTable);
                Execute(_builder.CreateSql(alterTable));
            }
            catch
            {
                if (ThrowOnError)
                {
                    throw;
                }
            }

            return this;
        }

        public SchemaBuilder DropTable(string name)
        {
            try
            {
                var deleteTable = new DropTableCommand(Prefix(name));
                Execute(_builder.CreateSql(deleteTable));
            }
            catch
            {
                if (ThrowOnError)
                {
                    throw;
                }
            }

            return this;
        }

        public SchemaBuilder CreateForeignKey(string name, string srcTable, string[] srcColumns, string destTable, string[] destColumns)
        {
            try
            {
                var command = new CreateForeignKeyCommand(Prefix(name), Prefix(srcTable), srcColumns, Prefix(destTable), destColumns);
                Execute(_builder.CreateSql(command));
            }
            catch
            {
                if (ThrowOnError)
                {
                    throw;
                }
            }

            return this;
        }

        public SchemaBuilder CreateForeignKey(string name, string srcModule, string srcTable, string[] srcColumns, string destTable, string[] destColumns)
        {
            try
            {
                var command = new CreateForeignKeyCommand(Prefix(name), Prefix(srcTable), srcColumns, Prefix(destTable), destColumns);
                Execute(_builder.CreateSql(command));
            }
            catch
            {
                if (ThrowOnError)
                {
                    throw;
                }
            }
            return this;
        }

        public SchemaBuilder CreateForeignKey(string name, string srcTable, string[] srcColumns, string destModule, string destTable, string[] destColumns)
        {
            try
            {
                var command = new CreateForeignKeyCommand(Prefix(name), Prefix(srcTable), srcColumns, Prefix(destTable), destColumns);
                Execute(_builder.CreateSql(command));

            }
            catch
            {
                if (ThrowOnError)
                {
                    throw;
                }
            }

            return this;
        }

        public SchemaBuilder CreateForeignKey(string name, string srcModule, string srcTable, string[] srcColumns, string destModule, string destTable, string[] destColumns)
        {
            try
            {
                var command = new CreateForeignKeyCommand(Prefix(name), Prefix(srcTable), srcColumns, Prefix(destTable), destColumns);
                Execute(_builder.CreateSql(command));
            }
            catch
            {
                if (ThrowOnError)
                {
                    throw;
                }
            }

            return this;
        }

        public SchemaBuilder DropForeignKey(string srcTable, string name)
        {
            try
            {
                var command = new DropForeignKeyCommand(Prefix(srcTable), Prefix(name));
                Execute(_builder.CreateSql(command));
            }
            catch
            {
                if (ThrowOnError)
                {
                    throw;
                }
            }

            return this;
        }
    }
}