using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.SqlServer;

namespace Home.Repository.HOME_SQL
{
    using global::Repository;
    using System;
    using System.ComponentModel;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Utilities;
    using System.Linq;
    using System.Reflection;
    [DbConfigurationType(typeof(System.Data.Entity.DbConfiguration))]
    internal sealed class Configuration : DbMigrationsConfiguration<MainBoundedContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            MigrationsDirectory = @"HOME_SQL";
            SetSqlGenerator("System.Data.SqlClient", new CustomSqlServerMigrationSqlGenerator<MainBoundedContext>());
        }

        protected override void Seed(MainBoundedContext context)
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

    internal class CustomSqlServerMigrationSqlGenerator<TContext> : SqlServerMigrationSqlGenerator where TContext : DbContext, new()
    {
        protected override void Generate(AddColumnOperation addColumnOperation)
        {
            //   SetCreatedUtcColumn(addColumnOperation.Column);

            base.Generate(addColumnOperation);
            using (var writer = Writer())
            {
                var table = addColumnOperation.Table.Replace("dbo.", "");
                var type = typeof(Home.DomainModel.AgeCompare).Assembly.GetTypes().FirstOrDefault(n => n.Name == table);
                var name = GetDisplayName(type, addColumnOperation.Column);
                if (name != addColumnOperation.Column.Name)
                {
                    writer.WriteLine("EXECUTE sp_addextendedproperty N'MS_Description', N'{2}', N'Schema', N'dbo', N'TABLE', N'{0}', N'column', N'{1}'", table, addColumnOperation.Column.Name, name);
                    Statement(writer);
                }
            }
        }
        /*
        protected override void Generate(DropColumnOperation dropColumnOperation)
        {
            using (var writer = Writer())
            {
                var table = dropColumnOperation.Table.Replace("dbo.", "");
                writer.WriteLine("EXECUTE sp_dropextendedproperty  N'MS_Description', N'Schema', N'dbo', N'TABLE', N'{0}', N'column',N'{1}' ", table, dropColumnOperation.Name);
                Statement(writer);
            }
            base.Generate(dropColumnOperation);
        }*/

        protected override void Generate(CreateTableOperation createTableOperation)
        {
            using (var writer = Writer())
            {
                SetCreatedUtcColumn(createTableOperation, writer);

                base.Generate(createTableOperation);

                var sql = writer.InnerWriter.ToString();
                if (!string.IsNullOrEmpty(sql))
                    Statement(sql, true);
            }
            //
            //  GetTableID(createTableOperation.Name);
        }

        private void SetCreatedUtcColumn(CreateTableOperation createTableOperation, IndentedTextWriter writer)
        {
            var table = createTableOperation.Name.Replace("dbo.", "");
            var type = typeof(Home.DomainModel.AgeCompare).Assembly.GetTypes().FirstOrDefault(n => n.Name == table);
            foreach (var columnModel in createTableOperation.Columns)
            {
                SetCreatedUtcColumn(columnModel);
                var name = GetDisplayName(type, columnModel);
                if (columnModel.Name != name)
                    writer.WriteLine("EXECUTE sp_addextendedproperty N'MS_Description', N'{2}', N'Schema', N'dbo', N'TABLE', N'{0}', N'column', N'{1}'", table, columnModel.Name, name);
            }
            {
                var name = GetDisplayName(type, table);
                if (name != table)
                    writer.WriteLine("EXECUTE sp_addextendedproperty @name=N'MS_Description',@value=N'{1}', @level0type=N'Schema', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'{0}'", table, name);
            }
        }

        private static string GetDisplayName(Type type, string table)
        {
            if (type == null) return table;
            var display =
                type.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() as
                    DisplayNameAttribute;
            if (display == null) return table;
            return display.DisplayName;
        }

        private static string GetDisplayName(Type type, ColumnModel columnModel)
        {
            string name = columnModel.Name;
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

        private static readonly string[] TimeColumnNames = { "Modified", "Created" };
        private static readonly string[] UserColumnNames = { "ModifiedBy", "CreatedBy" };

        private void SetCreatedUtcColumn(ColumnModel column)
        {
            if (column.IsNullable == false && column.Type == PrimitiveTypeKind.DateTime)
            {
                if (TimeColumnNames.Any(n => string.Equals(n, column.Name, StringComparison.OrdinalIgnoreCase)))
                    column.DefaultValueSql = "getdate()";
            }
            if (UserColumnNames.Any(n => string.Equals(n, column.Name, StringComparison.OrdinalIgnoreCase)))
                column.DefaultValueSql = "'db_script'";
            if (column.IsNullable == false && string.Equals("Id", column.Name, StringComparison.OrdinalIgnoreCase))
            {
                if (column.Type == PrimitiveTypeKind.Guid) column.DefaultValueSql = "newid()";
                if (column.Type == PrimitiveTypeKind.Int32) column.IsIdentity = true;
            }
            if (string.Equals("StatusCode", column.Name, StringComparison.OrdinalIgnoreCase) && column.Type == PrimitiveTypeKind.Int32)
            {
                column.DefaultValueSql = "2";
            }
        }
    }
}