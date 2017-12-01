using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using MySql.Data.Entity;

namespace Repository.Migrations
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    // [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    internal sealed class Configuration : DbMigrationsConfiguration<MainBoundedContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            MigrationsDirectory = @"Migrations";
            SetSqlGenerator("MySql.Data.MySqlClient", new CustomMySqlMigrationSqlGenerator<MainBoundedContext>());
        }

        protected override void Seed(Repository.MainBoundedContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }

    internal class CustomMySqlMigrationSqlGenerator<TContext> : MySqlMigrationSqlGenerator where TContext : DbContext
    {
        protected override MigrationStatement Generate(AddColumnOperation addColumnOperation)
        {
            SetCreatedUtcColumn(addColumnOperation.Column);

            return base.Generate(addColumnOperation);
        }

        protected override MigrationStatement Generate(CreateTableOperation createTableOperation)
        {
            
            SetCreatedUtcColumn(createTableOperation);
            var statement = base.Generate(createTableOperation);
            if (tablecalss != null)
            {
                var display = tablecalss.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() as DisplayNameAttribute;
                if (display != null)
                {
                    statement.Sql = string.Format("{0};ALTER TABLE {1} COMMENT='{2}'; ", statement.Sql, table, display.DisplayName);
                }
            }
            //   if (!string.IsNullOrEmpty(TRIGGER)) { statement.Sql = string.Format("{0};{1};", statement.Sql, TRIGGER); }
            return statement;
        }
        protected override string Generate(ColumnModel op)
        {
            var sql = base.Generate(op);
            var name = GetDisplayName(tablecalss, op.Name);
            if (name != op.Name)
            {
                sql = string.Format("{0} comment '{1}'", sql, name);
            }
            return sql;
        }
        protected override MigrationStatement Generate(DropColumnOperation op)
        {
            return base.Generate(op);
            
        }
        protected override MigrationStatement Generate(DropTableOperation op)
        {
            table = op.Name.Replace("dbo.", "");
            var sql = base.Generate(op);
            sql.Sql = string.Format("drop TABLE `{0}` ; ", table);
            return sql;
        }
        private static readonly string[] TimeColumnNames = { "Modified", "Created" };
        private static readonly string[] UserColumnNames = { "ModifiedBy", "CreatedBy" };
        string table;
        Type tablecalss;
        private void SetCreatedUtcColumn(CreateTableOperation createTableOperation)
        {
            table = createTableOperation.Name.Replace("dbo.", "");
            TRIGGER = null;
            tablecalss = typeof(Home.DomainModel.AgeCompare).Assembly.GetTypes().FirstOrDefault(n => n.Name == table);
            foreach (var columnModel in createTableOperation.Columns)
            {
                SetCreatedUtcColumn(columnModel);

                //      columnModel.
                //  writer.WriteLine("ALTER table {0} MODIFY N`{1}` datetime DEFAULT NULL COMMENT N'{2}';", table, columnModel.Name, name);



            }
        }
        string TRIGGER;
        private static string GetDisplayName(Type type, string name)
        {

            if (type == null) return name;
            if (string.Equals(name, "ID", StringComparison.OrdinalIgnoreCase))
            {
                var display = type.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() as DisplayNameAttribute;
                if (display == null) return name;
                return display.DisplayName + "ID";
            }
            PropertyInfo property = null;
            var arrname = name.Split('_');
            if (arrname.Length == 1)
            {
                property = type.GetProperty(name);
                if (property == null) return name;
                var display = property.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() as DisplayNameAttribute;
                if (display == null) return name;
                return display.DisplayName;

            }
            else
            {
                property = type.GetProperty(arrname[0]);
                if (property == null) return name;
                var display = property.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() as DisplayNameAttribute;
                if (display != null)
                    name = display.DisplayName;
                property = property.PropertyType.GetProperty(arrname[1]);
                if (property == null) return name;
                display = property.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() as DisplayNameAttribute;
                if (display == null) return name;
                return string.Format("{0}_{1}", name, display.DisplayName);
            }


        }
        private void SetCreatedUtcColumn(ColumnModel column)
        {
            if (column.IsNullable == false && column.Type == PrimitiveTypeKind.DateTime)
            {
                if (TimeColumnNames.Any(n => string.Equals(n, column.Name, StringComparison.OrdinalIgnoreCase)))
                    column.DefaultValueSql = "now()";
            }
            if (UserColumnNames.Any(n => string.Equals(n, column.Name, StringComparison.OrdinalIgnoreCase)))
                column.DefaultValueSql = "'script'";
            if (column.IsNullable == false && string.Equals("Id", column.Name, StringComparison.OrdinalIgnoreCase))
            {
                if (column.Type == PrimitiveTypeKind.Guid)
                {
                    TRIGGER = string.Format(@" DELIMITER ;;
CREATE TRIGGER `{0}_before_insert` 
BEFORE INSERT ON `{0}` FOR EACH ROW 
BEGIN
  IF new.{1} IS NULL or new.{1} = '' THEN
    SET new.{1} = uuid();
  END IF;
END;;
DELIMITER ;", table, column.Name);
                }
                if (column.Type == PrimitiveTypeKind.Int32) column.IsIdentity = true;
            }
            if (string.Equals("StatusCode", column.Name, StringComparison.OrdinalIgnoreCase) && column.Type == PrimitiveTypeKind.Int32)
            {
                column.DefaultValueSql = "2";
            }
        }
    }
}